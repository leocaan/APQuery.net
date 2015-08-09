
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL Expression.
	/// </summary>
	public abstract class APSqlExpr
	{

		#region [ Static ]


		/// <summary>
		/// The escape char '@'.
		/// </summary>
		internal const char EscapeChar = '@';


		/// <summary>
		/// Try to parse a string to APSqlExpr.
		/// </summary>
		/// <param name="raw">The raw string.</param>
		/// <returns>APSqlExpr</returns>
		internal static APSqlExpr FitStringToExpr(string raw)
		{
			if (raw == null)
				return new APSqlNullExpr();

			var length = raw.Length;
			if (length > 0 && raw[0] == EscapeChar)
			{
				if (length > 1 && raw[1] == EscapeChar)
					return new APSqlConstExpr(raw.Substring(1));
				return new APSqlRawExpr(raw.Substring(1));
			}

			return new APSqlConstExpr(raw);
		}


		internal static object TryFitStringToRawExpr(string raw)
		{
			var length = raw.Length;
			if (length > 0 && raw[0] == EscapeChar)
			{
				if (length > 1 && raw[1] == EscapeChar)
					return raw.Substring(1);
				return new APSqlRawExpr(raw.Substring(1));
			}

			return raw;
		}


		#endregion

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
