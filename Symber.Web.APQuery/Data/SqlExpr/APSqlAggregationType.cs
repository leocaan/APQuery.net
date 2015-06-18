
namespace Symber.Web.Data
{

	/// <summary>
	/// Sql Aggregation type.
	/// </summary>
	public enum APSqlAggregationType
	{

		/// <summary>
		/// Returns the average of the values in a group. Null values are ignored.
		/// </summary>
		AVG,

		/// <summary>
		/// Returns the binary checksum value computed over a row of a table or over a list of expressions. BINARY_CHECKSUM can be used to detect changes to a row of a table.
		/// </summary>
		BINARY_CHECKSUM,

		/// <summary>
		/// Returns the checksum value computed over a row of a table, or over a list of expressions. CHECKSUM is intended for use in building hash indices.
		/// </summary>
		CHECKSUM,
		
		/// <summary>
		/// Returns the checksum of the values in a group. Null values are ignored.
		/// </summary>
		CHECKSUM_AGG,
		
		/// <summary>
		/// Returns the number of items in a group.
		/// </summary>
		COUNT,
		
		/// <summary>
		/// Returns the number of items in a group. COUNT_BIG works like the COUNT function. The only difference between them is their return values: COUNT_BIG always returns a bigint data type value. COUNT always returns an int data type value.
		/// </summary>
		COUNT_BIG,
		
		/// <summary>
		/// Is an aggregate function that causes an additional column to be output with a value of 1 when the row is added by either the CUBE or ROLLUP operator, or 0 when the row is not the result of CUBE or ROLLUP.
		/// Grouping is allowed only in the select list associated with a GROUP BY clause that contains either the CUBE or ROLLUP operator.
		/// </summary>
		GROUPING,
		
		/// <summary>
		/// Returns the maximum value in the expression.
		/// </summary>
		MAX,
		
		/// <summary>
		/// Returns the minimum value in the expression.
		/// </summary>
		MIN,
		
		/// <summary>
		/// Returns the sum of all the values, or only the DISTINCT values, in the expression. SUM can be used with numeric columns only. Null values are ignored.
		/// </summary>
		SUM,
		
		/// <summary>
		/// Returns the statistical standard deviation of all values in the given expression.
		/// </summary>
		STDEV,
		
		/// <summary>
		/// Returns the statistical standard deviation for the population for all values in the given expression.
		/// </summary>
		STDEVP,
		
		/// <summary>
		/// Returns the statistical variance of all values in the given expression.
		/// </summary>
		VAR,
		
		/// <summary>
		/// Returns the statistical variance for the population for all values in the given expression.
		/// </summary>
		VARP

	}

}
