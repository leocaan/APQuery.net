using Symber.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Symber.Web.Report
{

	/// <summary>
	/// Build a Rpt Report to run.
	/// </summary>
	public class APRptReportBuilder
	{

		#region [ Const Fields ]


		private const string COUNT_ALIAS = "RPTCOUNT";


		#endregion


		#region [ Static ]


		/// <summary>
		/// Gets the index of week of year.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns>The index of week of year.</returns>
		public static int GetWeekOfYear(DateTime date)
		{
			CultureInfo culture = CultureInfo.CurrentCulture;
			DateTimeFormatInfo info = culture.DateTimeFormat;
			return culture.Calendar.GetWeekOfYear(date, info.CalendarWeekRule, info.FirstDayOfWeek);
		}


		#endregion


		#region [ Fields ]


		private APRptSource _source;
		private APRptReportDef _def;
		private Func<DateTime, APSqlDateGroupMode, string> _dateGroupText;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptViewBuilder.
		/// </summary>
		/// <param name="source">The view source.</param>
		/// <param name="def">The report defined.</param>
		public APRptReportBuilder(APRptSource source, APRptReportDef def)
		{
			_source = source;
			_def = def;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Get the delegate of date grouping text.
		/// </summary>
		public Func<DateTime, APSqlDateGroupMode, string> DateGroupText
		{
			get { return _dateGroupText; }
			set { _dateGroupText = value; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Build group query command.
		/// </summary>
		/// <returns>The command.</returns>
		public virtual APSqlSelectCommand BuildGroupQuery()
		{
			List<APSqlSelectPhrase> select = _source.GetGroupSelectPhrases();
			List<APSqlFromPhrase> from = _source.GetPrimeFormPhrases();
			List<APSqlWherePhrase> where = _source.GetPrimeWherePhrases();
			List<APSqlExprPhrase> group = _source.GetGroupPhrases();
			List<APSqlOrderPhrase> order = new List<APSqlOrderPhrase>();


			foreach (APRptGroupDef def in _def.Groups)
			{
				APRptColumn column = _source.AllColumns[def.ColumnId];

				// DateTime must grouping by datetime function.
				if (column.FilterType == APRptFilterType.DateTime)
				{
					APSqlExpr expr = new APSqlDateGroupExpr(column.SelectExpr, def.DateGroupMode);
					select.Add(expr);
					group.Add(expr);
				}
				else
				{
					column.AddToQuerySelectPhrases(select);
					column.AddToQueryGroupPhrases(group);
				}
				column.AddToQueryFromPhrases(from);
				column.AddToQueryWherePhrases(where);
				order.Add(column.GetQueryOrderByPhrase(def.According));
			}
			select.Add(APSqlAsteriskExpr.Asterisk.Count().As(COUNT_ALIAS));



			// Build query
			var query = APQuery
				.select(select)
				.from(from)
				.where(where);

			if (group.Count > 0)
			{
				query
					.group_by(group)
					.order_by(order);
			}


			return query;
		}


		/// <summary>
		/// Read report group summary info.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The group summary.</returns>
		public virtual APRptJsonGroupSummary ReadGroupSummary(IDataReader reader)
		{
			// Group root, none text
			APRptJsonGroupSummary rootSummary = new APRptJsonGroupSummary("");

			while (reader.Read())
			{
				APRptJsonGroupSummary summary = rootSummary;
				int count = (int)reader[COUNT_ALIAS];
				summary.count += count;

				foreach (APRptGroupDef groupDef in _def.Groups)
				{
					APRptColumn column = _source.AllColumns[groupDef.ColumnId];

					object value = column.Json(reader);
					object key;
					string text;

					if (value == null || value == DBNull.Value)
					{
						key = text = "";
					}
					else if (column is DateTimeAPRptColumn)
					{
						key = text = GetDateGroupText((DateTime)value, groupDef.DateGroupMode);
					}
					else if (column is LookupAPRptColumn)
					{
						key = reader[column.DataName];
						text = Convert.ToString((column as LookupAPRptColumn).RelationShowColumn.Json(reader));
					}
					else
					{
						key = value;
						text = Convert.ToString(value);
					}


					if (summary.subgroups != null && summary.subgroups.ContainsKey(key))
					{
						summary = summary.subgroups[key];
					}
					else
					{
						APRptJsonGroupSummary subSummary = new APRptJsonGroupSummary(text) { value = value };
						if (summary.subgroups == null)
							summary.subgroups = new Dictionary<object, APRptJsonGroupSummary>();
						summary.subgroups[key] = subSummary;
						summary = subSummary;
					}

					summary.count += count;
				}
			}

			return rootSummary;
		}


		/// <summary>
		/// Build maxtrix query command.
		/// </summary>
		/// <returns>The command.</returns>
		public virtual APSqlSelectCommand BuildMatrixQuery()
		{
			List<APSqlSelectPhrase> select = _source.GetPrimeSelectPhrases();
			List<APSqlFromPhrase> from = _source.GetPrimeFormPhrases();
			List<APSqlWherePhrase> where = _source.GetPrimeWherePhrases();
			List<APSqlOrderPhrase> order = new List<APSqlOrderPhrase>();


			foreach (APRptGroupDef def in _def.Groups)
			{
				APRptColumn column = _source.AllColumns[def.ColumnId];

				column.AddToQueryFromPhrases(from);
				column.AddToQueryWherePhrases(where);
				order.Add(column.GetQueryOrderByPhrase(def.According));
			}

			foreach (APRptReferDef def in _def.Refers)
			{
				APRptColumn column = _source.AllColumns[def.ColumnId];

				column.AddToQuerySelectPhrases(select);
				column.AddToQueryFromPhrases(from);
				column.AddToQueryWherePhrases(where);
			}

			foreach (APRptOrderDef def in _def.Orders)
			{
				APRptColumn column = _source.AllColumns[def.ColumnId];

				order.Add(column.GetQueryOrderByPhrase(def.According));
			}



			// Build query
			var query = APQuery
				.select(select)
				.from(from)
				.where(where);


			return query;
		}


		/// <summary>
		/// Read data list from data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The data list.</returns>
		public virtual IEnumerable<IList<object>> ReadMatrixList(IDataReader reader)
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
		public virtual IEnumerable<IDictionary<string, object>> ReadMatrixDictionay(IDataReader reader)
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


		private string GetDateGroupText(DateTime value, APSqlDateGroupMode mode)
		{
			if (_dateGroupText != null)
				return _dateGroupText(value, mode);


			switch (mode)
			{
				case APSqlDateGroupMode.Year:
					return String.Format(CultureInfo.CurrentCulture, "{0:yyyy}", value);
				case APSqlDateGroupMode.Month:
					return String.Format(CultureInfo.CurrentCulture, "{0:yyyy-MM}", value);
				case APSqlDateGroupMode.Day:
					return String.Format(CultureInfo.CurrentCulture, "{0:yyyy-MM-dd}", value);
				case APSqlDateGroupMode.Week:
					return String.Format(CultureInfo.CurrentCulture, "week {1}, {0:yyyy}", value, GetWeekOfYear(value));
				case APSqlDateGroupMode.Quarter:
					return String.Format(CultureInfo.CurrentCulture, "quarter {1}, {0:yyyy}", value, (value.Month+2) % 3);
				default:
					return "";
			}
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
