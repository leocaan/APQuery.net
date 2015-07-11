using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'DELETE' command extensions.
	/// </summary>
	public static class APSqlDeleteCommandExtensions
	{

		#region [ 'FROM' Extensions ]


		/// <summary>
		/// SQL 'FROM' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'FROM' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlDeleteCommand from(this APSqlDeleteCommand command, APSqlFromClause clause)
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
		public static APSqlDeleteCommand from(this APSqlDeleteCommand command, APSqlFromPhrase phrase)
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
		public static APSqlDeleteCommand from(this APSqlDeleteCommand command, params APSqlFromPhrase[] phrases)
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
		public static APSqlDeleteCommand from(this APSqlDeleteCommand command, IEnumerable<APSqlFromPhrase> phrases)
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
		public static APSqlDeleteCommand from_add(this APSqlDeleteCommand command, APSqlFromPhrase phrase)
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
		public static APSqlDeleteCommand from_add(this APSqlDeleteCommand command, params APSqlFromPhrase[] phrases)
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
		public static APSqlDeleteCommand from_add(this APSqlDeleteCommand command, IEnumerable<APSqlFromPhrase> phrases)
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
		public static APSqlDeleteCommand where(this APSqlDeleteCommand command, APSqlWhereClause clause)
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
		public static APSqlDeleteCommand where(this APSqlDeleteCommand command, APSqlWherePhrase phrase)
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
		public static APSqlDeleteCommand where(this APSqlDeleteCommand command, params APSqlWherePhrase[] phrases)
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
		public static APSqlDeleteCommand where(this APSqlDeleteCommand command, IEnumerable<APSqlWherePhrase> phrases)
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
		public static APSqlDeleteCommand where(this APSqlDeleteCommand command, APSqlConditionJoinType joinType, params APSqlWherePhrase[] phrases)
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
		public static APSqlDeleteCommand where(this APSqlDeleteCommand command, APSqlConditionJoinType joinType, IEnumerable<APSqlWherePhrase> phrases)
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
		public static APSqlDeleteCommand where_and(this APSqlDeleteCommand command, APSqlWherePhrase phrase)
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
		public static APSqlDeleteCommand where_or(this APSqlDeleteCommand command, APSqlWherePhrase phrase)
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
		public static void execute(this APSqlDeleteCommand command, APDatabase db)
		{
			db.ExecuteNonQuery(command);
		}


		#endregion

	}

}
