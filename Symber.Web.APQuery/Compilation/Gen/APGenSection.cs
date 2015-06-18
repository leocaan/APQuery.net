using System;
using System.IO;
using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate config section.
	/// </summary>
	public abstract partial class APGenSection : APGenElement
	{

		#region [ Fields ]


		private APGenSectionInformation _sectionInformation;
		private IAPGenSectionHandler _sectionHandler;
		private string _externalDataXml;
		private object _genContext;


		#endregion


		#region [ Constructors ]
		

		/// <summary>
		/// Create a new config section.
		/// </summary>
		protected APGenSection()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Section information.
		/// </summary>
		public APGenSectionInformation SectionInformation
		{
			get
			{
				if (_sectionInformation == null)
					_sectionInformation = new APGenSectionInformation();
				return _sectionInformation;
			}
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Generate code.
		/// </summary>
		/// <param name="gen">The specified APGen object.</param>
		public virtual void Generate(APGen gen)
		{
			gen.GetCodeNamespace(gen.DefaultNamespace)
				.Comments
				.Add(Comment(APResource.GetString(APResource.APGen_DefaultGenerated, GetType().FullName)));
		}


		/// <summary>
		/// Synchronize data.
		/// </summary>
		/// <param name="gen">The specified APGen object.</param>
		public virtual void SyncData(APGen gen)
		{
		}


		/// <summary>
		/// Initialize data.
		/// </summary>
		/// <param name="gen">The specified APGen object.</param>
		public virtual void InitData(APGen gen)
		{
		}


		#endregion


		#region [ Internal Properties ]


		internal string ExternalDataXml
		{
			get { return _externalDataXml; }
		}


		internal IAPGenSectionHandler SectionHandler
		{
			get { return _sectionHandler; }
			set { _sectionHandler = value; }
		}


		internal object GenContext
		{
			get { return _genContext; }
			set { _genContext = value; }
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Get runtime object.
		/// </summary>
		/// <returns>Runtime object.</returns>
		protected internal virtual object GetRuntimeObject()
		{
			if (SectionHandler != null)
			{
				try
				{
					XmlReader reader = new XmlTextReader(new StringReader(RawXml));

					DoDeserializeSection(reader);

					if (!String.IsNullOrEmpty(SectionInformation.Source))
					{
						RawXml = File.ReadAllText(SectionInformation.Source);
						SectionInformation.SetRawXml(RawXml);
					}
				}
				catch
				{
				}

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(RawXml);
				return SectionHandler.Create(GenContext, doc.DocumentElement);
			}

			return this;
		}


		/// <summary>
		/// Reads XML from the configuration file.
		/// </summary>
		/// <param name="reader">The XmlReader object, which reads from the configuration file.</param>
		protected internal virtual void DeserializeSection(XmlReader reader)
		{
			DoDeserializeSection(reader);
		}


		/// <summary>
		/// Creates an XML string containing an unmerged view of the ConfigurationSection object as a
		/// single section to write to a file.
		/// </summary>
		/// <param name="name">The name of the section to create.</param>
		/// <returns>An XML string containing an unmerged view of the ConfigurationSection object.</returns>
		protected internal virtual string SerializeSection(string name)
		{
			_externalDataXml = null;

			string retVal;
			StringWriter sw = new StringWriter();
			using (XmlTextWriter tw = new XmlTextWriter(sw))
			{
				tw.Formatting = Formatting.Indented;
				SerializeToXmlElement(tw, name);
			}
			retVal = sw.ToString();

			string source = SectionInformation.Source;

			if (String.IsNullOrEmpty(source))
				return retVal;

			_externalDataXml = retVal;

			bool haveName = !String.IsNullOrEmpty(name);
			sw = new StringWriter();

			using (XmlTextWriter tw = new XmlTextWriter(sw))
			{
				if (haveName)
					tw.WriteStartElement(name);

				tw.WriteAttributeString("APGenSource", source);

				if (haveName)
					tw.WriteEndElement();
			}

			return sw.ToString();
		}


		#endregion


		#region [ Private Methods ]


		private APGenElement CreateElement(Type type)
		{
			APGenElement element = Activator.CreateInstance(type) as APGenElement;
			element.Init();
			return element;
		}


		private void DoDeserializeSection(XmlReader reader)
		{
			reader.MoveToContent();
			SectionInformation.SetRawXml(RawXml);
			DeserializeElement(reader, false);
		}


		#endregion

	}
}
