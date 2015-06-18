
namespace Symber.Web.Data
{

	/// <summary>
	/// Interface of all phrase and clause.
	/// </summary>
	public interface IAPSqlPhrase
	{

		/// <summary>
		/// Set next phrase.
		/// </summary>
		/// <param name="phrase">The next phrase.</param>
		/// <returns>The next phrase.</returns>
		IAPSqlPhrase SetNext(IAPSqlPhrase phrase);

		/// <summary>
		/// Next phrase.
		/// </summary>
		IAPSqlPhrase Next { get; }


		/// <summary>
		/// Last phrase.
		/// </summary>
		IAPSqlPhrase Last  { get; }

	}

}
