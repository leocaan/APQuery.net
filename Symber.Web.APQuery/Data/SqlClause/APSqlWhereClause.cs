using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'WHERE' clause.
	/// </summary>
	public sealed class APSqlWhereClause : APSqlPhrase
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new 'WHERE' clause.
		/// </summary>
		public APSqlWhereClause()
		{
		}


		/// <summary>
		/// Create a new 'WHERE' clause with one 'WHERE' phrase.
		/// </summary>
		/// <param name="phrase">'WHERE' phrase.</param>
		public APSqlWhereClause(APSqlWherePhrase phrase)
		{
			SetNext(phrase);

			CheckValid();
		}

		/// <summary>
		/// Create a new 'WHERE' clause with join type and 'WHERE' phrases.
		/// </summary>
		/// <param name="joinType">Join type.</param>
		/// <param name="phrases">'WHERE' phrases.</param>
		public APSqlWhereClause(APSqlConditionJoinType joinType, params APSqlWherePhrase[] phrases)
		{
			if (joinType == APSqlConditionJoinType.AND)
				SetNext(new APSqlConditionAndPhrase(phrases));
			else
				SetNext(new APSqlConditionOrPhrase(phrases));

			CheckValid();
		}


		/// <summary>
		/// Create a new 'WHERE' clause with join type and 'WHERE' phrases.
		/// </summary>
		/// <param name="joinType">Join type.</param>
		/// <param name="phrases">The IEnumerable phrases.</param>
		public APSqlWhereClause(APSqlConditionJoinType joinType, IEnumerable<APSqlWherePhrase> phrases)
		{
			if (joinType == APSqlConditionJoinType.AND)
				SetNext(new APSqlConditionAndPhrase(phrases));
			else
				SetNext(new APSqlConditionOrPhrase(phrases));

			CheckValid();
		}


		private void CheckValid()
		{
			if (Next is APSqlConditionAndPhrase && (Next as APSqlConditionAndPhrase).Child == null)
				SetNextNull();
			else if (Next is APSqlConditionOrPhrase && (Next as APSqlConditionOrPhrase).Child == null)
				SetNextNull();
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
			if (phrase is APSqlWherePhrase)
				return base.SetNext(phrase);

			string typeName = phrase == null ? "null" : phrase.GetType().Name;
			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, typeName, typeof(APSqlConditionPhrase).Name));
		}


		#endregion

	}

}
