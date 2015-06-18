using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// Collection of APRptOrderDef.
	/// </summary>
	[APXmlCollection(typeof(APRptOrderDef))]
	public class APRptOrderDefCollection : APXmlElementCollection
	{

		#region [ Static Fields ]


		static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptOrderDefCollection()
		{
			properties = new APXmlPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a APRptOrderDef by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified APRptOrderDef.</returns>
		public APRptOrderDef this[int index]
		{
			get { return (APRptOrderDef)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a APRptOrderDef by serial key.
		/// </summary>
		/// <param name="columnId">The serial key to access.</param>
		/// <returns>The specified APRptOrderDef.</returns>
		public new APRptOrderDef this[string columnId]
		{
			get { return (APRptOrderDef)BaseGet(columnId); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add item.
		/// </summary>
		/// <param name="refer">Item.</param>
		public void Add(APRptOrderDef refer)
		{
			BaseAdd(refer);
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
		/// <param name="columnId">The serial key of the index in collection.</param>
		public void Remove(string columnId)
		{
			BaseRemove(columnId);
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
			return new APRptOrderDef();
		}


		/// <summary>
		/// Gets a element by key;
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>The key of element.</returns>
		protected override object GetElementKey(APXmlElement element)
		{
			return ((APRptOrderDef)element).ColumnId;
		}


		/// <summary>
		/// Gets the collection of properties
		/// </summary>
		protected internal override APXmlPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion

	}

}
