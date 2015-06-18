using System;
using System.Runtime.Serialization;

namespace Symber.Web.Report
{

	/// <summary>
	/// Parse Rpt Filter values exception.
	/// </summary>
	[Serializable]
	public class APRptFilterParseException : Exception
	{

		#region [ Static Methods ]


		/// <summary>
		/// Gets a APRptFilterParseException by ValuesCountCannotBeZero.
		/// </summary>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException ValuesCountCannotBeZero()
		{
			return new APRptFilterParseException(APResource.APRptFilter_ValuesCountCannotBeZero);
		}


		/// <summary>
		/// Gets a APRptFilterParseException by UnsupportFilterComparator.
		/// </summary>
		/// <param name="columnType">The column type.</param>
		/// <param name="comparator">Comparator.</param>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException UnsupportFilterComparator(Type columnType, APRptFilterComparator comparator)
		{
			return new APRptFilterParseException(APResource.GetString(APResource.APRptFilter_UnsupportComparator, columnType, comparator));
		}


		/// <summary>
		/// Gets a APRptFilterParseException by UnsupportMultiValues.
		/// </summary>
		/// <param name="comparator">Comparator.</param>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException UnsupportMultiValues(APRptFilterComparator comparator)
		{
			return new APRptFilterParseException(APResource.GetString(APResource.APRptFilter_UnsupportMultiValues, comparator));
		}


		/// <summary>
		/// Gets a APRptFilterParseException by BetweenMustHaveTwoValues.
		/// </summary>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException BetweenMustHaveTwoValues()
		{
			return new APRptFilterParseException(APResource.APRptFilter_BetweenMustHaveTwoValues);
		}


		/// <summary>
		/// Gets a APRptFilterParseException by InvalidNumber.
		/// </summary>
		/// <param name="numberType">The number type.</param>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException InvalidNumber(Type numberType)
		{
			return new APRptFilterParseException(APResource.GetString(APResource.APRptFilter_InvalidNumber, numberType));
		}


		/// <summary>
		/// Gets a APRptFilterParseException by InvalidDateTime.
		/// </summary>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException InvalidDatetime()
		{
			return new APRptFilterParseException(APResource.APRptFilter_InvalidDateTime);
		}


		/// <summary>
		/// Gets a APRptFilterParseException by UnsupportSpecialDateValueOrOnlyOne.
		/// </summary>
		/// <param name="comparator">Comparator.</param>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException UnsupportSpecialDateValueOrOnlyOne(APRptFilterComparator comparator)
		{
			return new APRptFilterParseException(APResource.GetString(APResource.APRptFilter_UnsupportSpecialDateValueOrOnlyOne, comparator));
		}


		/// <summary>
		/// Gets a APRptFilterParseException by InvalidValue.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="toType">Type of converted.</param>
		/// <param name="innerException">Inner exception.</param>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException InvalidValue(string value, Type toType, Exception innerException)
		{
			return new APRptFilterParseException(APResource.GetString(APResource.APRptFilter_InvalidValue, value, toType), innerException);
		}


		/// <summary>
		/// Gets a APColumnExFilterParseException by Unsupport DBNull.
		/// </summary>
		/// <returns>APRptFilterParseException.</returns>
		public static APRptFilterParseException UnsupportDBNull()
		{
			return new APRptFilterParseException(APResource.APRptFilter_UnsupportDBNull);
		}


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create new APFilterExParseException.
		/// </summary>
		/// <param name="message">Message.</param>
		public APRptFilterParseException(string message)
			: base(message)
		{
		}



		/// <summary>
		/// Create new APFilterExParseException.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected APRptFilterParseException(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		/// <summary>
		/// Create new APFilterExParseException.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="innerException">Inner exception.</param>
		public APRptFilterParseException(string message, Exception innerException)
			:base(message, innerException)
		{
		}


		#endregion

	}

}
