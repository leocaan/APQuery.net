using System;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Specifies the options to apply to a property.
	/// This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.
	/// </summary>
	[Flags]
	public enum APXmlPropertyOptions
	{

		/// <summary>
		/// None.
		/// </summary>
		None = 0,


		/// <summary>
		/// IsDefaultCollection.
		/// </summary>
		IsDefaultCollection = 1,


		/// <summary>
		/// IsRequired.
		/// </summary>
		IsRequired = 2,


		/// <summary>
		/// IsKey.
		/// </summary>
		IsKey = 4

	}

}
