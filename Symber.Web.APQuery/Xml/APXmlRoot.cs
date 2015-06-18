using System.IO;
using System.Text;
using System.Xml;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Represents a root xml element within a xml file.
	/// </summary>
	public abstract class APXmlRoot : APXmlElement
	{

		#region [ Constructors ]


		/// <summary>
		/// Creates a new instance of the APXmlRoot class.
		/// </summary>
		protected APXmlRoot()
		{
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Read data from open XML file. 
		/// </summary>
		/// <param name="filePath">File path of xml file.</param>
		public void Read(string filePath)
		{
			if (!File.Exists(filePath))
				throw new APXmlException(APResource.AP_FileNotFound);
			Read(new FileStream(filePath, FileMode.Open, FileAccess.Read));
		}


		/// <summary>
		/// Read data form stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		public void Read(Stream stream)
		{
			using (XmlTextReader reader = new XmlTextReader(stream))
			{
				DeserializeElement(reader, false);
			}
		}


		/// <summary>
		/// Read data from string.
		/// </summary>
		/// <param name="xml">The xml string.</param>
		public void ReadXml(string xml)
		{
			using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
			{
				Read(ms);
			}
		}


		/// <summary>
		/// Write data to xml file.
		/// </summary>
		/// <param name="filePath">File path of xml file.</param>
		public void Write(string filePath)
		{
			using (FileStream f = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				Write(f, null);
			}
		}


		/// <summary>
		/// Write data to stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="encoding">Encoding.</param>
		public void Write(Stream stream, Encoding encoding)
		{
			XmlTextWriter writer = new XmlTextWriter(stream, encoding);
			writer.WriteStartDocument();
			SerializeToXmlElement(writer, LocalName);
			writer.WriteEndDocument();
			writer.Flush();
		}


		/// <summary>
		/// Write data to a string.
		/// </summary>
		/// <returns>A string of write data.</returns>
		public string WriteXml()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				Write(ms, new UTF8Encoding());
				return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
			}
		}


		#endregion


		#region [ Protected Properties ]


		/// <summary>
		/// Root element local name.
		/// </summary>
		protected abstract string LocalName { get; }


		internal override bool HasValues()
		{
			return true;
		}


		#endregion

	}

}
