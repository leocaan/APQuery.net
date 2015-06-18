using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Decimal APRptColumn.
	/// </summary>
	public class DecimalAPRptColumn : NumberAPRptColumn
	{

		#region [ Fields ]


		private Decimal _minValue;
		private Decimal _maxValue;
		private int _scale;
		private string _format;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new DecimalAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DecimalAPRptColumn(DecimalAPColumnDef columnDef, Decimal minValue = Decimal.MinValue, Decimal maxValue = Decimal.MaxValue)
			: base(columnDef)
		{
			_minValue = minValue;
			_maxValue = maxValue;
			_scale = columnDef.Scale;
		}


		/// <summary>
		/// Create a new DecimalAPRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DecimalAPRptColumn(DecimalAPColumnDef columnDef, string id, string title, Decimal minValue = Decimal.MinValue, Decimal maxValue = Decimal.MaxValue)
			: base(columnDef, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
			_scale = columnDef.Scale;
		}
						

		/// <summary>
		/// Create a new DecimalAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="scale">Scale.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public DecimalAPRptColumn(APSqlOperateExpr selectExpr, string id, string title, int scale, Decimal minValue = Decimal.MinValue, Decimal maxValue = Decimal.MaxValue)
			: base(selectExpr, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
			_scale = scale;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets the min value.
		/// </summary>
		public Decimal MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}


		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		public Decimal MaxValue
		{
			get { return _maxValue; }
			set { _maxValue = value; }
		}


		/// <summary>
		/// Gets or sets data scale.
		/// </summary>
		public int Scale
		{
			get { return _scale; }
			set { _scale = value; _format = null; }
		}


		/// <summary>
		/// Gets the data render format
		/// </summary>
		public override string Format
		{
			get
			{
				if (_format == null)
					_format = "." + new String('0', _scale);
				return _format;
			}
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
			if (_minValue != Decimal.MinValue)
			{
				if (_maxValue != Decimal.MinValue)
				{
					attrs.Add("range", String.Format("[{0},{1}]", _minValue, _maxValue));
				}
				else
				{
					attrs.Add("min", _minValue.ToString());
				}
			}
			else if (_maxValue != Decimal.MaxValue)
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
			decimal retVal;
			if (!Decimal.TryParse(value, out retVal))
				throw APRptFilterParseException.InvalidNumber(typeof(Decimal));

			return retVal;
		}


		#endregion

	}

}
