using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Sql 'SELECT' phrase.
	/// </summary>
	public sealed class APSqlSelectPhrase : APSqlPhrase
	{

		#region [ Fields ]


		private APSqlExpr _expr;
		private string _alias;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new 'SELECT' phrase.
		/// </summary>
		/// <param name="expr">SQL 'SELECT' Expression.</param>
		/// <param name="alias">Alias.</param>
		public APSqlSelectPhrase(APSqlOperateExpr expr, string alias)
		{
			if (Object.Equals(expr, null))
				throw new ArgumentNullException("expr");

			_expr = expr;
			_alias = alias;
		}


		/// <summary>
		/// Create a new 'SELECT' phrase.
		/// </summary>
		/// <param name="expr">SQL 'SELECT' Expression.</param>
		public APSqlSelectPhrase(APSqlExpr expr)
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


		/// <summary>
		/// Alias.
		/// </summary>
		public string Alias
		{
			get { return _alias; }
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
			if (phrase is APSqlSelectPhrase || phrase == null)
				return base.SetNext(phrase);

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlSelectPhrase).Name));
		}


		#endregion

	}

}
