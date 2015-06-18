using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'INSERT' command extensions.
	/// </summary>
	public static class APSqlInsertCommandExtensions
	{

		#region [ 'VALUES' Extensions ]


		/// <summary>
		/// SQL 'VALUES' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'VALUES' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlInsertCommand values(this APSqlInsertCommand command, APSqlValuesClause clause)
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
		public static APSqlInsertCommand values(this APSqlInsertCommand command, APSqlSetPhrase phrase)
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
		public static APSqlInsertCommand values(this APSqlInsertCommand command, params APSqlSetPhrase[] phrases)
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
		public static APSqlInsertCommand values(this APSqlInsertCommand command, IEnumerable<APSqlSetPhrase> phrases)
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
		public static APSqlInsertCommand set(this APSqlInsertCommand command, APSqlSetPhrase phrase)
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
		public static APSqlInsertCommand set(this APSqlInsertCommand command, params APSqlSetPhrase[] phrases)
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
		public static APSqlInsertCommand set(this APSqlInsertCommand command, IEnumerable<APSqlSetPhrase> phrases)
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
		public static APSqlInsertCommand set(this APSqlInsertCommand command, APSqlColumnExpr assignmentExpr, object value, string paramName)
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
		public static APSqlInsertCommand set(this APSqlInsertCommand command, APSqlColumnExpr assignmentExpr, object value)
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
		public static APSqlInsertCommand set(this APSqlInsertCommand command, APSqlColumnExpr assignmentExpr, IAPSqlValueExpr valueExpr)
		{
			return set(command, new APSqlSetPhrase(assignmentExpr, valueExpr));
		}


		#endregion


		#region [ SubQuery Extensions ]


		/// <summary>
		/// SQL SubQuery clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="subQuery">SubQuery.</param>
		/// <param name="clause">The 'SELECT' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlInsertCommand subQuery(this APSqlInsertCommand command, APSqlSelectCommand subQuery, APSqlSelectClause clause)
		{
			command.SubQuery = subQuery;
			command.SelectClause = clause;
			return command;
		}


		/// <summary>
		/// SQL SubQuery clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="subQuery">SubQuery.</param>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlInsertCommand subQuery(this APSqlInsertCommand command, APSqlSelectCommand subQuery, params APSqlSelectPhrase[] phrases)
		{
			command.SubQuery = subQuery;
			command.SelectClause = new APSqlSelectClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL SubQuery clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="subQuery">SubQuery.</param>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlInsertCommand subQuery(this APSqlInsertCommand command, APSqlSelectCommand subQuery, IEnumerable<APSqlSelectPhrase> phrases)
		{
			command.SubQuery = subQuery;
			command.SelectClause = new APSqlSelectClause(phrases);
			return command;
		}


		#endregion


		#region [ Execute Extensions ]


		/// <summary>
		/// Execute.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="db">The database.</param>
		public static void execute(this APSqlInsertCommand command, APDatabase db)
		{
			db.ExecuteNonQuery(command);
		}

		/// <summary>
		/// Execute.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="db">The database.</param>
		/// <returns>The scale.</returns>
		public static object executeScale(this APSqlInsertCommand command, APDatabase db)
		{
			return db.ExecuteScalar(command);
		}


		#endregion

	}

}
