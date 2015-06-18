
namespace Symber.Web.Data
{

	/// <summary>
	/// Sub query scalar restrict.
	/// </summary>
	public enum APSqlSubQueryScalarRestrict
	{

		/// <summary>
		/// No restrict.
		/// </summary>
		None,

		/// <summary>
		/// ALL requires the scalar_expression to compare positively to every value that is returned by the subquery.
		/// </summary>
		All,

		/// <summary>
		/// SOME requires the scalar_expression to compare positively to at least one value returned by the subquery.
		/// </summary>
		Some,

		/// <summary>
		/// SOME requires the scalar_expression to compare positively to at least one value returned by the subquery.
		/// Equals SOME restrict.
		/// </summary>
		Any

	}

}
