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
	/// Manage data access in SQLServer database.
	/// </summary>
	public partial class SqlAPDalProvider : APDalProvider
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

						DbCommand dbCmd = db.CreateSqlCommand("select id from sysobjects where type = 'U' and name = '{0}'", table.TableName);
						object id = dbCmd.ExecuteScalar();
						if (id == null)
							CreateTable(db, table);
						else
							ModifyTable(db, table);
					}


					SyncCurrentId(db, businessModel);
					//_createCurrentIdProc(db);


					db.Commit();
				}
				catch (Exception ex)
				{
					db.Rollback();

					throw new ProviderException(String.Format("Table: {0}, Column: {1}", scanTableName, scanColumnName), ex);
				}
			}
		}


		static string currentIdTable = "ap_query_mapid";
		static string currentIdName = "name";
		static string currentIdValue = "value";
		//static string currentIdNewIdProc = "ap_Query_NewId";
		private APGenTable[] GetAllTables(APGenBusinessModelSection businessModel)
		{
			// CurrentId table also include in.

			APGenTable tableCurrentId = new APGenTable() { Name = currentIdTable, TableName = currentIdTable };
			tableCurrentId.Columns.Add(new APGenColumn() { Name = currentIdName, ColumnName = currentIdName, PrimaryKey = true, IsNullable = false, DBType = DbType.String, DataLength = 512 });
			tableCurrentId.Columns.Add(new APGenColumn() { Name = currentIdValue, ColumnName = currentIdValue, IsNullable = false, DBType = DbType.Int64 });

			APGenTable[] tables = new APGenTable[businessModel.Tables.Count + 1];
			tables[0] = tableCurrentId;
			businessModel.Tables.CopyTo(tables, 1);

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
			Dictionary<string, columninfo> deleteColumns = new Dictionary<string, columninfo>(dbColumns);

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
				string ix_name = index.Name;// IX_name(table, index);

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
					string ix_keys = IndKeys(table, index);

					if (info.indkeys == ix_keys)
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
				string ix_name = index.Name;// UQ_name(table, index);

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
					string uq_keys = IndKeys(table, index);

					if (info.indkeys == uq_keys)
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
				if (info.indtype == indexType.Primary && info.indkeys == pkKeys)
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


		private void SyncCurrentId(APDatabase db, APGenBusinessModelSection businessModel)
		{
			List<string> tcnames = new List<string>();
			DbCommand dbCmd = db.CreateSqlCommand("select {1} from {0}", currentIdTable, currentIdName);
			using (IDataReader reader = dbCmd.ExecuteReader())
			{
				while (reader.Read())
				{
					tcnames.Add(reader.GetString(0));
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
						string tcname = table.TableName + "." + column.ColumnName;
						if (!tcnames.Contains(tcname))
						{
							dbCmd = db.CreateSqlCommand("insert into {0} ({1}, {2}) values ('{3}', '{4}')",
								currentIdTable, currentIdName, currentIdValue,
								tcname, column.ProviderIdentityBase);
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
					sb.AppendFormat("\r\n\t{0} {1} identity {2} {3},",
						column.ColumnName, DBTypeName(column), IsNullableString(column), DF_constraint(table, column));
				else
					sb.AppendFormat("\r\n\t{0} {1} {2} {3},",
						column.ColumnName, DBTypeName(column), IsNullableString(column), DF_constraint(table, column));
			}
			sb.Length--;
			sb.Append("\r\n)");


			// create table table_name (
			//    column_name column_type [identity] [not] null [constraint ... default ...],
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

			sb.AppendFormat("create index {0} on {1} (", index.Name/* IX_name(table, index)*/, table.TableName);
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

			sb.AppendFormat("alter table {0} add constraint {1} unique (", table.TableName, index.Name/* UQ_name(table, index)*/);
			foreach (APGenOrder order in index.Orders)
			{
				sb.AppendFormat("\r\n\t{0} {1},", table.Columns[order.Name].ColumnName, order.According.ToString());
			}
			sb.Length--;
			sb.Append("\r\n)");

			// alter table table_name add constraint uq_name (
			//		column_name DESC,
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
				DbCommand dbCmd = db.CreateSqlCommand("drop index {1} on {0}", table.TableName, info.indname);
				dbCmd.ExecuteNonQuery();
			}
		}


		private void _dropColumn(APDatabase db, APGenTable table, columninfo info)
		{
			if (info.dfname != "")
				_dropConstraint(db, table, info.dfname);

			DbCommand dbCmd = db.CreateSqlCommand("alter table {0} drop column {1}", table.TableName, info.colname);
			dbCmd.ExecuteNonQuery();
		}


		private void _alterColumn(APDatabase db, APGenTable table, APGenColumn column, columninfo info)
		{
			if (info.dfname != "")
				_dropConstraint(db, table, info.dfname);


			DbCommand dbCmd;
			// no safe mode to change identity, so ingone this, throw sql exception.


			if (info.isnullable && !column.IsNullable)
			{
				dbCmd = db.CreateSqlCommand("select count(*) from {0} where {1} is null", table.TableName, column.ColumnName);
				if ((int)dbCmd.ExecuteScalar() > 0)
				{
					// when column nullable change to not nullable and has data,
					// set default value.
					dbCmd = db.CreateSqlCommand("update {0} set {1} = {2} where {1} is null", table.TableName, column.ColumnName, info.GetDefaultValue());
					dbCmd.ExecuteNonQuery();
				}
			}

			dbCmd = db.CreateSqlCommand("alter table {0} alter column {1} {2} {3}",
			  table.TableName, column.ColumnName, DBTypeName(column), IsNullableString(column));
			dbCmd.ExecuteNonQuery();


			if (column.DBDefaultValue != "")
			{
				dbCmd = db.CreateSqlCommand("alter table {0} add {1} for {2}",
					table.TableName, DF_constraint(table, column), column.ColumnName);
				dbCmd.ExecuteNonQuery();
			}

			// alter table table_nae alter column column_name type_name [not] null
			// go
			// alter table add constraint DF_tablename_columnname default dfvalue for column_name
			// go
		}


		private void _createColumn(APDatabase db, APGenTable table, APGenColumn column, bool isTableEmpty)
		{
			bool changeNullable = (!isTableEmpty && !column.IsNullable);
			if (changeNullable)
			{
				column.IsNullable = true;
			}

			string sql;

			if (column.IdentityType == APColumnIdentityType.Database)
				sql = string.Format("alter table {0} add {1} {2} IDENTITY {3} {4}",
					table.TableName,
					column.ColumnName,
					DBTypeName(column),
					IsNullableString(column),
					DF_constraint(table, column));
			else
				sql = string.Format("alter table {0} add {1} {2} {3} {4}",
					table.TableName,
					column.ColumnName,
					DBTypeName(column),
					IsNullableString(column),
					DF_constraint(table, column));

			DbCommand dbCmd = db.CreateSqlCommand(sql);
			dbCmd.ExecuteNonQuery();

			if (changeNullable)
			{
				column.IsNullable = false;
				string defaultValue = column.DBDefaultValue == "" ? "''" : column.DBDefaultValue;

				dbCmd = db.CreateSqlCommand("update {0} set {1} = {2}", table.TableName, column.ColumnName, defaultValue);
				dbCmd.ExecuteNonQuery();

				dbCmd = db.CreateSqlCommand("alter table {0} alter column {1} {2} {3}",
				  table.TableName, column.ColumnName, DBTypeName(column), IsNullableString(column));
				dbCmd.ExecuteNonQuery();
			}
		}


		class columninfo
		{
			public int colid;
			public string colname;
			public string typename;
			public bool variable;
			public int length;
			public int xprec;
			public int xscale;
			public bool isnullable;
			public string dfname;
			public string dfvalue;
			public bool is_identity;

			public void Resolve()
			{
				if (dfname == String.Empty)
					return;
				dfvalue = dfvalue.Substring(1, dfvalue.Length - 2);
				if (dfvalue[0] == '(')
					dfvalue = dfvalue.Substring(1, dfvalue.Length - 2);
			}

			public string GetTypeFullName()
			{
				// When typename is [tinyint, smallint, int, bigint
				// bit, float, real, datetime, uniqueidentifier, money],
				// the length is fixed.

				// When typename is [image, ntext, text], ignore the length.

				if (typename == "varbinary" || typename == "varchar" || typename == "char")
				{
					return String.Format("{0}({1})", typename, length);
				}
				else if (typename == "nvarchar" || typename == "nchar")
				{
					return String.Format("{0}({1})", typename, length / 2);
				}
				else if (typename == "decimal")
				{
					return String.Format("{0}({1}, {2})", typename, xprec, xscale);
				}

				return typename;
			}

			public string GetDefaultValue()
			{
				if (typename == "varbinary" || typename == "varchar" || typename == "char" || typename == "nvarchar" || typename == "nchar")
				{
					return "''";
				}
				else if (typename == "image" || typename == "ntext" || typename == "text")
				{
					return "''";
				}
				else if (typename == "datetime")
				{
					return "getdate()";
				}
				else if (typename == "uniqueidentifier")
				{
					return "newid()";
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
			DbCommand dbCmd = db.CreateSqlCommand(
@"select
	col.colid,
	col.name as colname,
	tp.name as typename,
	tp.variable,
	col.length,
	col.xprec,
	col.xscale,
	col.isnullable,
	df.name as dfname,
	df.definition as dfvalue,
	id.is_identity
from
	syscolumns as col
	inner join systypes as tp on col.xtype = tp.xtype and tp.name <> 'sysname'
	left join sys.default_constraints as df on col.cdefault = df.object_id
	left join sys.identity_columns as id on col.colid = id.column_id and id.object_id = col.id
where col.id = object_id('{0}')
order by col.colid", table.TableName);

			Dictionary<string, columninfo> dbColumns = new Dictionary<string, columninfo>();
			using (IDataReader reader = dbCmd.ExecuteReader())
			{
				while (reader.Read())
				{
					int idx = 0;
					columninfo data = new columninfo()
					{
						colid = Convert.ToInt32(reader.GetValue(idx++)),
						colname = Convert.ToString(reader.GetValue(idx++)),
						typename = Convert.ToString(reader.GetValue(idx++)),
						variable = Convert.ToBoolean(reader.GetValue(idx++)),
						length = Convert.ToInt32(reader.GetValue(idx++)),
						xprec = Convert.ToInt32(reader.GetValue(idx++)),
						xscale = Convert.ToInt32(reader.GetValue(idx++)),
						isnullable = Convert.ToBoolean(reader.GetValue(idx++)),
						dfname = Convert.ToString(reader.GetValue(idx++)),
						dfvalue = Convert.ToString(reader.GetValue(idx++)),
						is_identity = reader.IsDBNull(idx++) ? false : true,
					};
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
					if (p.Value)
						ix_keys += "(-)";
				}
				indkeys = ix_keys;
			}
		}

		private Dictionary<string, indexinfo> _getIndexes(APDatabase db, APGenTable table)
		{
			DbCommand dbCmd = db.CreateSqlCommand(
@"select
	idx.name,
	col.name as col,
	idxCol.is_descending_key,
	idx.is_unique,
	idx.is_primary_key,
	idx.is_unique_constraint
from
	sys.indexes as idx
	inner join sys.index_columns as idxCol on (idx.object_id = idxCol.object_id AND idx.index_id = idxCol.index_id)
	inner join sys.columns as col on (idx.object_id = col.object_id AND idxCol.column_id = col.column_id)
where idx.object_id = object_id('{0}')
order by idx.name", table.TableName);

			Dictionary<string, indexinfo> dbIndexes = new Dictionary<string, indexinfo>();
			using (IDataReader reader = dbCmd.ExecuteReader())
			{
				while (reader.Read())
				{
					string name = Convert.ToString(reader.GetValue(0));
					string col = Convert.ToString(reader.GetValue(1));
					bool is_descending_key = Convert.ToBoolean(reader.GetValue(2));
					bool is_unique = Convert.ToBoolean(reader.GetValue(3));
					bool is_primary_key = Convert.ToBoolean(reader.GetValue(4));
					bool is_unique_constraint = Convert.ToBoolean(reader.GetValue(5));

					indexinfo data;
					if (dbIndexes.ContainsKey(name))
					{
						data = dbIndexes[name];
					}
					else
					{
						data = new indexinfo() { indname = name };
						if (is_primary_key)
							data.indtype = indexType.Primary;
						else if (is_unique_constraint)
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


//		private void _createCurrentIdProc(APDatabase db)
//		{
//			string sql =
//@"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ap_Query_NewId]') AND type in (N'P', N'PC'))
//BEGIN
//EXEC dbo.sp_executesql @statement = N'
//CREATE PROCEDURE [dbo].[ap_Query_NewId]
//(
//	@name      nvarchar(512)
//)
//AS
//BEGIN
//
//	UPDATE ap_query_mapid
//	SET value = value + 1
//	WHERE name = @name
//
//	SELECT value
//	FROM ap_query_mapid
//	WHERE name = @name
//
//END
//' 
//END
//";
//			DbCommand cmd = db.CreateSqlCommand(sql);
//			cmd.ExecuteNonQuery();
//		}


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
					return "int";
				}
				else
				{
					if (dataType.IsGenericType && dataType.IsValueType)
						dataType = Nullable.GetUnderlyingType(dataType);

					if (dataType == typeof(bool))
						return "bit";
					else if (dataType == typeof(byte))
						return "tinyint";
					else if (dataType == typeof(byte[]))
					{
						if (column.DataLength == 0 || column.DataLength > 8000)
							return "image";
						else
							return "varbinary(" + column.DataLength.ToString() + ")";
					}
					else if (dataType == typeof(DateTime))
						return "datetime";
					else if (dataType == typeof(decimal))
					{
						if (column.Precision >= 1 && column.Precision <= 38 && column.Scale >= 0 && column.Scale <= column.Precision)
							return String.Format("decimal({0}, {1})", column.Precision, column.Scale);
						else
							ThrowColumnParsedTypeNotImplemented(dataType);
						return null;
					}
					else if (dataType == typeof(double))
						return "float";
					else if (dataType == typeof(float))
						return "real";
					else if (dataType == typeof(Guid))
						return "uniqueidentifier";
					else if (dataType == typeof(short))
						return "smallint";
					else if (dataType == typeof(int))
						return "int";
					else if (dataType == typeof(long))
						return "bigint";
					else if (dataType == typeof(string))
					{
						if (column.DataLength == 0 || column.DataLength > 4000)
							return "ntext";
						else
							return "nvarchar(" + column.DataLength.ToString() + ")";
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
						return "bit";
					case DbType.Byte:
						return "tinyint";
					case DbType.Binary:
						{
							if (column.DataLength == 0 || column.DataLength > 8000)
								return "image";
							else
								return "varbinary(" + column.DataLength.ToString() + ")";
						}
					case DbType.DateTime:
						return "datetime";
					case DbType.Decimal:
						{
							if (column.Precision >= 1 && column.Precision <= 38 && column.Scale >= 0 && column.Scale <= column.Precision)
								return String.Format("decimal({0}, {1})", column.Precision, column.Scale);
							else
								ThrowColumnParsedTypeNotImplemented(column.DBType);
							return null;
						}
					case DbType.Double:
						return "float";
					case DbType.Single:
						return "real";
					case DbType.Guid:
						return "uniqueidentifier";
					case DbType.Int16:
						return "smallint";
					case DbType.Int32:
						return "int";
					case DbType.Int64:
						return "bigint";
					case DbType.String:
						if (column.DataLength == 0 || column.DataLength > 4000)
							return "ntext";
						else
							return "nvarchar(" + column.DataLength.ToString() + ")";
					case DbType.AnsiString:
						if (column.DataLength == 0 || column.DataLength > 8000)
							return "text";
						else
							return "varchar(" + column.DataLength.ToString() + ")";
					case DbType.AnsiStringFixedLength:
						if (column.DataLength == 0)
							return "char(10)";
						else
							return "char(" + column.DataLength.ToString() + ")";
					case DbType.Currency:
						return "money";
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
				return String.Format("constraint {0} default {1}", DF_name(table, column), column.DBDefaultValue);
			return "";
		}


		private string IsNullableString(APGenColumn column)
		{
			return column.IsNullable ? "null" : "not null";
		}


		private string DF_name(APGenTable table, APGenColumn column)
		{
			return "DF_" + table.TableName + "_" + column.ColumnName;
		}


		//private string IX_name(APGenTable table, APGenIndex index)
		//{
		//	string ix = "IX";
		//	foreach (APGenOrder order in index.Orders)
		//	{
		//		ix += "_" + table.Columns[order.Name].ColumnName;
		//	}
		//	return ix;
		//}


		//private string UQ_name(APGenTable table, APGenIndex index)
		//{
		//	string ix = "UQ";
		//	foreach (APGenOrder order in index.Orders)
		//	{
		//		ix += "_" + table.Columns[order.Name].ColumnName;
		//	}
		//	return ix;
		//}


		private string PK_name(APGenTable table)
		{
			return "PK_" + table.TableName;
		}


		private string IndKeys(APGenTable table, APGenIndex index)
		{
			string ix_keys = "";
			foreach (APGenOrder order in index.Orders)
			{
				if (ix_keys != "") ix_keys += ",";
				ix_keys += table.Columns[order.Name].ColumnName;
				if (order.According == APSqlOrderAccording.Desc)
					ix_keys += "(-)";
			}
			return ix_keys;
		}


		#endregion

	}

}
