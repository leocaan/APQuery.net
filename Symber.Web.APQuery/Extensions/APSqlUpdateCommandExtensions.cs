using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'UPDATE' command extensions.
	/// </summary>
	public static class APSqlUpdateCommandExtensions
	{

		#region [ 'VALUES' Extensions ]


		/// <summary>
		/// SQL 'VALUES' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'VALUES' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand values(this APSqlUpdateCommand command, APSqlValuesClause clause)
		{
			command.ValuesClause = clause;
			return command;
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'SET' phrase</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand values(this APSqlUpdateCommand command, APSqlSetPhrase phrase)
		{
			command.ValuesClause = new APSqlValuesClause(phrase);
			return command;
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'SET' phrases</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand values(this APSqlUpdateCommand command, params APSqlSetPhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.ValuesClause = new APSqlValuesClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'SET' phrases</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand values(this APSqlUpdateCommand command, IEnumerable<APSqlSetPhrase> phrases)
		{
			if (phrases != null)
				command.ValuesClause = new APSqlValuesClause(phrases);
			return command;
		}


		#endregion


		#region [ 'SET' Extensions ]


		/// <summary>
		/// SQL 'VALUES' clause extensions. Add new 'SET' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'SET' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand set(this APSqlUpdateCommand command, APSqlSetPhrase phrase)
		{
			if (command.ValuesClause == null || command.ValuesClause.Next == null)
			{
				command.ValuesClause = new APSqlValuesClause(phrase);
			}
			else
			{
				APSqlSetPhrase exist = command.ValuesClause.Last as APSqlSetPhrase;
				exist.SetNext(phrase);

			}

			return command;
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions. Add new 'SET' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'SET' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand set(this APSqlUpdateCommand command, params APSqlSetPhrase[] phrases)
		{
			if (command.ValuesClause == null || command.ValuesClause.Next == null)
			{
				command.ValuesClause = new APSqlValuesClause(phrases);
			}
			else
			{
				APSqlSetPhrase exist = command.ValuesClause.Last as APSqlSetPhrase;
				exist.SetNext(phrases);

			}

			return command;
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions. Add new 'SET' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'SET' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand set(this APSqlUpdateCommand command, IEnumerable<APSqlSetPhrase> phrases)
		{
			if (command.ValuesClause == null || command.ValuesClause.Next == null)
			{
				command.ValuesClause = new APSqlValuesClause(phrases);
			}
			else
			{
				APSqlSetPhrase exist = command.ValuesClause.Last as APSqlSetPhrase;
				exist.SetNext(phrases);

			}

			return command;
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions. Add new 'SET' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="assignmentExpr">SQL assignment Expression.</param>
		/// <param name="value">Value.</param>
		/// <param name="paramName">Parameter name.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand set(this APSqlUpdateCommand command, APSqlColumnExpr assignmentExpr, object value, string paramName)
		{
			return set(command, new APSqlSetPhrase(assignmentExpr, value, paramName));
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions. Add new 'SET' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="assignmentExpr">SQL assignment Expression.</param>
		/// <param name="value">Value.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand set(this APSqlUpdateCommand command, APSqlColumnExpr assignmentExpr, object value)
		{
			return set(command, new APSqlSetPhrase(assignmentExpr, value));
		}


		/// <summary>
		/// SQL 'VALUES' clause extensions. Add new 'SET' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="assignmentExpr">SQL assignment Expression.</param>
		/// <param name="valueExpr">SQL value of assignment Expression.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand set(this APSqlUpdateCommand command, APSqlColumnExpr assignmentExpr, IAPSqlValueExpr valueExpr)
		{
			return set(command, new APSqlSetPhrase(assignmentExpr, valueExpr));
		}


		#endregion


		#region [ 'FROM' Extensions ]


		/// <summary>
		/// SQL 'FROM' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'FROM' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from(this APSqlUpdateCommand command, APSqlFromClause clause)
		{
			command.FromClause = clause;
			return command;
		}


		/// <summary>
		/// SQL 'FROM' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'FROM' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from(this APSqlUpdateCommand command, APSqlFromPhrase phrase)
		{
			command.FromClause = new APSqlFromClause(phrase);
			return command;
		}


		/// <summary>
		/// SQL 'FROM' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'FROM' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from(this APSqlUpdateCommand command, params APSqlFromPhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.FromClause = new APSqlFromClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'FROM' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'FROM' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from(this APSqlUpdateCommand command, IEnumerable<APSqlFromPhrase> phrases)
		{
			if (phrases != null)
				command.FromClause = new APSqlFromClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'FROM' clause extensions. Add new 'FROM' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'FROM' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from_add(this APSqlUpdateCommand command, APSqlFromPhrase phrase)
		{
			if (command.FromClause == null || command.FromClause.Next == null)
			{
				command.FromClause = new APSqlFromClause(phrase);
			}
			else
			{
				APSqlFromPhrase exist = command.FromClause.Last as APSqlFromPhrase;
				exist.SetNext(phrase);
			}

			return command;
		}


		/// <summary>
		/// SQL 'FROM' clause extensions. Add new 'FROM' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'FROM' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from_add(this APSqlUpdateCommand command, params APSqlFromPhrase[] phrases)
		{
			if (command.FromClause == null || command.FromClause.Next == null)
			{
				command.FromClause = new APSqlFromClause(phrases);
			}
			else
			{
				APSqlFromPhrase exist = command.FromClause.Last as APSqlFromPhrase;
				exist.SetNext(phrases);
			}

			return command;
		}


		/// <summary>
		/// SQL 'FROM' clause extensions. Add new 'FROM' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'FROM' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand from_add(this APSqlUpdateCommand command, IEnumerable<APSqlFromPhrase> phrases)
		{
			if (command.FromClause == null || command.FromClause.Next == null)
			{
				command.FromClause = new APSqlFromClause(phrases);
			}
			else
			{
				APSqlFromPhrase exist = command.FromClause.Last as APSqlFromPhrase;
				exist.SetNext(phrases);
			}

			return command;
		}


		#endregion


		#region [ 'WHERE' Extensions ]


		/// <summary>
		/// SQL 'WHERE' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'WHERE' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where(this APSqlUpdateCommand command, APSqlWhereClause clause)
		{
			command.WhereClause = clause;
			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'WHERE' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where(this APSqlUpdateCommand command, APSqlWherePhrase phrase)
		{
			command.WhereClause = new APSqlWhereClause(phrase);
			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'WHERE' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where(this APSqlUpdateCommand command, params APSqlWherePhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.WhereClause = new APSqlWhereClause(APSqlConditionJoinType.AND, phrases);
			else
				command.WhereClause = null;
			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'WHERE' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where(this APSqlUpdateCommand command, IEnumerable<APSqlWherePhrase> phrases)
		{
			if (phrases != null)
				command.WhereClause = new APSqlWhereClause(APSqlConditionJoinType.AND, phrases);
			else
				command.WhereClause = null;
			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="joinType">Join type.</param>
		/// <param name="phrases">The 'WHERE' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where(this APSqlUpdateCommand command, APSqlConditionJoinType joinType, params APSqlWherePhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.WhereClause = new APSqlWhereClause(joinType, phrases);
			else
				command.WhereClause = null;
			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="joinType">Join type.</param>
		/// <param name="phrases">The 'WHERE' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where(this APSqlUpdateCommand command, APSqlConditionJoinType joinType, IEnumerable<APSqlWherePhrase> phrases)
		{
			if (phrases != null)
				command.WhereClause = new APSqlWhereClause(joinType, phrases);
			else
				command.WhereClause = null;
			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions. Add new condition join 'AND' with exist condition.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The new 'WHERE' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where_and(this APSqlUpdateCommand command, APSqlWherePhrase phrase)
		{
			if (command.WhereClause == null || command.WhereClause.Next == null)
			{
				command.WhereClause = new APSqlWhereClause(phrase);
			}
			else
			{
				APSqlWherePhrase exist = command.WhereClause.Next as APSqlWherePhrase;
				if (exist is APSqlConditionAndPhrase)
					(exist as APSqlConditionAndPhrase).Child.Last.SetNext(phrase);
				else
					command.WhereClause = new APSqlWhereClause(APSqlConditionJoinType.AND, exist, phrase);
			}

			return command;
		}


		/// <summary>
		/// SQL 'WHERE' phrase extensions. Add new condition join 'OR' with exist condition.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The new 'WHERE' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand where_or(this APSqlUpdateCommand command, APSqlWherePhrase phrase)
		{
			if (command.WhereClause == null || command.WhereClause.Next == null)
			{
				command.WhereClause = new APSqlWhereClause(phrase);
			}
			else
			{
				APSqlWherePhrase exist = command.WhereClause.Next as APSqlWherePhrase;
				if (exist is APSqlConditionOrPhrase)
					(exist as APSqlConditionOrPhrase).Child.Last.SetNext(phrase);
				else
					command.WhereClause = new APSqlWhereClause(APSqlConditionJoinType.OR, exist, phrase);
			}

			return command;
		}


		#endregion


		#region [ Execute Extensions ]


		/// <summary>
		/// Execute.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="db">The database.</param>
		public static void execute(this APSqlUpdateCommand command, APDatabase db)
		{
			db.ExecuteNonQuery(command);
		}


		#endregion

	}

}
