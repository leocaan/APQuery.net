using Symber.Web.Xml;
using System;

namespace Symber.Web.Report
{

	/// <summary>
	/// Condition of APRptColumns.
	/// </summary>
	public class APRptConditionDef : APXmlRoot
	{

		#region [ Static Fields ]


		private static APXmlProperty logicProp;
		private static APXmlProperty rpnProp;
		private static APXmlProperty filtersProp;
		private static APXmlPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APRptConditionDef()
		{
			logicProp = new APXmlProperty("logic", typeof(string), "");
			rpnProp = new APXmlProperty("rpn", typeof(string), "");
			filtersProp = new APXmlProperty("", typeof(APRptFilterDefCollection), null, APXmlPropertyOptions.IsDefaultCollection);

			properties = new APXmlPropertyCollection();
			properties.Add(logicProp);
			properties.Add(rpnProp);
			properties.Add(filtersProp);
		}


		/// <summary>
		/// Create a new APRptConditionDef.
		/// </summary>
		public APRptConditionDef()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Low logic Expression of filters. Default is Empty, means 'AND' logic of every filter.
		/// </summary>
		[APXmlProperty("logic", DefaultValue = "")]
		public string Logic
		{
			get { return (string)base[logicProp]; }
			set { base[logicProp] = value; }
		}


		/// <summary>
		/// RPN logic Expression of filters. You need parse from Logic.
		/// </summary>
		[APXmlProperty("rpn", DefaultValue = "")]
		public string Rpn
		{
			get { return (string)base[rpnProp]; }
			set { base[rpnProp] = value; }
		}


		/// <summary>
		/// All filters in the view.
		/// </summary>
		[APXmlProperty("", IsDefaultCollection = true)]
		public APRptFilterDefCollection Filters
		{
			get { return (APRptFilterDefCollection)base[filtersProp]; }
			set { base[filtersProp] = value; }
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APXmlPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Parse Logic condition expression  to RPN expression.
		/// </summary>
		/// <param name="checkIdentifier">The delegate that check validate identifier.</param>
		public void ParseRpn(Func<object, bool> checkIdentifier)
		{
			Rpn = APRptConditionBuilder.ParseLogicRpn(Logic, checkIdentifier);
		}


		#endregion


		#region [ Override Implementation of APXmlRoot ]


		/// <summary>
		/// Gets the local name of xml root.
		/// </summary>
		protected override string LocalName
		{
			get { return "conditionDef"; }
		}


		#endregion

	}

}
