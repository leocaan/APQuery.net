
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


		/// <summary>
		/// Set subquery condition phrase 'ALL' scalar.
		/// </summary>
		/// <param name="phrase">The condition phrase.</param>
		/// <returns>The condition phrase.</returns>
		public static APSqlConditionPhrase ScalarAll(this APSqlConditionPhrase phrase)
		{
			phrase.SubQueryScalarRestrict = APSqlSubQueryScalarRestrict.All;

			return phrase;
		}


		/// <summary>
		/// Set subquery condition phrase 'ANY' scalar.
		/// </summary>
		/// <param name="phrase">The condition phrase.</param>
		/// <returns>The condition phrase.</returns>
		public static APSqlConditionPhrase ScalarAny(this APSqlConditionPhrase phrase)
		{
			phrase.SubQueryScalarRestrict = APSqlSubQueryScalarRestrict.Any;

			return phrase;
		}


		/// <summary>
		/// Set subquery condition phrase 'SOME' scalar.
		/// </summary>
		/// <param name="phrase">The condition phrase.</param>
		/// <returns>The condition phrase.</returns>
		public static APSqlConditionPhrase ScalarSome(this APSqlConditionPhrase phrase)
		{
			phrase.SubQueryScalarRestrict = APSqlSubQueryScalarRestrict.Some;

			return phrase;
		}


		#endregion

	}

}
