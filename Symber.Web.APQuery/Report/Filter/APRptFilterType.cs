
namespace Symber.Web.Report
{

	/// <summary>
	/// APRptColumn filter type.
	/// </summary>
	public enum APRptFilterType
	{

		/// <summary>
		/// Unknown type.
		/// No comparator
		/// </summary>
		Unknown,


		/// <summary>
		/// Text type.
		/// Comparator can be :
		///	Equals, NotEqual, GreaterThan, GreaterOrEqual, LessThan, LessOrEqual, StartsWith, Contains, DoesNotContain.
		/// </summary>
		Text,


		/// <summary>
		/// Boolean type.
		/// Comparator can be :
		///	Equals or NotEqual.
		/// </summary>
		Boolean,


		/// <summary>
		/// Number type.
		/// Comparator can be :
		///	Equals, NotEqual, GreaterThan, GreaterOrEqual, LessThan, LessOrEqual, Between.
		/// </summary>
		Number,


		/// <summary>
		/// DateTime type.
		/// Comparator can be :
		///	Equals, NotEqual, GreaterThan, GreaterOrEqual, LessThan, LessOrEqual, Between.
		/// </summary>
		DateTime,


		/// <summary>
		/// Enum or Integer Id.
		/// Comparator can be :
		///	Equals, NotEqual, GreaterThan, GreaterOrEqual, LessThan, LessOrEqual, Between.
		/// </summary>
		EnumOrId,


		/// <summary>
		/// MultiEnum
		/// Comparator can be :
		///	Equals, NotEqual, Includes, Excludes.
		/// </summary>
		MultiEnum

	}

}
