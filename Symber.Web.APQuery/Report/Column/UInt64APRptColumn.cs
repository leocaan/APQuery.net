using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// UInt64 APRptColumn.
	/// </summary>
	public class UInt64APRptColumn : NumberAPRptColumn
	{

		#region [ Fields ]


		private UInt64 _minValue;
		private UInt64 _maxValue;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new UInt64APRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public UInt64APRptColumn(UInt64APColumnDef columnDef, UInt64 minValue = UInt64.MinValue, UInt64 maxValue = UInt64.MaxValue)
			: base(columnDef)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}


		/// <summary>
		/// Create a new UInt64APRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public UInt64APRptColumn(UInt64APColumnDef columnDef, string id, string title, UInt64 minValue = UInt64.MinValue, UInt64 maxValue = UInt64.MaxValue)
			: base(columnDef, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}
						

		/// <summary>
		/// Create a new UInt64APRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public UInt64APRptColumn(APSqlOperateExpr selectExpr, string id, string title, UInt64 minValue = UInt64.MinValue, UInt64 maxValue = UInt64.MaxValue)
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
		public UInt64 MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}


		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		public UInt64 MaxValue
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
			attrs.Add("digits", null);
			if (_minValue != UInt64.MinValue)
			{
				if (_maxValue != UInt64.MinValue)
				{
					attrs.Add("range", String.Format("[{0},{1}]", _minValue, _maxValue));
				}
				else
				{
					attrs.Add("min", _minValue.ToString());
				}
			}
			else if (_maxValue != UInt64.MaxValue)
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
			ulong retVal;
			if (!UInt64.TryParse(value, out retVal))
				throw APRptFilterParseException.InvalidNumber(typeof(UInt64));

			return retVal;
		}


		#endregion

	}

}
