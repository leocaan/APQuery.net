using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'GROUP BY' clause.
	/// </summary>
	public sealed class APSqlGroupByClause : APSqlPhrase
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new 'GROUP BY' clause.
		/// </summary>
		public APSqlGroupByClause()
		{
		}


		/// <summary>
		/// Create a new 'GROUP BY' clause with one 'expression' phrase.
		/// </summary>
		/// <param name="phrase">'SELECT' phrase.</param>
		public APSqlGroupByClause(APSqlExprPhrase phrase)
		{
			SetNext(phrase);
		}
		

		/// <summary>
		/// Create a new 'GROUP BY' clause with 'expression' phrases
		/// </summary>
		/// <param name="phrases">'SELECT' phrases.</param>
		public APSqlGroupByClause(params APSqlExprPhrase[] phrases)
		{
			SetNext(phrases);
		}


		/// <summary>
		/// Create a new 'GROUP BY' clause with 'expression' phrases.
		/// </summary>
		/// <param name="phrases">The IEnumerable phrases.</param>
		public APSqlGroupByClause(IEnumerable<APSqlExprPhrase> phrases)
		{
			SetNext(phrases);
		}


		#endregion


		#region [ Override Implementation of APSqlPhrase ]


		/// <summary>
		/// Set next phrase.
		/// </summary>
		/// <param name="phrase">The next phrase.</param>
		/// <returns>The next phrase.</returns>
		public override IAPSqlPhrase SetNext(IAPSqlPhrase phrase)
		{
			if (phrase is APSqlExprPhrase || phrase == null)
				return base.SetNext(phrase);

			string typeName = phrase.GetType().Name;
			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, typeName, typeof(APSqlExprPhrase).Name));
		}


		#endregion

	}

}
