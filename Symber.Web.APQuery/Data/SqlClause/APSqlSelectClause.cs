using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'SELECT' clause.
	/// </summary>
	public sealed class APSqlSelectClause : APSqlPhrase
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new 'SELECT' clause.
		/// </summary>
		public APSqlSelectClause()
		{
		}


		/// <summary>
		/// Create a new 'SELECT' clause with 'SELECT' phrase.
		/// </summary>
		/// <param name="phrase">The 'SELECT' phrase.</param>
		public APSqlSelectClause(APSqlSelectPhrase phrase)
		{
			SetNext(phrase);
		}
		

		/// <summary>
		/// Create a new 'SELECT' clause with 'SELECT' phrases.
		/// </summary>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public APSqlSelectClause(params APSqlSelectPhrase[] phrases)
		{
			SetNext(phrases);
		}


		/// <summary>
		/// Create a new 'SELECT' clause with 'SELECT' phrases.
		/// </summary>
		/// <param name="phrases">The 'SELECT' phrases.</param>
		public APSqlSelectClause(IEnumerable<APSqlSelectPhrase> phrases)
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
			if (phrase is APSqlSelectPhrase || phrase == null)
				return base.SetNext(phrase);

			string typeName = phrase.GetType().Name;
			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, typeName, typeof(APSqlSelectPhrase).Name));
		}


		#endregion

	}

}
