using System;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// TimeSpanMinutesOrInfiniteAPConverter.
	/// </summary>
	public sealed class TimeSpanMinutesOrInfiniteAPConverter : TimeSpanMinutesAPConverter
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
			if ((string)value == "Infinite")
				return TimeSpan.MaxValue;
			else
				return base.ConvertFrom(context, culture, value);
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

			if (((TimeSpan)value) == TimeSpan.MaxValue)
				return "Infinite";
			else
				return base.ConvertTo(context, culture, value, destinationType);
		}


		#endregion

	}
}
