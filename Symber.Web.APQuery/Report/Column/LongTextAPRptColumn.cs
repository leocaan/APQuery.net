using Symber.Web.Data;

namespace Symber.Web.Report
{

	/// <summary>
	/// LongText APRptColumn.
	/// </summary>
	public class LongTextAPRptColumn : MultiLineTextAPRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new LongTextAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		public LongTextAPRptColumn(StringAPColumnDef columnDef)
			: base(columnDef)
		{
		}


		/// <summary>
		/// Create a new LongTextAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public LongTextAPRptColumn(StringAPColumnDef columnDef, string id, string title)
			: base(columnDef, id, title)
		{
		}
					

		/// <summary>
		/// Create a new LongTextAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dataLength">Data length.</param>
		public LongTextAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int dataLength)
			: base(selectExpr, id, title, dataLength)
		{
		}


		#endregion

	}

}
