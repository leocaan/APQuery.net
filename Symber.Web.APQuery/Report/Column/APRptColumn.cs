using Symber.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Symber.Web.Report
{

	/// <summary>
	/// Virtual Report Column with SQL 'SELECT' Expression.
	/// </summary>
	public abstract class APRptColumn
	{

		#region [ Fields ]


		private APSqlOperateExpr _selectExpr;
		private string _id;
		private string _title;
		private string _dataName;
		private bool _required;
		private bool _isNullable;
		private bool _isIdentifier;
		private string _queryAlias;
		private Func<IDataReader, object> _jsonHandler;
		private string _renderFormatter;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		protected APRptColumn(APSqlOperateExpr selectExpr)
			: this(selectExpr, null, null)
		{
		}


		/// <summary>
		/// Create a new APRptColumn.
		/// </summary>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Column Title.</param>
		protected APRptColumn(string id, string title)
		{
			_id = id;
			_title = title;
		}


		/// <summary>
		/// Create a new APRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Column Title.</param>
		protected APRptColumn(APSqlOperateExpr selectExpr, string id, string title)
		{
			_selectExpr = selectExpr;

			if (selectExpr is APSqlColumnExpr)
			{
				var column = (selectExpr as APSqlColumnExpr).ColumnDef;

				_id = String.IsNullOrEmpty(id) ? column.SelectExpr : id;
				_title = String.IsNullOrEmpty(title) ? column.Display : title;
				_dataName = column.ColumnName;
				_required = column.Required;
				_isNullable = column.IsNullable;
			}
			else
			{
				var paramName = (selectExpr as APSqlOperateExpr).ParamName;

				_id = String.IsNullOrEmpty(id) ? paramName : id;
				_title = String.IsNullOrEmpty(title) ? paramName : title;
				_dataName = paramName;
				_required = false;
				_isNullable = true;
			}
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets SQL 'SELECT' Expression.
		/// </summary>
		public virtual APSqlOperateExpr SelectExpr
		{
			get { return _selectExpr; }
		}


		/// <summary>
		/// Whether column is nullable.
		/// </summary>
		public virtual bool IsNullable
		{
			get { return _isNullable; }
			set { _isNullable = value; }
		}


		/// <summary>
		/// Gets or sets column is required.
		/// </summary>
		public virtual bool IsRequired
		{
			get { return _required; }
			set { _required = value; }
		}


		/// <summary>
		/// Gets or sets column unique ID.
		/// </summary>
		public virtual string ID
		{
			get { return _id; }
			set { _id = value; }
		}


		/// <summary>
		/// Gets or sets column title.
		/// </summary>
		public virtual string Title
		{
			get { return _title; }
			set { _title = value; }
		}


		/// <summary>
		/// Gets or sets query alias.
		/// </summary>
		public virtual string QueryAlias
		{
			get { return _queryAlias; }
			set { _queryAlias = value; }
		}


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public virtual APRptFilterType FilterType
		{
			get { return APRptFilterType.Unknown; }
		}


		/// <summary>
		/// The column data access name in IDataReader or DataTable.
		/// </summary>
		public virtual string DataName
		{
			get { return _queryAlias ?? _dataName; }
		}


		/// <summary>
		/// Gets or sets column is Identifier. If true, that willl CanBeField allway return false.
		/// </summary>
		public bool IsIdentifier
		{
			get { return _isIdentifier; }
			set { _isIdentifier = value; }
		}


		/// <summary>
		/// Gets a value whether the column can be field show in grid.
		/// </summary>
		public virtual bool CanBeField
		{
			get { return !_isIdentifier; }
		}


		/// <summary>
		/// Gets or sets JSON handler.
		/// </summary>
		public virtual Func<IDataReader, object> JsonHandler
		{
			get { return _jsonHandler; }
			set { _jsonHandler = value; }
		}


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public virtual string RenderFormatter
		{
			get { return _renderFormatter; }
			set { _renderFormatter = value; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Gets JSON value from data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The value.</returns>
		public object Json(IDataReader reader)
		{
			if (_jsonHandler != null)
				return _jsonHandler(reader);

			return DefaultJson(reader);
		}


		/// <summary>
		/// Gets JSON value of the column filter field.
		/// </summary>
		/// <returns>The filter field, default return type 'APRptJsonFilterField', you can return other type.</returns>
		public virtual object GetFilterField()
		{
			return new APRptJsonFilterField(ID, Title, FilterType.ToString());
		}


		/// <summary>
		/// Gets client validate attributes.
		/// </summary>
		/// <returns>Attributes dictionary.</returns>
		public virtual IDictionary<string, string> GetValidateAttributes()
		{
			Dictionary<string, string> attrs = new Dictionary<string, string>();

			if (IsRequired)
				attrs.Add("required", null);

			return attrs;
		}


		/// <summary>
		/// Add an APSqlSelectPhrase to list with the column.
		/// </summary>
		/// <param name="phrases">The list of APSqlSelectPhrase.</param>
		public virtual void AddToQuerySelectPhrases(List<APSqlSelectPhrase> phrases)
		{
			if (CanBeField)
				phrases.Add(new APSqlSelectPhrase(_selectExpr, _queryAlias));
		}


		/// <summary>
		/// Add an APSqlFromPhrase to list with the column.
		/// </summary>
		/// <param name="phrases">This list of APSqlFromPhrase.</param>
		public virtual void AddToQueryFromPhrases(List<APSqlFromPhrase> phrases)
		{
			//APTableDef table = _selectExpr.MaybeTableDef;
			//if (table == null)
			//	return;

			//foreach(var exist in phrases)
			//{
			//	if (exist.TableDef == table)
			//		return;
			//}

			//if (phrases.Count > 0 && phrases[0].JoinType != APSqlJoinType.None)
			//	phrases.Insert(0, new APSqlFromPhrase(table));
			//else
			//	phrases.Add(new APSqlFromPhrase(table));
		}


		/// <summary>
		/// Add an APSqlWherePhrase to list with the column.
		/// </summary>
		/// <param name="phrases">The list of APSqlWherePhrase.</param>
		public virtual void AddToQueryWherePhrases(List<APSqlWherePhrase> phrases)
		{
			// Do nothing
		}


		/// <summary>
		/// Get an APSqlOrderPhrase.
		/// </summary>
		/// <param name="according">Order according.</param>
		/// <returns>The APSqlOrderPhrase.</returns>
		public virtual APSqlOrderPhrase GetQueryOrderByPhrase(APSqlOrderAccording according)
		{
			return new APSqlOrderPhrase(_selectExpr, according);
		}


		/// <summary>
		/// Parse query where phrase.
		/// </summary>
		/// <param name="comparator">Comparator.</param>
		/// <param name="values">Values.</param>
		/// <returns>An APSqlWherePhrase.</returns>
		/// <exception cref="APRptFilterParseException">Throw exception on parse error.</exception>
		public abstract APSqlWherePhrase ParseQueryWherePhrase(APRptFilterComparator comparator, params string[] values);


		/// <summary>
		/// Add an APSqlSelectPhrase to list with the column.
		/// </summary>
		/// <param name="phrases">The list of APSqlSelectPhrase.</param>
		public virtual void AddToQueryGroupPhrases(List<APSqlExprPhrase> phrases)
		{
			if (CanBeField)
				phrases.Add(new APSqlExprPhrase(_selectExpr));
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Parse 'WHERE' phrase value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Parsed value.</returns>
		protected virtual object TryGetFilterValue(string value)
		{
			if (value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"')
				value = value.Substring(1, value.Length - 2);

			return value;
		}


		/// <summary>
		/// Get 'WHERE' phrase.
		/// </summary>
		/// <param name="op">Condition operator.</param>
		/// <param name="value">Value.</param>
		/// <returns>An APSqlWherePhrase.</returns>
		protected virtual APSqlWherePhrase GetQueryWherePhrase(APSqlConditionOperator op, string value)
		{
			object v = null;

			if (value == "NULL")
			{
				v = DBNull.Value;
			}
			else
			{
				v = TryGetFilterValue(value);
			}

			return new APSqlConditionPhrase(_selectExpr, op, v);
		}


		/// <summary>
		/// Get 'WHERE' phrase.
		/// </summary>
		/// <param name="op">Condition operator.</param>
		/// <param name="joinType">Condition join type.</param>
		/// <param name="values">Values.</param>
		/// <returns>An APSqlWherePhrase.</returns>
		protected virtual APSqlWherePhrase GetQueryWherePhrase(APSqlConditionOperator op, APSqlConditionJoinType joinType, string[] values)
		{
			List<APSqlWherePhrase> list = new List<APSqlWherePhrase>();

			foreach (string value in values)
				list.Add(GetQueryWherePhrase(op, value));

			if (list.Count == 1)
				return list[0];

			if (joinType == APSqlConditionJoinType.AND)
				return new APSqlConditionAndPhrase(list);
			else
				return new APSqlConditionOrPhrase(list);
		}


		/// <summary>
		/// Inner used. if other use may unsafe.
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		internal Array GetFilterValues(string[] values)
		{
			List<object> list = new List<object>();

			foreach (string value in values)
			{
				list.Add(TryGetFilterValue(value));
			}

			return list.ToArray();
		}


		/// <summary>
		/// Gets default JSON value from data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The value.</returns>
		protected virtual object DefaultJson(IDataReader reader)
		{
			return reader[DataName];
		}


		#endregion

	}

}
