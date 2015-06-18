using Symber.Web.Data;

namespace Symber.Web.Report
{

	/// <summary>
	/// MultiLine Text APRptColumn.
	/// </summary>
	public class MultiLineTextAPRptColumn : TextAPRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new MultiLineTextAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		public MultiLineTextAPRptColumn(StringAPColumnDef columnDef)
			: base(columnDef)
		{
		}


		/// <summary>
		/// Create a new MultiLineTextAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public MultiLineTextAPRptColumn(StringAPColumnDef columnDef, string id, string title)
			: base(columnDef, id, title)
		{
		}
					

		/// <summary>
		/// Create a new MultiLineTextAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dataLength">Data length.</param>
		public MultiLineTextAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int dataLength)
			: base(selectExpr, id, title, dataLength)
		{
		}


		#endregion

	}

}
