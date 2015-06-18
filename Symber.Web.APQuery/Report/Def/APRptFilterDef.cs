using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// Filter of APRptColumn.
	/// </summary>
	public class APRptFilterDef : APXmlElement
	{

		#region [ Static Fields ]


		private static APXmlProperty serialProp;
		private static APXmlProperty columnIdProp;
		private static APXmlProperty comparatorProp;
		private static APXmlProperty valuesProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptFilterDef()
		{
			serialProp = new APXmlProperty("serial", typeof(string), "", APXmlPropertyOptions.IsRequired | APXmlPropertyOptions.IsKey);
			columnIdProp = new APXmlProperty("columnId", typeof(string), "", APXmlPropertyOptions.IsRequired);
			comparatorProp = new APXmlProperty("comparator", typeof(APRptFilterComparator), APRptFilterComparator.Equals,
				new GenericEnumAPConverter(typeof(APRptFilterComparator)),
				APCVHelper.DefaultValidator,
				APXmlPropertyOptions.IsRequired);
			valuesProp = new APXmlProperty("values", typeof(string), "");

			properties = new APXmlPropertyCollection();
			properties.Add(serialProp);
			properties.Add(columnIdProp);
			properties.Add(comparatorProp);
			properties.Add(valuesProp);
		}


		/// <summary>
		/// Create a new APRptFilterDef.
		/// </summary>
		public APRptFilterDef()
		{
		}


		/// <summary>
		/// Create a new APRptFilterDef.
		/// </summary>
		/// <param name="serial">Serial of filter.</param>
		/// <param name="columnId">ID of APRptColumn.</param>
		/// <param name="comparator">Comparator of filter.</param>
		/// <param name="values">Values of filter.</param>
		public APRptFilterDef(string serial, string columnId, APRptFilterComparator comparator, string values)
		{
			Serial = serial;
			ColumnId = columnId;
			Comparator = comparator;
			Values = values;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Serial of APRptFilterDef.
		/// </summary>
		[APXmlProperty("serial", IsRequired = true, IsKey = true)]
		public string Serial
		{
			get { return (string)base[serialProp]; }
			set { base[serialProp] = value; }
		}


		/// <summary>
		/// ID of APRptColumn
		/// </summary>
		[APXmlProperty("columnId", IsRequired = true)]
		public string ColumnId
		{
			get { return (string)base[columnIdProp]; }
			set { base[columnIdProp] = value; }
		}


		/// <summary>
		/// Comparator of filter.
		/// </summary>
		[APXmlProperty("comparator", DefaultValue = APRptFilterComparator.Equals, IsRequired = true)]
		public APRptFilterComparator Comparator
		{
			get { return (APRptFilterComparator)base[comparatorProp]; }
			set { base[comparatorProp] = value; }
		}


		/// <summary>
		/// Values of APRptFilterDef.
		/// </summary>
		[APXmlProperty("values", DefaultValue = "")]
		public string Values
		{
			get { return (string)base[valuesProp]; }
			set { base[valuesProp] = value; }
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
