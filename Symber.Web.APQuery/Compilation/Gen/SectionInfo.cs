using System;
using System.IO;
using System.Xml;

namespace Symber.Web.Compilation
{
	internal class SectionInfo : GenInfo
	{

		#region [ Constructors ]


		public SectionInfo()
		{
		}


		public SectionInfo(string sectionName, APGenSectionInformation info)
		{
			Name = sectionName;
			TypeName = info.Type;
		}


		#endregion


		#region [ Override Implementation of GenInfo ]


		public override object CreateInstance()
		{
			object obj = base.CreateInstance();
			APGenSection section = obj as APGenSection;

			if (section != null)
			{
				section.SectionInformation.SetName(Name);
			}
			return obj;
		}


		public override bool HasDataContent(APGen gen)
		{
			return gen.GetSectionInstance(this, false) != null || gen.GetSectionXml(this) != null;
		}


		public override void ReadGen(APGen gen, string streamName, XmlTextReader reader)
		{
			StreamName = streamName;
			GenHost = gen.GenHost;

			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
					case "type": TypeName = reader.Value; break;
					case "name": Name = reader.Value; break;
					default: ThrowException(APResource.GetString(APResource.APGen_UnrecognizedAttribute, reader.Name), reader); break;
				}
			}

			if (Name == null || TypeName == null)
				ThrowException(APResource.GetString(APResource.APGen_MissingRequiredAttribute, "section", "name or type"), reader);

			reader.MoveToElement();
			reader.Skip();
		}


		public override void WriteGen(APGen gen, XmlWriter writer)
		{
			writer.WriteStartElement("section");
			writer.WriteAttributeString("name", Name);
			writer.WriteAttributeString("type", TypeName);
			writer.WriteEndElement();
		}


		public override void ReadData(APGen gen, XmlTextReader reader)
		{
			if (gen.GetSectionXml(this) != null)
				ThrowException(APResource.GetString(APResource.APGen_RedefinedSection, Name), reader);
			gen.SetSectionXml(this, reader.ReadOuterXml());
		}


		public override void WriteData(APGen gen, XmlWriter writer)
		{
			string xml;

			APGenSection section = gen.GetSectionInstance(this, false);

			if (section != null)
			{
				xml = section.SerializeSection(Name);

				string externalDataXml = section.ExternalDataXml;
				string filePath = gen.FileName;

				if (!String.IsNullOrEmpty(filePath) && !String.IsNullOrEmpty(externalDataXml))
				{
					using (StreamWriter sw = new StreamWriter(filePath))
					{
						sw.Write(externalDataXml);
					}
				}
			}
			else
			{
				xml = gen.GetSectionXml(this);
			}

			if (xml != null)
			{
				writer.WriteRaw(xml);
			}
		}


		#endregion

	}
}
