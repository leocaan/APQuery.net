using Symber.Web.Data;
using System;

namespace Symber.Web.Report
{

	/// <summary>
	/// Currency APRptColumn.
	/// </summary>
	public class CurrencyAPRptColumn : DecimalAPRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new CurrencyAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public CurrencyAPRptColumn(DecimalAPColumnDef columnDef, Decimal minValue = Decimal.MinValue, Decimal maxValue = Decimal.MaxValue)
			: base(columnDef, minValue, maxValue)
		{
		}


		/// <summary>
		/// Create a new CurrencyAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public CurrencyAPRptColumn(DecimalAPColumnDef columnDef, string id, string title, Decimal minValue = Decimal.MinValue, Decimal maxValue = Decimal.MaxValue)
			: base(columnDef, id, title, minValue, maxValue)
		{
		}


		/// <summary>
		/// Create a new CurrencyAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="scale">Scale.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public CurrencyAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int scale = 2, Decimal minValue = Decimal.MinValue, Decimal maxValue = Decimal.MaxValue)
			: base(selectExpr, id, title, scale, minValue, maxValue)
		{
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public override string RenderFormatter
		{
			get { return base.RenderFormatter ?? "Currency"; }
			set { base.RenderFormatter = value; }
		}


		#endregion

	}

}
