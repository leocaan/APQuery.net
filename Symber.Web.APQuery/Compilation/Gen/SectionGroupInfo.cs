using System.Xml;

namespace Symber.Web.Compilation
{
	internal class SectionGroupInfo : GenInfo
	{

		#region [ Static Fields ]


		static GenInfoCollection emptyList = new GenInfoCollection();


		#endregion


		#region [ Fields ]


		private GenInfoCollection _sections;
		private GenInfoCollection _groups;


		#endregion


		#region [ Constructors ]


		public SectionGroupInfo()
		{
			Type = typeof(APGenSectionGroup);
		}


		public SectionGroupInfo(string groupName, string typeName)
		{
			Name = groupName;
			TypeName = typeName;
		}


		#endregion


		#region [ Properties ]


		public GenInfoCollection Sections
		{
			get
			{
				if (_sections == null)
					return emptyList;
				else
					return _sections;
			}
		}


		public GenInfoCollection Groups
		{
			get
			{
				if (_groups == null)
					return emptyList;
				else
					return _groups;
			}
		}


		#endregion


		#region [ Methods ]


		public void AddChild(GenInfo data)
		{
			data.Parent = this;
			if (data is SectionInfo)
			{
				if (_sections == null)
					_sections = new GenInfoCollection();
				_sections[data.Name] = data;
			}
			else
			{
				if (_groups == null)
					_groups = new GenInfoCollection();
				_groups[data.Name] = data;
			}
		}


		public void Clear()
		{
			if (_sections != null)
				_sections.Clear();
			if (_groups != null)
				_groups.Clear();
		}


		public bool HasChild(string name)
		{
			if (_sections != null && _sections[name] != null)
				return true;
			return (_groups != null && _groups[name] != null);
		}


		public void RemoveChild(string name)
		{
			if (_sections != null)
				_sections.Remove(name);
			if (_groups != null)
				_groups.Remove(name);
		}


		public SectionInfo GetChildSection(string name)
		{
			if (_sections != null)
				return _sections[name] as SectionInfo;
			else
				return null;
		}


		public SectionGroupInfo GetChildGroup(string name)
		{
			if (_groups != null)
				return _groups[name] as SectionGroupInfo;
			else
				return null;
		}


		public void ReadRootData(XmlTextReader reader, APGen gen)
		{
			reader.MoveToContent();
			ReadContent(reader, gen, true);
		}


		internal void WriteRootData(XmlWriter writer, APGen gen)
		{
			WriteContent(writer, gen, false);
		}


		public void ReadContent(XmlTextReader reader, APGen gen, bool root)
		{
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
					continue;
				}

				GenInfo data = GetGenInfo(reader, this);
				if (data != null)
					data.ReadData(gen, reader);
				else
					ThrowException(APResource.GetString(APResource.APGen_UnrecognizedSection, reader.LocalName), reader);
			}
		}


		internal void WriteContent(XmlWriter writer, APGen gen, bool writeElement)
		{
			foreach (GenInfoCollection col in new object[] { Sections, Groups })
			{
				foreach (string key in col)
				{
					GenInfo info = col[key];
					if (info.HasDataContent(gen))
						info.WriteData(gen, writer);
				}
			}
		}


		#endregion


		#region [ Override Implementation of GenInfo ]


		public override bool HasDataContent(APGen gen)
		{
			foreach (GenInfoCollection col in new object[] { Sections, Groups })
			{
				foreach (string key in col)
				{
					GenInfo info = col[key];
					if (info.HasDataContent(gen))
						return true;
				}
			}
			return false;
		}


		public override void ReadGen(APGen gen, string streamName, XmlTextReader reader)
		{
			StreamName = streamName;
			GenHost = gen.GenHost;

			if (reader.LocalName != "genSections")
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.Name == "name")
					{
						Name = reader.Value;
					}
					else if (reader.Name == "type")
					{
						TypeName = reader.Value;
						Type = null;
					}
					else
					{
						ThrowException(APResource.GetString(APResource.APGen_UnrecognizedAttribute, ""), reader);
					}
				}

				if (Name == null)
					ThrowException(APResource.GetString(APResource.APGen_MissingRequiredAttribute, "sectionGroup", "name"), reader);
			}

			if (TypeName == null)
				TypeName = typeof(APGenSectionGroup).FullName;

			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}

			reader.ReadStartElement();
			reader.MoveToContent();

			while (reader.NodeType != XmlNodeType.EndElement)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
					continue;
				}

				string name = reader.LocalName;
				GenInfo info = null;

				if (name == "section")
					info = new SectionInfo();
				else if (name == "sectionGroup")
					info = new SectionGroupInfo();
				else
					ThrowException(APResource.GetString(APResource.APGen_UnrecognizedElement, reader.Name), reader);


				info.ReadGen(gen, streamName, reader);
				GenInfo actInfo = Groups[info.Name];
				if (actInfo == null)
					actInfo = Sections[info.Name];

				if (actInfo != null)
				{
					if (actInfo.GetType() != info.GetType())
						ThrowException(APResource.GetString(APResource.APGen_SectionNameAlreadyExists, info.Name), reader);

					actInfo.StreamName = streamName;
				}
				else
					AddChild(info);
			}

			reader.ReadEndElement();
		}


		public override void WriteGen(APGen gen, XmlWriter writer)
		{
			if (Name != null)
			{
				writer.WriteStartElement("sectionGroup");
				writer.WriteAttributeString("name", Name);
				if (TypeName != null && TypeName != "" && TypeName != "Symber.Web.Compilation.Gen.APGenSectionGroup")
					writer.WriteAttributeString("type", TypeName);
			}
			else
				writer.WriteStartElement("genSections");

			foreach (GenInfoCollection col in new object[] { Sections, Groups })
			{
				foreach (string key in col)
				{
					GenInfo info = col[key];
					if (info.HasDataContent(gen))
						info.WriteGen(gen, writer);
				}
			}

			writer.WriteEndElement();
		}


		public override void ReadData(APGen gen, XmlTextReader reader)
		{
			reader.MoveToContent();
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				ReadContent(reader, gen, false);
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			else
				reader.Read();
		}


		public override void WriteData(APGen gen, XmlWriter writer)
		{
			writer.WriteStartElement(Name);
			WriteContent(writer, gen, true);
			writer.WriteEndElement();
		}


		#endregion


		#region [ Private Methods  ]


		private GenInfo GetGenInfo(XmlReader reader, SectionGroupInfo current)
		{
			GenInfo data = null;

			if (current._sections != null)
				data = current._sections[reader.LocalName];
			if (data != null)
				return data;

			if (current._groups != null)
				data = current._groups[reader.LocalName];
			if (data != null)
				return data;

			if (current._groups == null)
				return null;

			foreach (string key in current._groups.AllKeys)
			{
				data = GetGenInfo(reader, (SectionGroupInfo)current._groups[key]);
				if (data != null)
					return data;
			}

			return null;
		}


		#endregion

	}
}
