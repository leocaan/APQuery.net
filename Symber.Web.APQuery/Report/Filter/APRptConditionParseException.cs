using System;
using System.Runtime.Serialization;

namespace Symber.Web.Report
{

	/// <summary>
	/// Parse Rpt Condition exception.
	/// </summary>
	[Serializable]
	public class APRptConditionParseException : Exception
	{

		#region [ Constructors ]


		/// <summary>
		/// Create new APRptConditionParseException.
		/// </summary>
		/// <param name="message">Message.</param>
		public APRptConditionParseException(string message)
			: base(message)
		{
		}


		/// <summary>
		/// Create new APRptConditionParseException.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected APRptConditionParseException(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		/// <summary>
		/// Create new APRptConditionParseException.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="innerException">Inner exception.</param>
		public APRptConditionParseException(string message, Exception innerException)
			:base(message, innerException)
		{
		}


		#endregion

	}

}
