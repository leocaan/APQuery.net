using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// UInt32 APRptColumn.
	/// </summary>
	public class UInt32APRptColumn : NumberAPRptColumn
	{

		#region [ Fields ]


		private UInt32 _minValue;
		private UInt32 _maxValue;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new UInt32APRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public UInt32APRptColumn(UInt32APColumnDef columnDef, UInt32 minValue = UInt32.MinValue, UInt32 maxValue = UInt32.MaxValue)
			: base(columnDef)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}


		/// <summary>
		/// Create a new UInt32APRptColumn.
		/// </summary>
		/// <param name="columnDef">Column define.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public UInt32APRptColumn(UInt32APColumnDef columnDef, string id, string title, UInt32 minValue = UInt32.MinValue, UInt32 maxValue = UInt32.MaxValue)
			: base(columnDef, id, title)
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}
				

		/// <summary>
		/// Create a new UInt32APRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		public UInt32APRptColumn(APSqlOperateExpr selectExpr, string id, string title, UInt32 minValue = UInt32.MinValue, UInt32 maxValue = UInt32.MaxValue)
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
		public UInt32 MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}


		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		public UInt32 MaxValue
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
			if (_minValue != UInt32.MinValue)
			{
				if (_maxValue != UInt32.MinValue)
				{
					attrs.Add("range", String.Format("[{0},{1}]", _minValue, _maxValue));
				}
				else
				{
					attrs.Add("min", _minValue.ToString());
				}
			}
			else if (_maxValue != UInt32.MaxValue)
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
			uint retVal;
			if (!UInt32.TryParse(value, out retVal))
				throw APRptFilterParseException.InvalidNumber(typeof(UInt32));

			return retVal;
		}


		#endregion

	}

}
