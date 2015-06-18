
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'WHERE' phrase.
	/// </summary>
	public abstract class APSqlWherePhrase : APSqlPhrase
	{

		#region [ Fields ]


		private bool _isNot;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new 'WHERE' phrase.
		/// </summary>
		public APSqlWherePhrase()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Is the phrase is negative.
		/// </summary>
		public bool IsNot
		{
			get { return _isNot; }
			set { _isNot = value; }
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
			if (phrase is APSqlWherePhrase || phrase == null)
				return base.SetNext(phrase);

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlWherePhrase).Name));
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// And operator.
		/// </summary>
		/// <param name="left">Left where phrase.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>And condition group.</returns>
		public static APSqlConditionAndPhrase operator &(APSqlWherePhrase left, APSqlWherePhrase right)
		{
			return new APSqlConditionAndPhrase(left, right);
		}


		/// <summary>
		/// Or operator.
		/// </summary>
		/// <param name="left">Left where phrase.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>Or condition group.</returns>
		public static APSqlConditionOrPhrase operator |(APSqlWherePhrase left, APSqlWherePhrase right)
		{
			return new APSqlConditionOrPhrase(left, right);
		}


		#endregion

	}

}
