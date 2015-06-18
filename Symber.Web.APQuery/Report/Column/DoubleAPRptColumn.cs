using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Double APRptColumn.
	/// </summary>
	public class DoubleAPRptColumn : NumberAPRptColumn
	{

		#region [ Fields ]


		private Double _minValue;
		private Double _maxValue;


		#endregion


		#region [ Constructors ]

		/// <summary>
		/// Create a new DoubleAPColumnDef.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DoubleAPRptColumn(DoubleAPColumnDef columnDef, Double minValue = Double.MinValue, Double maxValue = Double.MaxValue)
			: base(columnDef)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}


		/// <summary>
		/// Create a new DoubleAPColumnDef.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DoubleAPRptColumn(DoubleAPColumnDef columnDef, string id, string title, Double minValue = Double.MinValue, Double maxValue = Double.MaxValue)
			: base(columnDef, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}
						

		/// <summary>
		/// Create a new DoubleAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DoubleAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, Double minValue = Double.MinValue, Double maxValue = Double.MaxValue)
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
		public Double MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}


		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		public Double MaxValue
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
			if (_minValue != Double.MinValue)
			{
				if (_maxValue != Double.MinValue)
				{
					attrs.Add("range", String.Format("[{0},{1}]", _minValue, _maxValue));
				}
				else
				{
					attrs.Add("min", _minValue.ToString());
				}
			}
			else if (_maxValue != Double.MaxValue)
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
