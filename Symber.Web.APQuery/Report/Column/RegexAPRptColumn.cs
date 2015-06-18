using Symber.Web.Data;

namespace Symber.Web.Report
{

	/// <summary>
	/// Regex APRptColumn.
	/// </summary>
	public class RegexAPRptColumn : TextAPRptColumn
	{

		// public static readonly string RegexEmail
		//	= @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		//	+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		//	+ @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{2,4})$";

		/// <summary>
		/// Regex of e-mail
		/// </summary>
		public static readonly string RegexEmail
			= @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";


		//public static readonly string RegexUrl
		//	// Look for ftp, http, https
		//	= @"^(((ht|f)tp(s?)\:\/\/|~/|/)?"
		//	// Userinfo (optional)
		//	+ @"(([\w\.\-\+%!$&'\(\)*\+,;=]+:)*[\w\.\-\+%!$&'\(\)*\+,;=]+)?"
		//	//			+ @"(([\w\.\-\+%!$&'\(\)*\+,;=]+:)*[\w\.\-\+%!$&'\(\)*\+,;=]+@)?"
		//	// IP address directly
		//	+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		//	// or the domain
		//	+ @"([a-zA-Z]{1}([\w\-]+\.)+([\w]{2,5})))"
		//	// Server port number (optional)
		//	+ @"(:[\d]{1,5})?"
		//	// The path (optional)
		//	+ @"([\/|\?][-\w#!:\.\?\+=&%@!$'~*,;\/\(\)\[\]\-]*)?)$";

		/// <summary>
		/// Regex of URL
		/// </summary>
		public static readonly string RegexUrl
			= @"^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$";


		#region [ Fields ]


		private string _pattern;
		private string _message;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new RegexAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="pattern">Pattern of Regex.</param>
		/// <param name="message">Invalidated message.</param>
		public RegexAPRptColumn(StringAPColumnDef columnDef, string pattern, string message)
			: base(columnDef)
		{
			_pattern = pattern;
			_message = message;
		}


		/// <summary>
		/// Create a new RegexAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="pattern">Pattern of Regex.</param>
		/// <param name="message">Invalidated message.</param>
		public RegexAPRptColumn(StringAPColumnDef columnDef, string id, string title, string pattern, string message)
			: base(columnDef, id, title)
		{
			_pattern = pattern;
			_message = message;
		}
					

		/// <summary>
		/// Create a new RegexAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dataLength">Data length.</param>
		/// <param name="pattern">Pattern of Regex.</param>
		/// <param name="message">Invalidated message.</param>
		public RegexAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int dataLength, string pattern, string message)
			: base(selectExpr, id, title, dataLength)
		{
			_pattern = pattern;
			_message = message;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Invalidated message.
		/// </summary>
		public virtual string Message
		{
			get { return _message; }
			set { _message = value; }
		}


		#endregion

	}

}
