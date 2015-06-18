using System;
using System.Runtime.Serialization;

namespace Symber.Web.Data
{

	/// <summary>
	/// The exception that is thrown when operate the database.
	/// </summary>
	[Serializable]
	public sealed class APDataException : Exception
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new data exception.
		/// </summary>
		public APDataException() : base() { }



		/// <summary>
		/// Create a new data exception.
		/// </summary>
		/// <param name="info">info.</param>
		/// <param name="context">context.</param>
		private APDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Create a new data exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public APDataException(string message) : base(message) { }


		/// <summary>
		/// Create a new data exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		public APDataException(string message, Exception innerException) : base(message, innerException) { }


		#endregion

	}

}
