using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'NULL' Expression.
	/// </summary>
	public class APSqlNullExpr : APSqlExpr, IAPSqlValueExpr
	{

		#region [ Static ]


		/// <summary>
		/// The static SQL "NULL" Expression.
		/// </summary>
		public static APSqlNullExpr Expr = new APSqlNullExpr();


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlNullExpr.
		/// </summary>
		public APSqlNullExpr()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get
			{
				return "NULL";
			}
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return null; }
		}


		/// <summary>
		/// SQL value of assignment phrase expression.
		/// </summary>
		public string ValueExpr
		{
			get
			{
				return SelectExpr;
			}
		}


		#endregion

	}

}
