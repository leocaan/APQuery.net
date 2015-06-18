﻿using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Double colume definition.
	/// </summary>
	[Serializable]
	public class DoubleAPColumnDef : APColumnDef
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new double column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		public DoubleAPColumnDef(string tableName, string columnName)
			: base(tableName, columnName)
		{
		}


		/// <summary>
		/// Create a new double column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		public DoubleAPColumnDef(string tableName, string columnName, bool isNullable)
			: base(tableName, columnName, isNullable)
		{
		}


		/// <summary>
		/// Create a new double column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		public DoubleAPColumnDef(APTableDef tableDef, string columnName)
			: base(tableDef, columnName)
		{
		}


		/// <summary>
		/// Create a new double column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		public DoubleAPColumnDef(APTableDef tableDef, string columnName, bool isNullable)
			: base(tableDef, columnName, isNullable)
		{
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(DoubleAPColumnDef column, Double value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(Double value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(DoubleAPColumnDef column, Double value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(Double value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(DoubleAPColumnDef column, Double? value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(Double? value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(DoubleAPColumnDef column, Double? value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(Double? value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;'
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&gt;'.</returns>
		public static APSqlConditionPhrase operator >(DoubleAPColumnDef column, Double value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThan, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&gt;'.</returns>
		public static APSqlConditionPhrase operator >(Double value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(DoubleAPColumnDef column, Double value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThan, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(Double value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(DoubleAPColumnDef column, Double value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(Double value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThan, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(DoubleAPColumnDef column, Double value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(Double value, DoubleAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThan, value);
		}


		/// <summary>
		/// Build condition phrase of 'IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'IN'.</returns>
		public APSqlConditionPhrase In(params Double[] value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.In, value);
		}


		/// <summary>
		/// Build condition phrase of 'NOT IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'NOT IN'.</returns>
		public APSqlConditionPhrase NotIn(params Double[] value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotIn, value);
		}


		/// <summary>
		/// Build condition phrase of 'BETWEEN'.
		/// </summary>
		/// <param name="begin">Begin value.</param>
		/// <param name="end">End value.</param>
		/// <returns>Condition phrase of 'BETWEEN'.</returns>
		public APSqlConditionPhrase Between(Double begin, Double end)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Between, new Double[2] { begin, end });
		}


		/// <summary>
		/// Build condition phrase of 'NOT BETWEEN'.
		/// </summary>
		/// <param name="begin">Begin value.</param>
		/// <param name="end">End value.</param>
		/// <returns>Condition phrase of 'NOT BETWEEN'.</returns>
		public APSqlConditionPhrase NotBetween(Double begin, Double end)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotBetween, new Double[2] { begin, end });
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
			return base.Equals(obj);
		}


		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current Object.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}


		#endregion

	}

}
