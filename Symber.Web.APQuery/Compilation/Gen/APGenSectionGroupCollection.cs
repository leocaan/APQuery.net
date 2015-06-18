using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Represents a collection of APGenSectionGroup objects.
	/// </summary>
	[Serializable]
	public sealed class APGenSectionGroupCollection : NameObjectCollectionBase
	{

		#region [ Fields ]


		private SectionGroupInfo _groupInfo;
		private APGen _gen;


		#endregion


		#region [ Constructors ]


		internal APGenSectionGroupCollection(APGen gen, SectionGroupInfo groupInfo)
			: base(StringComparer.Ordinal)
		{
			_gen = gen;
			_groupInfo = groupInfo;
		}

		
		internal APGenSectionGroupCollection(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a APGenSectionGroup object contained in this APGenSectionGroupCollection object.
		/// </summary>
		/// <param name="name">The name of the APGenSectionGroup object to be returned.</param>
		/// <returns>The APGenSectionGroup object with the specified name.</returns>
		public APGenSectionGroup this[string name]
		{
			get
			{
				APGenSectionGroup section = BaseGet(name) as APGenSectionGroup;
				if (section == null)
				{
					SectionGroupInfo sectionInfo = _groupInfo.Groups[name] as SectionGroupInfo;
					if (sectionInfo == null)
						return null;
					section = _gen.GetSectionGroupInstance(sectionInfo);
					BaseSet(name, section);
				}
				return section;
			}
		}


		/// <summary>
		/// Gets or sets a APGenSectionGroup object contained in this APGenSectionGroupCollection object.
		/// </summary>
		/// <param name="index">The index of the APGenSectionGroup object to be returned.</param>
		/// <returns>The APGenSectionGroup object at the specified index.</returns>
		public APGenSectionGroup this[int index]
		{
			get { return this[GetKey(index)]; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Adds a APGenSectionGroup object to this APGenSectionGroupCollection object.
		/// </summary>
		/// <param name="name">The name of the APGenSectionGroup object to be added.</param>
		/// <param name="sectionGroup">The APGenSectionGroup object to be added.</param>
		public void Add(string name, APGenSectionGroup sectionGroup)
		{
			_gen.CreateSectionGroup(_groupInfo, name, sectionGroup);
		}


		/// <summary>
		/// Clears the collection.
		/// </summary>
		public void Clear()
		{
			if (_groupInfo.Groups != null)
			{
				foreach (GenInfo data in _groupInfo.Groups)
					_gen.RemoveGenInfo(data);
			}
		}


		/// <summary>
		/// Copies this APGenSectionGroupCollection object to an array.
		/// </summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		public void CopyTo(APGenSectionGroup[] array, int index)
		{
			for (int n = 0; n < _groupInfo.Groups.Count; n++)
				array[n + index] = this[n];
		}


		/// <summary>
		/// Gets the specified APGenSectionGroup object contained in the collection.
		/// </summary>
		/// <param name="index">The index of the APGenSectionGroup object to be returned.</param>
		/// <returns>The APGenSectionGroup object at the specified index.</returns>
		public APGenSectionGroup Get(int index)
		{
			return this[index];
		}


		/// <summary>
		/// Gets the specified APGenSectionGroup object contained in the collection.
		/// </summary>
		/// <param name="name">The name of the APGenSectionGroup object to be returned.</param>
		/// <returns>The APGenSectionGroup object with the specified name.</returns>
		public APGenSectionGroup Get(string name)
		{
			return this[name];
		}


		/// <summary>
		/// Gets the specified APGenSectionGroup object contained in the collection.
		/// </summary>
		/// <param name="index">The index of the APGenSectionGroup object to be returned.</param>
		/// <returns>The APGenSectionGroup object at the specified index.</returns>
		public string GetKey(int index)
		{
			return _groupInfo.Groups.GetKey(index);
		}


		/// <summary>
		/// Removes the APGenSectionGroup object whose name is specified from this APGenSectionGroupCollection object.
		/// </summary>
		/// <param name="name">The name of the section group to be removed.</param>
		public void Remove(string name)
		{
			SectionGroupInfo secData = _groupInfo.Groups[name] as SectionGroupInfo;
			if (secData != null)
				_gen.RemoveGenInfo(secData);
		}


		/// <summary>
		/// Removes the APGenSectionGroup object whose index is specified from this APGenSectionGroupCollection object.
		/// </summary>
		/// <param name="index">The index of the section group to be removed.</param>
		public void RemoveAt(int index)
		{
			SectionGroupInfo secData = _groupInfo.Groups[index] as SectionGroupInfo;
			_gen.RemoveGenInfo(secData);
		}


		#endregion


		#region [ Override Implementation of NameObjectCollectionBase ]


		/// <summary>
		/// Gets the keys to all APGenSectionGroup objects contained in this APGenSectionGroupCollection object.
		/// </summary>
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get { return _groupInfo.Groups.Keys; }
		}


		/// <summary>
		/// Gets the number of section groups in the collection.
		/// </summary>
		public override int Count
		{
			get { return _groupInfo.Groups.Count; }
		}


		/// <summary>
		/// Gets an enumerator that can iterate through the APGenSectionGroupCollection object.
		/// </summary>
		/// <returns>An IEnumerator that can be used to iterate through the APGenSectionGroupCollection object.</returns>
		public override IEnumerator GetEnumerator()
		{
			return _groupInfo.Groups.AllKeys.GetEnumerator();
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
