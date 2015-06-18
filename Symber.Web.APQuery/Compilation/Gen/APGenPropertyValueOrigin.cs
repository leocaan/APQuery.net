
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Specifies the level in the configuration hierarchy where a configuration property value originated.
	/// </summary>
	public enum APGenPropertyValueOrigin
	{

		/// <summary>
		/// The configuration property value originates from the DefaultValue property.
		/// </summary>
		Default = 0,


		/// <summary>
		/// The configuration property value is inherited from a parent level in the configuration.
		/// </summary>
		Inherited = 1,


		/// <summary>
		/// The configuration property value is defined at the current level of the hierarchy.
		/// </summary>
		SetHere = 2

	}
}
