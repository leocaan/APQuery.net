using Symber.Web.Data;
using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// Group of APRptColumn.
	/// </summary>
	public class APRptGroupDef : APXmlElement
	{

		#region [ Static Fields ]


		private static APXmlProperty columnIdProp;
		private static APXmlProperty accordingProp;
		private static APXmlProperty dateGroupModeProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptGroupDef()
		{
			columnIdProp = new APXmlProperty("columnId", typeof(string), "", APXmlPropertyOptions.IsRequired | APXmlPropertyOptions.IsKey);
			accordingProp = new APXmlProperty("according", typeof(APSqlOrderAccording), APSqlOrderAccording.Asc,
				new GenericEnumAPConverter(typeof(APSqlOrderAccording)),
				APCVHelper.DefaultValidator,
				APXmlPropertyOptions.IsRequired);
			dateGroupModeProp = new APXmlProperty("dateGroupMode", typeof(APSqlDateGroupMode), APSqlDateGroupMode.Day,
				new GenericEnumAPConverter(typeof(APSqlDateGroupMode)),
				APCVHelper.DefaultValidator,
				APXmlPropertyOptions.None);

			properties = new APXmlPropertyCollection();
			properties.Add(columnIdProp);
			properties.Add(accordingProp);
			properties.Add(dateGroupModeProp);
		}


		/// <summary>
		/// Create a new APRptGroupDef.
		/// </summary>
		public APRptGroupDef()
		{
		}


		/// <summary>
		/// Create a new APRptGroupDef.
		/// </summary>
		/// <param name="columnId">ID of APRptColumn.</param>
		/// <param name="according">Order according.</param>
		/// <param name="dateGroupMode">Date group mode of APRptColumn.</param>
		public APRptGroupDef(string columnId, APSqlOrderAccording according, APSqlDateGroupMode dateGroupMode)
		{
			ColumnId = columnId;
			According = according;
			DateGroupMode = dateGroupMode;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// ID of APRptColumn
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
		/// Date group mode of APRptColumn.
		/// </summary>
		[APXmlProperty("dateGroupMode", DefaultValue = APSqlDateGroupMode.Day)]
		public APSqlDateGroupMode DateGroupMode
		{
			get { return (APSqlDateGroupMode)base[dateGroupModeProp]; }
			set { base[dateGroupModeProp] = value; }
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
