using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Symber.Web.Data
{

	public abstract partial class APQueryParser
	{

		/// <summary>
		/// Parser writer.
		/// </summary>
		public class ParserWriter : StringWriter
		{

			#region [ Fields ]


			private int _idented;
			private Dictionary<string, int> _parameterNameDict;
			private bool _cmdNameSuitable;


			#endregion


			#region [ Constructors ]


			/// <summary>
			/// Create a new instance of ParserWriter.
			/// </summary>
			/// <param name="sb">String builder</param>
			/// <param name="commandNameSuitable">Enable support suitable command name.</param>
			public ParserWriter(StringBuilder sb, bool commandNameSuitable)
				: base(sb)
			{
				_cmdNameSuitable = commandNameSuitable;
			}


			#endregion


			#region [ Properties ]


			/// <summary>
			/// Idented.
			/// </summary>
			public int Idented
			{
				get { return _idented; }
				set { _idented = value; }
			}


			#endregion


			#region [ Methods ]


			/// <summary>
			/// Write direct.
			/// </summary>
			/// <param name="value">Value.</param>
			public void WriteDirect(string value)
			{
				base.Write(value);
			}

			/// <summary>
			/// Get suitable SQL command parameter name.
			/// </summary>
			/// <param name="originalName">Original name.</param>
			/// <returns>Suitable name.</returns>
			public string GetSuitableParameterName(string originalName)
			{
				if (!_cmdNameSuitable)
					return originalName;

				if (_parameterNameDict == null)
					_parameterNameDict = new Dictionary<string, int>();

				if (_parameterNameDict.ContainsKey(originalName))
				{
					return originalName + (++_parameterNameDict[originalName]);
				}
				else
				{
					_parameterNameDict.Add(originalName, 0);
					return originalName;
				}
			}


			/// <summary>
			/// Clear suitable dictionary, used with paging query mode.
			/// </summary>
			public void ResetSuitable()
			{
				if (_parameterNameDict != null)
					_parameterNameDict.Clear();
			}


			#endregion


			#region [ Override Implementation of StringWriter ]


			/// <summary>
			/// Write.
			/// </summary>
			/// <param name="value">Value.</param>
			public override void Write(string value)
			{
				base.Write(" ");
				base.Write(value);
			}


			/// <summary>
			/// Write.
			/// </summary>
			/// <param name="format">Format.</param>
			/// <param name="arg0">Argument.</param>
			public override void Write(string format, object arg0)
			{
				base.Write(" ");
				base.Write(format, arg0);
			}


			/// <summary>
			/// Write.
			/// </summary>
			/// <param name="format">Format.</param>
			/// <param name="arg0">Argument.</param>
			/// <param name="arg1">Argument.</param>
			public override void Write(string format, object arg0, object arg1)
			{
				base.Write(" ");
				base.Write(format, arg0, arg1);
			}


			/// <summary>
			/// Write.
			/// </summary>
			/// <param name="format">Format.</param>
			/// <param name="arg0">Argument.</param>
			/// <param name="arg1">Argument.</param>
			/// <param name="arg2">Argument.</param>
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				base.Write(" ");
				base.Write(format, arg0, arg1, arg2);
			}


			/// <summary>
			/// Write.
			/// </summary>
			/// <param name="format">Format.</param>
			/// <param name="arg">Arument array.</param>
			public override void Write(string format, params object[] arg)
			{
				base.Write(" ");
				base.Write(format, arg);
			}


			/// <summary>
			/// Write line.
			/// </summary>
			public override void WriteLine()
			{
				base.WriteLine();
				base.Write("".PadLeft(_idented, '\t'));
			}


			#endregion

		}

	}

}
