using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Contains a collection of APXmlPropertyInformation objects. This class cannot be inherited.
	/// </summary>
	[Serializable]
	public sealed class APXmlPropertyInformationCollection : NameObjectCollectionBase
	{

		#region [ Inner Class ]


		class APXmlPropertyInformationEnumerator : IEnumerator
		{

			#region [ Fields ]


			private APXmlPropertyInformationCollection _collection;
			private int _position;


			#endregion


			#region [ Constructors ]


			public APXmlPropertyInformationEnumerator(APXmlPropertyInformationCollection collection)
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


		internal APXmlPropertyInformationCollection()
			: base(StringComparer.Ordinal)
		{
		}


		internal APXmlPropertyInformationCollection(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the APXmlPropertyInformation object in the collection, based on the specified property name.
		/// </summary>
		/// <param name="propertyName">The name of the attribute contained in the APXmlPropertyInformationCollection object.</param>
		/// <returns>A APXmlPropertyInformation object.</returns>
		public APXmlPropertyInformation this[string propertyName]
		{
			get { return (APXmlPropertyInformation)BaseGet(propertyName); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Copies the entire APXmlPropertyInformationCollection collection to a compatible one-dimensional Array,
		/// starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">A one-dimensional Array that is the destination of the elements copied from the
		/// APXmlPropertyInformationCollection collection. The Array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(APXmlPropertyInformation[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}


		#endregion


		#region [ Internal Methods ]


		internal void Add(APXmlPropertyInformation property)
		{
			BaseAdd(property.Name, property);
		}


		#endregion


		#region [ Override Implementation of NameObjectCollectionBase ]


		/// <summary>
		/// Gets an IEnumerator object, which is used to iterate through this APXmlPropertyInformationCollection collection.
		/// </summary>
		/// <returns>an IEnumerator object.</returns>
		public override IEnumerator GetEnumerator()
		{
			return new APXmlPropertyInformationEnumerator(this);
		}


		#endregion

	}

}
