using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL '[table_name.]*' Expression.
	/// </summary>
	public class APSqlAsteriskExpr : APSqlExpr
	{

		#region [ Static ]


		/// <summary>
		/// The static SQL "*" Expression.
		/// </summary>
		public static APSqlAsteriskExpr Expr = new APSqlAsteriskExpr();


		#endregion


		#region [ Fields ]


		private readonly APTableDef _tableDef;
		private string _expression;


		#endregion
		

		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlAsteriskExpr.
		/// </summary>
		public APSqlAsteriskExpr()
		{
		}


		/// <summary>
		/// Create a new APSqlAsteriskExpr.
		/// </summary>
		/// <param name="tableName">Table name in database.</param>
		public APSqlAsteriskExpr(string tableName)
		{
			_tableDef = new APTableDef(tableName);
		}


		/// <summary>
		/// Create a new APSqlAsteriskExpr.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		public APSqlAsteriskExpr(APTableDef tableDef)
		{
			_tableDef = tableDef;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Table defined in database.
		/// </summary>
		public APTableDef TableDef
		{
			get { return _tableDef; }
		}



		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get
			{
				if (_expression == null)
				{
					string tn = _tableDef == null ? "" : _tableDef.SelectExpr;
					_expression = String.IsNullOrEmpty(tn) ? "*" : tn + ".*";
				}
				return _expression;
			}
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return _tableDef; }
		}


		#endregion

	}

}
