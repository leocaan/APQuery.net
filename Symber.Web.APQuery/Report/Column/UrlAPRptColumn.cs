using Symber.Web.Data;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// URL APRptColumn.
	/// </summary>
	public class UrlAPRptColumn : RegexAPRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new UrlAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		public UrlAPRptColumn(StringAPColumnDef columnDef)
			: base(columnDef, RegexAPRptColumn.RegexUrl, "")
		{
		}


		/// <summary>
		/// Create a new UrlAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public UrlAPRptColumn(StringAPColumnDef columnDef, string id, string title)
			: base(columnDef, id, title, RegexAPRptColumn.RegexUrl, "")
		{
		}
							

		/// <summary>
		/// Create a new UrlAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dataLength">Data Length</param>
		public UrlAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int dataLength = 2048)
			: base(selectExpr, id, title, dataLength, RegexAPRptColumn.RegexUrl, "")
		{
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public override string RenderFormatter
		{
			get { return base.RenderFormatter ?? "Url"; }
			set { base.RenderFormatter = value; }
		}


		/// <summary>
		/// Gets client validate attributes.
		/// </summary>
		/// <returns>Attributes dictionary.</returns>
		public override IDictionary<string, string> GetValidateAttributes()
		{
			var attrs = base.GetValidateAttributes();
			attrs.Add("url", null);

			return attrs;
		}


		/// <summary>
		/// Invalidated message.
		/// </summary>
		public override string Message
		{
			get
			{
				return APResource.GetString(APResource.APProject_Regex_Invalidate_Url);
			}
		}


		#endregion

	}

}
