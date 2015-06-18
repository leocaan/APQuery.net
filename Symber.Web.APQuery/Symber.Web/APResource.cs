using Symber.Web.Properties;
using System;
using System.Globalization;

namespace Symber.Web
{
	internal class APResource : Resources
	{

		#region [ Static Methods ]

		public static string GetString(string value, object param1)
		{
			return String.Format(value, param1);
		}

		public static string GetString(CultureInfo culture, string value, object param1)
		{
			return string.Format(culture, value, param1);
		}

		public static string GetString(string value, object param1, object param2)
		{
			return String.Format(value, param1, param2);
		}

		public static string GetString(CultureInfo culture, string value, object param1, object param2)
		{
			return string.Format(culture, value, param1, param2);
		}

		public static string GetString(string value, object param1, object param2, object param3)
		{
			return String.Format(value, param1, param2, param3);
		}

		public static string GetString(CultureInfo culture, string value, object param1, object param2, object param3)
		{
			return string.Format(culture, value, param1, param2, param3);
		}

		public static string GetString(string value, object param1, object param2, object param3, object param4)
		{
			return String.Format(value, param1, param2, param3, param4);
		}

		public static string GetString(CultureInfo culture, string value, object param1, object param2, object param3, object param4)
		{
			return string.Format(culture, value, param1, param2, param3, param4);
		}

		public static string GetString(string value, params object[] param)
		{
			return string.Format(value, param);
		}

		public static string GetString(CultureInfo culture, string value, params object[] param)
		{
			return string.Format(culture, value, param);
		}

		#endregion

	}
}
