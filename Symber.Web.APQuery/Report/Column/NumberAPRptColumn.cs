using Symber.Web.Data;
using System;

namespace Symber.Web.Report
{

	/// <summary>
	/// Number APRptColumn.
	/// </summary>
	public abstract class NumberAPRptColumn : APRptColumn
	{

		#region [ Fields ]


		private string _format;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new NumberAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		public NumberAPRptColumn(APColumnDef columnDef)
			: base(columnDef)
		{
		}


		/// <summary>
		/// Create a new NumberAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public NumberAPRptColumn(APColumnDef columnDef, string id, string title)
			: base(columnDef, id, title)
		{
		}
				

		/// <summary>
		/// Create a new NumberAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public NumberAPRptColumn(APSqlOperateExpr selectExpr, string id, string title)
			: base(selectExpr, id, title)
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets Currency format string.
		/// </summary>
		public virtual string Format
		{
			get { return _format; }
			set { _format = value; }
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public override APRptFilterType FilterType
		{
			get { return APRptFilterType.Number; }
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

			APSqlOperateExpr expr = SelectExpr;

			if (comparator == APRptFilterComparator.Equals)
			{
				if (values.Length == 1)
					return GetQueryWherePhrase(APSqlConditionOperator.Equals, values[0]);
				
				// Change operator to 'IN'
				return new APSqlConditionPhrase(expr, APSqlConditionOperator.In, GetFilterValues(values));
			}

			else if (comparator == APRptFilterComparator.NotEqual)
			{
				if (values.Length == 1)
					return GetQueryWherePhrase(APSqlConditionOperator.NotEqual, values[0]);

				// Change operator to 'NOT IN'
				return new APSqlConditionPhrase(expr, APSqlConditionOperator.NotIn, GetFilterValues(values));
			}

			else if (comparator == APRptFilterComparator.LessThan
				|| comparator == APRptFilterComparator.LessOrEqual
				|| comparator == APRptFilterComparator.GreaterThan
				|| comparator == APRptFilterComparator.GreaterOrEqual)
			{
				if (values.Length > 1)
					throw APRptFilterParseException.UnsupportMultiValues(comparator);

				return GetQueryWherePhrase((APSqlConditionOperator)comparator, values[0]);
			}

			else if (comparator == APRptFilterComparator.Between)
			{
				if (values.Length != 2)
					throw APRptFilterParseException.BetweenMustHaveTwoValues();

				object v1 = TryGetFilterValue(values[0]);
				object v2 = TryGetFilterValue(values[1]);
			
				if (v1 == DBNull.Value || v2 == DBNull.Value)
					throw APRptFilterParseException.UnsupportDBNull();

				return new APSqlConditionPhrase(expr, APSqlConditionOperator.Between, new object[2] { v1, v2 });
			}

			throw APRptFilterParseException.UnsupportFilterComparator(GetType(), comparator);
		}


		#endregion

	}

}
