using System;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// TimeSpanMinutesAPConverter.
	/// </summary>
	public class TimeSpanMinutesAPConverter : APConverterBase
	{

		#region [ Override Implementation of TypeConverter ]


		/// <summary>
		/// ConvertFrom.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext.</param>
		/// <param name="culture">CultureInfo.</param>
		/// <param name="value">Value.</param>
		/// <returns>Object.</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return TimeSpan.FromMinutes((double)Int64.Parse((string)value));
		}


		/// <summary>
		/// ConvertTo.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext.</param>
		/// <param name="culture">CultureInfo.</param>
		/// <param name="value">Value.</param>
		/// <param name="destinationType">Destination type.</param>
		/// <returns>Object.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value.GetType() != typeof(TimeSpan))
				throw new ArgumentException();

			return ((Int64)((TimeSpan)value).TotalMinutes).ToString();
		}


		#endregion

	}
}
