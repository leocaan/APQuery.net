using System;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// TimeSpanSecondsAPConverter.
	/// </summary>
	public class TimeSpanSecondsAPConverter : APConverterBase
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
			long data;
			if (!(value is string))
				throw new ArgumentException();

			if (!Int64.TryParse((string)value, out data))
				throw new ArgumentException();

			return TimeSpan.FromSeconds(data);
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

			return ((Int64)((TimeSpan)value).TotalSeconds).ToString();
		}


		#endregion

	}
}
