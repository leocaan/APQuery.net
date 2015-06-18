
namespace Symber.Web.Data
{

	/// <summary>
	/// Relation cascade type.
	/// </summary>
	public enum APRelationCascadeType
	{

		/// <summary>
		/// No cascade.
		/// </summary>
		None,

		/// <summary>
		/// Warning if there are relative data.
		/// </summary>
		Warning,

		/// <summary>
		/// Cascade update and delete all relative data.
		/// </summary>
		UpdateDelete

	}

}
