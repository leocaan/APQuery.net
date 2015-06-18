using System;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// WhiteSpaceTrimStringAPConverter.
	/// </summary>
	public sealed class WhiteSpaceTrimStringAPConverter : APConverterBase
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
			return ((string)value).Trim();
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
			if (value == null)
				return String.Empty;

			if (!(value is string))
				throw new ArgumentException();

			return ((string)value).Trim();
		}


		#endregion

	}
}
