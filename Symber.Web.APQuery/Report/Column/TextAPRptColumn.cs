using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Text APRptColumn.
	/// </summary>
	public class TextAPRptColumn : APRptColumn
	{

		#region [ Fields ]

		private bool _zipSpace;
		private string _illegalContains;
		private int _dataLength;
		private int _minLength;

		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new TextAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define</param>
		public TextAPRptColumn(StringAPColumnDef columnDef)
			: base(columnDef)
		{
			_dataLength = columnDef.Length;
		}


		/// <summary>
		/// Create a new TextAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public TextAPRptColumn(StringAPColumnDef columnDef, string id, string title)
			: base(columnDef, id, title)
		{
			_dataLength = columnDef.Length;
		}
		

		/// <summary>
		/// Create a new TextAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="dataLength">Data length.</param>
		public TextAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int dataLength)
			: base(selectExpr, id, title)
		{
			_dataLength = dataLength;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets data length.
		/// </summary>
		public virtual int DataLength
		{
			get { return _dataLength; }
		}


		/// <summary>
		/// Gets or sets min length.
		/// </summary>
		public virtual int MinLength
		{
			get { return _minLength; }
			set { _minLength = value; }
		}


		/// <summary>
		/// Zip space of input string.
		/// </summary>
		public bool ZipSpace
		{
			get { return _zipSpace; }
			set { _zipSpace = value; }
		}


		/// <summary>
		/// Illegal Contains sub String
		/// </summary>
		public string IllegalContains
		{
			get { return _illegalContains; }
			set { _illegalContains = value; }
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public override APRptFilterType FilterType
		{
			get { return APRptFilterType.Text; }
		}


		/// <summary>
		/// Gets client validate attributes.
		/// </summary>
		/// <returns>Attributes dictionary.</returns>
		public override IDictionary<string, string> GetValidateAttributes()
		{
			var attrs = base.GetValidateAttributes();
			if (_minLength != 0)
			{
				if (_dataLength != 0)
				{
					attrs.Add("rangelength", String.Format("[{0},{1}]", _minLength, _dataLength));
				}
				else
				{
					attrs.Add("minlength", _minLength.ToString());
				}
			}
			else if (_dataLength != Int32.MaxValue)
			{
				attrs.Add("maxlength", _dataLength.ToString());

			}

			return attrs;
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

			// todo: here use APDatabase.Provider.Wildcard, when multi-providers may by error wildcard char.

			else if (comparator == APRptFilterComparator.StartsWith)
			{
				if (values.Length == 1)
				{
					object v = TryGetFilterValue(values[0]);
				
					if (v == DBNull.Value)
						throw APRptFilterParseException.UnsupportDBNull();

					string likeString = (v as String) + APDalProvider.Wildcard;
					return new APSqlConditionPhrase(expr, APSqlConditionOperator.Like, likeString);
				}
				List<APSqlWherePhrase> list = new List<APSqlWherePhrase>();
				foreach (string s in values)
				{
					object v = TryGetFilterValue(s);
				
					if (v == DBNull.Value)
						throw APRptFilterParseException.UnsupportDBNull();

					string likeString = (v as String) + APDalProvider.Wildcard;
					list.Add(new APSqlConditionPhrase(expr, APSqlConditionOperator.Like, likeString));
				}
				return new APSqlConditionOrPhrase(list);
			}
			else if (comparator == APRptFilterComparator.Contains || comparator == APRptFilterComparator.DoesNotContain)
			{
				if (values.Length == 1)
				{
					object v = TryGetFilterValue(values[0]);
				
					if (v == DBNull.Value)
						throw APRptFilterParseException.UnsupportDBNull();

					string likeString = APDalProvider.Wildcard + (v as String) + APDalProvider.Wildcard;

					if (comparator == APRptFilterComparator.Contains)
						return new APSqlConditionPhrase(expr, APSqlConditionOperator.Like, likeString);
					else
						return new APSqlConditionPhrase(expr, APSqlConditionOperator.NotLike, likeString);
				}
				List<APSqlWherePhrase> list = new List<APSqlWherePhrase>();
				foreach (string s in values)
				{
					object v = TryGetFilterValue(s);

					if (v == DBNull.Value)
						throw APRptFilterParseException.UnsupportDBNull();

					string likeString = APDalProvider.Wildcard + (v as String) + APDalProvider.Wildcard;
					list.Add(new APSqlConditionPhrase(expr, APSqlConditionOperator.NotLike, likeString));
				}
				if (comparator == APRptFilterComparator.Contains)
					return new APSqlConditionOrPhrase(list);
				return !new APSqlConditionOrPhrase(list);
			}

			throw APRptFilterParseException.UnsupportFilterComparator(GetType(), comparator);
		}


		#endregion

	}

}
