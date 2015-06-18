using Symber.Web.Xml;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Collection of APRptReferDef.
	/// </summary>
	[APXmlCollection(typeof(APRptReferDef))]
	public class APRptReferDefCollection : APXmlElementCollection
	{

		#region [ Static Fields ]


		static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptReferDefCollection()
		{
			properties = new APXmlPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a APRptReferDef by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified APRptReferDef.</returns>
		public APRptReferDef this[int index]
		{
			get { return (APRptReferDef)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a APRptReferDef by serial key.
		/// </summary>
		/// <param name="columnId">The serial key to access.</param>
		/// <returns>The specified APRptReferDef.</returns>
		public new APRptReferDef this[string columnId]
		{
			get { return (APRptReferDef)BaseGet(columnId); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add item.
		/// </summary>
		/// <param name="refer">Item.</param>
		public void Add(APRptReferDef refer)
		{
			BaseAdd(refer);
		}


		/// <summary>
		/// Add item by column Id.
		/// </summary>
		/// <param name="columnId">Column Id</param>
		public void Add(string columnId)
		{
			Add(new APRptReferDef(columnId));
		}


		/// <summary>
		/// Add items by column Id.
		/// </summary>
		/// <param name="columnIds">Column Ids.</param>
		public void AddRange(IEnumerable<string> columnIds)
		{
			foreach (var item in columnIds)
			{
				Add(new APRptReferDef(item));
			}
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
			return new APRptReferDef();
		}


		/// <summary>
		/// Gets a element by key;
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>The key of element.</returns>
		protected override object GetElementKey(APXmlElement element)
		{
			return ((APRptReferDef)element).ColumnId;
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
