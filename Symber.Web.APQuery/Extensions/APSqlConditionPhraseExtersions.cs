
namespace Symber.Web.Data
{

	/// <summary>
	/// APSqlConditionPhrase Extensions.
	/// </summary>
	public static class APSqlConditionPhraseExtersions
	{

		#region [ Scalar Extensions ]


		/// <summary>
		/// Set subquery condition phrase scalar.
		/// </summary>
		/// <param name="phrase">The condition phrase.</param>
		/// <param name="scalar">The scalar of subquery.</param>
		/// <returns>The condition phrase.</returns>
		public static APSqlConditionPhrase Scalar(this APSqlConditionPhrase phrase, APSqlSubQueryScalarRestrict scalar)
		{
			phrase.SubQueryScalarRestrict = scalar;

			return phrase;
		}


		#endregion

	}

}
