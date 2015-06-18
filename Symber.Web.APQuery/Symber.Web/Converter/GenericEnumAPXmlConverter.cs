using System;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// GenericEnumAPConverter.
	/// </summary>
	public sealed class GenericEnumAPConverter : APConverterBase
	{

		#region [ Fields ]


		private Type _enumType;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Constructors new GenericEnumAPConverter.
		/// </summary>
		/// <param name="enumType">Enum type.</param>
		public GenericEnumAPConverter(Type enumType)
		{
			if (enumType == null)
				throw new ArgumentNullException("enumType");
			_enumType = enumType;
		}


		#endregion


		#region [ Override Implementation of APConverterBase ]


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
				throw new ArgumentNullException("value");

			return Enum.Parse(_enumType, (string)value);
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
			return value.ToString();
		}


		#endregion

	}
}
