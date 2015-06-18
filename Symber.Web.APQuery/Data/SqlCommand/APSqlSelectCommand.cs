using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'SELECT' command.
	/// </summary>
	public sealed class APSqlSelectCommand : APSqlCommand
	{

		#region [ Fields ]


		private APSqlSelectMode _selectMode;
		private APSqlSelectClause _selectExprClause;
		private APSqlFromClause _fromClause;
		private APSqlWhereClause _whereClause;
		private APSqlGroupByClause _groupByClause;
		private APSqlWhereClause _havingClause;
		private APSqlOrderByClause _orderbyClause;
		private APSqlExpr _primaryKeyExpr;
		private int? _take;
		private int? _skip;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new 'SELECT' command.
		/// </summary>
		public APSqlSelectCommand()
		{
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' clause.
		/// </summary>
		/// <param name="clause">The 'SELECT' clause.</param>
		public APSqlSelectCommand(APSqlSelectClause clause)
		{
			_selectExprClause = clause;
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' phrase.
		/// </summary>
		/// <param name="phrase">The 'SELECT' phrase.</param>
		public APSqlSelectCommand(APSqlSelectPhrase phrase)
		{
			_selectExprClause = new APSqlSelectClause(phrase);
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' phrases.
		/// </summary>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public APSqlSelectCommand(params APSqlSelectPhrase[] phrases)
		{
			_selectExprClause = new APSqlSelectClause(phrases);
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' phrases.
		/// </summary>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public APSqlSelectCommand(IEnumerable<APSqlSelectPhrase> phrases)
		{
			_selectExprClause = new APSqlSelectClause(phrases);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// 'SELECT' mode, 'ALL' or 'DISTINCT'.
		/// </summary>
		public APSqlSelectMode SelectMode
		{
			get { return _selectMode; }
			set { _selectMode = value; }
		}


		/// <summary>
		/// 'SELECT' clause.
		/// </summary>
		public APSqlSelectClause SelectExprClause
		{
			get { return _selectExprClause; }
			set { _selectExprClause = value; }
		}


		/// <summary>
		/// 'FROM' clause.
		/// </summary>
		public APSqlFromClause FromClause
		{
			get { return _fromClause; }
			set { _fromClause = value; }
		}


		/// <summary>
		/// 'WHERE' clause.
		/// </summary>
		public APSqlWhereClause WhereClause
		{
			get { return _whereClause; }
			set { _whereClause = value; }
		}


		/// <summary>
		/// 'GROUP BY' clause.
		/// </summary>
		public APSqlGroupByClause GroupByClause
		{
			get { return _groupByClause; }
			set { _groupByClause = value; }
		}

		/// <summary>
		/// 'HAVING' clause.
		/// </summary>
		public APSqlWhereClause HavingClause
		{
			get { return _havingClause; }
			set { _havingClause = value; }
		}


		/// <summary>
		/// 'ORDER BY' clause.
		/// </summary>
		public APSqlOrderByClause OrderByClause
		{
			get { return _orderbyClause; }
			set { _orderbyClause = value; }
		}


		/// <summary>
		/// Primary key Expression.
		/// </summary>
		public APSqlExpr PrimeryKeyExpr
		{
			get { return _primaryKeyExpr; }
			set { _primaryKeyExpr = value; }
		}


		/// <summary>
		/// 'SELECT' query take size.
		/// </summary>
		public int? Take
		{
			get { return _take; }
			set { _take = value; }
		}


		/// <summary>
		/// 'SELECT' query skip size.
		/// </summary>
		public int? Skip
		{
			get { return _skip; }
			set { _skip = value; }
		}


		#endregion

	}

}
