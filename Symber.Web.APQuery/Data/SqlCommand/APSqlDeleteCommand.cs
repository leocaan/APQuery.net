using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'DELETE' command.
	/// </summary>
	public sealed class APSqlDeleteCommand : APSqlCommand
	{

		#region [ Fields ]


		private APTableDef _tableDef;
		private APSqlWhereClause _whereClause;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new 'DELETE' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		public APSqlDeleteCommand(APTableDef tableDef)
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
