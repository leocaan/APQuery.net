using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// The group summary of the report.
	/// </summary>
	public class APRptJsonGroupSummary
	{

		#region [ Fileds ]


		private string _text;
		private object _value;
		private int _count;
		private Dictionary<object, APRptJsonGroupSummary> _subgroups;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create new APRptJsonGroupSummary.
		/// </summary>
		/// <param name="text">The text.</param>
		public APRptJsonGroupSummary(string text)
		{
			_text = text;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string text
		{
			get { return _text; }
			set { _text = value; }
		}


		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		public int count
		{
			get { return _count; }
			set { _count = value; }
		}


		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public object value
		{
			get { return _value; }
			set { _value = value; }
		}


		/// <summary>
		/// Gets or sets sub groups.
		/// </summary>
		public Dictionary<object, APRptJsonGroupSummary> subgroups
		{
			get { return _subgroups; }
			set { _subgroups = value; }
		}


		#endregion

	}

}
