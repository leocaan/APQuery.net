using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'UPDATE' command.
	/// </summary>
	public sealed class APSqlUpdateCommand : APSqlCommand
	{

		#region [ Fields ]


		private APTableDef _tableDef;
		private APSqlValuesClause _valuesClause;
		private APSqlFromClause _fromClause;
		private APSqlWhereClause _whereClause;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new 'UPDATE' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		public APSqlUpdateCommand(APTableDef tableDef)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");
			_tableDef = tableDef;
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


		#endregion

	}

}
