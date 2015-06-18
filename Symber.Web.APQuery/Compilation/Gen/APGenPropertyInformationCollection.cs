using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Contains a collection of APGenPropertyInformation objects. This class cannot be inherited.
	/// </summary>
	[Serializable]
	public sealed class APGenPropertyInformationCollection : NameObjectCollectionBase
	{

		#region [ Inner Class ]


		class APGenPropertyInformationEnumerator : IEnumerator
		{
			#region [ Fields ]


			private APGenPropertyInformationCollection _collection;
			private int _position;


			#endregion

			#region [ Constructors ]


			public APGenPropertyInformationEnumerator(APGenPropertyInformationCollection collection)
			{
				_collection = collection;
				_position = -1;
			}


			#endregion

			#region [ Implementation of IEnumerator ]


			public object Current
			{
				get
				{
					if ((_position < _collection.Count) && (_position >= 0))
						return _collection.BaseGet(_position);
					else
						throw new InvalidOperationException();
				}
			}


			public bool MoveNext()
			{
				return (++_position < _collection.Count) ? true : false;
			}


			public void Reset()
			{
				_position = -1;
			}


			#endregion
		}


		#endregion


		#region [ Constructors ]


		internal APGenPropertyInformationCollection()
			: base(StringComparer.Ordinal)
		{
		}


		internal APGenPropertyInformationCollection(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the APGenPropertyInformation object in the collection, based on the specified property name.
		/// </summary>
		/// <param name="propertyName">The name of the configuration attribute contained in the
		/// APGenPropertyInformationCollection object.</param>
		/// <returns>A APGenPropertyInformation object.</returns>
		public APGenPropertyInformation this[string propertyName]
		{
			get { return (APGenPropertyInformation)BaseGet(propertyName); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Copies the entire APGenPropertyInformationCollection collection to a compatible one-dimensional
		/// Array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">A one-dimensional Array that is the destination of the elements copied from
		/// the APGenPropertyInformationCollection collection. The Array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(APGenPropertyInformation[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}


		#endregion


		#region [ Internal Methods ]


		internal void Add(APGenPropertyInformation property)
		{
			BaseAdd(property.Name, property);
		}


		#endregion


		#region [ Override Implementation of NameObjectCollectionBase ]


		/// <summary>
		/// Gets an IEnumerator object, which is used to iterate through this APGenPropertyInformationCollection collection.
		/// </summary>
		/// <returns>An IEnumerator object, which is used to iterate through this APGenPropertyInformationCollection.</returns>
		public override IEnumerator GetEnumerator()
		{
			return new APGenPropertyInformationEnumerator(this);
		}


		#endregion

	}
}
