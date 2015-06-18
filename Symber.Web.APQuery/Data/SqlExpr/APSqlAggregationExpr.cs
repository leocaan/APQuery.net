using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL aggregation Expression.
	/// </summary>
	public class APSqlAggregationExpr : APSqlOperateExpr
	{

		#region [ Fields ]


		private APSqlExpr _rowSelectExpr;
		private APSqlAggregationType _aggregationType;
		private APSqlSelectMode _selectMode;


		#endregion
		

		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlAggregationExpr.
		/// </summary>
		/// <param name="rowSelectExpr">Row SQL 'SELECT' Expression.</param>
		/// <param name="aggregationType">Aggregation type.</param>
		/// <param name="selectMode">Select mode.</param>
		public APSqlAggregationExpr(APSqlExpr rowSelectExpr, APSqlAggregationType aggregationType, APSqlSelectMode selectMode)
		{
			_rowSelectExpr = rowSelectExpr;
			_aggregationType = aggregationType;
			_selectMode = selectMode;
		}


		/// <summary>
		/// Create a new APSqlAggregationExpr.
		/// </summary>
		/// <param name="rowSelectExpr">Row SQL 'SELECT' Expression.</param>
		/// <param name="aggregationType">Aggregation type.</param>
		public APSqlAggregationExpr(APSqlExpr rowSelectExpr, APSqlAggregationType aggregationType)
			: this(rowSelectExpr, aggregationType, APSqlSelectMode.ALL)
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Row APSqlExpr.
		/// </summary>
		public APSqlExpr RowSelectExpr
		{
			get { return _rowSelectExpr; }
		}


		/// <summary>
		/// Aggregation type.
		/// </summary>
		public APSqlAggregationType AggregationType
		{
			get { return _aggregationType; }
		}


		/// <summary>
		/// Select mode, all or distinct.
		/// </summary>
		public APSqlSelectMode SelectMode
		{
			get { return _selectMode; }
		}


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get
			{
				// support in APQueryParser.

				throw new NotSupportedException();
			}
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return _rowSelectExpr.MaybeTableDef; }
		}


		/// <summary>
		/// Command parameter name.
		/// </summary>
		public override string ParamName
		{
			get
			{
				return RowSelectExpr is APSqlOperateExpr
					? (RowSelectExpr as APSqlOperateExpr).ParamName + AggregationType.ToString()
					: AggregationType.ToString();
			}
		}


		#endregion

	}

}
