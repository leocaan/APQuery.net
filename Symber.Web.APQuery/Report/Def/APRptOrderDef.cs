using Symber.Web.Data;
using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// Order of APRptColumn.
	/// </summary>
	public class APRptOrderDef : APXmlElement
	{

		#region [ Static Fields ]


		private static APXmlProperty columnIdProp;
		private static APXmlProperty accordingProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptOrderDef()
		{
			columnIdProp = new APXmlProperty("columnId", typeof(string), "", APXmlPropertyOptions.IsRequired | APXmlPropertyOptions.IsKey);
			accordingProp = new APXmlProperty("according", typeof(APSqlOrderAccording), APSqlOrderAccording.Asc,
				new GenericEnumAPConverter(typeof(APSqlOrderAccording)),
				APCVHelper.DefaultValidator,
				APXmlPropertyOptions.IsRequired);

			properties = new APXmlPropertyCollection();
			properties.Add(columnIdProp);
			properties.Add(accordingProp);
		}


		/// <summary>
		/// Create a new APRptOrderDef. 
		/// </summary>
		public APRptOrderDef()
		{
		}


		/// <summary>
		/// Create a new APRptOrderDef. 
		/// </summary>
		/// <param name="columnId">The APRptColumn ID.</param>
		/// <param name="according">Order according.</param>
		public APRptOrderDef(string columnId, APSqlOrderAccording according)
		{
			ColumnId = columnId;
			According = according;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// ID of APRptColumn.
		/// </summary>
		[APXmlProperty("columnId", IsRequired = true, IsKey = true)]
		public string ColumnId
		{
			get { return (string)base[columnIdProp]; }
			set { base[columnIdProp] = value; }
		}


		/// <summary>
		/// Order according.
		/// </summary>
		[APXmlProperty("according", DefaultValue = APSqlOrderAccording.Asc, IsRequired = true)]
		public APSqlOrderAccording According
		{
			get { return (APSqlOrderAccording)base[accordingProp]; }
			set { base[accordingProp] = value; }
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
