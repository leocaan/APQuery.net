
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'ORDER BY' phrase.
	/// </summary>
	public sealed class APSqlOrderPhrase : APSqlPhrase
	{

		#region [ Fields ]


		private APSqlOperateExpr _expr;
		private APSqlOrderAccording _orderAccording;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a 'ORDER BY' phrase.
		/// </summary>
		/// <param name="expr">SQL 'SELECT' Expression.</param>
		/// <param name="orderAccording">Order according.</param>
		public APSqlOrderPhrase(APSqlOperateExpr expr, APSqlOrderAccording orderAccording)
		{
			_expr = expr;
			_orderAccording = orderAccording;
		}


		/// <summary>
		/// Create a 'ORDER BY' phrase.
		/// </summary>
		/// <param name="expr">SQL 'SELECT' Expression.</param>
		public APSqlOrderPhrase(APSqlOperateExpr expr)
			: this(expr, APSqlOrderAccording.Asc)
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL 'SELECT' Expression.
		/// </summary>
		public APSqlOperateExpr Expr
		{
			get { return _expr; }
		}


		/// <summary>
		/// Order according.
		/// </summary>
		public APSqlOrderAccording OrderAccording
		{
			get { return _orderAccording; }
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

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlOrderPhrase).Name));
		}


		#endregion

	}

}
