using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate config section collection.
	/// </summary>
	[Serializable]
	public sealed class APGenSectionCollection : NameObjectCollectionBase
	{

		#region [ Fields ]


		private SectionGroupInfo _group;
		private APGen _gen;


		#endregion


		#region [ Constructors ]


		internal APGenSectionCollection(APGen gen, SectionGroupInfo group)
			: base (StringComparer.Ordinal)
		{
			_gen = gen;
			_group = group;
		}


		internal APGenSectionCollection(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Get a section by name.
		/// </summary>
		/// <param name="name">The specified section name.</param>
		/// <returns>The section object.</returns>
		public APGenSection this[string name]
		{
			get
			{
				APGenSection section = BaseGet(name) as APGenSection;
				if (section == null)
				{
					SectionInfo sectionInfo = _group.Sections[name] as SectionInfo;
					if (sectionInfo == null)
						return null;
					section = _gen.GetSectionInstance(sectionInfo, true);
					if (section == null)
						return null;
					BaseSet(name, section);
				}
				return section;
			}
		}


		/// <summary>
		/// Get a section by index.
		/// </summary>
		/// <param name="index">The specified index.</param>
		/// <returns>The section object.</returns>
		public APGenSection this[int index]
		{
			get { return this[GetKey(index)]; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Adds a APGenSection object to the APGenSectionCollection.
		/// </summary>
		/// <param name="name">The name of the section to be added.</param>
		/// <param name="section">The section to be added.</param>
		public void Add(string name, APGenSection section)
		{
			_gen.CreateSection(_group, name, section);
		}


		/// <summary>
		/// Clears this APGenSectionCollection.
		/// </summary>
		public void Clear()
		{
			if (_group.Sections != null)
			{
				foreach (GenInfo data in _group.Sections)
					_gen.RemoveGenInfo(data);
			}
		}


		/// <summary>
		/// Copies this APGenSectionCollection object to an array.
		/// </summary>
		/// <param name="array">The array to which to copy.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		public void CopyTo(APGenSection[] array, int index)
		{
			for (int n = 0; n < _group.Sections.Count; n++)
				array[n + index] = this[n];
		}


		/// <summary>
		/// Gets the specified APGenSection object contained in this APGenSectionCollection.
		/// </summary>
		/// <param name="index">The index of the APGenSection object to be returned.</param>
		/// <returns>The APGenSection object at the specified index.</returns>
		public APGenSection Get(int index)
		{
			return this[index];
		}


		/// <summary>
		/// Gets the specified APGenSection object contained in this APGenSectionCollection.
		/// </summary>
		/// <param name="name">The name of the APGenSection object to be returned.</param>
		/// <returns>The APGenSection object with the specified name.</returns>
		public APGenSection Get(string name)
		{
			return this[name];
		}


		/// <summary>
		/// Gets the key of the specified APGenSection object contained in this APGenSectionCollection.
		/// </summary>
		/// <param name="index">The index of the APGenSection object whose key is to be returned.</param>
		/// <returns>Key of the ConfigurationSection at the specified index.</returns>
		public string GetKey(int index)
		{
			return _group.Sections.GetKey(index);
		}


		/// <summary>
		/// Removes the specified APGenSection object from this APGenSectionCollection.
		/// </summary>
		/// <param name="name">The name of the section to be removed.</param>
		public void Remove(string name)
		{
			SectionInfo secData = _group.Sections[name] as SectionInfo;
			if (secData != null)
				_gen.RemoveGenInfo(secData);
		}


		/// <summary>
		/// Removes the specified APGenSection object from this APGenSectionCollection.
		/// </summary>
		/// <param name="index">The index of the section to be removed.</param>
		public void RemoveAt(int index)
		{
			SectionInfo secData = _group.Sections[index] as SectionInfo;
			_gen.RemoveGenInfo(secData);
		}


		#endregion


		#region [ Override Implementation of NameObjectCollectionBase ]


		/// <summary>
		/// Gets the keys to all APGenSection objects contained in this APGenSectionCollection.
		/// </summary>
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get { return _group.Sections.Keys; }
		}

		
		/// <summary>
		/// Gets the number of sections in this APGenSectionCollection.
		/// </summary>
		public override int Count 
		{
			get { return _group.Sections.Count; }
		}


		/// <summary>
		/// Returns an enumerator that iterates through the APGenSectionCollection.
		/// </summary>
		/// <returns>An IEnumerator for the APGenSectionCollection instance.</returns>
		public override IEnumerator GetEnumerator()
		{
			foreach (string key in _group.Sections.AllKeys)
				yield return this[key];
		}


		/// <summary>
		/// GetObjectData.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}


		#endregion

	}
}
