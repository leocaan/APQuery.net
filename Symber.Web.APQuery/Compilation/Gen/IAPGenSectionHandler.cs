using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate section handler interface.
	/// </summary>
	public interface IAPGenSectionHandler
	{

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="genContext">Generate context.</param>
		/// <param name="section">Generate section.</param>
		/// <returns>Created object.</returns>
		object Create(object genContext, XmlNode section);

	}
}
