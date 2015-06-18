using System;

namespace Symber.Web.Project
{

	/// <summary>
	/// Radix 64 positive integer code.
	/// Like Radix 10, use the characters are in the range [0-9].
	/// The radix 64, where the characters are in the range [0-9][A-Z][a-z][$_]. that's 64 total characters.
	/// </summary>
	public class APRadix64
	{

		#region [ Fields ]


		private static char[] codeMap = new char[64] {
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'$', '_'
			};


		private static char[] zeroInt64 = new char[11] {
			'0', '0','0', '0','0', '0','0', '0','0', '0','0'
			};


		private static Int64 mod = 0x3f;


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Parse from char.
		/// </summary>
		/// <param name="c">Source.</param>
		/// <returns>The long value.</returns>
		public static long Parse(char c)
		{
			if (c >= '0' && c <= '9')
				return (long)(c - '0');
			else if (c >= 'A' && c <= 'Z')
				return (long)(c - 'A') + 10;
			else if (c >= 'a' && c <= 'z')
				return (long)(c - 'a') + 36;
			else if (c == '$')
				return 62;
			else if (c == '_')
				return 63;
			throw new FormatException("Invalid radix64 character.");
		}


		/// <summary>
		/// Parse from string.
		/// </summary>
		/// <param name="s">Source.</param>
		/// <returns>The long value.</returns>
		public static long Parse(string s)
		{
			char[] buf = s.ToCharArray();
			int length = buf.Length;

			if (length > 11 || (length == 11 && buf[0] > '8'))
				throw new OverflowException("Radix64 can parse max value be the 32th power of 2.");

			int i = 0;
			long result = 0;
			long v;
			char c;

			while (i < length)
			{
				result <<= 6;

				v = 0;
				c = buf[i++];

				if (c >= '0' && c <= '9')
					v = (long)(c - '0');
				else if (c >= 'A' && c <= 'Z')
					v = (long)(c - 'A') + 10;
				else if (c >= 'a' && c <= 'z')
					v = (long)(c - 'a') + 36;
				else if (c == '$')
					v = 62;
				else if (c == '_')
					v = 63;
				else
					throw new FormatException("Invalid radix64 character.");

				result |= v;
			}

			return result;
		}


		/// <summary>
		/// Try to parse from char.
		/// </summary>
		/// <param name="c">Source.</param>
		/// <param name="result">The long value.</param>
		/// <returns>If ture parse success</returns>
		public static bool TryParse(char c, out long result)
		{
			result = 0;

			if (c >= '0' && c <= '9')
				result = (long)(c - '0');
			else if (c >= 'A' && c <= 'Z')
				result = (long)(c - 'A') + 10;
			else if (c >= 'a' && c <= 'z')
				result = (long)(c - 'a') + 36;
			else if (c == '$')
				result = 62;
			else if (c == '_')
				result = 63;
			else
				return false;

			return true;
		}


		/// <summary>
		/// Try to parse from string.
		/// </summary>
		/// <param name="s">Source.</param>
		/// <param name="result">The long value.</param>
		/// <returns>If ture parse success.</returns>
		public static bool TryParse(string s, out long result)
		{
			char[] buf = s.ToCharArray();
			int length = buf.Length;
			result = 0;

			if (length > 11 || (length == 11 && buf[0] > '8'))
				return false;

			int i = 0;
			long v;
			char c;

			while (i < length)
			{
				result <<= 6;

				v = 0;
				c = buf[i++];

				if (c >= '0' && c <= '9')
					v = (long)(c - '0');
				else if (c >= 'A' && c <= 'Z')
					v = (long)(c - 'A') + 10;
				else if (c >= 'a' && c <= 'z')
					v = (long)(c - 'a') + 36;
				else if (c == '$')
					v = 62;
				else if (c == '_')
					v = 63;
				else
					return false;

				result |= v;
			}

			return true;
		}


		/// <summary>
		/// Convert the long value to radix64 string.
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <returns>Radix64 string.</returns>
		public static string ToString(long value)
		{
			if (value < 0)
				throw new OverflowException("Unsupport negative convert.");

			char[] buf = (char[])zeroInt64.Clone();
			int i = 10;

			while (i >= 0 && value > 0)
			{
				buf[i--] = codeMap[value & mod];
				value >>= 6;
			}

			return new String(buf);
		}


		/// <summary>
		/// Convert the long value to radix64 string with specified length.
		/// </summary>
		/// <param name="value">The long value.</param>
		/// <param name="length">Length.</param>
		/// <returns>Radix64 string.</returns>
		public static string ToString(long value, int length)
		{
			if (value < 0)
				throw new OverflowException("Unsupport negative convert.");
			if (length > 11)
				throw new OverflowException("Radix64 can parse max value be the 32th power of 2.");

			char[] buf = (char[])zeroInt64.Clone();
			int i = 10;

			while (i >= 0 && value > 0)
			{
				buf[i--] = codeMap[value & mod];
				value >>= 6;
			}

			if ((10 - i) > length)
				throw new OverflowException("The value convert to string out of length");

			return new String(buf, 11 - length, length);
		}


		#endregion

	}

}
