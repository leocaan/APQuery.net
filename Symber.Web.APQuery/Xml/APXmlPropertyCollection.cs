using System;
using System.Collections;
using System.Collections.Generic;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Represents a collection of xml-element properties.
	/// </summary>
	public class APXmlPropertyCollection : ICollection, IEnumerable
	{

		#region [ Fields ]


		private List<APXmlProperty> _collection;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Creates a new instance of APXmlPropertyCollection.
		/// </summary>
		public APXmlPropertyCollection()
		{
			_collection = new List<APXmlProperty>();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the number of properties in the collection.
		/// </summary>
		public int Count
		{
			get { return _collection.Count; }
		}


		/// <summary>
		/// Allows access to the collection.
		/// </summary>
		/// <param name="name">The APXmlProperty to return.</param>
		/// <returns>The APXmlProperty with the specified name.</returns>
		public APXmlProperty this[string name]
		{
			get
			{
				foreach (APXmlProperty property in _collection)
				{
					if (property.Name == name)
						return property;
				}

				return null;
			}
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Adds a APXmlProperty to the collection.
		/// </summary>
		/// <param name="property">The APXmlProperty to add.</param>
		public void Add(APXmlProperty property)
		{
			_collection.Add(property);
		}


		/// <summary>
		/// Specifies whether the APXmlProperty is contained in this collection.
		/// </summary>
		/// <param name="name">An identifier for the APXmlProperty to verify.</param>
		/// <returns>true if the specified APXmlProperty is contained in the collection; otherwise, false.</returns>
		public bool Contains(string name)
		{
			APXmlProperty property = this[name];

			if (property == null)
				return false;

			return _collection.Contains(property);
		}


		/// <summary>
		/// Copies this APXmlPropertyCollection to an array.
		/// </summary>
		/// <param name="array">Array to which to copy.</param>
		/// <param name="index">Index at which to begin copying.</param>
		public void CopyTo(APXmlProperty[] array, int index)
		{
			_collection.CopyTo(array, index);
		}


		/// <summary>
		/// Removes a APXmlProperty from the collection.
		/// </summary>
		/// <param name="name">The APXmlProperty to remove.</param>
		/// <returns>true if the specified APXmlProperty was removed; otherwise, false.</returns>
		public bool Remove(string name)
		{
			return _collection.Remove(this[name]);
		}


		/// <summary>
		/// Removes all configuration property objects from the collection.
		/// </summary>
		public void Clear()
		{
			_collection.Clear();
		}


		#endregion


		#region [ Override Implementation of ICollection ]


		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)_collection).CopyTo(array, index);
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
			get { return _collection; }
		}


		/// <summary>
		/// Get the enumerator
		/// </summary>
		/// <returns>The enumerator of source.</returns>
		public IEnumerator GetEnumerator()
		{
			return _collection.GetEnumerator();
		}


		#endregion


		#region [ Override Implementation of IEnumerable ]


		IEnumerator IEnumerable.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}


		#endregion

	}

}
