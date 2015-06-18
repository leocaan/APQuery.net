using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'ORDER BY' clause.
	/// </summary>
	public sealed class APSqlOrderByClause : APSqlPhrase
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a 'ORDER BY' clause.
		/// </summary>
		public APSqlOrderByClause()
		{
		}


		/// <summary>
		/// Create a 'ORDER BY' clause with one 'ORDER BY' phrase.
		/// </summary>
		/// <param name="phrase">'ORDER BY' phrase.</param>
		public APSqlOrderByClause(APSqlOrderPhrase phrase)
		{
			SetNext(phrase);
		}


		/// <summary>
		/// Create a 'ORDER BY' clause with 'ORDER BY' phrases.
		/// </summary>
		/// <param name="phrases">'ORDER BY' phrase array.</param>
		public APSqlOrderByClause(params APSqlOrderPhrase[] phrases)
		{
			SetNext(phrases);
		}


		/// <summary>
		/// Create a 'ORDER BY' clause with 'ORDER BY' phrases.
		/// </summary>
		/// <param name="phrases">The IEnumerable phrases.</param>
		public APSqlOrderByClause(IEnumerable<APSqlOrderPhrase> phrases)
		{
			SetNext(phrases);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Next phrase in list.
		/// </summary>
		public new APSqlOrderPhrase Next
		{
			get { return base.Next as APSqlOrderPhrase; }
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
			if (phrase is APSqlOrderPhrase || phrase == null)
				return base.SetNext(phrase);

			string typeName = phrase.GetType().Name;
			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, typeName, typeof(APSqlOrderPhrase).Name));
		}


		#endregion

	}

}
