using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// APQuery.
	/// </summary>
	public static class APQuery
	{

		#region [ 'SELECT' command ]


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' clause.
		/// </summary>
		/// <param name="clause">The 'SELECT' clause.</param>
		/// <returns>The command.</returns>
		public static APSqlSelectCommand select(APSqlSelectClause clause)
		{
			return new APSqlSelectCommand(clause);
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' phrase.
		/// </summary>
		/// <param name="phrase">The 'SELECT' phrase.</param>
		public static APSqlSelectCommand select(APSqlSelectPhrase phrase)
		{
			return new APSqlSelectCommand(phrase);
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' phrases.
		/// </summary>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public static APSqlSelectCommand select(params APSqlSelectPhrase[] phrases)
		{
			return new APSqlSelectCommand(phrases);
		}


		/// <summary>
		/// Create a new 'SELECT' command with 'SELECT' phrases.
		/// </summary>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public static APSqlSelectCommand select(IEnumerable<APSqlSelectPhrase> phrases)
		{
			return new APSqlSelectCommand(phrases);
		}


		#endregion


		#region [ 'DELETE' command ]


		/// <summary>
		/// Create a new 'DELETE' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <returns>The command.</returns>
		public static APSqlDeleteCommand delete(APTableDef tableDef)
		{
			return new APSqlDeleteCommand(tableDef);
		}


		#endregion


		#region [ 'UPDATE' command ]


		/// <summary>
		/// Create a new 'UPDATE' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <returns>The command.</returns>
		public static APSqlUpdateCommand update(APTableDef tableDef)
		{
			return new APSqlUpdateCommand(tableDef);
		}


		#endregion


		#region [ 'INSERT' command ]


		/// <summary>
		/// Create a new 'INSERT' command.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <returns>The command.</returns>
		public static APSqlInsertCommand insert(APTableDef tableDef)
		{
			return new APSqlInsertCommand(tableDef);
		}


		#endregion

	}

}
