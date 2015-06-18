using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Property options.
	/// </summary>
	[Flags]
	public enum APGenPropertyOptions
	{

		/// <summary>
		/// None.
		/// </summary>
		None = 0,


		/// <summary>
		/// Is default collection.
		/// </summary>
		IsDefaultCollection = 1,


		/// <summary>
		/// Is required.
		/// </summary>
		IsRequired = 2,


		/// <summary>
		/// Is key.
		/// </summary>
		IsKey = 4

	}
}
