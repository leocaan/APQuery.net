using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// APViewExDef xml format, you can see APRptViewDef.xml.
	/// </summary>
	public class APRptViewDef : APXmlRoot
	{

		#region [ Static Fields ]


		private static APXmlProperty nameProp;
		private static APXmlProperty refersProp;
		private static APXmlProperty ordersProp;
		private static APXmlProperty conditionProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptViewDef()
		{
			nameProp = new APXmlProperty("name", typeof(string), "");
			refersProp = new APXmlProperty("refers", typeof(APRptReferDefCollection));
			ordersProp = new APXmlProperty("orders", typeof(APRptOrderDefCollection));
			conditionProp = new APXmlProperty("condition", typeof(APRptConditionDef));

			properties = new APXmlPropertyCollection();
			properties.Add(nameProp);
			properties.Add(refersProp);
			properties.Add(ordersProp);
			properties.Add(conditionProp);
		}

		
		/// <summary>
		/// Create a new APRptViewDef.
		/// </summary>
		public APRptViewDef()
		{
		}


		/// <summary>
		/// Create a new APRptViewDef, load from xml.
		/// </summary>
		/// <param name="xml">The source xml.</param>
		public APRptViewDef(string xml)
		{
			ReadXml(xml);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets name of APRptViewDef.
		/// </summary>
		[APXmlProperty("name")]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// All refers of the view.
		/// </summary>
		[APXmlProperty("refers")]
		public APRptReferDefCollection Refers
		{
			get { return (APRptReferDefCollection)base[refersProp]; }
		}


		/// <summary>
		/// All orders of the view.
		/// </summary>
		[APXmlProperty("orders")]
		public APRptOrderDefCollection Orders
		{
			get { return (APRptOrderDefCollection)base[ordersProp]; }
		}


		/// <summary>
		/// Condition of the view.
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
			get { return "viewDef"; }
		}


		#endregion

	}

}
