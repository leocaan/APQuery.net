using System;
using System.CodeDom;
using System.Collections;
using System.IO;
using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// APGen code generator.
	/// </summary>
	public sealed class APGen
	{

		#region [ Fields ]


		private IAPGenHost _host;
		private string _genPath;
		private string _streamName;
		private string _rootNamespace = String.Empty;
		private SectionGroupInfo _rootGroupInfo;
		private APGenSectionGroup _rootSectionGroup;
		private Hashtable _elementData = new Hashtable();

		private CodeCompileUnit _codeCompileUnit;
		private Hashtable _namespace = new Hashtable();


		#endregion


		#region [ Constructors ]


		internal APGen(IAPGenHost host)
		{
			_host = host;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Code generate config root section group.
		/// </summary>
		public APGenSectionGroup RootSectionGroup
		{
			get
			{
				if (_rootSectionGroup == null)
				{
					_rootSectionGroup = new APGenSectionGroup();
					_rootSectionGroup.Initialize(this, _rootGroupInfo);
				}
				return _rootSectionGroup;
			}
		}


		/// <summary>
		/// All section groups.
		/// </summary>
		public APGenSectionGroupCollection SectionGroups
		{
			get { return RootSectionGroup.SectionGroups; }
		}


		/// <summary>
		/// All sections.
		/// </summary>
		public APGenSectionCollection Sections
		{
			get { return RootSectionGroup.Sections; }
		}


		/// <summary>
		/// Default namespace.
		/// </summary>
		public string DefaultNamespace
		{
			get { return _rootNamespace; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Get a config section by path.
		/// </summary>
		/// <param name="path">The path to get section.</param>
		/// <returns>Config section.</returns>
		public APGenSection GetSection(string path)
		{
			string[] parts = path.Split('/');
			if (parts.Length == 1)
				return Sections[parts[0]];

			APGenSectionGroup group = SectionGroups[parts[0]];
			for (int i = 1; group != null && i < parts.Length - 1; i++)
				group = group.SectionGroups[parts[i]];

			if (group != null)
				return group.Sections[parts[parts.Length - 1]];
			else
				return null;
		}


		/// <summary>
		/// Get a config section group by path.
		/// </summary>
		/// <param name="path">The path to get section group.</param>
		/// <returns>Config section group.</returns>
		public APGenSectionGroup GetSectionGroup(string path)
		{
			string[] parts = path.Split('/');

			APGenSectionGroup group = SectionGroups[parts[0]];
			for (int i = 1; group != null && i < parts.Length; i++)
				group = group.SectionGroups[parts[i]];
			return group;
		}


		/// <summary>
		/// Get a code namespace object by key.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <returns>Code namespace object.</returns>
		public CodeNamespace GetCodeNamespace(string key)
		{
			if (key == null)
				key = String.Empty;

			if (_namespace.ContainsKey(key))
				return _namespace[key] as CodeNamespace;

			CodeNamespace namedNamespace = new CodeNamespace(key);
			_namespace.Add(key, namedNamespace);
			_codeCompileUnit.Namespaces.Add(namedNamespace);
			return namedNamespace;
		}


		/// <summary>
		/// Get code compile unit.
		/// </summary>
		/// <returns>Code compile unit.</returns>
		public CodeCompileUnit GetCodeCompileUnit()
		{
			return _codeCompileUnit;
		}


		/// <summary>
		/// Generate code.
		/// </summary>
		/// <returns>The generated code compile unit.</returns>
		public CodeCompileUnit Generate()
		{
			RootSectionGroup.Generate(this);
			return _codeCompileUnit;
		}


		/// <summary>
		/// Synchronize data.
		/// </summary>
		public void SyncData()
		{
			RootSectionGroup.SyncData(this);
		}


		/// <summary>
		/// Initialize data.
		/// </summary>
		public void InitData()
		{
			RootSectionGroup.InitData(this);
		}


		#endregion


		#region [ Internal Properties ]


		internal string FileName
		{
			get { return _streamName; }
		}


		internal IAPGenHost GenHost
		{
			get { return _host; }
		}


		#endregion


		#region [ Internal Methods ]


		internal void Init(string virtualPath)
		{
			_genPath = virtualPath;
			_streamName = _host.GetStreamName(virtualPath);
			_rootGroupInfo = new SectionGroupInfo();
			_rootGroupInfo.StreamName = _streamName;

			if (!String.IsNullOrEmpty(_streamName))
			{
				using (XmlTextReader reader = new XmlTextReader(_host.OpenStreamForRead(_streamName)))
				{
					ReadGenFile(reader, _streamName);
				}
			}

			_codeCompileUnit = new CodeCompileUnit();
			CodeNamespace defaultNamespace = new CodeNamespace(_rootNamespace);
			_namespace.Add(_rootNamespace, defaultNamespace);
			_codeCompileUnit.Namespaces.Add(defaultNamespace);
		}


		internal string GetSectionXml(SectionInfo sectionInfo)
		{
			return _elementData[sectionInfo] as string;
		}


		internal void SetSectionXml(SectionInfo sectionInfo, string data)
		{
			_elementData[sectionInfo] = data;
		}


		internal APGenSection GetSectionInstance(SectionInfo sectionInfo, bool createDefaultInstance)
		{
			object data = _elementData[sectionInfo];
			APGenSection section = data as APGenSection;
			if (section != null || !createDefaultInstance)
				return section;

			object sectionObj = sectionInfo.CreateInstance();
			section = sectionObj as APGenSection;
			if (section == null)
			{
				APGenDefaultSection defaultSection = new APGenDefaultSection();
				defaultSection.SectionHandler = sectionObj as IAPGenSectionHandler;
				section = defaultSection;
			}

			if (data != null)
			{
				string xml = data as string;

				section.RawXml = xml;

				using (XmlTextReader reader = new XmlTextReader(new StringReader(xml)))
				{
					section.DeserializeSection(reader);
				}
			}

			_elementData[sectionInfo] = section;
			return section;
		}


		internal void CreateSection(SectionGroupInfo groupInfo, string name, APGenSection section)
		{
			if (groupInfo.HasChild(name))
				throw new APGenException(APResource.GetString(APResource.APGen_SectionAlreadyExists, name));

			if (section.SectionInformation.Type == null)
				section.SectionInformation.Type = GenHost.GetGenTypeName(section.GetType());

			SectionInfo sectionInfo = new SectionInfo(name, section.SectionInformation);
			sectionInfo.StreamName = _streamName;
			sectionInfo.GenHost = GenHost;
			groupInfo.AddChild(sectionInfo);
			_elementData[sectionInfo] = section;
		}


		internal APGenSectionGroup GetSectionGroupInstance(SectionGroupInfo groupInfo)
		{
			APGenSectionGroup sectionGroup = groupInfo.CreateInstance() as APGenSectionGroup;
			if (sectionGroup != null)
				sectionGroup.Initialize(this, groupInfo);
			return sectionGroup;
		}


		internal void CreateSectionGroup(SectionGroupInfo parentGroup, string name, APGenSectionGroup section)
		{
			if (parentGroup.HasChild(name))
				throw new APGenException(APResource.GetString(APResource.APGen_SectionAlreadyExists, name));

			if (section.Type == null)
				section.Type = GenHost.GetGenTypeName(section.GetType());
			section.SetName(name);

			SectionGroupInfo sectionInfo = new SectionGroupInfo(name, section.Type);
			sectionInfo.StreamName = _streamName;
			sectionInfo.GenHost = GenHost;
			parentGroup.AddChild(sectionInfo);
			_elementData[sectionInfo] = section;

			section.Initialize(this, sectionInfo);
		}


		internal void RemoveGenInfo(GenInfo genInfo)
		{
			_elementData.Remove(genInfo);
		}


		#endregion


		#region [ Private Methods ]


		private void ReadGenFile(XmlTextReader reader, string fileName)
		{
			reader.MoveToContent();

			if (reader.NodeType != XmlNodeType.Element || reader.Name != "gen")
				ThrowException(APResource.APGen_InvalidRootElement, reader);

			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "namespace")
					{
						_rootNamespace = reader.Value;
						continue;
					}
					else if (reader.LocalName == "xmlns")
					{
						continue;
					}

					ThrowException(APResource.GetString(APResource.APGen_UnrecognizedAttributeInElement, "gen", reader.LocalName), reader);
				}
			}

			reader.MoveToElement();

			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}

			reader.ReadStartElement();
			reader.MoveToContent();

			if (reader.LocalName == "genSections")
			{
				if (reader.HasAttributes)
					ThrowException(APResource.GetString(APResource.APGen_UnrecognizedAttributeInElement, "genSections", "..."), reader);
				_rootGroupInfo.ReadGen(this, fileName, reader);
			}

			_rootGroupInfo.ReadRootData(reader, this);
		}


		private void ThrowException(string text, XmlTextReader reader)
		{
			throw new APGenException(text, _streamName, reader.LineNumber);
		}


		#endregion

	}
}
