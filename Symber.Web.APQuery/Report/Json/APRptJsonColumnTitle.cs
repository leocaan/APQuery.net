
namespace Symber.Web.Report
{

	/// <summary>
	/// The column title of report.
	/// </summary>
	public class APRptJsonColumnTitle
	{

		#region [ Fileds ]


		private string _id;
		private string _text;
		private string _formatter;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create new APRptJsonColumnTitle.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="text">The text.</param>
		/// <param name="formatter">The formatter.</param>
		public APRptJsonColumnTitle(string id, string text, string formatter)
		{
			_id = id;
			_text = text;
			_formatter = formatter;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public string id
		{
			get { return _id; }
			set { _id = value; }
		}


		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string text
		{
			get { return _text; }
			set { _text = value; }
		}


		/// <summary>
		/// Gets or sets the formatter.
		/// </summary>
		public string formatter
		{
			get { return _formatter; }
			set { _formatter = value; }
		}


		#endregion

	}

}
