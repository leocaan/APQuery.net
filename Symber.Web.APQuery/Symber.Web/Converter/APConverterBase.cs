using System;
using System.ComponentModel;

namespace Symber.Web
{
	/// <summary>
	/// APConverterBase.
	/// </summary>
	public abstract class APConverterBase : TypeConverter
	{

		#region [ Override Implementation of TypeConverter ]


		/// <summary>
		/// ConvertFrom.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext.</param>
		/// <param name="type">Type.</param>
		/// <returns>Object.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type type)
		{
			if (type == typeof(string))
				return true;

			return base.CanConvertFrom(context, type);
		}


		/// <summary>
		/// ConvertTo.
		/// </summary>
		/// <param name="context">ITypeDescriptorContext.</param>
		/// <param name="type">Type.</param>
		/// <returns>Object.</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type type)
		{
			if (type == typeof(string))
				return true;

			return base.CanConvertTo(context, type);
		}


		#endregion

	}
}
