using Symber.Web.Data;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Email APRptColumn.
	/// </summary>
	public class EmailAPRptColumn : RegexAPRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new EmailAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		public EmailAPRptColumn(StringAPColumnDef columnDef)
			: base(columnDef, RegexAPRptColumn.RegexEmail, "")
		{
		}


		/// <summary>
		/// Create a new EmailAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public EmailAPRptColumn(StringAPColumnDef columnDef, string id, string title)
			: base(columnDef, id, title, RegexAPRptColumn.RegexEmail, "")
		{
		}
					

		/// <summary>
		/// Create a new EmailAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dataLength">Data Length</param>
		public EmailAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int dataLength = 320)
			: base(selectExpr, id, title, dataLength, RegexAPRptColumn.RegexEmail, "")
		{
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public override string RenderFormatter
		{
			get { return base.RenderFormatter ?? "Email"; }
			set { base.RenderFormatter = value; }
		}


		/// <summary>
		/// Gets client validate attributes.
		/// </summary>
		/// <returns>Attributes dictionary.</returns>
		public override IDictionary<string, string> GetValidateAttributes()
		{
			var attrs = base.GetValidateAttributes();
			attrs.Add("email", null);

			return attrs;
		}


		/// <summary>
		/// Invalidated message.
		/// </summary>
		public override string Message
		{
			get
			{
				return APResource.GetString(APResource.APProject_Regex_Invalidate_Email);
			}
		}


		#endregion

	}

}
