
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'JOIN' type.
	/// </summary>
	public enum APSqlJoinType
	{

		/// <summary>
		/// Not join.
		/// </summary>
		None,

		/// <summary>
		/// Inner join.
		/// </summary>
		Inner,

		/// <summary>
		/// Left outer join.
		/// </summary>
		Left,

		/// <summary>
		/// Right outer join.
		/// </summary>
		Right,

		/// <summary>
		/// Full outer join.
		/// </summary>
		Full,

		/// <summary>
		/// Cross join.
		/// </summary>
		Cross,

	}

}
