using System;
using System.Collections;
using System.Collections.Generic;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Represents a collection of configuration-element properties.
	/// </summary>
	public class APGenPropertyCollection : ICollection, IEnumerable
	{

		#region [ Fields ]


		private List<APGenProperty> _collection;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of the APGenPropertyCollection class.
		/// </summary>
		public APGenPropertyCollection()
		{
			_collection = new List<APGenProperty>();
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
		/// Gets the collection item with the specified name.
		/// </summary>
		/// <param name="name">The APGenProperty to return.</param>
		/// <returns>The APGenProperty with the specified name.</returns>
		public APGenProperty this[string name]
		{
			get
			{
				foreach (APGenProperty property in _collection)
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
		/// Adds a configuration property to the collection.
		/// </summary>
		/// <param name="property">The APGenProperty to add.</param>
		public void Add(APGenProperty property)
		{
			_collection.Add(property);
		}


		/// <summary>
		/// Specifies whether the configuration property is contained in this collection.
		/// </summary>
		/// <param name="name">An identifier for the APGenProperty to verify.</param>
		/// <returns>true if the specified APGenProperty is contained in the collection; otherwise, false.</returns>
		public bool Contains(string name)
		{
			APGenProperty property = this[name];

			if (property == null)
				return false;

			return _collection.Contains(property);
		}


		/// <summary>
		/// Copies this APGenPropertyCollection to an array.
		/// </summary>
		/// <param name="array">Array to which to copy.</param>
		/// <param name="index">Index at which to begin copying.</param>
		public void CopyTo(APGenProperty[] array, int index)
		{
			_collection.CopyTo(array, index);
		}


		/// <summary>
		/// Removes a configuration property from the collection.
		/// </summary>
		/// <param name="name">The APGenProperty to remove.</param>
		/// <returns>true if the specified APGenProperty was removed; otherwise, false.</returns>
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
