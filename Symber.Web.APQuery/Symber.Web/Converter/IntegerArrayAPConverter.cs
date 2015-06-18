using System;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// IntegerArrayAPConverter.
	/// </summary>
	public sealed class IntegerArrayAPConverter : APConverterBase
	{

		#region [ Override Implementation of TypeConverter ]


		/// <summary>
		/// CanConvertFrom.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext.</param>
		/// <param name="type">Type.</param>
		/// <returns>Boolean value.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type type)
		{
			if (type == typeof(string))
				return true;
			return base.CanConvertFrom(context, type);
		}


		/// <summary>
		/// ConvertFrom.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext.</param>
		/// <param name="culture">CultureInfo.</param>
		/// <param name="value">Value.</param>
		/// <returns>Object.</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
				return null;

			if (value is string)
			{
				string[] stringValues = ((string)value).Split(',');
				int[] intValues = new int[stringValues.Length];
				for (int i = 0, len = stringValues.Length; i < len; i++)
				{
					intValues[i] = Convert.ToInt32(stringValues[i].Trim(), 10);
				}
				return intValues;
			}

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
			if (value is int[] && destinationType == typeof(string))
			{
				int[] intValues = value as int[];
				string[] stringValues = new string[intValues.Length];
				for (int i = 0, len = intValues.Length; i < len; i++)
				{
					stringValues[i] = Convert.ToString(intValues[i]);
				}

				return string.Join(",", stringValues);
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}


		#endregion

	}
}
