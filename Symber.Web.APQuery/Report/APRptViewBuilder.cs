using Symber.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Symber.Web.Report
{

	/// <summary>
	/// Build a Rpt View to run.
	/// </summary>
	public class APRptViewBuilder
	{

		#region [ Fields ]


		private APRptSource _source;
		private APRptViewDef _def;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptViewBuilder.
		/// </summary>
		/// <param name="source">The view source.</param>
		/// <param name="def">The view defined.</param>
		public APRptViewBuilder(APRptSource source, APRptViewDef def)
		{
			_source = source;
			_def = def;
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Build a APQuery.
		/// </summary>
		/// <param name="additionCondition">Addition condition.</param>
		/// <param name="additionOrders">Addition orders</param>
		/// <param name="fuzzySearchString">Fuzzy search string.</param>
		/// <returns>The APQuery.</returns>
		public virtual APSqlSelectCommand BuildQuery(APSqlWherePhrase additionCondition = null,
													 IEnumerable<APSqlOrderPhrase> additionOrders = null,
													 string fuzzySearchString = null)
		{
			List<APSqlSelectPhrase> select = _source.GetPrimeSelectPhrases();
			List<APSqlFromPhrase> from = _source.GetPrimeFormPhrases();
			List<APSqlWherePhrase> where = _source.GetPrimeWherePhrases();

			// Base build.

			foreach (APRptReferDef refer in _def.Refers)
			{
				APRptColumn column = _source.AllColumns[refer.ColumnId];

				column.AddToQuerySelectPhrases(select);
				column.AddToQueryFromPhrases(from);
				column.AddToQueryWherePhrases(where);
			}

			var query = APQuery
				.select(select)
				.from(from)
				.primary(_source.GetPrimaryExpr());



			// Order build.

			List<APSqlOrderPhrase> orderby = new List<APSqlOrderPhrase>();
			if (_def.Orders.Count > 0)
			{
				foreach (APRptOrderDef order in _def.Orders)
				{
					orderby.Add(_source.AllColumns[order.ColumnId].GetQueryOrderByPhrase(order.According));
				}
			}
			if (additionOrders != null)
			{
				foreach (APSqlOrderPhrase order in additionOrders)
				{
					orderby.Add(order);
				}
			}
			query.order_by(orderby);



			// Filter build.
			if (_def.Condition.Filters.Count > 0)
				where.Add(new APRptConditionBuilder(_def.Condition, _source.AllColumns).BuildCondition());


			// Additions condition.
			if (additionCondition != null)
				where.Add(additionCondition);

			// Fuzzy search condition.
			APSqlWherePhrase fuzzyWhere;
			if (!String.IsNullOrEmpty(fuzzySearchString) && (fuzzyWhere = _source.GetFuzzySearchPhrase(fuzzySearchString)) != null)
				where.Add(fuzzyWhere);


			query.where(where);


			return query;
		}


		/// <summary>
		/// Read data list from data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The data list.</returns>
		public virtual IEnumerable<IList<object>> ReadList(IDataReader reader)
		{
			var avaliable = GetAvaliableColumns();

			while (reader.Read())
			{
				List<object> row = new List<object>();
				foreach (APRptColumn column in avaliable)
				{
					row.Add(column.Json(reader));
				}

				yield return row;
			}
		}


		/// <summary>
		/// Read data dictionay from data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The data dictionay.</returns>
		public virtual IEnumerable<IDictionary<string, object>> ReadDictionay(IDataReader reader)
		{
			var avaliable = GetAvaliableColumns();

			while (reader.Read())
			{
				Dictionary<string, object> row = new Dictionary<string, object>();
				foreach (APRptColumn column in avaliable)
				{
					row.Add(column.ID, column.Json(reader));
				}

				yield return row;
			}
		}


		/// <summary>
		/// Get the title list of the columns. 
		/// </summary>
		/// <returns>The title list.</returns>
		public virtual List<APRptJsonColumnTitle> GetColumnTitles()
		{
			var avaliable = GetAvaliableColumns();
			List<APRptJsonColumnTitle> list = new List<APRptJsonColumnTitle>();

			foreach (APRptColumn column in avaliable)
			{
				list.Add(new APRptJsonColumnTitle(column.ID, column.Title, column.RenderFormatter));
			}

			return list;
		}


		private APRptColumnCollection GetAvaliableColumns()
		{
			APRptColumnCollection avaliable = new APRptColumnCollection();

			foreach (APRptReferDef def in _def.Refers)
			{
				avaliable.Add(_source.AllColumns[def.ColumnId]);
			}

			return avaliable;
		}


		#endregion

	}

}
