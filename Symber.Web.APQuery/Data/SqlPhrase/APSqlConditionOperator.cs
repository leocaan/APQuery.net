
namespace Symber.Web.Data
{

	/// <summary>
	/// Condition operator.
	/// </summary>
	public enum APSqlConditionOperator
	{

		/// <summary>
		/// Equals.
		/// </summary>
		Equals,


		/// <summary>
		/// Not equal.
		/// </summary>
		NotEqual,


		/// <summary>
		/// Greater than.
		/// </summary>
		GreaterThan,


		/// <summary>
		/// Greater than or equal.
		/// </summary>
		GreaterThanOrEqual,


		/// <summary>
		/// Less than.
		/// </summary>
		LessThan,


		/// <summary>
		/// Less than or equal.
		/// </summary>
		LessThanOrEqual,


		/// <summary>
		/// Between.
		/// </summary>
		Between,


		/// <summary>
		/// Not Between.
		/// </summary>
		NotBetween,


		/// <summary>
		/// Like.
		/// </summary>
		Like,


		/// <summary>
		/// Not Like.
		/// </summary>
		NotLike,


		/// <summary>
		/// In.
		/// </summary>
		In,


		/// <summary>
		/// Not in.
		/// </summary>
		NotIn,


		/// <summary>
		/// Exists, only for 'EXISTS ( subquery )'.
		/// </summary>
		Exists,


		/// <summary>
		/// Not Exists, only for 'NOT EXISTS ( subquery )'.
		/// </summary>
		NotExists

	}

}
