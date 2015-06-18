using System;

namespace Symber.Web.Cdss
{

	internal class ExpressionReader
	{

		#region [ Fields ]


		private string _expression;
		private int _nextChar;
		private int _length;


		#endregion


		#region [ Constructors ]


		public ExpressionReader(string expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			_expression = expression;
			_nextChar = 0;
			_length = _expression.Length;
		}


		#endregion


		#region [ Properties ]


		public bool IsEnd
		{
			get { return _nextChar > _length; }
		}


		public string Remain
		{
			get { return _expression.Substring(_nextChar); }
		}


		public int Position
		{
			get { return _nextChar; }
			set { _nextChar = value; }
		}


		#endregion


		#region [ Methods ]


		public int Peek()
		{
			if (_nextChar >= _length)
				return -1;
			return (int)_expression[_nextChar];
		}


		public int Read()
		{
			if (_nextChar >= _length)
				return -1;
			return (int)_expression[_nextChar++];
		}


		public string ReadToEnd()
		{
			string toEnd = _expression.Substring(_nextChar, _length - _nextChar);
			_nextChar = _length;
			return toEnd;
		}


		public void Seek(int count)
		{
			if (_nextChar + count > _length)
				throw new IndexOutOfRangeException();
			_nextChar += count;
		}


		#endregion
	}

}
