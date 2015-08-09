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
			if (phrase is APSqlExprPhrase)
				return base.SetNext(phrase);
			else if (phrase == null)
				return base.SetNext(new APSqlExprPhrase(APSqlNullExpr.Expr));

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlExprPhrase).Name));
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Boolean value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Byte value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Char value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(DateTime value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Decimal value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Double value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Guid value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Int16 value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Int32 value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Int64 value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(SByte value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(Single value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(String value)
		{
			return new APSqlExprPhrase(APSqlExpr.FitStringToExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(UInt16 value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(UInt32 value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(UInt64 value)
		{
			return new APSqlExprPhrase(new APSqlConstExpr(value));
		}


		#endregion

	}

}
