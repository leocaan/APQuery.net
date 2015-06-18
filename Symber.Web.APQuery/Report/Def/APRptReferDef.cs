using Symber.Web.Xml;

namespace Symber.Web.Report
{

	/// <summary>
	/// ID refer of APRptColumn.
	/// </summary>
	public class APRptReferDef : APXmlElement
	{

		#region [ Static Fields ]


		private static APXmlProperty columnIdProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptReferDef()
		{
			columnIdProp = new APXmlProperty("columnId", typeof(string), "", APXmlPropertyOptions.IsRequired | APXmlPropertyOptions.IsKey);

			properties = new APXmlPropertyCollection();
			properties.Add(columnIdProp);
		}


		/// <summary>
		/// Create a new APRptReferDef. 
		/// </summary>
		public APRptReferDef()
		{
		}


		/// <summary>
		/// Create a new APRptReferDef. 
		/// </summary>
		/// <param name="columnId">The APRptColumn ID.</param>
		public APRptReferDef(string columnId)
		{
			ColumnId = columnId;
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
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APXmlPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion

	}

}
