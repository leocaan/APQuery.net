using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// APGen exception.
	/// </summary>
	[Serializable]
	public class APGenException : SystemException
	{

		#region [ Static Methods ]


		/// <summary>
		/// Get filename with xml reader.
		/// </summary>
		/// <param name="reader">Xml reader.</param>
		/// <returns>The filename of the xml reader.</returns>
		public static string GetFilename(XmlReader reader)
		{
			if (reader is XmlTextReader)
				return ((XmlTextReader)reader).BaseURI;
			else
				return String.Empty;
		}


		/// <summary>
		/// Get line number with xml reader.
		/// </summary>
		/// <param name="reader">Xml reader.</param>
		/// <returns>The line nubmer of the xml reader.</returns>
		public static int GetLineNumber(XmlReader reader)
		{
			if (reader is XmlTextReader)
				return ((XmlTextReader)reader).LineNumber;
			else
				return 0;
		}


		/// <summary>
		/// Get filename with xml node.
		/// </summary>
		/// <param name="node">Xml node.</param>
		/// <returns>The filename of the xml node.</returns>
		public static string GetFilename(XmlNode node)
		{
			if (!(node is IAPGenXmlNode))
				return String.Empty;

			return ((IAPGenXmlNode)node).Filename;
		}


		/// <summary>
		/// Get line number with xml node.
		/// </summary>
		/// <param name="node">Xml node.</param>
		/// <returns>The line number of xml node.</returns>
		public static int GetLineNumber(XmlNode node)
		{
			if (!(node is IAPGenXmlNode))
				return 0;

			return ((IAPGenXmlNode)node).LineNumber;
		}


		#endregion


		#region [ Fields ]


		private string _bareMessage;
		private string _filename;
		private int _line;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new exception.
		/// </summary>
		public APGenException()
		{
		}


		/// <summary>
		/// Create a new exception with message.
		/// </summary>
		/// <param name="message">Message.</param>
		public APGenException(string message)
			: base(message)
		{
			_bareMessage = message;
		}


		/// <summary>
		/// Create a new exception with with serialization info and streaming context.
		/// </summary>
		/// <param name="info">Serialization info.</param>
		/// <param name="context">Streaming context.</param>
		protected APGenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_filename = info.GetString("APGen_Filename");
			_line = info.GetInt32("APGen_Line");
		}


		/// <summary>
		/// Create a new exception with message and inner exception.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner exception.</param>
		public APGenException(string message, Exception inner)
			: base(message, inner)
		{
		}


		/// <summary>
		/// Create a new exception with message and xml node.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="node">Xml node.</param>
		public APGenException(string message, XmlNode node)
			: this(message, null, GetFilename(node), GetLineNumber(node))
		{
		}


		/// <summary>
		/// Create a new exception with message, inner exception and xml node.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner exception.</param>
		/// <param name="node">Xml node.</param>
		public APGenException(string message, Exception inner, XmlNode node)
			: this(message, inner, GetFilename(node), GetLineNumber(node))
		{
		}


		/// <summary>
		/// Create a new exception with message and xml reader.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="reader">Xml reader.</param>
		public APGenException(string message, XmlReader reader)
			: this(message, null, GetFilename(reader), GetLineNumber(reader))
		{
		}


		/// <summary>
		/// Create a new exception with message, inner exception and xml reader.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner exception.</param>
		/// <param name="reader">Xml reader.</param>
		public APGenException(string message, Exception inner, XmlReader reader)
			: this(message, inner, GetFilename(reader), GetLineNumber(reader))
		{
		}


		/// <summary>
		/// Create a new exception with message, filename and line number.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="filename">Filename.</param>
		/// <param name="line">Line nubmer.</param>
		public APGenException(string message, string filename, int line)
			: this(message, null, filename, line)
		{
		}


		/// <summary>
		/// Create a new exception with message, inner exception, filename and line number.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner exception.</param>
		/// <param name="filename">Filename.</param>
		/// <param name="line">Line number.</param>
		public APGenException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			_bareMessage = message;
			_filename = filename;
			_line = line;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Bare message.
		/// </summary>
		public string BareMessage
		{
			get { return _bareMessage; }
		}


		/// <summary>
		/// Filename.
		/// </summary>
		public string Filename
		{
			get { return _filename; }
		}


		/// <summary>
		/// Line number.
		/// </summary>
		public int Line
		{
			get { return _line; }
		}


		#endregion


		#region [ Override Implementation of SystemException ]


		/// <summary>
		/// Message.
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
		/// When overridden in a derived class, sets the SerializationInfo with information about the exception.
		/// </summary>
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("APGen_Filename", _filename);
			info.AddValue("APGen_Line", _line);
		}


		#endregion

	}
}