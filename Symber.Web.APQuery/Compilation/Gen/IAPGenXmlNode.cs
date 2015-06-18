
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate xml node.
	/// </summary>
	interface IAPGenXmlNode
	{

		/// <summary>
		/// Filename.
		/// </summary>
		string Filename { get; }


		/// <summary>
		/// Line number.
		/// </summary>
		int LineNumber { get; }

	}
}
