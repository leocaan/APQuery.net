using System;
using System.Collections.Generic;
using System.Data;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'SELECT' command extensions.
	/// </summary>
	public static class APSqlSelectCommandExtensions
	{

		#region [ 'SELECT' mode Extensions ]


		/// <summary>
		/// SQL 'SELECT' mode extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="selectMode">The 'SELECT' mode.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand mode(this APSqlSelectCommand command, APSqlSelectMode selectMode)
		{
			command.SelectMode = selectMode;
			return command;
		}


		#endregion


		#region [ 'FROM' Extensions ]


		/// <summary>
		/// SQL 'FROM' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'FROM' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand from(this APSqlSelectCommand command, APSqlFromClause clause)
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
		public static APSqlSelectCommand from(this APSqlSelectCommand command, APSqlFromPhrase phrase)
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
		public static APSqlSelectCommand from(this APSqlSelectCommand command, params APSqlFromPhrase[] phrases)
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
		public static APSqlSelectCommand from(this APSqlSelectCommand command, IEnumerable<APSqlFromPhrase> phrases)
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
		public static APSqlSelectCommand from_add(this APSqlSelectCommand command, APSqlFromPhrase phrase)
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
		public static APSqlSelectCommand from_add(this APSqlSelectCommand command, params APSqlFromPhrase[] phrases)
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
		public static APSqlSelectCommand from_add(this APSqlSelectCommand command, IEnumerable<APSqlFromPhrase> phrases)
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
		public static APSqlSelectCommand where(this APSqlSelectCommand command, APSqlWhereClause clause)
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
		public static APSqlSelectCommand where(this APSqlSelectCommand command, APSqlWherePhrase phrase)
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
		public static APSqlSelectCommand where(this APSqlSelectCommand command, params APSqlWherePhrase[] phrases)
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
		public static APSqlSelectCommand where(this APSqlSelectCommand command, IEnumerable<APSqlWherePhrase> phrases)
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
		public static APSqlSelectCommand where(this APSqlSelectCommand command, APSqlConditionJoinType joinType, params APSqlWherePhrase[] phrases)
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
		public static APSqlSelectCommand where(this APSqlSelectCommand command, APSqlConditionJoinType joinType, IEnumerable<APSqlWherePhrase> phrases)
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
		public static APSqlSelectCommand where_and(this APSqlSelectCommand command, APSqlWherePhrase phrase)
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
		public static APSqlSelectCommand where_or(this APSqlSelectCommand command, APSqlWherePhrase phrase)
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


		#region [ '[NOT] EXISTS' Extensions ]


		/// <summary>
		/// SQL 'EXIST sub-query' phrase extensions.
		/// </summary>
		/// <param name="command">The Sub query.</param>
		/// <returns>The 'WHERE' phrase</returns>
		public static APSqlConditionPhrase exist(this APSqlSelectCommand command)
		{
			return new APSqlConditionPhrase(command, APSqlConditionOperator.Exists);
		}


		/// <summary>
		/// SQL 'NOT EXIST sub-query' phrase extensions.
		/// </summary>
		/// <param name="command">The Sub query.</param>
		/// <returns>The 'WHERE' phrase</returns>
		public static APSqlConditionPhrase notexist(this APSqlSelectCommand command)
		{
			return new APSqlConditionPhrase(command, APSqlConditionOperator.NotExists);
		}


		#endregion


		#region [ 'GROUP BY' Extensions ]


		/// <summary>
		/// SQL 'GROUP BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'GROUP BY' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by(this APSqlSelectCommand command, APSqlGroupByClause clause)
		{
			command.GroupByClause = clause;
			return command;
		}


		/// <summary>
		/// SQL 'GROUP BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The Expression phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by(this APSqlSelectCommand command, APSqlExprPhrase phrase)
		{
			command.GroupByClause = new APSqlGroupByClause(phrase);
			return command;
		}


		/// <summary>
		/// SQL 'GROUP BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The Expression phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by(this APSqlSelectCommand command, params APSqlExprPhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.GroupByClause = new APSqlGroupByClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'GROUP BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The Expression phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by(this APSqlSelectCommand command, IEnumerable<APSqlExprPhrase> phrases)
		{
			if (phrases != null)
				command.GroupByClause = new APSqlGroupByClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'GROUP BY' clause extensions. Add new 'GROUP BY' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The Expression phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by_add(this APSqlSelectCommand command, APSqlExprPhrase phrase)
		{
			if (command.GroupByClause == null || command.GroupByClause.Next == null)
			{
				command.GroupByClause = new APSqlGroupByClause(phrase);
			}
			else
			{
				APSqlExprPhrase exist = command.FromClause.Last as APSqlExprPhrase;
				exist.SetNext(phrase);
			}

			return command;
		}


		/// <summary>
		/// SQL 'GROUP BY' clause extensions. Add new 'GROUP BY' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The Expression phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by_add(this APSqlSelectCommand command, params APSqlExprPhrase[] phrases)
		{
			if (command.GroupByClause == null || command.GroupByClause.Next == null)
			{
				command.GroupByClause = new APSqlGroupByClause(phrases);
			}
			else
			{
				APSqlExprPhrase exist = command.FromClause.Last as APSqlExprPhrase;
				exist.SetNext(phrases);
			}

			return command;
		}


		/// <summary>
		/// SQL 'GROUP BY' clause extensions. Add new 'GROUP BY' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The Expression phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand group_by_add(this APSqlSelectCommand command, IEnumerable<APSqlExprPhrase> phrases)
		{
			if (command.GroupByClause == null || command.GroupByClause.Next == null)
			{
				command.GroupByClause = new APSqlGroupByClause(phrases);
			}
			else
			{
				APSqlExprPhrase exist = command.FromClause.Last as APSqlExprPhrase;
				exist.SetNext(phrases);
			}

			return command;
		}


		#endregion


		#region [ 'HAVING' Extensions ]


		/// <summary>
		/// SQL 'HAVING' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'HAVING' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having(this APSqlSelectCommand command, APSqlWhereClause clause)
		{
			command.HavingClause = clause;
			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'HAVING' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having(this APSqlSelectCommand command, APSqlWherePhrase phrase)
		{
			command.HavingClause = phrase == null ? null : new APSqlWhereClause(phrase);
			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'HAVING' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having(this APSqlSelectCommand command, params APSqlWherePhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.HavingClause = new APSqlWhereClause(APSqlConditionJoinType.AND, phrases);
			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The The IEnumerable phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having(this APSqlSelectCommand command, IEnumerable<APSqlWherePhrase> phrases)
		{
			if (phrases != null)
				command.HavingClause = new APSqlWhereClause(APSqlConditionJoinType.AND, phrases);
			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="joinType">Join type.</param>
		/// <param name="phrases">The 'HAVING' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having(this APSqlSelectCommand command, APSqlConditionJoinType joinType, params APSqlWherePhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.HavingClause = new APSqlWhereClause(joinType, phrases);
			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="joinType">Join type.</param>
		/// <param name="phrases">The 'HAVING' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having(this APSqlSelectCommand command, APSqlConditionJoinType joinType, IEnumerable<APSqlWherePhrase> phrases)
		{
			if (phrases != null)
				command.HavingClause = new APSqlWhereClause(joinType, phrases);
			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions. Add new condition join 'AND' with exist condition.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The new 'WHERE' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having_and(this APSqlSelectCommand command, APSqlWherePhrase phrase)
		{
			if (command.HavingClause == null || command.HavingClause.Next == null)
			{
				command.HavingClause = new APSqlWhereClause(phrase);
			}
			else
			{
				APSqlWherePhrase exist = command.HavingClause.Next as APSqlWherePhrase;
				if (exist is APSqlConditionAndPhrase)
					(exist as APSqlConditionAndPhrase).Child.Last.SetNext(phrase);
				else
					command.HavingClause = new APSqlWhereClause(APSqlConditionJoinType.AND, exist, phrase);
			}

			return command;
		}


		/// <summary>
		/// SQL 'HAVING' phrase extensions. Add new condition join 'OR' with exist condition.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The new 'WHERE' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand having_or(this APSqlSelectCommand command, APSqlWherePhrase phrase)
		{
			if (command.HavingClause == null || command.HavingClause.Next == null)
			{
				command.HavingClause = new APSqlWhereClause(phrase);
			}
			else
			{
				APSqlWherePhrase exist = command.HavingClause.Next as APSqlWherePhrase;
				if (exist is APSqlConditionOrPhrase)
					(exist as APSqlConditionOrPhrase).Child.Last.SetNext(phrase);
				else
					command.HavingClause = new APSqlWhereClause(APSqlConditionJoinType.OR, exist, phrase);
			}

			return command;
		}


		#endregion


		#region [ 'ORDER BY' Extensions ]


		/// <summary>
		/// SQL 'ORDER BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="clause">The 'ORDER BY' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by(this APSqlSelectCommand command, APSqlOrderByClause clause)
		{
			command.OrderByClause = clause;
			return command;
		}


		/// <summary>
		/// SQL 'ORDER BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'ORDER BY' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by(this APSqlSelectCommand command, APSqlOrderPhrase phrase)
		{
			command.OrderByClause = new APSqlOrderByClause(phrase);
			return command;
		}


		/// <summary>
		/// SQL 'ORDER BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'ORDER BY' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by(this APSqlSelectCommand command, params APSqlOrderPhrase[] phrases)
		{
			if (phrases != null && phrases.Length != 0)
				command.OrderByClause = new APSqlOrderByClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'ORDER BY' clause extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'ORDER BY' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by(this APSqlSelectCommand command, IEnumerable<APSqlOrderPhrase> phrases)
		{
			if (phrases != null)
				command.OrderByClause = new APSqlOrderByClause(phrases);
			return command;
		}


		/// <summary>
		/// SQL 'ORDER BY' clause extensions. Add new 'ORDER BY' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrase">The 'ORDER BY' phrase.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by_add(this APSqlSelectCommand command, APSqlOrderPhrase phrase)
		{
			if (command.OrderByClause == null || command.OrderByClause.Next == null)
			{
				command.OrderByClause = new APSqlOrderByClause(phrase);
			}
			else
			{
				APSqlOrderPhrase exist = command.OrderByClause.Last as APSqlOrderPhrase;
				exist.SetNext(phrase);
			}

			return command;
		}


		/// <summary>
		/// SQL 'ORDER BY' clause extensions. Add new 'ORDER BY' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'ORDER BY' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by_add(this APSqlSelectCommand command, params APSqlOrderPhrase[] phrases)
		{
			if (command.OrderByClause == null || command.OrderByClause.Next == null)
			{
				command.OrderByClause = new APSqlOrderByClause(phrases);
			}
			else
			{
				APSqlOrderPhrase exist = command.OrderByClause.Last as APSqlOrderPhrase;
				exist.SetNext(phrases);
			}

			return command;
		}


		/// <summary>
		/// SQL 'ORDER BY' clause extensions. Add new 'ORDER BY' in clause.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="phrases">The 'ORDER BY' phrases.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand order_by_add(this APSqlSelectCommand command, IEnumerable<APSqlOrderPhrase> phrases)
		{
			if (command.OrderByClause == null || command.OrderByClause.Next == null)
			{
				command.OrderByClause = new APSqlOrderByClause(phrases);
			}
			else
			{
				APSqlOrderPhrase exist = command.OrderByClause.Last as APSqlOrderPhrase;
				exist.SetNext(phrases);
			}

			return command;
		}


		#endregion


		#region [ PrimeryKeyColumnDef Extensions ]


		/// <summary>
		/// PrimeryKeyColumnDef extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="primaryExpr">The Primary Expression.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand primary(this APSqlSelectCommand command, APSqlExpr primaryExpr)
		{
			command.PrimeryKeyExpr = primaryExpr;
			return command;
		}


		#endregion


		#region [ Take Extensions ]


		/// <summary>
		/// Take extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="take">Take size of the command result.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand take(this APSqlSelectCommand command, int? take)
		{
			command.Take = take;
			return command;
		}


		#endregion


		#region [ Skip Extensions ]


		/// <summary>
		/// Skip extensions.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="skip">Skip size of the command result.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand skip(this APSqlSelectCommand command, int? skip)
		{
			command.Skip = skip;
			return command;
		}


		#endregion


		#region [ Execute Extensions ]


		/// <summary>
		/// Execute.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="db">The database.</param>
		/// <returns>The scale.</returns>
		public static object executeScale(this APSqlSelectCommand command, APDatabase db)
		{
			return db.ExecuteScalar(command);
		}

		/// <summary>
		/// Execute.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="db">The database.</param>
		/// <returns>The reader.</returns>
		public static IDataReader executeReader(this APSqlSelectCommand command, APDatabase db)
		{
			return db.ExecuteReader(command);
		}

		/// <summary>
		/// Execute.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="db">The database.</param>
		/// <returns>The size of the select.</returns>
		public static int count(this APSqlSelectCommand command, APDatabase db)
		{
			return db.ExecuteSizeOfSelect(command);
		}


		/// <summary>
		/// Execute
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command"></param>
		/// <param name="db"></param>
		/// <param name="fillup"></param>
		/// <returns></returns>
		public static IEnumerable<T> query<T>(this APSqlSelectCommand command, APDatabase db, Func<IDataReader, T> fillup)
		{
			return db.Query(command, fillup);
		}


		#endregion

	}

}
