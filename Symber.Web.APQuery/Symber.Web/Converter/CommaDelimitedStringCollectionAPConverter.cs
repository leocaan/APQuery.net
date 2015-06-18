using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace Symber.Web
{
	/// <summary>
	/// CommaDelimitedStringCollectionAPConverter.
	/// </summary>
	public sealed class CommaDelimitedStringCollectionAPConverter : APConverterBase
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
			StringCollection col = new StringCollection();
			string[] datums = ((string)value).Split(',');

			foreach (string datum in datums)
				col.Add(datum.Trim());

			return col;
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
				return null;

			StringCollection sc = value as StringCollection;
			if (sc == null)
				throw new ArgumentException();

			string[] ss = new string[sc.Count];
			sc.CopyTo(ss, 0);

			return String.Join(",", ss);
		}


		#endregion

	}
}
