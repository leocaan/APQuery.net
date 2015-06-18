using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Represents a xml element containing a collection of child elements.
	/// </summary>
	[DebuggerDisplayAttribute("Count = {Count}")]
	public abstract partial class APXmlElementCollection : APXmlElement, ICollection, IEnumerable
	{

		#region [ Fields ]


		private bool _keyMode;
		private ArrayList _list = new ArrayList();
		private IComparer _comparer;
		private string _addElementName = "add";


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Creates a new instance of the APXmlElementCollection class.
		/// </summary>
		protected APXmlElementCollection()
			: this(null, true)
		{
		}


		/// <summary>
		/// Creates a new instance of the APXmlElementCollection class.
		/// </summary>
		/// <param name="keyMode">Can be key mode.</param>
		protected APXmlElementCollection(bool keyMode)
			: this(null, keyMode)
		{
		}


		/// <summary>
		/// Creates a new instance of the APXmlElementCollection class.
		/// </summary>
		/// <param name="comparer">The IComparer comparer to use.</param>
		protected APXmlElementCollection(IComparer comparer)
			: this(comparer, true)
		{
		}


		/// <summary>
		/// Creates a new instance of the APXmlElementCollection class.
		/// </summary>
		/// <param name="comparer">The IComparer comparer to use.</param>
		/// <param name="keyMode">The key mode.</param>
		protected APXmlElementCollection(IComparer comparer, bool keyMode)
		{
			_comparer = comparer;
			_keyMode = keyMode;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the number of elements in the collection.
		/// </summary>
		public int Count
		{
			get { return _list.Count; }
		}


		/// <summary>
		/// The collection is key mode.
		/// </summary>
		public bool KeyMode
		{
			get { return _keyMode; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Copies the contents of the APXmlElementCollection to an array.
		/// </summary>
		/// <param name="array">Array to which to copy the contents of the APXmlElementCollection.</param>
		/// <param name="index">Index location at which to begin copying.</param>
		public void CopyTo(APXmlElement[] array, int index)
		{
			_list.CopyTo(array, index);
		}


		#endregion


		#region [ Protected Properties ]


		/// <summary>
		/// Gets the name used to identify this collection of elements in the xml file when
		/// overridden in a derived class.
		/// </summary>
		protected virtual string ElementName
		{
			get { return string.Empty; }
		}


		/// <summary>
		/// Gets a value indicating whether an attempt to add a duplicate APXmlElement to
		/// the APXmlElementCollection will cause an exception to be thrown.
		/// </summary>
		protected virtual bool ThrowOnDuplicate
		{
			get { return true; }
		}


		/// <summary>
		/// Gets or sets the name of the APXmlElement to associate with the add operation
		/// in the APXmlElementCollection when overridden in a derived class.
		/// </summary>
		protected internal string AddElementName
		{
			get { return _addElementName; }
			set { _addElementName = value; }
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Adds a APXmlElement to the APXmlElementCollection.
		/// </summary>
		/// <param name="element">The APXmlElement to add.</param>
		protected virtual void BaseAdd(APXmlElement element)
		{
			BaseAdd(element, ThrowOnDuplicate);
		}


		/// <summary>
		/// Adds a APXmlElement to the APXmlElementCollection.
		/// </summary>
		/// <param name="element">The APXmlElement to add.</param>
		/// <param name="throwIfExists">true to throw an exception if the APXmlElement specified
		/// is already contained in the APXmlElementCollection; otherwise, false.</param>
		protected void BaseAdd(APXmlElement element, bool throwIfExists)
		{
			if (_keyMode)
			{
				int oldIndex = IndexOfKey(GetElementKey(element));
				if (oldIndex >= 0)
				{
					if (element.Equals(_list[oldIndex]))
						return;
					if (throwIfExists)
						throw new APXmlException(APResource.APXml_DuplicateElementInCollection);
					_list.RemoveAt(oldIndex);
				}
			}
			_list.Add(element);
		}


		/// <summary>
		/// Adds a APXmlElement to the APXmlElementCollection.
		/// </summary>
		/// <param name="index">The index location at which to add the specified APXmlElement.</param>
		/// <param name="element">The APXmlElement to add.</param>
		protected virtual void BaseAdd(int index, APXmlElement element)
		{
			if (_keyMode)
			{
				if (ThrowOnDuplicate && BaseIndexOf(element) != -1)
					throw new APXmlException(APResource.APXml_DuplicateElementInCollection);
			}
			_list.Insert(index, element);
		}


		/// <summary>
		/// Removes all APXmlElement objects from the collection.
		/// </summary>
		protected internal void BaseClear()
		{
			_list.Clear();
		}


		/// <summary>
		/// Gets the APXmlElement at the specified index location.
		/// </summary>
		/// <param name="index">The index location of the APXmlElement to return.</param>
		/// <returns>The APXmlElement at the specified index.</returns>
		protected internal APXmlElement BaseGet(int index)
		{
			return _list[index] as APXmlElement;
		}


		/// <summary>
		/// Returns the APXmlElement with the specified key.
		/// </summary>
		/// <param name="key">The key of the element to return.</param>
		/// <returns>The APXmlElement with the specified key; otherwise, a null reference.</returns>
		protected internal APXmlElement BaseGet(object key)
		{
			int index = IndexOfKey(key);
			if (index != -1)
				return _list[index] as APXmlElement;
			else
				return null;
		}


		/// <summary>
		/// Returns an array of the keys for all of the APXmlElement contained in the APXmlElementCollection.
		/// </summary>
		/// <returns>An array containing the keys for all of the APXmlElement objects contained
		/// in the APXmlElementCollection.</returns>
		protected internal object[] BaseGetAllKeys()
		{
			object[] keys = new object[_list.Count];
			for (int i = 0; i < _list.Count; i++)
				keys[i] = BaseGetKey(i);
			return keys;
		}


		/// <summary>
		/// Gets the key for the APXmlElement at the specified index location.
		/// </summary>
		/// <param name="index">The index location for the APXmlElement.</param>
		/// <returns>The key for the specified APXmlElement.</returns>
		protected internal object BaseGetKey(int index)
		{
			if (index < 0 || index >= _list.Count)
				throw new APXmlException(APResource.GetString(APResource.AP_IndexOutOfRange, index));

			return GetElementKey((APXmlElement)_list[index]).ToString();
		}


		/// <summary>
		/// The index of the specified APXmlElementCollection.
		/// </summary>
		/// <param name="element">The APXmlElement for the specified index location.</param>
		/// <returns>The index of the specified APXmlElement; otherwise, -1.</returns>
		protected int BaseIndexOf(APXmlElement element)
		{
			return _list.IndexOf(element);
		}


		/// <summary>
		/// Removes a APXmlElement from the collection.
		/// </summary>
		/// <param name="key">The key of the APXmlElement to remove.</param>
		protected internal void BaseRemove(object key)
		{
			int index = IndexOfKey(key);
			if (index != -1)
			{
				BaseRemoveAt(index);
			}
		}


		/// <summary>
		/// Removes the APXmlElement at the specified index location.
		/// </summary>
		/// <param name="index">The index location of the APXmlElement to remove.</param>
		protected internal void BaseRemoveAt(int index)
		{
			APXmlElement elem = (APXmlElement)_list[index];

			_list.RemoveAt(index);
		}


		/// <summary>
		/// When overridden in a derived class, creates a new APXmlElement.
		/// </summary>
		/// <param name="elementName">The name of the APXmlElement to create.</param>
		/// <returns>A new APXmlElement.</returns>
		protected virtual APXmlElement CreateNewElement(string elementName)
		{
			return CreateNewElement();
		}


		/// <summary>
		/// Indicates whether the specified APXmlElement exists in the APXmlElementCollection.
		/// </summary>
		/// <param name="elementName">The name of the element to verify.</param>
		/// <returns>true if the element exists in the collection; otherwise, false. The default is false.</returns>
		protected virtual bool IsElementName(string elementName)
		{
			return false;
		}


		/// <summary>
		/// Creates a new APXmlElement when overridden in a derived class.
		/// </summary>
		/// <returns>A new APXmlElement.</returns>
		protected abstract APXmlElement CreateNewElement();


		/// <summary>
		/// Gets the element key for a specified configuration element when overridden in a derived class.
		/// </summary>
		/// <param name="element">The APXmlElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified APXmlElement.</returns>
		protected abstract object GetElementKey(APXmlElement element);


		#endregion


		#region [ Private Methods ]


		private int IndexOfKey(object key)
		{
			if (!_keyMode)
				throw new APXmlException(APResource.APXml_IsNotKeyModeCollection);

			for (int i = 0; i < _list.Count; i++)
			{
				if (CompareKeys(GetElementKey((APXmlElement)_list[i]), key))
					return i;
			}
			return -1;
		}


		private bool CompareKeys(object key1, object key2)
		{
			if (!_keyMode)
				throw new APXmlException(APResource.APXml_IsNotKeyModeCollection);

			if (_comparer != null)
				return _comparer.Compare(key1, key2) == 0;
			else
				return object.Equals(key1, key2);
		}


		private APXmlElement CreateNewElementInternal(string elementName)
		{
			APXmlElement elemment;
			if (elementName == null)
				elemment = CreateNewElement();
			else
				elemment = CreateNewElement(elementName);
			elemment.Init();
			return elemment;
		}


		#endregion


		#region [ Override Implemmentation of APXmlElement ]


		internal override void InitFromProperty(APXmlPropertyInformation propertyInfo)
		{
			APXmlCollectionAttribute attr = propertyInfo.Property.CollectionAttribute;
			if (attr == null)
				attr = Attribute.GetCustomAttribute(propertyInfo.Type, typeof(APXmlCollectionAttribute)) as APXmlCollectionAttribute;

			if (attr != null)
				_addElementName = attr.AddItemName;

			base.InitFromProperty(propertyInfo);
		}


		internal override bool HasValues()
		{
			return _list.Count > 0;
		}


		/// <summary>
		/// Overridden. Writes the data to an XML element in the xml file when overridden in a derived class.
		/// </summary>
		/// <param name="writer">Output stream that writes XML to the xml file.</param>
		/// <param name="serializeCollectionKey">true to serialize the collection key; otherwise, false.</param>
		/// <returns>true if the APXmlElementCollection was written to the xml file successfully.</returns>
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (serializeCollectionKey)
			{
				return base.SerializeElement(writer, serializeCollectionKey);
			}

			bool wroteData = false;

			for (int i = 0; i < _list.Count; i++)
			{
				APXmlElement elem = (APXmlElement)_list[i];
				elem.SerializeToXmlElement(writer, _addElementName);
			}

			wroteData = wroteData || _list.Count > 0;

			return wroteData;
		}


		/// <summary>
		/// Overridden. Causes the system to throw an exception.
		/// </summary>
		/// <param name="elementName">The name of the unrecognized element.</param>
		/// <param name="reader">An input stream that reads XML from the xml file.</param>
		/// <returns>true if the unrecognized element was deserialized successfully; otherwise, false. The default is false.</returns>
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (elementName == _addElementName)
			{
				APXmlElement elem = CreateNewElementInternal(null);
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
			APXmlElementCollection other = compareTo as APXmlElementCollection;
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
