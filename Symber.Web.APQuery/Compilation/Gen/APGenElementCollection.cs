using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of config element.
	/// </summary>
	[DebuggerDisplayAttribute("Count = {Count}")]
	public abstract partial class APGenElementCollection : APGenElement, ICollection, IEnumerable
	{

		#region [ Fields ]


		private ArrayList _list = new ArrayList();
		private IComparer _comparer;
		private string _addElementName = "add";


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new element collection.
		/// </summary>
		protected APGenElementCollection ()
		{
		}


		/// <summary>
		/// Create a new element collection with comparer.
		/// </summary>
		/// <param name="comparer">Comparer</param>
		protected APGenElementCollection(IComparer comparer)
		{
			_comparer = comparer;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// The count of the collection.
		/// </summary>
		public int Count
		{
			get { return _list.Count; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Copies the entire collection values to a one-dimensional array of strings, starting at
		/// the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array of strings that is the destination of the
		/// elements copied from collection. The Array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(APGenElement[] array, int index)
		{
			_list.CopyTo(array, index);
		}


		#endregion


		#region [ Protected Properties ]


		/// <summary>
		/// Element name.
		/// </summary>
		protected virtual string ElementName
		{
			get { return string.Empty; }
		}


		/// <summary>
		/// whether to throw an exception when duplicate name found.
		/// </summary>
		protected virtual bool ThrowOnDuplicate
		{
			get { return true; }
		}


		/// <summary>
		/// Add element name.
		/// </summary>
		protected internal string AddElementName
		{
			get { return _addElementName; }
			set { _addElementName = value; }
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Adds a configuration element to the configuration element collection.
		/// </summary>
		/// <param name="element">The APGenElement to add.</param>
		protected virtual void BaseAdd(APGenElement element)
		{
			BaseAdd(element, ThrowOnDuplicate);
		}


		/// <summary>
		/// Adds a configuration element to the configuration element collection.
		/// </summary>
		/// <param name="element">The APGenElement to add.</param>
		/// <param name="throwIfExists">true to throw an exception if the APGenElement specified is already contained
		/// in the collection; otherwise, false.</param>
		protected void BaseAdd(APGenElement element, bool throwIfExists)
		{
			int oldIndex = IndexOfKey(GetElementKey(element));
			if (oldIndex >= 0)
			{
				if (element.Equals(_list[oldIndex]))
					return;
				if (throwIfExists)
					throw new APGenException(APResource.APGen_DuplicateElementInCollection);
				_list.RemoveAt(oldIndex);
			}
			_list.Add(element);
		}


		/// <summary>
		/// Adds a configuration element to the configuration element collection.
		/// </summary>
		/// <param name="index">The index location at which to add the specified APGenElement.</param>
		/// <param name="element">The APGenElement to add.</param>
		protected virtual void BaseAdd(int index, APGenElement element)
		{
			if (ThrowOnDuplicate && BaseIndexOf(element) != -1)
				throw new APGenException(APResource.APGen_DuplicateElementInCollection);

			_list.Insert(index, element);
		}


		/// <summary>
		/// Removes all configuration element objects from the collection.
		/// </summary>
		protected internal void BaseClear()
		{
			_list.Clear();
		}


		/// <summary>
		/// Gets the configuration element at the specified index location.
		/// </summary>
		/// <param name="index">The index location of the APGenElement to return.</param>
		/// <returns>The APGenElement at the specified index.</returns>
		protected internal APGenElement BaseGet(int index)
		{
			return _list[index] as APGenElement;
		}


		/// <summary>
		/// Returns the configuration element with the specified key.
		/// </summary>
		/// <param name="key">The key of the element to return.</param>
		/// <returns>The APGenElement with the specified key; otherwise, null.</returns>
		protected internal APGenElement BaseGet(object key)
		{
			int index = IndexOfKey(key);
			if (index != -1)
				return _list[index] as APGenElement;
			else
				return null;
		}


		/// <summary>
		/// Returns an array of the keys for all of the configuration elements contained in the collection.
		/// </summary>
		/// <returns></returns>
		protected internal object[] BaseGetAllKeys()
		{
			object[] keys = new object[_list.Count];
			for (int i = 0; i < _list.Count; i++)
				keys[i] = BaseGetKey(i);
			return keys;
		}


		/// <summary>
		/// Gets the key for the APGenElement at the specified index location.
		/// </summary>
		/// <param name="index">The index location for the APGenElement.</param>
		/// <returns>The key for the specified APGenElement.</returns>
		protected internal object BaseGetKey(int index)
		{
			if (index < 0 || index >= _list.Count)
				throw new APGenException(APResource.GetString(APResource.APGen_IndexOutOfRange, index));

			return GetElementKey((APGenElement)_list[index]).ToString();
		}


		/// <summary>
		/// The index of the specified APGenElement.
		/// </summary>
		/// <param name="element">The APGenElement for the specified index location.</param>
		/// <returns>The index of the specified APGenElement; otherwise, -1.</returns>
		protected int BaseIndexOf(APGenElement element)
		{
			return _list.IndexOf(element);
		}


		/// <summary>
		/// Removes a APGenElement from the collection.
		/// </summary>
		/// <param name="key">The key of the APGenElement to remove.</param>
		protected internal void BaseRemove(object key)
		{
			int index = IndexOfKey(key);
			if (index != -1)
			{
				BaseRemoveAt(index);
			}
		}


		/// <summary>
		/// Removes the APGenElement at the specified index location.
		/// </summary>
		/// <param name="index">The index location of the APGenElement to remove.</param>
		protected internal void BaseRemoveAt(int index)
		{
			APGenElement elem = (APGenElement)_list[index];

			_list.RemoveAt(index);
		}


		/// <summary>
		/// Creates a new APGenElement when overridden in a derived class.
		/// </summary>
		/// <param name="elementName">The name of the APGenElement to create.</param>
		/// <returns>A new APGenElement.</returns>
		protected virtual APGenElement CreateNewElement(string elementName)
		{
			return CreateNewElement();
		}


		/// <summary>
		/// Indicates whether the specified APGenElement exists in the collection.
		/// </summary>
		/// <param name="elementName">The name of the element to verify.</param>
		/// <returns>true if the element exists in the collection; otherwise, false. The default is false.</returns>
		protected virtual bool IsElementName(string elementName)
		{
			return false;
		}


		/// <summary>
		/// When overridden in a derived class, creates a new APGenElement.
		/// </summary>
		/// <returns>A new APGenElement.</returns>
		protected abstract APGenElement CreateNewElement();


		/// <summary>
		/// Gets the element key for a specified configuration element when overridden in a derived class.
		/// </summary>
		/// <param name="element">The APGenElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified APGenElement.</returns>
		protected abstract object GetElementKey(APGenElement element);


		#endregion


		#region [ Private Methods ]


		private int IndexOfKey(object key)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (CompareKeys(GetElementKey((APGenElement)_list[i]), key))
					return i;
			}
			return -1;
		}


		private bool CompareKeys(object key1, object key2)
		{
			if (_comparer != null)
				return _comparer.Compare(key1, key2) == 0;
			else
				return object.Equals(key1, key2);
		}


		private APGenElement CreateNewElementInternal(string elementName)
		{
			APGenElement elemment;
			if (elementName == null)
				elemment = CreateNewElement();
			else
				elemment = CreateNewElement(elementName);
			elemment.Init();
			return elemment;
		}


		#endregion


		#region [ Override Implemmentation of APGenElement ]


		internal override void InitFromProperty(APGenPropertyInformation propertyInfo)
		{
			APGenCollectionAttribute attr = propertyInfo.Property.CollectionAttribute;
			if (attr == null)
				attr = Attribute.GetCustomAttribute(propertyInfo.Type, typeof(APGenCollectionAttribute)) as APGenCollectionAttribute;

			if (attr != null)
				_addElementName = attr.AddItemName;

			base.InitFromProperty(propertyInfo);
		}


		internal override bool HasValues()
		{
			return _list.Count > 0;
		}


		/// <summary>
		/// Writes the configuration data to an XML element in the configuration file when overridden in a derived class.
		/// </summary>
		/// <param name="writer">Output stream that writes XML to the configuration file.</param>
		/// <param name="serializeCollectionKey">true to serialize the collection key; otherwise, false.</param>
		/// <returns>true if the collection was written to the configuration file successfully.</returns>
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (serializeCollectionKey)
			{
				return base.SerializeElement(writer, serializeCollectionKey);
			}

			bool wroteData = false;

			for (int i = 0; i < _list.Count; i++)
			{
				APGenElement elem = (APGenElement)_list[i];
				elem.SerializeToXmlElement(writer, _addElementName);
			}

			wroteData = wroteData || _list.Count > 0;

			return wroteData;
		}


		/// <summary>
		/// Causes the configuration system to throw an exception.
		/// </summary>
		/// <param name="elementName">The name of the unrecognized element.</param>
		/// <param name="reader">An input stream that reads XML from the configuration file.</param>
		/// <returns>true if the unrecognized element was deserialized successfully; otherwise, false. The default is false.</returns>
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (elementName == _addElementName)
			{
				APGenElement elem = CreateNewElementInternal(null);
				elem.DeserializeElement(reader, false);
				BaseAdd(elem);
				return true;
			}

			return false;
		}


		#endregion


		#region [ Override Implementation of Object ]


		/// <summary>
		/// Determines whether two Object instances are equal.
		/// </summary>
		/// <param name="compareTo">The Object to compare with the current Object.</param>
		/// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
		public override bool Equals(object compareTo)
		{
			APGenElementCollection other = compareTo as APGenElementCollection;
			if (other == null)
				return false;
			
			if (GetType() != other.GetType())
				return false;
			
			if (Count != other.Count)
				return false;

			for (int i = 0; i < Count; i++)
			{
				if (!BaseGet(i).Equals(other.BaseGet(i)))
					return false;
			}
			return true;
		}


		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current Object.</returns>
		public override int GetHashCode()
		{
			int code = 0;
			for (int i = 0; i < Count; i++)
				code += BaseGet(i).GetHashCode();
			return code;
		}


		#endregion


		#region [ Implemmentation of ICollection ]


		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)_list).CopyTo(array, index);
		}


		int ICollection.Count
		{
			get { return Count; }
		}


		/// <summary>
		/// IsSynchronized.
		/// </summary>
		public bool IsSynchronized
		{
			get { return false; }
		}


		/// <summary>
		/// SyncRoot.
		/// </summary>
		public object SyncRoot
		{
			get { return this; }
		}

		/// <summary>
		/// Get the enumerator
		/// </summary>
		/// <returns>The enumerator of source.</returns>
		public IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}


		#endregion


		#region [ Implemmentation of IEnumerable ]


		IEnumerator IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}


		#endregion

	}
}
