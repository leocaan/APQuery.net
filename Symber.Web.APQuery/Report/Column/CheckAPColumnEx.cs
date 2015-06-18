using Symber.Web.Data;
using System;

namespace Symber.Web.Report
{

	/// <summary>
	/// Check APRptColumn.
	/// </summary>
	public class CheckAPRptColumn : APRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new CheckAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		public CheckAPRptColumn(BooleanAPColumnDef columnDef)
			: base(columnDef)
		{
		}


		/// <summary>
		/// Create a new CheckAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public CheckAPRptColumn(BooleanAPColumnDef columnDef, string id, string title)
			: base(columnDef, id, title)
		{
		}


		/// <summary>
		/// Create a new CheckAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public CheckAPRptColumn(APSqlOperateExpr selectExpr, string id, string title)
			: base(selectExpr, id, title)
		{
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public override APRptFilterType FilterType
		{
			get { return APRptFilterType.Boolean; }
		}


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public override string RenderFormatter
		{
			get { return base.RenderFormatter ?? "Boolean"; }
			set { base.RenderFormatter = value; }
		}


		/// <summary>
		/// Parse query where phrase.
		/// </summary>
		/// <param name="comparator">Comparator.</param>
		/// <param name="values">Values.</param>
		/// <returns>An APSqlWherePhrase.</returns>
		/// <exception cref="APRptFilterParseException">Throw exception on parse error.</exception>
		public override APSqlWherePhrase ParseQueryWherePhrase(APRptFilterComparator comparator, params string[] values)
		{
			if (values.Length == 0)
				throw APRptFilterParseException.ValuesCountCannotBeZero();

			if (values.Length > 1)
				throw APRptFilterParseException.UnsupportMultiValues(comparator);


			if (comparator == APRptFilterComparator.Equals)
				return GetQueryWherePhrase(APSqlConditionOperator.Equals, values[0]);

			else if (comparator == APRptFilterComparator.NotEqual)
				return GetQueryWherePhrase(APSqlConditionOperator.NotEqual, values[0]);

			throw APRptFilterParseException.UnsupportFilterComparator(GetType(), comparator);
		}


		/// <summary>
		/// Parse 'WHERE' phrase value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Parsed value.</returns>
		protected override object TryGetFilterValue(string value)
		{
			bool retVal;
			if (!Boolean.TryParse(value, out retVal))
				throw APRptFilterParseException.InvalidValue(value, typeof(bool), null);

			return retVal;
		}


		#endregion

	}

}
