using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'WHERE' phrase extensions.
	/// </summary>
	public static class APSqlWherePhraseExtensions
	{

		#region [ IEnumerable<APSqlWherePhrase> ]


		/// <summary>
		/// Join phrases to 'AND' phrase.
		/// </summary>
		/// <param name="phrases">The phrases.</param>
		/// <returns>The 'AND' phrase.</returns>
		public static APSqlConditionAndPhrase JoinAnd(this IEnumerable<APSqlWherePhrase> phrases)
		{
			return new APSqlConditionAndPhrase(phrases);
		}


		/// <summary>
		/// Join phrases to 'OR' phrase.
		/// </summary>
		/// <param name="phrases">The phrases.</param>
		/// <returns>The 'OR' phrase.</returns>
		public static APSqlConditionOrPhrase JoinOr(this IEnumerable<APSqlWherePhrase> phrases)
		{
			return new APSqlConditionOrPhrase(phrases);
		}


		#endregion

	}

}
