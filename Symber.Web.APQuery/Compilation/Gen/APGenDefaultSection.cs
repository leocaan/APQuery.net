using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Default section.
	/// </summary>
	public sealed class APGenDefaultSection : APGenSection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenDefaultSection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Override Implementation of APGenSection ]


		/// <summary>
		/// Reads XML from the configuration file.
		/// </summary>
		/// <param name="xmlReader">The XmlReader object, which reads from the configuration file.</param>
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (RawXml == null)
				RawXml = xmlReader.ReadOuterXml();
			else
				xmlReader.Skip();
		}


		/// <summary>
		/// Properties.
		/// </summary>
		protected internal override APGenPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion

	}
}
