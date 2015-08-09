
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL Expression.
	/// </summary>
	public abstract class APSqlExpr
	{

		#region [ Properties ]


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public abstract string SelectExpr { get; }


		/// <summary>
		/// May be about TableDef
		/// </summary>
		internal abstract APTableDef MaybeTableDef { get; }


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="expr">Source.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(APSqlExpr expr)
		{
			return new APSqlExprPhrase(expr);
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="expr">Source.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(APSqlExpr expr)
		{
			return new APSqlSelectPhrase(expr);
		}


		#endregion

	}

}
