using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// A SQL Operate Expression.
	/// </summary>
	public abstract class APSqlOperateExpr : APSqlExpr
	{

		#region [ Properties ]


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public abstract string ParamName { get; }


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right value.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(APSqlOperateExpr left, object right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.Equals, right);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="left">Left value.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(object left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.Equals, left);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(APSqlOperateExpr left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.Equals, right);
		}


		/// <summary>
		/// Build condition phrase of '!='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right value.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(APSqlOperateExpr left, object right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.NotEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '!='.
		/// </summary>
		/// <param name="left">Left value.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(object left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.NotEqual, left);
		}


		/// <summary>
		/// Build condition phrase of '!='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(APSqlOperateExpr left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.NotEqual, left);
		}


		/// <summary>
		/// Build condition phrase of '>'.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right value.</param>
		/// <returns>Condition phrase of '>'.</returns>
		public static APSqlConditionPhrase operator >(APSqlOperateExpr left, object right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThan, right);
		}


		/// <summary>
		/// Build condition phrase of '>'.
		/// </summary>
		/// <param name="left">Left value.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '>'.</returns>
		public static APSqlConditionPhrase operator >(object left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.LessThanOrEqual, left);
		}


		/// <summary>
		/// Build condition phrase of '>'.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '>'.</returns>
		public static APSqlConditionPhrase operator >(APSqlOperateExpr left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right value.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(APSqlOperateExpr left, object right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="left">Left value.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(object left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.GreaterThanOrEqual, left);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(APSqlOperateExpr left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right value.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(APSqlOperateExpr left, object right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="left">Left value.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(object left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.LessThan, left);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(APSqlOperateExpr left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right value.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(APSqlOperateExpr left, object right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="left">Left value.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(object left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(right, APSqlConditionOperator.GreaterThan, left);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="left">Left SQL Expression.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(APSqlOperateExpr left, APSqlOperateExpr right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of 'IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'IN'.</returns>
		public APSqlConditionPhrase In(Array value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.In, value);
		}


		/// <summary>
		/// Build condition phrase of 'NOT IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'NOT IN'.</returns>
		public APSqlConditionPhrase NotIn(Array value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotIn, value);
		}


		/// <summary>
		/// Build condition phrase of 'BETWEEN'.
		/// </summary>
		/// <param name="begin">Begin value.</param>
		/// <param name="end">End value.</param>
		/// <returns>Condition phrase of 'BETWEEN'.</returns>
		public APSqlConditionPhrase Between(object begin, object end)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Between, new object[2] { begin, end });
		}


		/// <summary>
		/// Build condition phrase of 'NOT BETWEEN'.
		/// </summary>
		/// <param name="begin">Begin value.</param>
		/// <param name="end">End value.</param>
		/// <returns>Condition phrase of 'NOT BETWEEN'.</returns>
		public APSqlConditionPhrase NotBetween(object begin, object end)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotBetween, new object[2] { begin, end });
		}


		/// <summary>
		/// Build condition phrase of 'IN'.
		/// </summary>
		/// <param name="subQuery">Sub query.</param>
		/// <returns>Condition phrase of 'IN'.</returns>
		public APSqlConditionPhrase In(APSqlCommand subQuery)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.In, subQuery);
		}


		/// <summary>
		/// Build condition phrase of 'NOT IN'.
		/// </summary>
		/// <param name="subQuery">Sub query.</param>
		/// <returns>Condition phrase of 'NOT IN'.</returns>
		public APSqlConditionPhrase NotIn(APSqlCommand subQuery)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotIn, subQuery);
		}


		#endregion


		#region [ Override Implementation of Object ]


		/// <summary>
		/// Determines whether two Object instances are equal.
		/// </summary>
		/// <param name="obj">The Object to compare with the current Object.</param>
		/// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			APSqlOperateExpr other = obj as APSqlOperateExpr;
			if (!Object.ReferenceEquals(other, null))
				return SelectExpr == other.SelectExpr;

			return false;
		}


		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current Object.</returns>
		public override int GetHashCode()
		{
			return SelectExpr.GetHashCode();
		}


		#endregion

	}

}
