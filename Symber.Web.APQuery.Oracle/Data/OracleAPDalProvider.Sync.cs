using Oracle.ManagedDataAccess.Client;
using Symber.Web.Compilation;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Symber.Web.Data
{

	/// <summary>
	/// Manage data access in Oracle database.
	/// </summary>
	public partial class OracleAPDalProvider : APDalProvider
	{
		private string scanTableName;
		private string scanColumnName;
		private void _setScanTable(string tableName)
		{
			scanTableName = tableName;
		}
		private void _setScanColumn(string columnName)
		{
			scanColumnName = columnName;
		}

		/// <summary>
		/// Synchronize database.
		/// </summary>
		/// <param name="businessModel">Business model.</param>
		public override void Sync(APGenBusinessModelSection businessModel)
		{
		   using (APDatabase db = new APDatabase(this))
		   {
		      try
		      {
					db.BeginTrans();

					foreach (APGenTable table in GetAllTables(businessModel))
					{
						_setScanTable(table.TableName);

						DbCommand dbCmd = db.CreateSqlCommand("select table_name from user_tables where temporary = 'N' and table_name = '{0}'", table.TableName.ToUpper());
						object id = dbCmd.ExecuteScalar();
						if (id == null)
							CreateTable(db, table);
						else
							ModifyTable(db, table);
					}


					SyncSequence(db, businessModel);

					db.Commit();
				}
				catch(Exception ex)
				{
					db.Rollback();

					throw new ProviderException(String.Format("Table: {0}, Column: {1}", scanTableName, scanColumnName), ex);
				}
			}
		}


		private APGenTable[] GetAllTables(APGenBusinessModelSection businessModel)
		{
			APGenTable[] tables = new APGenTable[businessModel.Tables.Count];
			businessModel.Tables.CopyTo(tables, 0);

			return tables;
		}


		private void CreateTable(APDatabase db, APGenTable table)
		{
			_createTable(db, table);
			_createPrimaryKey(db, table);
			foreach (APGenIndex index in table.Indexes)
			{
				_createIndex(db, table, index);
			}
			foreach (APGenIndex unique in table.Uniques)
			{
				_createUnique(db, table, unique);
			}
		}


		private void ModifyTable(APDatabase db, APGenTable table)
		{
			Dictionary<string, columninfo> dbColumns = _getColumns(db, table);
			Dictionary<string, indexinfo> dbIndexes = _getIndexes(db, table);
			bool isTableEmpty = _isTableEmpty(db, table);



			// analysis columns

			List<APGenColumn> listCreateColumn = new List<APGenColumn>();
			List<APGenColumn> listModifyColumn = new List<APGenColumn>();
			Dictionary<string, columninfo> deleteColumns = new Dictionary<string, columninfo>(dbColumns, StringComparer.InvariantCultureIgnoreCase);

			foreach (APGenColumn column in table.Columns)
			{
				_setScanColumn(column.ColumnName);

				string colname = column.ColumnName;

				if (!dbColumns.ContainsKey(colname))
				{
					// db has not this column, wait for create.
					listCreateColumn.Add(column);
				}
				else
				{
					deleteColumns.Remove(colname);

					columninfo colinfo = dbColumns[colname];

					if (column.IsNullable != colinfo.isnullable
						|| column.DBDefaultValue != colinfo.dfvalue
						|| DBTypeName(column) != colinfo.GetTypeFullName()
						// no safe mode to change identity, so ingone change this
						// || (column.IdentityType == APColumnIdentityType.Database ^ colinfo.is_identity)
						)
					{
						listModifyColumn.Add(column);
					}
				}
			}


			// analysis indexes

			List<APGenIndex> listCreateIndex = new List<APGenIndex>();

			foreach (APGenIndex index in table.Indexes)
			{
				string ix_name = index.Name;

				if (!dbIndexes.ContainsKey(ix_name))
				{
					// db has not this index, wait for create.
					listCreateIndex.Add(index);
				}
				else
				{

					// db has this index and columns according fitted, then to nothing.
					// elsewise, wait fo modify this index.
					// drop in db indexes residual in dbIndexes.
					indexinfo info = dbIndexes[ix_name];
					string ix_keys = IndKeys(table, index, false);

					if (info.indkeys.Equals(ix_keys, StringComparison.InvariantCultureIgnoreCase))
					{
						bool maybe = false;
						foreach (APGenColumn column in listModifyColumn)
						{
							if (info.columns.ContainsKey(column.ColumnName))
							{
								listCreateIndex.Add(index);
								maybe = true;
								break;
							}
						}
						if (!maybe)
							dbIndexes.Remove(ix_name);
					}
					else
					{
						listCreateIndex.Add(index);
					}
				}
			}


			// analysis uniques

			List<APGenIndex> listCreateUnique = new List<APGenIndex>();

			foreach (APGenIndex index in table.Uniques)
			{
				string ix_name = index.Name;

				if (!dbIndexes.ContainsKey(ix_name))
				{
					// db has not this unique, wait for create.
					listCreateUnique.Add(index);
				}
				else
				{

					// db has this unique, then to nothing.
					// elsewise, wait fo modify this unique.
					// drop in db uniques residual in dbIndexes.
					indexinfo info = dbIndexes[ix_name];
					string uq_keys = IndKeys(table, index, true);

					if (info.indkeys.Equals(uq_keys, StringComparison.InvariantCultureIgnoreCase))
					{
						bool maybe = false;
						foreach (APGenColumn column in listModifyColumn)
						{
							if (info.columns.ContainsKey(column.ColumnName))
							{
								listCreateUnique.Add(index);
								maybe = true;
								break;
							}
						}
						if (!maybe)
							dbIndexes.Remove(ix_name);
					}
					else
					{
						listCreateUnique.Add(index);
					}
				}
			}

			// 1. dbIndexes for 'drop', but PK_ index analysis columns.
			// 2. listCreateIndex for 'create'.
			// 2. listCreateUnique for 'create'.


			// process

			string pkKeys = "";
			foreach (APGenColumn column in table.PrimaryKeyColumns)
			{
				if (pkKeys != "") pkKeys += ",";
				pkKeys += column.ColumnName;
			}


			// 1. drop indexes

			bool needAddPrimary = true;
			foreach (indexinfo info in dbIndexes.Values)
			{
				if (info.indtype == indexType.Primary && info.indkeys.Equals(pkKeys, StringComparison.InvariantCultureIgnoreCase))
					needAddPrimary = false;
				else
					_dropIndex(db, table, info);
			}

			// 2. drop columns

			foreach (columninfo info in deleteColumns.Values)
			{
				_dropColumn(db, table, info);
			}

			// 3. modify columns
			
			foreach (APGenColumn column in listModifyColumn)
			{
				_alterColumn(db, table, column, dbColumns[column.ColumnName]);
			}

			// 4. create columns

			foreach (APGenColumn column in listCreateColumn)
			{
				_createColumn(db, table, column, isTableEmpty);
			}

			// 5. mayby primary key

			if (needAddPrimary)
			{
				_createPrimaryKey(db, table);
			}

			// 6. create indexes

			foreach (APGenIndex index in listCreateIndex)
			{
				_createIndex(db, table, index);
			}

			// 7. create unique

			foreach (APGenIndex unique in listCreateUnique)
			{
				_createUnique(db, table, unique);
			}
		}


		private void SyncSequence(APDatabase db, APGenBusinessModelSection businessModel)
		{
			List<string> seqnames = new List<string>();
			DbCommand dbCmd = db.CreateSqlCommand("select sequence_name from user_sequences");
			using (IDataReader reader = dbCmd.ExecuteReader())
			{
				while (reader.Read())
				{
					seqnames.Add(reader.GetString(0));
				}
			}

			foreach (APGenTable table in businessModel.Tables)
			{
				foreach (APGenColumn column in table.Columns)
				{
					if (column.IdentityType == APColumnIdentityType.Provider
						&& businessModel.CanIdentityRelyOnProvider(column)
						&& column.ParsedType != typeof(Guid))
					{
						string seqname = SEQ_name(table.TableName, column.ColumnName).ToUpper();
						if (!seqnames.Contains(seqname))
						{
							dbCmd = db.CreateSqlCommand("create sequence {0} increment by 1 start with {1} nomaxvalue nocycle nocache",
								seqname, column.ProviderIdentityBase);
							dbCmd.ExecuteNonQuery();
						}
					}
				}
			}
		}


		#region [ Single Process ]


		private void _createTable(APDatabase db, APGenTable table)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("create table {0} (", table.TableName);
			foreach (APGenColumn column in table.Columns)
			{
				if (column.IdentityType == APColumnIdentityType.Database)
					ThrowDBUnsupport("identityType=\"Database\"");
				else
					sb.AppendFormat("\r\n\t{0} {1} {2} {3},",
						column.ColumnName, DBTypeName(column), DF_constraint(table, column), IsNullableString(column));
			}
			sb.Length--;
			sb.Append("\r\n)");


			// create table table_name (
			//    column_name column_type [default ...] [not] null,
			//    ...
			// )

			DbCommand dbCmd = db.CreateSqlCommand(sb.ToString());
			dbCmd.ExecuteNonQuery();
		}


		private void _createPrimaryKey(APDatabase db, APGenTable table)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("alter table {0} add constraint {1} primary key (", table.TableName, PK_name(table));
			foreach (APGenColumn column in table.PrimaryKeyColumns)
			{
				sb.AppendFormat("\r\n\t{0},", column.ColumnName);
			}
			sb.Length--;
			sb.Append("\r\n)");

			// alter table tablen_ame add constraint PK_table_name primary key(
			//    column_name,
			//    ...
			// )

			DbCommand dbCmd = db.CreateSqlCommand(sb.ToString());
			dbCmd.ExecuteNonQuery();
		}


		private void _createIndex(APDatabase db, APGenTable table, APGenIndex index)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("create index {0} on {1} (", index.Name, table.TableName);
			foreach (APGenOrder order in index.Orders)
			{
				sb.AppendFormat("\r\n\t{0} {1},", table.Columns[order.Name].ColumnName, order.According.ToString());
			}
			sb.Length--;
			sb.Append("\r\n)");

			// create index index_name on (
			//		column_name [ASC|DESC],
			//		...
			// )

			DbCommand dbCmd = db.CreateSqlCommand(sb.ToString());
			dbCmd.ExecuteNonQuery();
		}


		private void _createUnique(APDatabase db, APGenTable table, APGenIndex index)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("alter table {0} add constraint {1} unique (", table.TableName, index.Name);
			foreach (APGenOrder order in index.Orders)
			{
				sb.AppendFormat("\r\n\t{0},", table.Columns[order.Name].ColumnName);
			}
			sb.Length--;
			sb.Append("\r\n)");

			// alter table table_name add constraint uq_name (
			//		column_name,
			//		...
			// )

			DbCommand dbCmd = db.CreateSqlCommand(sb.ToString());
			dbCmd.ExecuteNonQuery();
		}


		private void _dropConstraint(APDatabase db, APGenTable table, string name)
		{
			DbCommand dbCmd = db.CreateSqlCommand("alter table {0} drop constraint {1}", table.TableName, name);
			dbCmd.ExecuteNonQuery();
		}


		private void _dropIndex(APDatabase db, APGenTable table, indexinfo info)
		{
			if (info.indtype != indexType.Index)
			{
				_dropConstraint(db, table, info.indname);
			}
			else
			{
				DbCommand dbCmd = db.CreateSqlCommand("drop index {0}", info.indname);
				dbCmd.ExecuteNonQuery();
			}
		}


		private void _dropColumn(APDatabase db, APGenTable table, columninfo info)
		{
			DbCommand dbCmd = db.CreateSqlCommand("alter table {0} drop column {1}", table.TableName, info.colname);
			dbCmd.ExecuteNonQuery();
		}


		private void _tempSql(string sql)
		{
			DbCommand dbCmd;
			using (APDatabase dbtemp = new APDatabase(this))
			{
				try
				{
					dbtemp.BeginTrans();
					dbCmd = dbtemp.CreateSqlCommand(sql);
					dbCmd.ExecuteNonQuery();
					dbtemp.Commit();
				}
				catch
				{
					dbtemp.Rollback();
				}
			}
		}


		private bool _sameType(string type1, string type2)
		{
			int index = type1.IndexOf('(');
			if (index != -1)
				type1 = type1.Substring(0, index);
			index = type2.IndexOf('(');
			if (index != -1)
				type2 = type2.Substring(0, index);

			if (type1 == type2)
				return true;

			return false;
		}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		private void _alterColumn(APDatabase db, APGenTable table, APGenColumn column, columninfo info)
		{
			DbCommand dbCmd;
			int totalCount, nullCount = 0;

			dbCmd = db.CreateSqlCommand("select count(*) from {0}", table.TableName, column.ColumnName);
			totalCount = Convert.ToInt32(dbCmd.ExecuteScalar());
			if (totalCount > 0)
			{
				dbCmd = db.CreateSqlCommand("select count(*) from {0} where {1} is null", table.TableName, column.ColumnName);
				nullCount = Convert.ToInt32(dbCmd.ExecuteScalar());

			}

			if (totalCount > 0)
			{
				if (!_sameType(DBTypeName(column), info.GetTypeFullName()) && nullCount != totalCount)
				{
					// change db type, the column value must be null
					if (!info.isnullable)
					{
						dbCmd = db.CreateSqlCommand("alter table {0} modify {1} null",
						  table.TableName, column.ColumnName);
						dbCmd.ExecuteNonQuery();
						info.isnullable = true;
					}
					_tempSql(String.Format("update {0} set {1} = null where {1} is not null", table.TableName, column.ColumnName));
					nullCount = totalCount;
				}
			}

			string sqlPadding = "";
			if (DBTypeName(column) != info.GetTypeFullName())
				sqlPadding += " " + DBTypeName(column);
			if (column.DBDefaultValue != info.dfvalue)
				sqlPadding += " " + DF_constraint(table, column);
			bool lazyNotNullable = false;
			if (column.IsNullable != info.isnullable)
			{
				if (column.IsNullable || nullCount == 0)
					sqlPadding += " " + IsNullableString(column);
				else
					lazyNotNullable = true;
			}
			if (sqlPadding != "")
			{
				dbCmd = db.CreateSqlCommand("alter table {0} modify {1}", table.TableName, column.ColumnName);
				dbCmd.CommandText += sqlPadding;
				dbCmd.ExecuteNonQuery();
			}

			if (lazyNotNullable)
			{
				string dfv = column.DBDefaultValue;
				if (dfv == "")
					dfv = "0";
				_tempSql(String.Format("update {0} set {1} = {2} where {1} is null", table.TableName, column.ColumnName, dfv));

				dbCmd = db.CreateSqlCommand("alter table {0} modify {1} not null", table.TableName, column.ColumnName);
				dbCmd.ExecuteNonQuery();
			}


			// alter table table_nae modify column_name type_name default dfvalue [not] null
			// go
		}


		private void _createColumn(APDatabase db, APGenTable table, APGenColumn column, bool isTableEmpty)
		{
			string sql = "";

			if (column.IdentityType == APColumnIdentityType.Database)
				ThrowDBUnsupport("identityType=\"Database\"");
			else
				sql = string.Format("alter table {0} add {1} {2} {3} {4}",
					table.TableName,
					column.ColumnName,
					DBTypeName(column),
					DF_constraint(table, column),
					IsNullableString(column));

			DbCommand dbCmd = db.CreateSqlCommand(sql);
			dbCmd.ExecuteNonQuery();
		}


		class columninfo
		{
			public int colid;
			public string colname;
			public string typename;
			public int length;
			public int xprec;
			public int xscale;
			public bool isnullable;
			public int dflength;
			public string dfvalue;

			public void Resolve()
			{
				if (typename == "number" && xprec == 0 && xscale == 0)
					xprec = 38;

				// because oracle can not cancel default value expression.
				if (dfvalue.ToLower() == "null")
				{
					dflength = 0;
					dfvalue = "";
				}
			}

			public string GetTypeFullName()
			{
				// When typename is [binary_double, binary_float, date],
				// the length is fixed.

				// When typename is [blob, clob, nclob], ignore the length.

				// unsupport [long, long raw, interval day to second, interval year to month, timestamp,
				// timestamp with local time zone, timestamp with time zone].

				if (typename == "raw" || typename == "varchar2" || typename == "char")
				{
					return String.Format("{0}({1})", typename, length);
				}
				else if (typename == "nvarchar2")
				{
					return String.Format("{0}({1})", typename, length / 2);
				}
				else if (typename == "number")
				{
					return String.Format("{0}({1}, {2})", typename, xprec, xscale);
				}

				return typename;
			}

			public string GetDefaultValue()
			{
				if (typename == "raw" || typename == "varchar2" || typename == "char" || typename == "nvarchar2")
				{
					return "''";
				}
				else if (typename =="blob" || typename == "clob" || typename=="nclob")
				{
					return "''";
				}
				else if (typename == "date")
				{
					return "sysdate";
				}
				else
				{
					return "0";
				}
			}
		}
		private bool _isTableEmpty(APDatabase db, APGenTable table)
		{
			DbCommand dbCmd = db.CreateSqlCommand(
@"select count(*)
from {0}", table.TableName);
			int count = Convert.ToInt32(dbCmd.ExecuteScalar());
			return count == 0;
		}
		private Dictionary<string, columninfo> _getColumns(APDatabase db, APGenTable table)
		{
			OracleCommand dbCmd = db.CreateSqlCommand(
@"select
	column_id,
	column_name,
	data_type,
	data_length,
	nvl(data_precision, 0),
	nvl(data_scale, 0),
	nullable,
	nvl(default_length, 0),
	data_default
from user_tab_columns
where table_name = '{0}'
order by column_id", table.TableName.ToUpper()) as OracleCommand;
			dbCmd.InitialLONGFetchSize = -1;

			Dictionary<string, columninfo> dbColumns = new Dictionary<string, columninfo>(StringComparer.InvariantCultureIgnoreCase);
			using (OracleDataReader reader = dbCmd.ExecuteReader())
			{
				while (reader.Read())
				{
					int idx = 0;

					columninfo data = new columninfo(){
						colid = Convert.ToInt32(reader.GetValue(idx++)),
						colname = Convert.ToString(reader.GetValue(idx++)),
						typename = Convert.ToString(reader.GetValue(idx++)).ToLower(),
						length = Convert.ToInt32(reader.GetValue(idx++)),
						xprec = Convert.ToInt32(reader.GetValue(idx++)),
						xscale = Convert.ToInt32(reader.GetValue(idx++)),
						isnullable = Convert.ToString(reader.GetValue(idx++)) == "Y",
						dflength = Convert.ToInt32(reader.GetValue(idx++)),
						dfvalue = "",
					};
					if (data.dflength > 0)
						data.dfvalue = reader.GetString(idx++).Trim();
					data.Resolve();
					dbColumns.Add(data.colname, data);
				}
			}
			return dbColumns;
		}

		enum indexType
		{
			Primary,
			Unique,
			Index
		}

		class indexinfo
		{
			public string indname;
			public indexType indtype;
			public Dictionary<string, bool> columns = new Dictionary<string, bool>();
			public string indkeys;

			public void pickKeys()
			{
				string ix_keys = "";
				foreach (var p in columns)
				{
					if (ix_keys != "") ix_keys += ",";
					ix_keys += p.Key;
					if (indtype == indexType.Index && p.Value)
						ix_keys += "(-)";
				}
				indkeys = ix_keys;
			}
		}

		private Dictionary<string, indexinfo> _getIndexes(APDatabase db, APGenTable table)
		{
			OracleCommand dbCmd = db.CreateSqlCommand(
@"select col.index_name, col.column_name, expr.column_expression, col.descend, nvl(cst.constraint_type,'C')
from user_ind_columns col
  left join user_ind_expressions expr
    on col.index_name=expr.index_name and col.table_name=expr.table_name and col.column_position=expr.column_position
  left join user_constraints cst on col.table_name=cst.table_name and col.index_name=cst.constraint_name
where col.table_name = '{0}'
order by col.index_name, col.column_position", table.TableName.ToUpper()) as OracleCommand;
			dbCmd.InitialLONGFetchSize = -1;

			Dictionary<string, indexinfo> dbIndexes = new Dictionary<string, indexinfo>(StringComparer.InvariantCultureIgnoreCase);
			using (OracleDataReader reader = dbCmd.ExecuteReader())
			{
				while (reader.Read())
				{
					string name = Convert.ToString(reader.GetValue(0));
					string col = Convert.ToString(reader.GetValue(1));
					if (!reader.IsDBNull(2))
					{
						col = reader.GetString(2);
						col = col.Substring(1, col.Length - 2);
					}
					bool is_descending_key = Convert.ToString(reader.GetValue(3)) == "DESC";
					string type = Convert.ToString(reader.GetValue(4));

					indexinfo data;
					if (dbIndexes.ContainsKey(name))
					{
						data = dbIndexes[name];
					}
					else
					{
						data = new indexinfo() { indname = name };
						if (type == "P")
							data.indtype = indexType.Primary;
						else if (type == "U")
							data.indtype = indexType.Unique;
						else
							data.indtype = indexType.Index;
						dbIndexes.Add(name, data);
					}
					data.columns.Add(col, is_descending_key);
				}
			}
			foreach (var item in dbIndexes.Values)
			{
				item.pickKeys();
			}
			return dbIndexes;
		}


		#endregion


		#region [ Helper ]


		private string DBTypeName(APGenColumn column)
		{
			if (column.DBType == DbType.Object)
			{
				// auto detect dbtype
				Type dataType = column.ParsedType;
				if (column.IsEnum || dataType == null || dataType.IsEnum)
				{
					return "number(10, 0)";
				}
				else
				{
					if (dataType.IsGenericType && dataType.IsValueType)
						dataType = Nullable.GetUnderlyingType(dataType);

					if (dataType == typeof(bool))
						return "number(1, 0)";
					else if (dataType == typeof(byte))
						return "number(3, 0)";
					else if (dataType == typeof(byte[]))
					{
						if (column.DataLength == 0 || column.DataLength > 2000)
							return "blob";
						else
							return "raw(" + column.DataLength.ToString() + ")";
					}
					else if (dataType == typeof(DateTime))
						return "date";
					else if (dataType == typeof(decimal))
					{
						if (column.Precision >= 1 && column.Precision <= 38 && column.Scale >= 0 && column.Scale <= column.Precision)
							return String.Format("number({0}, {1})", column.Precision, column.Scale);
						else
							ThrowColumnParsedTypeNotImplemented(dataType);
						return null;
					}
					else if (dataType == typeof(double))
						return "binary_double";
					else if (dataType == typeof(float))
						return "binary_float";
					else if (dataType == typeof(Guid))
						return "raw(16)";
					else if (dataType == typeof(short))
						return "number(5, 0)";
					else if (dataType == typeof(int))
						return "number(10, 0)";
					else if (dataType == typeof(long))
						return "number(19, 0)";
					else if (dataType == typeof(string))
					{
						if (column.DataLength == 0 || column.DataLength > 2000)
							return "nclob";
						else
							return "nvarchar2(" + column.DataLength.ToString() + ")";
					}
					else
					{
						ThrowColumnParsedTypeNotImplemented(dataType);
						return null;
					}
				}
			}
			else
			{
				switch (column.DBType)
				{
					case DbType.Boolean:
						return "number(1, 0)";
					case DbType.Byte:
						return "number(3, 0)";
					case DbType.Binary:
						{
							if (column.DataLength == 0 || column.DataLength > 2000)
								return "blob";
							else
								return "raw(" + column.DataLength.ToString() + ")";
						}
					case DbType.DateTime:
						return "date";
					case DbType.Decimal:
						{
							if (column.Precision >= 1 && column.Precision <= 38 && column.Scale >= 0 && column.Scale <= column.Precision)
								return String.Format("number({0}, {1})", column.Precision, column.Scale);
							else
								ThrowColumnParsedTypeNotImplemented(column.DBType);
							return null;
						}
					case DbType.Double:
						return "binary_double";
					case DbType.Single:
						return "binary_float";
					case DbType.Guid:
						return "raw(16)";
					case DbType.Int16:
						return "number(5, 0)";
					case DbType.Int32:
						return "number(10, 0)";
					case DbType.Int64:
						return "number(19, 0)";
					case DbType.String:
						if (column.DataLength == 0 || column.DataLength > 2000)
							return "nclob";
						else
							return "nvarchar2(" + column.DataLength.ToString() + ")";
					case DbType.AnsiString:
						if (column.DataLength == 0 || column.DataLength > 4000)
							return "clob";
						else
							return "varchar2(" + column.DataLength.ToString() + ")";
					case DbType.AnsiStringFixedLength:
						if (column.DataLength == 0)
							return "char(10)";
						else
							return "char(" + column.DataLength.ToString() + ")";
					case DbType.Currency:
						return "number(19, 4)";
					case DbType.StringFixedLength:
						if (column.DataLength == 0)
							return "nchar(10)";
						else
							return "nchar(" + column.DataLength.ToString() + ")";
					default:
						ThrowColumnParsedTypeNotImplemented(column.DBType);
						return null;
				}
			}
		}


		private string DF_constraint(APGenTable table, APGenColumn column)
		{
			if (column.DBDefaultValue != "")
				return String.Format("default {0}", column.DBDefaultValue);
			return "default null";
		}


		private string IsNullableString(APGenColumn column)
		{
			return column.IsNullable ? "null" : "not null";
		}


		private string SEQ_name(APGenTable table, APGenColumn column)
		{
			return "SEQ_" + table.TableName + "_" + column.ColumnName;
		}


		private string SEQ_name(string tableName, string columnName)
		{
			return "SEQ_" + tableName + "_" + columnName;
		}


		private string PK_name(APGenTable table)
		{
			return "PK_" + table.TableName;
		}


		private string IndKeys(APGenTable table, APGenIndex index, bool isunique)
		{
			string ix_keys = "";
			foreach (APGenOrder order in index.Orders)
			{
				if (ix_keys != "") ix_keys += ",";
				ix_keys += table.Columns[order.Name].ColumnName;
				if (!isunique && order.According == APSqlOrderAccording.Desc)
					ix_keys += "(-)";
			}
			return ix_keys;
		}


		#endregion

	}

}
