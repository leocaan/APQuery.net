using Symber.Web.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Enum or List APRptColumn.
	/// </summary>
	/// <typeparam name="T">Type of Enum.</typeparam>
	public class EnumAPRptColumn<T> : APRptColumn, IDictionaryAPRptColumn where T : struct
	{

		#region [ Fields ]


		private Dictionary<T, string> _dictionary;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new EnumAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="dictionary">Enum or List string dictionary.</param>
		public EnumAPRptColumn(EnumAPColumnDef<T> columnDef, Dictionary<T, string> dictionary)
			: base(columnDef)
		{
			_dictionary = dictionary;
		}


		/// <summary>
		/// Create a new EnumAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dictionary">Enum or List string dictionary.</param>
		public EnumAPRptColumn(EnumAPColumnDef<T> columnDef, string id, string title, Dictionary<T, string> dictionary)
			: base(columnDef, id, title)
		{
			_dictionary = dictionary;
		}


		/// <summary>
		/// Create a new EnumAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="dictionary">Enum or List string dictionary.</param>
		public EnumAPRptColumn(APColumnDef columnDef, Dictionary<T, string> dictionary)
			: base(columnDef)
		{
			_dictionary = dictionary;
		}


		/// <summary>
		/// Create a new EnumAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dictionary">Enum or List string dictionary.</param>
		public EnumAPRptColumn(APColumnDef columnDef, string id, string title, Dictionary<T, string> dictionary)
			: base(columnDef, id, title)
		{
			_dictionary = dictionary;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// The dictionary.
		/// </summary>
		public Dictionary<T, string> Dictionary
		{
			get { return _dictionary; }
		}


		IDictionary IDictionaryAPRptColumn.Dictionary
		{
			get { return Dictionary; }
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public override APRptFilterType FilterType
		{
			get { return APRptFilterType.EnumOrId; }
		}


		/// <summary>
		/// Parse 'WHERE' phrase value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Parsed value.</returns>
		protected override object TryGetFilterValue(string value)
		{
			try
			{
				foreach (KeyValuePair<T, string> pair in Dictionary)
				{
					if (pair.Value == value)
						return pair.Key;
				}
				if (typeof(T).IsEnum)
					return (T)Enum.Parse(typeof(T), value);
				else
					return (T)Convert.ChangeType(value, typeof(T));
			}
			catch (Exception ex)
			{
				throw APRptFilterParseException.InvalidValue(value, typeof(T), ex);
			}
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


		/// <summary>
		/// Gets default JSON value from data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <returns>The value.</returns>
		protected override object DefaultJson(System.Data.IDataReader reader)
		{
			return Dictionary[(T)base.DefaultJson(reader)];
		}


		#endregion

	}

}
