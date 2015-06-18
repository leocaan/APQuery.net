using Symber.Web.Cdss;
using System;
using System.Collections.Generic;
using System.Text;

namespace Symber.Web.Report
{

	internal class APRptLogicParser
	{

		#region [ Inner Class Token ]


		class Token
		{
			public const int EOF = 257;
			public const int NONE = 258;
			public const int ERROR = 259;
			public const int OPEN_PARENS = 368;
			public const int CLOSE_PARENS = 369;
			public const int LOGIC_NOT = 377;
			public const int BITWISE_AND = 383;
			public const int BITWISE_OR = 384;
			public const int INTEGER = 436;
			public const int IDENTIFIER = 437;
		}


		#endregion


		#region [ Inner Class Tokenizer ]


		class Tokenizer
		{

			#region [ Static & Const ]


			const int MaxIDSize = 512;
			const int MaxNumberSize = 512;
			private static Dictionary<string, int> keywords;


			private static bool IsIdentifierStartCharacter(char chr)
			{
				return chr == '_' || Char.IsLetter(chr);
			}


			private static bool IsIdentifierPartCharacter(char chr)
			{
				return chr == '_' || Char.IsLetter(chr) || Char.IsNumber(chr);
			}


			#endregion


			#region [ Fields ]


			private ExpressionReader _reader;

			private int _putbackChar;
			private object _value;

			private bool _tokensSeen = false;
			private bool _anyTokenSeen = false;

			private char[] _idBuilder = new char[MaxIDSize];
			private char[] _numberBuilder = new char[MaxNumberSize];
			private int _numberPos;
			private StringBuilder _stringBuilder;


			#endregion


			#region [ Constructors ]


			static Tokenizer()
			{
				keywords = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

				keywords.Add("and", Token.BITWISE_AND);
				keywords.Add("or", Token.BITWISE_OR);
				keywords.Add("not", Token.LOGIC_NOT);
			}


			public Tokenizer(ExpressionReader reader)
			{
				_reader = reader;
				_putbackChar = 0;
				_stringBuilder = new StringBuilder();
			}


			#endregion


			#region [ Properties ]


			public bool EOF
			{
				get { return PeekChar() == -1; }
			}


			public object CurrentValue
			{
				get { return _value; }
			}


			#endregion


			#region [ Methods ]


			public int GetToken()
			{
				int token;
				int chr;

				_value = null;


				while ((chr = GetChar()) != -1)
				{

					#region [ Whitespace ]

					if (chr == '\t' || chr == ' ' || chr == '\f' || chr == '\v' || chr == 0xa0 || chr == 0)
						continue;

					if (chr == '\r')
					{
						if (PeekChar() == '\n')
							GetChar();

						_anyTokenSeen |= _tokensSeen;
						_tokensSeen = false;
						continue;
					}

					#endregion


					#region [ Identifier ]


					if (IsIdentifierStartCharacter((char)chr))
					{
						_tokensSeen = true;
						return ConsumeIdentifier(chr);
					}


					#endregion


					#region [ Punctuation ]


					if ((token = IsPunct((char)chr)) != Token.ERROR)
					{
						_tokensSeen = true;
						return token;
					}


					#endregion


					#region [ Whitespace ]


					if (chr == '\n')
					{
						_anyTokenSeen |= _tokensSeen;
						_tokensSeen = false;
						continue;
					}


					#endregion


					#region [ Number ]


					if (chr >= '0' && chr <= '9')
					{
						_tokensSeen = true;
						return IsNumber(chr);
					}


					#endregion


					return Token.ERROR;
				}

				return Token.EOF;
			}


			private int GetChar()
			{
				int chr;

				if (_putbackChar != -1)
				{
					chr = _putbackChar;
					_putbackChar = -1;
				}
				else
				{
					chr = _reader.Read();
				}

				return chr;
			}


			private int PeekChar()
			{
				if (_putbackChar != -1)
					return _putbackChar;

				_putbackChar = _reader.Read();
				return _putbackChar;
			}


			private int PeekChar2()
			{
				if (_putbackChar != -1)
					return _putbackChar;

				return _reader.Peek();
			}


			private void Putback(int chr)
			{
				if (_putbackChar != -1)
					throw new Exception("This should not happen putback on putback.");

				_putbackChar = chr;
			}


