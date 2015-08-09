using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL Condition phrase.
	/// </summary>
	public sealed class APSqlConditionPhrase : APSqlWherePhrase
	{

		#region [ Fields ]


		private APSqlOperateExpr _expr;
		private APSqlConditionOperator _conditionOperator;
		private string _paramName;
		private object _value;
		private APSqlSubQueryScalarRestrict _subQueryScalarRestrict;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new condition phrase.
		/// </summary>
		/// <param name="relationDef">Relation definition.</param>
		public APSqlConditionPhrase(APRelationDef relationDef)
		{
			if (relationDef == null)
				throw new ArgumentNullException("relationDef");
			_expr = new APSqlColumnExpr(relationDef.Master);
			_conditionOperator = APSqlConditionOperator.Equals;
			_value = new APSqlColumnExpr(relationDef.Slave);
		}


		/// <summary>
		/// Create a new condition phrase.
		/// </summary>
		/// <param name="expr">SQL Expression.</param>
		/// <param name="conditionOperator">Condition operator.</param>
		/// <param name="value">Value.</param>
		/// <param name="paramName">Parameter name.</param>
		public APSqlConditionPhrase(APSqlOperateExpr expr, APSqlConditionOperator conditionOperator, object value, string paramName)
		{
			if (Object.Equals(expr, null))
				throw new ArgumentNullException("columnDef");
			if (conditionOperator == APSqlConditionOperator.Exists || conditionOperator == APSqlConditionOperator.NotExists)
				throw new ArgumentException(APResource.APDB_OperatorExists, "conditionOperator");

			_expr = expr;
			_paramName = paramName;
			if (String.IsNullOrEmpty(paramName))
			{
				_paramName = expr.ParamName;
			}

			if (value is String)
				value = APSqlExpr.TryFitStringToRawExpr((string)value);

			_value = value;
			_conditionOperator = conditionOperator;
		}


		/// <summary>
		/// Create a new condition phrase.
		/// </summary>
		/// <param name="expr">SQL Expression.</param>
		/// <param name="conditionOperator">Condition operator.</param>
		/// <param name="value">Value.</param>
		public APSqlConditionPhrase(APSqlOperateExpr expr, APSqlConditionOperator conditionOperator, object value)
			: this(expr, conditionOperator, value, null)
		{
		}


		/// <summary>
		/// Create a new condition phrase where sub query when use 'ANY | SOME | ALL'.
		/// </summary>
		/// <param name="expr">SQL Expression.</param>
		/// <param name="conditionOperator">Condition operator.</param>
		/// <param name="subQuery">Sub Query.</param>
		/// <param name="subQueryScalarRestrict">Sub query scalar restrict.</param>
		public APSqlConditionPhrase(APSqlOperateExpr expr, APSqlConditionOperator conditionOperator, APSqlSelectCommand subQuery, APSqlSubQueryScalarRestrict subQueryScalarRestrict)
			: this(expr, conditionOperator, subQuery, null)
		{
			_subQueryScalarRestrict = subQueryScalarRestrict;
		}


		/// <summary>
		/// Create a new condition phrase where sub query when use '[NOT] EXISTS'
		/// </summary>
		/// <param name="subQuery">Sub Query.</param>
		/// <param name="conditionOperator">'EXISTS' or 'NOT EXISTS'</param>
		public APSqlConditionPhrase(APSqlSelectCommand subQuery, APSqlConditionOperator conditionOperator)
		{
			if (subQuery == null)
				throw new ArgumentNullException("subQuery");
			if (conditionOperator != APSqlConditionOperator.Exists && conditionOperator != APSqlConditionOperator.NotExists)
				throw new ArgumentException(APResource.APDB_OperatorExists, "conditionOperator");

			_expr = null;
			_conditionOperator = conditionOperator;
			_value = subQuery;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL Expression.
		/// </summary>
		public APSqlOperateExpr Expr
		{
			get { return _expr; }
		}


		/// <summary>
		/// Condition operator.
		/// </summary>
		public APSqlConditionOperator ConditionOperator
		{
			get { return _conditionOperator; }
		}


		/// <summary>
		/// Parameter name.
		/// </summary>
		public string ParamName
		{
			get { return _paramName; }
		}


		/// <summary>
		/// Value.
		/// </summary>
		public object Value
		{
			get { return _value; }
		}


		/// <summary>
		/// Sub query scalar restrict. only use in 'column { ALL | SOME | ANY} ( subquery )'.
		/// </summary>
		public APSqlSubQueryScalarRestrict SubQueryScalarRestrict
		{
			get { return _subQueryScalarRestrict; }
			internal set { _subQueryScalarRestrict = value; }
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// And operator.
		/// </summary>
		/// <param name="left">Left condition phrase.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>And condition group.</returns>
		public static APSqlConditionAndPhrase operator &(APSqlConditionPhrase left, APSqlWherePhrase right)
		{
			return new APSqlConditionAndPhrase(left, right);
		}


		/// <summary>
		/// Or operator.
		/// </summary>
		/// <param name="left">Left condition phrase.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>Or condition group.</returns>
		public static APSqlConditionOrPhrase operator |(APSqlConditionPhrase left, APSqlWherePhrase right)
		{
			return new APSqlConditionOrPhrase(left, right);
		}


		/// <summary>
		/// Not operator.
		/// </summary>
		/// <param name="phrase">Condition phrase.</param>
		/// <returns>Condition phrase.</returns>
		public static APSqlConditionPhrase operator !(APSqlConditionPhrase phrase)
		{
			phrase.IsNot = !phrase.IsNot;
			return phrase;
		}


		#endregion

	}

}
