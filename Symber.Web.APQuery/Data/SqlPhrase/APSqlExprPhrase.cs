using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Sql Expression phrase.
	/// </summary>
	public sealed class APSqlExprPhrase : APSqlPhrase
	{

		#region [ Fields ]


		private APSqlExpr _expr;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new Expression phrase.
		/// </summary>
		/// <param name="expr">SQL Expression.</param>
		public APSqlExprPhrase(APSqlExpr expr)
		{
			if (Object.Equals(expr, null))
				throw new ArgumentNullException("expr");

			_expr = expr;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL Expression.
		/// </summary>
		public APSqlExpr Expr
		{
			get { return _expr; }
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
			if (phrase is APSqlExprPhrase || phrase == null)
				return base.SetNext(phrase);

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlExprPhrase).Name));
		}


		#endregion

	}

}
