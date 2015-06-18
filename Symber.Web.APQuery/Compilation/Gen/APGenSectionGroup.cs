using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Represents a group of related sections within a configuration file.
	/// </summary>
	public class APGenSectionGroup
	{

		#region [ Fields ]


		private string _name;
		private string _typeName;
		private APGenSectionCollection _sections;
		private APGenSectionGroupCollection _groups;
		private APGen _gen;
		private SectionGroupInfo _groupInfo;
		private bool _initialized;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of the APGenSectionGroup class.
		/// </summary>
		public APGenSectionGroup()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the name property of this APGenSectionGroup object.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}


		/// <summary>
		/// Gets the section group name associated with this APGenSectionGroup.
		/// </summary>
		public string SectionGroupName
		{
			get { return _groupInfo.XPath; }
		}


		/// <summary>
		/// Gets a APGenSectionGroupCollection object that contains all the APGenSectionGroup
		/// objects that are children of this APGenSectionGroup object.
		/// </summary>
		public APGenSectionGroupCollection SectionGroups
		{
			get
			{
				if (_groups == null)
					_groups = new APGenSectionGroupCollection(Gen, _groupInfo);
				return _groups;
			}
		}


		/// <summary>
		/// Gets a APGenSectionCollection object that contains all of APGenSection objects
		/// within this APGenSectionGroup object.
		/// </summary>
		public APGenSectionCollection Sections
		{
			get
			{
				if (_sections == null)
					_sections = new APGenSectionCollection(Gen, _groupInfo);
				return _sections;
			}
		}


		/// <summary>
		/// Gets or sets the type for this APGenSectionGroup object.
		/// </summary>
		public string Type
		{
			get { return _typeName; }
			set { _typeName = value; }
		}


		#endregion


		#region [ Internal Methods ]


		internal void Initialize(APGen gen, SectionGroupInfo groupInfo)
		{
			if (_initialized)
				throw new SystemException(APResource.GetString(APResource.APGen_SectionInitTwice, GetType()));

			_initialized = true;
			_gen = gen;
			_groupInfo = groupInfo;
		}


		internal void SetName(string name)
		{
			this._name = name;
		}


		internal void Generate(APGen gen)
		{
			foreach (APGenSection section in Sections)
				section.Generate(gen);
			for (int i = 0; i < SectionGroups.Count; i++)
				SectionGroups[i].Generate(gen);
		}


		internal void SyncData(APGen gen)
		{
			foreach (APGenSection section in Sections)
				section.SyncData(gen);
			for (int i = 0; i < SectionGroups.Count; i++)
				SectionGroups[i].SyncData(gen);
		}


		internal void InitData(APGen gen)
		{
			foreach (APGenSection section in Sections)
				section.InitData(gen);
			for (int i = 0; i < SectionGroups.Count; i++)
				SectionGroups[i].InitData(gen);
		}


		#endregion


		#region [ Private Properties ]


		private APGen Gen
		{
			get
			{
				if (_gen == null)
					throw new InvalidOperationException();
				return _gen;
			}
		}


		#endregion

	}
}
