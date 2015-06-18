using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// APRptReportDef xml format, you can see APRptReportDef.xml.
	/// </summary>
	public class APRptReportDef : APXmlRoot
	{

		#region [ Static Fields ]


		private static APXmlProperty nameProp;
		private static APXmlProperty refersProp;
		private static APXmlProperty ordersProp;
		private static APXmlProperty groupsProp;
		private static APXmlProperty conditionProp;
		private static APXmlProperty showSummaryProp;
		private static APXmlProperty frameColumnIdProp;
		private static APXmlProperty frameBeginProp;
		private static APXmlProperty frameEndProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptReportDef()
		{
			nameProp = new APXmlProperty("name", typeof(string), "");
			refersProp = new APXmlProperty("refers", typeof(APRptReferDefCollection));
			ordersProp = new APXmlProperty("orders", typeof(APRptOrderDefCollection));
			groupsProp = new APXmlProperty("groups", typeof(APRptGroupDefCollection));
			conditionProp = new APXmlProperty("condition", typeof(APRptConditionDef));
			showSummaryProp = new APXmlProperty("showSummary", typeof(bool), true);
			frameColumnIdProp = new APXmlProperty("frameColumnId", typeof(string), "");
			frameBeginProp = new APXmlProperty("frameBegin", typeof(string), "");
			frameEndProp = new APXmlProperty("frameEnd", typeof(string), "");

			properties = new APXmlPropertyCollection();
			properties.Add(nameProp);
			properties.Add(refersProp);
			properties.Add(ordersProp);
			properties.Add(groupsProp);
			properties.Add(showSummaryProp);
			properties.Add(frameColumnIdProp);
			properties.Add(frameBeginProp);
			properties.Add(frameEndProp);
			properties.Add(conditionProp);
		}

		
		/// <summary>
		/// Create a new APRptReportDef.
		/// </summary>
		public APRptReportDef()
		{
		}


		/// <summary>
		/// Create a new APRptReportDef, load from xml.
		/// </summary>
		/// <param name="xml">The source xml.</param>
		public APRptReportDef(string xml)
		{
			ReadXml(xml);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets name of APRptReportDef.
		/// </summary>
		[APXmlProperty("name")]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// All refers of the report.
		/// </summary>
		[APXmlProperty("refers")]
		public APRptReferDefCollection Refers
		{
			get { return (APRptReferDefCollection)base[refersProp]; }
		}


		/// <summary>
		/// All orders of the report.
		/// </summary>
		[APXmlProperty("orders")]
		public APRptOrderDefCollection Orders
		{
			get { return (APRptOrderDefCollection)base[ordersProp]; }
		}


		/// <summary>
		/// All groups of the report.
		/// </summary>
		[APXmlProperty("groups")]
		public APRptGroupDefCollection Groups
		{
			get { return (APRptGroupDefCollection)base[groupsProp]; }
		}


		/// <summary>
		/// If true, report show summary.
		/// </summary>
		[APXmlProperty("showSummary", DefaultValue = true)]
		public bool ShowSummary
		{
			get { return (bool)base[showSummaryProp]; }
			set { base[showSummaryProp] = value; }
		}


		/// <summary>
		/// Addition condition, the date frame column id.
		/// </summary>
		[APXmlProperty("frameColumnId")]
		public string FrameColumnId
		{
			get { return (string)base[frameColumnIdProp]; }
			set { base[frameColumnIdProp] = value; }
		}


		/// <summary>
		/// Addition condition, the date frame begin value.
		/// </summary>
		[APXmlProperty("frameBegin", DefaultValue = "")]
		public string FrameBegin
		{
			get { return (string)base[frameBeginProp]; }
			set { base[frameBeginProp] = value; }
		}


		/// <summary>
		/// Addition condition, the date frame end value.
		/// </summary>
		[APXmlProperty("frameEnd", DefaultValue = "")]
		public string FrameEnd
		{
			get { return (string)base[frameEndProp]; }
			set { base[frameEndProp] = value; }
		}


		/// <summary>
		/// Condition of the report.
		/// </summary>
		[APXmlProperty("condition")]
		public APRptConditionDef Condition
		{
			get { return (APRptConditionDef)base[conditionProp]; }
			set { base[conditionProp] = value; }
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APXmlPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion


		#region [ Override Implementation of APXmlRoot ]


		/// <summary>
		/// Gets the local name of xml root.
		/// </summary>
		protected override string LocalName
		{
			get { return "reportDef"; }
		}


		#endregion

	}

}
