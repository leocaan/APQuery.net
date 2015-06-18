using System;
using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'INSERT' command.
	/// </summary>
	public sealed class APSqlInsertCommand : APSqlCommand
	{

		#region [ Fields ]


		private APTableDef _tableDef;
		private APSqlValuesClause _valuesClause;
		private APSqlSelectCommand _subQuery;
		private APSqlSelectClause _selectClause;
		private bool _needReturnScalar;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a 'INSERT' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		public APSqlInsertCommand(APTableDef tableDef)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");
			_tableDef = tableDef;
		}


		/// <summary>
		/// Create a 'INSERT' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <param name="subQuery">SubQuery.</param>
		/// <param name="clause">The 'SELECT' clause.</param>
		public APSqlInsertCommand(APTableDef tableDef, APSqlSelectCommand subQuery, APSqlSelectClause clause)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");

			_tableDef = tableDef;
			_subQuery = subQuery;
			_selectClause = clause;
		}


		/// <summary>
		/// Create a 'INSERT' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <param name="subQuery">SubQuery.</param>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public APSqlInsertCommand(APTableDef tableDef, APSqlSelectCommand subQuery, params APSqlSelectPhrase[] phrases)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");

			_tableDef = tableDef;
			_subQuery = subQuery;
			_selectClause = new APSqlSelectClause(phrases);
		}


		/// <summary>
		/// Create a 'INSERT' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <param name="subQuery">SubQuery.</param>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public APSqlInsertCommand(APTableDef tableDef, APSqlSelectCommand subQuery, IEnumerable<APSqlSelectPhrase> phrases)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");

			_tableDef = tableDef;
			_subQuery = subQuery;
			_selectClause = new APSqlSelectClause(phrases);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Table definition.
		/// </summary>
		public APTableDef TableDef
		{
			get { return _tableDef; }
		}


		/// <summary>
		/// 'VALUES' clause.
		/// </summary>
		public APSqlValuesClause ValuesClause
		{
			get { return _valuesClause; }
			set { _valuesClause = value; }
		}


		/// <summary>
		/// SubQuery.
		/// </summary>
		public APSqlSelectCommand SubQuery
		{
			get { return _subQuery; }
			internal set { _subQuery = value; }
		}


		/// <summary>
		/// 'SELECT' clause.
		/// </summary>
		public APSqlSelectClause SelectClause
		{
			get { return _selectClause; }
			set { _selectClause = value; }
		}


		/// <summary>
		/// If SQL 'INSERT' need return id, this value will be auto sets true;
		/// </summary>
		public bool NeedReturnAutoIncrement
		{
			get { return _needReturnScalar; }
			internal set { _needReturnScalar = value; }
		}


		#endregion

	}

}
