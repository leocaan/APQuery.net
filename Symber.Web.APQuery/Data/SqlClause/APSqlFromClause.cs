using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'FROM' clause.
	/// </summary>
	public sealed class APSqlFromClause : APSqlPhrase
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new 'FROM' clause.
		/// </summary>
		public APSqlFromClause()
		{
		}


		/// <summary>
		/// Create a 'FROM' clause with one 'FROM' phrase.
		/// </summary>
		/// <param name="phrase">'FROM' phrase.</param>
		public APSqlFromClause(APSqlFromPhrase phrase)
		{
			SetNext(phrase);
		}


		/// <summary>
		/// Create a 'FROM' clause with 'FROM' phrases.
		/// </summary>
		/// <param name="phrases">'FROM' phrase array.</param>
		public APSqlFromClause(params APSqlFromPhrase[] phrases)
		{
			SetNext(phrases);
		}


		/// <summary>
		/// Create a 'FROM' clause with 'FROM' phrases.
		/// </summary>
		/// <param name="phrases">The IEnumerable phrases.</param>
		public APSqlFromClause(IEnumerable<APSqlFromPhrase> phrases)
		{
			SetNext(phrases);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Next from phrase in list
		/// </summary>
		public new APSqlFromPhrase Next
		{
			get { return base.Next as APSqlFromPhrase; }
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
			if (phrase is APSqlFromPhrase || phrase == null)
				return base.SetNext(phrase);

			string typeName = phrase.GetType().Name;
			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, typeName, typeof(APSqlFromPhrase).Name));
		}


		#endregion

	}

}
