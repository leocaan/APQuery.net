using Symber.Web.Data;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Symber.Web.Report
{

	/// <summary>
	/// DateTime APRptColumn.
	/// </summary>
	public class DateTimeAPRptColumn : APRptColumn
	{

		#region [ Inner Class DateRange ]


		/// <summary>
		/// Date range.
		/// </summary>
		public class DateRange
		{

			private DateTime _start;
			private DateTime _end;


			/// <summary>
			/// Create a new DateRange.
			/// </summary>
			public DateRange()
			{
			}


			/// <summary>
			/// Create a new DateRange.
			/// </summary>
			/// <param name="start">Start datetime</param>
			/// <param name="end">End datetime</param>
			public DateRange(DateTime start, DateTime end)
			{
				_start = start;
				_end = end;
			}


			/// <summary>
			/// Gets or sets start datetime.
			/// </summary>
			public DateTime Start
			{
				get { return _start; }
				set { _start = value; }
			}


			/// <summary>
			/// Gets or sets end datetime.
			/// </summary>
			public DateTime End
			{
				get { return _end; }
				set { _end = value; }
			}

		}


		#endregion


		#region [ Fields ]


		private APRptDateTimeType _dateTimeType;
		private DateTime _minValue;
		private DateTime _maxValue;
		private bool _iso;
		private string _format;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new DateTimeAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="dateTimeType">DateTime type.</param>
		public DateTimeAPRptColumn(DateTimeAPColumnDef columnDef, APRptDateTimeType dateTimeType)
			:this(columnDef, dateTimeType, DateTime.MinValue, DateTime.MaxValue)
		{
		}


		/// <summary>
		/// Create a new DateTimeAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="dateTimeType">DateTime type.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DateTimeAPRptColumn(DateTimeAPColumnDef columnDef, APRptDateTimeType dateTimeType, DateTime minValue, DateTime maxValue)
			: base(columnDef)
		{
			_dateTimeType = dateTimeType;
			_minValue = minValue;
			_maxValue = maxValue;
		}


		/// <summary>
		/// Create a new DateTimeAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dateTimeType">DateTime type.</param>
		public DateTimeAPRptColumn(DateTimeAPColumnDef columnDef, string id, string title, APRptDateTimeType dateTimeType)
			: this(columnDef, id, title, dateTimeType, DateTime.MinValue, DateTime.MaxValue)
		{
			_dateTimeType = dateTimeType;
		}


		/// <summary>
		/// Create a new DateTimeAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dateTimeType">DateTime type.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DateTimeAPRptColumn(DateTimeAPColumnDef columnDef, string id, string title, APRptDateTimeType dateTimeType, DateTime minValue, DateTime maxValue)
			: base(columnDef, id, title)
		{
			_dateTimeType = dateTimeType;
			_minValue = minValue;
			_maxValue = maxValue;
		}


		/// <summary>
		/// Create a new DateTimeAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dateTimeType">DateTime type.</param>
		public DateTimeAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, APRptDateTimeType dateTimeType)
			: this(selectExpr, id, title, dateTimeType, DateTime.MinValue, DateTime.MaxValue)
		{
			_dateTimeType = dateTimeType;
		}


		/// <summary>
		/// Create a new DateTimeAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dateTimeType">DateTime type.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DateTimeAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, APRptDateTimeType dateTimeType, DateTime minValue, DateTime maxValue)
			: base(selectExpr, id, title)
		{
			_dateTimeType = dateTimeType;
			_minValue = minValue;
			_maxValue = maxValue;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the column datetime type
		/// </summary>
		public virtual APRptDateTimeType DateTimeType
		{
			get { return _dateTimeType; }
		}


		/// <summary>
		/// Gets or sets the min value.
		/// </summary>
		public DateTime MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}


		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		public DateTime MaxValue
		{
			get { return _maxValue; }
			set { _maxValue = value; }
		}


		/// <summary>
		/// Gets or sets the value is ISO.
		/// </summary>
		public bool ISO
		{
			get { return _iso; }
			set { _iso = value; }
		}


		/// <summary>
		/// Gets or sets DateTime format string.
		/// </summary>
		public string Format
		{
			get { return _format; }
			set { _format = value; }
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Parse string to date range.
		/// </summary>
		/// <param name="value">The string</param>
		/// <returns>DateRange</returns>
		protected virtual DateRange ParseDateRange(string value)
		{
			DateRange range = new DateRange();
			DateTime today = DateTime.Today;

			if (value == "YESTERDAY")
			{
				range.Start = today.AddDays(-1);
				range.End = range.Start.AddDays(1);
				return range;
			}
			else if (value == "TODAY")
			{
				range.Start = today;
				range.End = range.Start.AddDays(1);
				return range;
			}
			else if (value == "TOMORROW")
			{
				range.Start = today.AddDays(1);
				range.End = range.Start.AddDays(1);
				return range;
			}
			else if (value == "LAST WEEK")
			{
				range.Start = today.AddDays(-(int)today.DayOfWeek - 7);
				range.End = range.Start.AddDays(8);
				return range;
			}
			else if (value == "THIS WEEK")
			{
				range.Start = today.AddDays(-(int)today.DayOfWeek);
				range.End = range.Start.AddDays(8);
				return range;
			}
			else if (value == "NEXT WEEK")
			{
				range.Start = today.AddDays(-(int)today.DayOfWeek + 7);
				range.End = range.Start.AddDays(8);
				return range;
			}
			else if (value == "LAST MONTH")
			{
				range.Start = today.AddDays(-(today.Day - 1)).AddMonths(-1);
				range.End = range.Start.AddMonths(1);
				return range;
			}
			else if (value == "THIS MONTH")
			{
				range.Start = today.AddDays(-(today.Day - 1));
				range.End = range.Start.AddMonths(1);
				return range;
			}
			else if (value == "NEXT MONTH")
			{
				range.Start = today.AddDays(-(today.Day - 1)).AddMonths(1);
				range.End = range.Start.AddMonths(1);
				return range;
			}
			else if (value == "LAST QUARTER")
			{
				range.Start = today.AddDays(-(today.Day - 1));
				range.Start.AddMonths(-((range.Start.Month + 2) % 3 + 3));
				range.End = range.Start.AddMonths(4);
				return range;
			}
			else if (value == "THIS QUARTER")
			{
				range.Start = today.AddDays(-(today.Day - 1));
				range.Start.AddMonths(-((range.Start.Month + 2) % 3));
				range.End = range.Start.AddMonths(4);
				return range;
			}
			else if (value == "NEXT QUARTER")
			{
				range.Start = today.AddDays(-(today.Day - 1));
				range.Start.AddMonths(-((range.Start.Month + 2) % 3 - 3));
				range.End = range.Start.AddMonths(4);
				return range;
			}
			else if (value == "LAST YEAR")
			{
				range.Start = new DateTime(today.Year - 1, 1, 1);
				range.End = range.Start.AddYears(1);
				return range;
			}
			else if (value == "THIS YEAR")
			{
				range.Start = new DateTime(today.Year, 1, 1);
				range.End = range.Start.AddYears(1);
				return range;
			}
			else if (value == "NEXT YEAR")
			{
				range.Start = new DateTime(today.Year + 1, 1, 1);
				range.End = range.Start.AddYears(1);
				return range;
			}
			else if (value.StartsWith("LAST "))
			{
				Match match;
				int tmp;

				match = Regex.Match(value, @"^LAST (\d+) DAYS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = today.AddDays(-tmp);
						range.End = DateTime.Now;
						return range;
					}
					else
					{
						return null;
					}
				}
				match = Regex.Match(value, @"^LAST (\d+) MONTHS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = today.AddDays(-(today.Day - 1)).AddMonths(-tmp);
						range.End = range.Start.AddMonths(tmp);
						return range;
					}
					else
					{
						return null;
					}
				}
				match = Regex.Match(value, @"^LAST (\d+) QUARTERS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						tmp = tmp * 3;
						range.Start = today.AddDays(-(today.Day - 1));
						range.Start.AddMonths(-((range.Start.Month + 2) % 3 + tmp));
						range.End = range.Start.AddMonths(tmp);
						return range;
					}
					else
					{
						return null;
					}
				}
				match = Regex.Match(value, @"^LAST (\d+) YEARS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = new DateTime(today.Year - tmp, 1, 1);
						range.End = range.Start.AddYears(tmp);
						return range;
					}
					else
					{
						return null;
					}
				}
			}
			else if (value.StartsWith("NEXT "))
			{
				Match match;
				int tmp;

				match = Regex.Match(value, @"^NEXT (\d+) DAYS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = today.AddDays(1);
						range.End = range.Start.AddDays(tmp);
						return range;
					}
					else
					{
						return null;
					}
				}
				match = Regex.Match(value, @"^NEXT (\d+) MONTHS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = today.AddDays(-(today.Day - 1)).AddMonths(1);
						range.End = range.Start.AddMonths(tmp);
						return range;
					}
					else
					{
						return null;
					}
				}
				match = Regex.Match(value, @"^NEXT (\d+) QUARTERS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = today.AddDays(-(today.Day - 1));
						range.Start.AddMonths(-((range.Start.Month + 2) % 3 - 3));
						range.End = range.Start.AddMonths(tmp * 3);
						return range;
					}
					else
					{
						return null;
					}
				}
				match = Regex.Match(value, @"^NEXT (\d+) YEARS$");
				if (match.Success)
				{
					if (Int32.TryParse(match.Groups[1].Value, out tmp) && tmp > 0)
					{
						range.Start = new DateTime(today.Year + 1, 1, 1);
						range.End = range.Start.AddYears(tmp);
						return range;
					}
					else
					{
						return null;
					}
				}
			}

			return null;
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public override APRptFilterType FilterType
		{
			get { return APRptFilterType.DateTime; }
		}


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public override string RenderFormatter
		{
			get { return base.RenderFormatter ?? DateTimeType.ToString(); }
			set { base.RenderFormatter = value; }
		}


		/// <summary>
		/// Gets client validate attributes.
		/// </summary>
		/// <returns>Attributes dictionary.</returns>
		public override IDictionary<string, string> GetValidateAttributes()
		{
			var attrs = base.GetValidateAttributes();
			attrs.Add(_iso ? "dateISO" : "date", null);
			return attrs;
		}


		/// <summary>
		/// Parse 'WHERE' phrase value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Parsed value.</returns>
		protected override object TryGetFilterValue(string value)
		{
			value = (string)base.TryGetFilterValue(value);

			DateTime tmp;

			if (DateTime.TryParse(value, out tmp))
				return tmp;
			DateRange range = ParseDateRange(value);
			if (range != null)
				return range;

			throw APRptFilterParseException.InvalidDatetime();
		}


		/// <summary>
		/// Parse query where phrase.
		/// Can parse special date value, include:<br/>
		/// 
		/// <b>YESTERDAY</b><br/>
		///		Starts at 12:00:00 a.m. on the day before the current day and continues for 24 hours.<br/>
		/// <b>TODAY</b><br/>
		///		Starts at 12:00:00 a.m. on the current day and continues for 24 hours.<br/>
		/// <b>TOMORROW</b><br/>
		///		Starts at 12:00:00 a.m. on the day after the current day and continues for 24 hours.<br/>
		/// <b>LAST WEEK</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the week before the current week and continues for seven days.<br/>
		/// <b>THIS WEEK</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the current week and continues for seven days.<br/>
		/// <b>NEXT WEEK</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the week after the current week and continues for seven days.<br/>
		/// <b>LAST MONTH</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the month before the current month and continues for all the days of that month.<br/>
		/// <b>THIS MONTH</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the current month and continues for all the days of that month.<br/>
		/// <b>NEXT MONTH</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the month after the current month and continues for all the days of that month.<br/>
		/// <b>LAST 90 DAYS</b><br/>
		///		Starts at 12:00:00 a.m. 90 days before the current day and continues up to the current second. (The range includes today.)<br/>
		/// <b>NEXT 90 DAYS</b><br/>
		///		Starts at 12:00:00 a.m. on the day after the current day and continues for 90 days. (The range does not include today.)<br/>
		/// <b>LAST n DAYS</b><br/>
		///		Starts at 12:00:00 a.m. n days before the current day and continues up to the current second. (The range includes today.)<br/>
		/// <b>NEXT n DAYS</b><br/>
		///		Starts at 12:00:00 a.m. on the next day and continues for the next n days. (The range does not include today.)<br/>
		/// <b>LAST n MONTHS</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the month n months ago and continues to the end of the month before the current month. (The range does not include the current month.)<br/>
		/// <b>NEXT n MONTHS</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the month after the current month and continues to the end of the month n months in the future. (The range does not include the current month.)<br/>
		/// <b>LAST QUARTER</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the quarter before the current quarter and continues to the end of that quarter.<br/>
		/// <b>THIS QUARTER</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the current quarter and continues to the end of the quarter.<br/>
		/// <b>NEXT QUARTER</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the quarter after the current quarter and continues to the end of that quarter.<br/>
		/// <b>LAST n QUARTERS</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the quarter n quarters ago and continues to the end of the quarter before the current quarter. (The range does not include the current quarter.)<br/>
		/// <b>NEXT n QUARTERS</b><br/>
		///		Starts at 12:00:00 a.m. on the first day of the quarter after the current quarter and continues to the end of the quarter n quarters in the future. (The range does not include the current quarter.)<br/>
		/// <b>LAST YEAR</b><br/>
		///		Starts at 12:00:00 a.m. on January 1 of the year before the current year and continues through the end of December 31 of that year.<br/>
		/// <b>THIS YEAR</b><br/>
		///		Starts at 12:00:00 a.m. on January 1 of the current year and continues through the end of December 31 of the current year.<br/>
		/// <b>NEXT YEAR</b><br/>
		///		Starts at 12:00:00 a.m. on January 1 of the year after the current year and continues through the end of December 31 of that year.<br/>
		/// <b>LAST n YEARS</b><br/>
		///		Starts at 12:00:00 a.m. on January 1 of the year n years ago and continues through December 31 of the year before the current year.<br/>
		/// <b>NEXT n YEARS</b><br/>
		///		Starts at 12:00:00 a.m. on January 1 of the year after the current year and continues through the end of December 31 of the nth year.<br/>
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
				{
					object v = TryGetFilterValue(values[0]);
					if (v is DateRange)
					{
						DateRange dr = (DateRange)v;
						return new APSqlConditionPhrase(expr, APSqlConditionOperator.GreaterThanOrEqual, dr.Start)
							& new APSqlConditionPhrase(expr, APSqlConditionOperator.LessThan, dr.End);
					}
					else if (v is DateTime)
					{
						DateTime dt = (DateTime)v;
						return new APSqlConditionPhrase(expr, APSqlConditionOperator.Equals, dt);
					}

					throw APRptFilterParseException.InvalidDatetime();
				}

				else
				{
					List<APSqlWherePhrase> list = new List<APSqlWherePhrase>();
					foreach (string value in values)
					{
						object v = TryGetFilterValue(value);
						if (v is DateTime)
							list.Add(new APSqlConditionPhrase(expr, APSqlConditionOperator.Equals, v));
						else if (v is DateRange)
							throw APRptFilterParseException.UnsupportSpecialDateValueOrOnlyOne(comparator);
						else
							throw APRptFilterParseException.InvalidDatetime();
					}
					return new APSqlConditionOrPhrase(list);
				}

			}

			else if (comparator == APRptFilterComparator.NotEqual)
			{

				if (values.Length == 1)
				{
					object v = TryGetFilterValue(values[0]);
					if (v is DateRange)
					{
						DateRange dr = (DateRange)v;
						return new APSqlConditionPhrase(expr, APSqlConditionOperator.LessThan, dr.Start)
							| new APSqlConditionPhrase(expr, APSqlConditionOperator.GreaterThanOrEqual, dr.End);
					}
					else if (v is DateTime)
					{
						DateTime dt = (DateTime)v;
						return new APSqlConditionPhrase(expr, APSqlConditionOperator.NotEqual, dt);
					}

					throw APRptFilterParseException.InvalidDatetime();
				}

				else
				{
					List<APSqlWherePhrase> list = new List<APSqlWherePhrase>();
					foreach (string value in values)
					{
						object v = TryGetFilterValue(value);
						if (v is DateTime)
							list.Add(new APSqlConditionPhrase(expr, APSqlConditionOperator.NotEqual, v));
						else if (v is DateRange)
							throw APRptFilterParseException.UnsupportSpecialDateValueOrOnlyOne(comparator);
						else
							throw APRptFilterParseException.InvalidDatetime();
					}
					return new APSqlConditionAndPhrase(list);
				}

			}

			else if (comparator == APRptFilterComparator.LessThan
				|| comparator == APRptFilterComparator.LessOrEqual
				|| comparator == APRptFilterComparator.GreaterThan
				|| comparator == APRptFilterComparator.GreaterOrEqual)
			{
				if (values.Length > 1)
					throw APRptFilterParseException.UnsupportMultiValues(comparator);

				object v = TryGetFilterValue(values[0]);
				if (v is DateTime)
					return new APSqlConditionPhrase(expr, (APSqlConditionOperator)comparator, v);
				else if (v is DateRange)
					throw APRptFilterParseException.UnsupportSpecialDateValueOrOnlyOne(comparator);
				else
					throw APRptFilterParseException.InvalidDatetime();
			}
			else if (comparator == APRptFilterComparator.Between)
			{
				if (values.Length != 2)
					throw APRptFilterParseException.BetweenMustHaveTwoValues();

				object v1 = TryGetFilterValue(values[0]);
				object v2 = TryGetFilterValue(values[1]);

				if (v1 == DBNull.Value || v2 == DBNull.Value)
					throw APRptFilterParseException.UnsupportDBNull();

				if (v1 is DateRange)
					v1 = (v1 as DateRange).Start;
				if (v2 is DateRange)
					v2 = (v2 as DateRange).Start;


				if (v1 is DateTime && v2 is DateTime)
					return new APSqlConditionPhrase(expr, APSqlConditionOperator.Between, new object[2] { v1, v2 });

				throw APRptFilterParseException.InvalidDatetime();
			}

			throw APRptFilterParseException.UnsupportFilterComparator(GetType(), comparator);
		}


		#endregion

	}

}
