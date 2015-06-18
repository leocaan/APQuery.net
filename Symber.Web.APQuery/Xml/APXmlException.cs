using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

namespace Symber.Web.Xml
{

	/// <summary>
	/// The exception that is thrown when a xml file error has occurred.
	/// </summary>
	[Serializable]
	public class APXmlException : SystemException
	{

		#region [ Static Methods ]


		/// <summary>
		/// Gets the path to the xml file when this exception was thrown.
		/// </summary>
		/// <param name="reader">The XmlReader that reads from the xml file.</param>
		/// <returns>A string representing the file name.</returns>
		public static string GetFilename(XmlReader reader)
		{
			if (reader is XmlTextReader)
				return ((XmlTextReader)reader).BaseURI;
			else
				return String.Empty;
		}


		/// <summary>
		/// Gets the line number within the xml file this exception was thrown.
		/// </summary>
		/// <param name="reader">The XmlReader that reads from the xml file.</param>
		/// <returns>An int representing the node line number.</returns>
		public static int GetLineNumber(XmlReader reader)
		{
			if (reader is XmlTextReader)
				return ((XmlTextReader)reader).LineNumber;
			else
				return 0;
		}


		#endregion


		#region [ Fields ]


		private string _bareMessage;
		private string _filename;
		private int _line;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		public APXmlException()
		{
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="message">A message describing why this APXmlException exception was thrown.</param>
		public APXmlException(string message)
			: base(message)
		{
			_bareMessage = message;
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		protected APXmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_filename = info.GetString("APXml_Filename");
			_line = info.GetInt32("APXml_Line");
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="message">A message describing why this APXmlException exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this APXmlException to be thrown, if any.</param>
		public APXmlException(string message, Exception inner)
			: base(message, inner)
		{
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="message">A message describing why this APXmlException exception was thrown.</param>
		/// <param name="reader">The XmlReader that reads from the xml file.</param>
		public APXmlException(string message, XmlReader reader)
			: this(message, null, GetFilename(reader), GetLineNumber(reader))
		{
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="message">A message describing why this APXmlException exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this APXmlException to be thrown, if any.</param>
		/// <param name="reader">The XmlReader that reads from the xml file.</param>
		public APXmlException(string message, Exception inner, XmlReader reader)
			: this(message, inner, GetFilename(reader), GetLineNumber(reader))
		{
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="message">A message describing why this APXmlException exception was thrown.</param>
		/// <param name="filename">Xml file name.</param>
		/// <param name="line">Xml line number when exception was thrown.</param>
		public APXmlException(string message, string filename, int line)
			: this(message, null, filename, line)
		{
		}


		/// <summary>
		/// Initializes a new instance of the APXmlException class.
		/// </summary>
		/// <param name="message">A message describing why this APXmlException exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this APXmlException to be thrown, if any.</param>
		/// <param name="filename">Xml file name.</param>
		/// <param name="line">Xml line number when exception was thrown.</param>
		public APXmlException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			_bareMessage = message;
			_filename = filename;
			_line = line;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets a description of why this exception was thrown.
		/// </summary>
		public string BareMessage
		{
			get { return _bareMessage; }
		}


		/// <summary>
		/// Gets the path to the xml file that caused this exception to be thrown.
		/// </summary>
		public string Filename
		{
			get { return _filename; }
		}


		/// <summary>
		/// Gets the line number within the xml file at which this exception was thrown.
		/// </summary>
		public int Line
		{
			get { return _line; }
		}


		#endregion


		#region [ Override Implementation of SystemException ]


		/// <summary>
		/// Gets an extended description of why this exception was thrown.
		/// </summary>
		public override string Message
		{
			get
			{
				string baseMsg = base.Message;
				string f = (_filename == null) ? String.Empty : _filename;
				string l = (_line == 0) ? String.Empty : (" line " + _line);

				return baseMsg + " (" + f + l + ")";
			}
		}


		/// <summary>
		/// GetObjectData.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}


		#endregion

	}

}
