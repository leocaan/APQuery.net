using System;
using System.Collections;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	internal class APRptFilterValuesParser
	{

		#region [ Inner Class Token ]


		class Token
		{
			public const int EOF = 257;
			public const int ERROR = 259;
			public const int SPLIT = 385;
			public const int STRING = 437;
		}


		#endregion


		#region [ Inner Class Tokenizer ]


		class Tokenizer
		{

			private string str;
			private string value;
			private int pos;
			private int begin;
			private int end;


			public Tokenizer(string str)
			{
				if (str == null)
					throw new ArgumentNullException("str");

				this.str = str;
				end = str.Length;
			}


			public string Value
			{
				get { return value; }
			}


			public int Begin
			{
				get { return begin; }
			}


			public int GetToken()
			{
				if (pos == end)
					return Token.EOF;

				char chr;
				value = null;

				while (pos != end)
				{
					chr = str[pos];

					if (chr == ',')
					{
						if (begin != pos)
						{
							value = str.Substring(begin, pos - begin).Trim();
							begin = pos;
							return Token.STRING;
						}
						else
						{
							begin = ++pos;
							return Token.SPLIT;
						}
					}
					else if (chr == '"')
					{
						if (begin != pos)
						{
							value = str.Substring(begin, pos - begin).Trim();
							begin = pos;
							return Token.STRING;
						}
						else
						{
							begin = ++pos;
							bool esc = false;
							while (pos != end)
							{
								chr = str[pos];
								if (chr == '"')
								{
									if (pos + 1 != end && str[pos + 1] == '"')
									{
										esc = true;
										pos++;
									}
									else
									{
										value = str.Substring(begin, pos - begin);
										if (esc)
										{
											value = value.Replace("\"\"", "\"");
										}
										begin = ++pos;
										return Token.STRING;
									}
								}

								pos++;
							}
							return Token.ERROR;
						}
					}

					pos++;
				}

				if (begin != pos)
				{
					value = str.Substring(begin, pos - begin).Trim();
					begin = pos;
					return Token.STRING;
				}

				return Token.EOF;
			}

		}


		#endregion


		public static string[] Parse(string valueExpression)
		{
			Tokenizer tokenizer = new Tokenizer(valueExpression);
			Queue tokens = new Queue();


			int token;
			while ((token = tokenizer.GetToken()) != Token.EOF)
			{
				if (token == Token.ERROR)
					throw new APRptFilterParseException(APResource.APRptFilter_ParseError);

				tokens.Enqueue(token);

				if (token == Token.STRING)
					tokens.Enqueue(tokenizer.Value);
			}


			int tPre = Token.SPLIT, tCur;
			string v;
			List<string> arr = new List<string>();


			while (tokens.Count > 0)
			{
				tCur = (int)tokens.Dequeue();


				if (tCur == Token.STRING)
				{
					v = (string)tokens.Dequeue();

					if (tPre == Token.SPLIT)
					{
						arr.Add(v);
					}
					else if (v != "")
					{
						throw new APRptFilterParseException(APResource.APRptFilter_ParseError);
					}
				}
				else
				{
					if (tPre == Token.SPLIT)
					{
						arr.Add("");
					}
				}

				tPre = tCur;
			}


			if (tPre == Token.SPLIT)
			{
				arr.Add("");
			}


			return arr.ToArray();
		}

	}

}
