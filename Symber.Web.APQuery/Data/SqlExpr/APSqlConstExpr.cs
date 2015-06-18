using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL const value Expression.
	/// </summary>
	public class APSqlConstExpr : APSqlOperateExpr, IAPSqlValueExpr
	{

		#region [ Static ]


		/// <summary>
		/// Create a new APSqlConstExpr;
		/// </summary>
		/// <param name="value">The Value.</param>
		/// <returns>APSqlConstExpr.</returns>
		public static APSqlConstExpr Expr(object value)
		{
			return new APSqlConstExpr(value);
		}


		/// <summary>
		/// Create a new APSqlConstExpr;
		/// </summary>
		/// <param name="value">The Value.</param>
		/// <param name="paramName">Command parameter name.</param>
		/// <returns>APSqlConstExpr.</returns>
		public static APSqlConstExpr Expr(object value, string paramName)
		{
			return new APSqlConstExpr(value, paramName);
		}


		#endregion


		#region [ Fields ]


		private object _constValue;
		private string _paramName;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlConstExpr
		/// </summary>
		/// <param name="constValue">Const value.</param>
		public APSqlConstExpr(object constValue)
			: this(constValue, null)
		{
		}


		/// <summary>
		/// Create a new APSqlConstExpr
		/// </summary>
		/// <param name="constValue">Const value.</param>
		/// <param name="paramName">Command parameter name.</param>
		public APSqlConstExpr(object constValue, string paramName)
		{
			_constValue = constValue;
			_paramName = paramName;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get
			{
				if (_constValue == null)
					return "NULL";
				else if (_constValue is string)
					return "'" + ((string)_constValue).ToString() + "'";
				else if (_constValue is DateTime)
					return "'" + ((DateTime)_constValue).ToString() + "'";
				else if (_constValue is Guid)
					return "'" + ((Guid)_constValue).ToString() + "'";
				else if (_constValue.GetType().IsEnum)
					return ((int)_constValue).ToString();
				else
					return _constValue.ToString();
			}
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return null; }
		}


		/// <summary>
		/// Command parameter name.
		/// </summary>
		public override string ParamName
		{
			get
			{
				return _paramName;
			}
		}


		/// <summary>
		/// SQL value of assignment phrase expression.
		/// </summary>
		public string ValueExpr
		{
			get
			{
				return SelectExpr;
			}
		}


		#endregion

	}

}
