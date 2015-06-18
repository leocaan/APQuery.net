
namespace Symber.Web.Report
{

	/// <summary>
	/// The filter field of the report.
	/// </summary>
	public class APRptJsonFilterField
	{

		#region [ Fileds ]


		private string _id;
		private string _text;
		private string _type;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create new APRptJsonFilterField.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="text">The text.</param>
		/// <param name="type">The type.</param>
		public APRptJsonFilterField(string id, string text, string type)
		{
			_id = id;
			_text = text;
			_type = type;
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
		/// Gets or sets the type.
		/// </summary>
		public string type
		{
			get { return _type; }
			set { _type = value; }
		}


		#endregion

	}

}
