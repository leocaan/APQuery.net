using Symber.Web.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Build a Rpt Condition to run.
	/// </summary>
	public class APRptConditionBuilder
	{

		#region [ Static Methods ]


		/// <summary>
		/// Parse Filter values.
		/// </summary>
		/// <param name="valueExpression">The values expression.</param>
		/// <returns>The values.</returns>
		public static string[] ParseValues(string valueExpression)
		{
			return APRptFilterValuesParser.Parse(valueExpression);
		}


		/// <summary>
		/// Parse Filter condition to RPN Expression.
		/// </summary>
		/// <param name="conditionExpression">The condition expression.</param>
		/// <param name="checkIdentifier">The delegate that check validate identifier.</param>
		/// <returns></returns>
		public static string ParseLogicRpn(string conditionExpression, Func<object, bool> checkIdentifier)
		{
			return new APRptLogicParser(conditionExpression, checkIdentifier)
				.Parser(false)
				.GetDef()
				.TrimStart();
		}


		#endregion


		#region [ Fields ]


		private APRptConditionDef _def;
		private APRptColumnCollection _avaliableColumns;
		private Dictionary<string, APRptFilter> _filters;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptConditionBuilder.
		/// </summary>
		/// <param name="def">The condition defined of Rpt.</param>
		/// <param name="avaliableColumns">The avaliable columns.</param>
		public APRptConditionBuilder(APRptConditionDef def, APRptColumnCollection avaliableColumns)
		{
			if (def == null)
				throw new ArgumentNullException("def");

			_def = def;
			_avaliableColumns = avaliableColumns;
			_filters = new Dictionary<string, APRptFilter>();


			foreach (APRptFilterDef filter in def.Filters)
			{
				_filters.Add(filter.Serial, new APRptFilter(
					_avaliableColumns[filter.ColumnId],
					filter.Comparator,
					ParseValues(filter.Values)));
			}
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Avaliable APRptColumns.
		/// </summary>
		public APRptColumnCollection AvaliableColumns
		{
			get
			{
				return _avaliableColumns;
			}
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Build SQL 'WHERE' phrase.
		/// </summary>
		/// <returns>The APSqlWherePhrase.</returns>
		public virtual APSqlWherePhrase BuildCondition()
		{
			if (_filters.Count == 0)
				return null;

			string rpnCode = _def.Rpn;


			// If RPN expression is empty, return 'AND' of all filter.
			if (rpnCode == "")
			{
				List<APSqlWherePhrase> list = new List<APSqlWherePhrase>();

				foreach (var filter in _filters.Values)
				{
					list.Add(filter.ParseQueryWherePhrase());
				}

				return new APSqlConditionAndPhrase(list);

			}

			// Has RPN expression, build.

			Stack<APSqlWherePhrase> stack = new Stack<APSqlWherePhrase>();
			APSqlWherePhrase left, right;

			foreach (string token in rpnCode.Split(' '))
			{
				if (token == "&")
				{
					right = stack.Pop();
					left = stack.Pop();
					stack.Push(new APSqlConditionAndPhrase(left, right));
				}
				else if (token == "|")
				{
					right = stack.Pop();
					left = stack.Pop();
					stack.Push(new APSqlConditionOrPhrase(left, right));
				}
				else if (token == "!")
				{
					right = stack.Pop();
					right.IsNot = !right.IsNot;
					stack.Push(right);
				}
				else
				{
					if (_filters.ContainsKey(token))
						stack.Push(_filters[token].ParseQueryWherePhrase());
					else
						throw new APRptConditionParseException(APResource.GetString(APResource.APRptFilter_LogicSerialNotFound, token));
				}
			}

			return stack.Pop();
		}


		#endregion

	}

}
