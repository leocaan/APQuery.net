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
			if (phrase is APSqlSelectPhrase)
				return base.SetNext(phrase);
			else if (phrase == null)
				return base.SetNext(new APSqlSelectPhrase(APSqlNullExpr.Expr));

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlSelectPhrase).Name));
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Boolean value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Byte value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Char value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(DateTime value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Decimal value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Double value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Guid value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Int16 value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Int32 value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Int64 value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(SByte value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(Single value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(String value)
		{
			APSqlExpr expr;
			if (value == null)
				expr = new APSqlNullExpr();
			else if (value.StartsWith("~~"))
				expr = new APSqlConstExpr(value.Substring(1));
			else if (value.StartsWith("~"))
				expr = new APSqlThroughExpr(value.Substring(1));
			else
				expr = new APSqlConstExpr(value);
			return new APSqlSelectPhrase(expr);
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(UInt16 value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(UInt32 value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="value">Const Value.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(UInt64 value)
		{
			return new APSqlSelectPhrase(new APSqlConstExpr(value));
		}


		#endregion

	}

}
