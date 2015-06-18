
namespace Symber.Web.Data
{

	/// <summary>
	/// Column Identity Type.
	/// </summary>
	public enum APColumnIdentityType
	{

		/// <summary>
		/// None
		/// </summary>
		None,

		/// <summary>
		/// Rely on Provider.
		/// For this, APColumnDef data type can be 'sbyte', 'short', 'int', 'long', 'byte', 'ushort', 'uint', 'ulong', 'decimal', 'float', 'double', 'Guid'.
		/// </summary>
		Provider,

		/// <summary>
		/// Rely on Database.
		/// For this, APColumnDef data type can be 'sbyte', 'short', 'int', 'long', 'byte', 'ushort', 'uint', 'ulong', 'decimal'.
		/// </summary>
		Database,

	}

}