			private int ConsumeIdentifier(int chr)
			{
				int pos = 1;
				int d2 = -1;

				_idBuilder[0] = (char)chr;

				while ((d2 = GetChar()) != -1)
				{
					if (IsIdentifierPartCharacter((char)d2))
					{
						if (pos == MaxIDSize)
							return Token.ERROR;

						_idBuilder[pos++] = (char)d2;
					}
					else
					{
						Putback(d2);
						break;
					}
				}


				//
				// Optimization: avoids doing the keyword lookup on uppercase letters and _
				//
				int keyword = GetKeyword(_idBuilder, pos);
				if (keyword != -1)
					return keyword;

				_value = new String(_idBuilder, 0, pos);
				return Token.IDENTIFIER;
			}


			private int GetKeyword(char[] id, int len)
			{
				string key = new String(id, 0, len);

				if (keywords.ContainsKey(key))
					return keywords[key];

				return -1;
			}


			private int IsPunct(char chr)
			{
				switch (chr)
				{
					case '(': return Token.OPEN_PARENS;
					case ')': return Token.CLOSE_PARENS;
				}

				return Token.ERROR;
			}


			private int IsNumber(int chr)
			{
				int d2;

				_numberPos = 0;
				_numberBuilder[_numberPos++] = (char)chr;

				while ((d2 = PeekChar2()) != -1)
				{
					if (d2 >= '0' && d2 <= '9')
					{
						_numberBuilder[_numberPos++] = (char)d2;
						GetChar();
					}
					else
					{
						if (IsIdentifierStartCharacter((char)d2))
							return Token.ERROR;
						break;
					}
				}

				if (_numberPos <= 9)
				{
					int ui = (int)(_numberBuilder[0] - '0');
					for (int i = 1; i < _numberPos; i++)
						ui = (ui * 10) + ((int)(_numberBuilder[i] - '0'));
					_value = ui;
					return Token.INTEGER;
				}

				return Token.ERROR;
			}


			#endregion

		}


		#endregion


		#region [ Fields ]


		private Tokenizer _lexer;
		private Func<object, bool> _checkIdentifier;


		#endregion


		#region [ Constructors ]


		public APRptLogicParser(string conditionExpression, Func<object, bool> checkIdentifier)
		{
			_lexer = new Tokenizer(new ExpressionReader(conditionExpression));
			_checkIdentifier = checkIdentifier;
		}


		#endregion


		#region [ Methods ]


		private bool IsUncompletedRpnUnit(RptUnit rpnUnit)
		{
			return rpnUnit is IRightRpnUnit && !(rpnUnit as IRightRpnUnit).IsCompleted;
		}


		public RptUnit Parser(bool needParens)
		{
			int token;
			RptUnit rpnUnit = null, cur;
			bool meedParens = false;


			while ((token = _lexer.GetToken()) != Token.EOF)
			{
				cur = null;

				if (token == Token.INTEGER)
				{
					int number = (int)_lexer.CurrentValue;

					if (_checkIdentifier != null && !_checkIdentifier(number))
						throw new APRptConditionParseException(APResource.GetString(APResource.APRptFilter_LogicIdentifierInvalidate, number));

					cur = new OperandRpnUnit(number.ToString());
				}
				else if (token == Token.IDENTIFIER)
				{
					string identifier = (string)_lexer.CurrentValue;

					if (_checkIdentifier != null && !_checkIdentifier(identifier))
						throw new APRptConditionParseException(APResource.GetString(APResource.APRptFilter_LogicIdentifierInvalidate, identifier));

					cur = new OperandRpnUnit(identifier);

				}
				else if (token == Token.BITWISE_AND || token == Token.BITWISE_OR)
				{
					if (rpnUnit == null || IsUncompletedRpnUnit(rpnUnit))
						throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);

					cur = new BinaryRpnUnit(rpnUnit, token == Token.BITWISE_AND ? RpnOperator.AND : RpnOperator.OR);

					rpnUnit = null;
				}
				else if (token == Token.LOGIC_NOT)
				{
					if (rpnUnit != null && !IsUncompletedRpnUnit(rpnUnit))
						throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);

					cur = new UnaryNotRpnUnit();
				}
				else if (token == Token.OPEN_PARENS)
				{
					cur = Parser(true);
				}
				else if (token == Token.CLOSE_PARENS)
				{
					meedParens = true;

					if (!needParens)
						throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);

					break;
				}
				else
				{
					throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);
				}

				if (cur == null)
					throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);

				if (rpnUnit == null)
					rpnUnit = cur;
				else if (IsUncompletedRpnUnit(rpnUnit))
					(rpnUnit as IRightRpnUnit).SetRight(cur);
				else
					throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);
			}

			if (needParens && !meedParens)
				throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);

			if (IsUncompletedRpnUnit(rpnUnit))
				throw new APRptConditionParseException(APResource.APRptFilter_LogicExpressionInvalidate);

			return rpnUnit;
		}


		#endregion

	}

}
