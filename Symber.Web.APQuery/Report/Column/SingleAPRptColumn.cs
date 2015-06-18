using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Single APRptColumn.
	/// </summary>
	public class SingleAPRptColumn : NumberAPRptColumn
	{

		#region [ Fields ]


		private Single _minValue;
		private Single _maxValue;


		#endregion


		#region [ Constructors ]

		/// <summary>
		/// Create a new SingleAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public SingleAPRptColumn(DoubleAPColumnDef columnDef, Single minValue = Single.MinValue, Single maxValue = Single.MaxValue)
			: base(columnDef)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}


		/// <summary>
		/// Create a new SingleAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public SingleAPRptColumn(DoubleAPColumnDef columnDef, string id, string title, Single minValue = Single.MinValue, Single maxValue = Single.MaxValue)
			: base(columnDef, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}
						

		/// <summary>
		/// Create a new SingleAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public SingleAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, Single minValue = Single.MinValue, Single maxValue = Single.MaxValue)
			: base(selectExpr, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets the min value.
		/// </summary>
		public Single MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}


		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		public Single MaxValue
		{
			get { return _maxValue; }
			set { _maxValue = value; }
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets client validate attributes.
		/// </summary>
		/// <returns>Attributes dictionary.</returns>
		public override IDictionary<string, string> GetValidateAttributes()
		{
			var attrs = base.GetValidateAttributes();
			attrs.Add("number", null);
			if (_minValue != Single.MinValue)
			{
				if (_maxValue != Single.MinValue)
				{
					attrs.Add("range", String.Format("[{0},{1}]", _minValue, _maxValue));
				}
				else
				{
					attrs.Add("min", _minValue.ToString());
				}
			}
			else if (_maxValue != Single.MaxValue)
			{
				attrs.Add("max", _maxValue.ToString());

			}

			return attrs;
		}


		/// <summary>
		/// Parse 'WHERE' phrase value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Parsed value.</returns>
		protected override object TryGetFilterValue(string value)
		{
			double retVal;
			if (!Double.TryParse(value, out retVal))
				throw APRptFilterParseException.InvalidNumber(typeof(Double));
			
			return retVal;
		}


		#endregion

	}

}
