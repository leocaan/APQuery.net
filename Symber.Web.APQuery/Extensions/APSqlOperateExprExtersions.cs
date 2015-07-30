
namespace Symber.Web.Data
{

	/// <summary>
	/// APSqlOperateExpr Extersions.
	/// </summary>
	public static class APSqlOperateExprExtersions
	{

		#region [ 'AS' Extensions ]


		/// <summary>
		/// Create a new 'SELECT' phrase with alias.
		/// </summary>
		/// <param name="selectExpr">The SQL 'SELECT' Expression, can be alias.</param>
		/// <param name="alias">Alias.</param>
		/// <returns>'SELECT' phrase.</returns>
		public static APSqlSelectPhrase As(this APSqlOperateExpr selectExpr, string alias)
		{
			return new APSqlSelectPhrase(selectExpr, alias);
		}


		#endregion


		#region [ Aggregation Extensions ]


		/// <summary>
		/// Build a SQL aggregation Expression of average.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of average.</returns>
		public static APSqlAggregationExpr Avg(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.AVG);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of average.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of average.</returns>
		public static APSqlAggregationExpr Avg(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.AVG, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of binary checksum.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of binary checksum.</returns>
		public static APSqlAggregationExpr BinaryChecksum(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.BINARY_CHECKSUM);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of checksum.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of checksum.</returns>
		public static APSqlAggregationExpr Checksum(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.CHECKSUM);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of checksum agg.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of checksum agg.</returns>
		public static APSqlAggregationExpr ChecksumAgg(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.CHECKSUM_AGG);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of checksum agg.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of checksum agg.</returns>
		public static APSqlAggregationExpr ChecksumAgg(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.CHECKSUM_AGG, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of count.</returns>
		public static APSqlAggregationExpr Count(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.COUNT);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of count.</returns>
		public static APSqlAggregationExpr Count(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.COUNT, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count big.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of count big.</returns>
		public static APSqlAggregationExpr CountBig(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.COUNT_BIG);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count big.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of count big.</returns>
		public static APSqlAggregationExpr CountBig(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.COUNT_BIG, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of grouping.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of grouping.</returns>
		public static APSqlAggregationExpr Grouping(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.GROUPING);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of max.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of max.</returns>
		public static APSqlAggregationExpr Max(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.MAX);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of max.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of max.</returns>
		public static APSqlAggregationExpr Max(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.MAX, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of min.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of min.</returns>
		public static APSqlAggregationExpr Min(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.MIN);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of min.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of min.</returns>
		public static APSqlAggregationExpr Min(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.MIN, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of sum.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of sum.</returns>
		public static APSqlAggregationExpr Sum(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.SUM);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of sum.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of sum.</returns>
		public static APSqlAggregationExpr Sum(this APSqlOperateExpr expr, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.SUM, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical standard deviation.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of statistical standard deviation.</returns>
		public static APSqlAggregationExpr Stdev(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.STDEV);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical standard deviation for the population.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of statistical standard deviation for the population.</returns>
		public static APSqlAggregationExpr Stdevp(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.STDEVP);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical variance.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of statistical variance.</returns>
		public static APSqlAggregationExpr Var(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.VAR);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical variance for the population.
		/// </summary>
		/// <param name="expr">Operate expression.</param>
		/// <returns>SQL aggregation Expression of statistical variance for the population.</returns>
		public static APSqlAggregationExpr Varp(this APSqlOperateExpr expr)
		{
			return new APSqlAggregationExpr(expr, APSqlAggregationType.VARP);
		}


		#endregion

	}

}
