using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// Collection of APRptFilterDef.
	/// </summary>
	[APXmlCollection(typeof(APRptFilterDef))]
	public class APRptFilterDefCollection : APXmlElementCollection
	{

		#region [ Static Fields ]


		static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptFilterDefCollection()
		{
			properties = new APXmlPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a APRptFilterDef by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified APRptFilterDef.</returns>
		public APRptFilterDef this[int index]
		{
			get { return (APRptFilterDef)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a APRptFilterDef by serial key.
		/// </summary>
		/// <param name="serial">The serial key to access.</param>
		/// <returns>The specified APRptFilterDef.</returns>
		public new APRptFilterDef this[string serial]
		{
			get { return (APRptFilterDef)BaseGet(serial); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add item.
		/// </summary>
		/// <param name="filter">Item.</param>
		public void Add(APRptFilterDef filter)
		{
			BaseAdd(filter);
		}


		/// <summary>
		/// Clear the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}

		/// <summary>
		/// Remove item by serial key of the index in collection.
		/// </summary>
		/// <param name="serial">The serial key of the index in collection.</param>
		public void Remove(string serial)
		{
			BaseRemove(serial);
		}


		/// <summary>
		/// Remove item by index of the index in collection.
		/// </summary>
		/// <param name="index">The index of the index in collection.</param>
		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}


		#endregion


		#region [ Override Implementation of APXmlElementCollection ]


		/// <summary>
		/// Create a new element.
		/// </summary>
		/// <returns>The element.</returns>
		protected override APXmlElement CreateNewElement()
		{
			return new APRptFilterDef();
		}


		/// <summary>
		/// Gets a element by key;
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>The key of element.</returns>
		protected override object GetElementKey(APXmlElement element)
		{
			return ((APRptFilterDef)element).Serial;
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APXmlPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion

	}

}
