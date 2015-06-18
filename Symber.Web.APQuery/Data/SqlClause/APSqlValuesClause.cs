using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'VALUES' clause.
	/// </summary>
	public sealed class APSqlValuesClause : APSqlPhrase
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new 'SET' clause.
		/// </summary>
		public APSqlValuesClause()
		{
		}


		/// <summary>
		/// Create a new 'SET' clause with one 'SET' phrase.
		/// </summary>
		/// <param name="phrase">'SET' phrase.</param>
		public APSqlValuesClause(APSqlSetPhrase phrase)
		{
			SetNext(phrase);
		}


		/// <summary>
		/// Create a new 'SET' clause with 'SET' phrases.
		/// </summary>
		/// <param name="phrases">'SET' phrases.</param>
		public APSqlValuesClause(params APSqlSetPhrase[] phrases)
		{
			SetNext(phrases);
		}


		/// <summary>
		/// Create a new 'SET' clause with 'SET' phrases.
		/// </summary>
		/// <param name="phrases">The IEnumerable phrases.</param>
		public APSqlValuesClause(IEnumerable<APSqlSetPhrase> phrases)
		{
			SetNext(phrases);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Next set phrase.
		/// </summary>
		public new APSqlSetPhrase Next
		{
			get { return base.Next as APSqlSetPhrase; }
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
			if (phrase is APSqlSetPhrase || phrase == null)
				return base.SetNext(phrase);

			string typeName = phrase.GetType().Name;
			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, typeName, typeof(APSqlSetPhrase).Name));
		}


		#endregion

	}

}
