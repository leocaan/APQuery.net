
namespace Symber.Web.Data
{

	/// <summary>
	/// APSqlAsteriskExpr Extensions.
	/// </summary>
	public static class APSqlAsteriskExprExtersions
	{

		#region [ Aggregation Extensions ]


		/// <summary>
		/// Build a aggregation phrase of count with SQL '[table_name.]*' Expression.
		/// </summary>
		/// <param name="expr">SQL '[table_name.]*' Expression.</param>
		/// <returns>Aggregation phrase of count.</returns>
		public static APSqlAggregationExpr Count(this APSqlAsteriskExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.COUNT);
		}


		#endregion

	}

}
