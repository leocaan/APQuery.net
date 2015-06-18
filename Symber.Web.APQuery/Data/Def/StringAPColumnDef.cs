using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// String column definition.
	/// </summary>
	[Serializable]
	public class StringAPColumnDef : APColumnDef
	{

		#region [ Fields ]


		private int _length = 0;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new string column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		public StringAPColumnDef(string tableName, string columnName)
			: base(tableName, columnName)
		{
		}


		/// <summary>
		/// Create a new string column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		/// <param name="length">Data length.</param>
		public StringAPColumnDef(string tableName, string columnName, bool isNullable, int length)
			: base(tableName, columnName, isNullable)
		{
			_length = length;
		}


		/// <summary>
		/// Create a new string column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		public StringAPColumnDef(APTableDef tableDef, string columnName)
			: base(tableDef, columnName)
		{
		}


		/// <summary>
		/// Create a new string column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		/// <param name="length">Data length.</param>
		public StringAPColumnDef(APTableDef tableDef, string columnName, bool isNullable, int length)
			: base(tableDef, columnName, isNullable)
		{
			_length = length;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Data length
		/// </summary>
		public int Length
		{
			get { return _length; }
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(StringAPColumnDef column, String value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(String value, StringAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(StringAPColumnDef column, String value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(String value, StringAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;'
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&gt;'.</returns>
		public static APSqlConditionPhrase operator >(StringAPColumnDef column, String value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThan, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&gt;'.</returns>
		public static APSqlConditionPhrase operator >(String value, StringAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(StringAPColumnDef column, String value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThan, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(String value, StringAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(StringAPColumnDef column, String value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(String value, StringAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThan, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(StringAPColumnDef column, String value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.LessThanOrEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(String value, StringAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.GreaterThan, value);
		}


		/// <summary>
		/// Build condition phrase of 'LIKE'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of 'LIKE'.</returns>
		public APSqlConditionPhrase Like(String value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Like, value);
		}


		/// <summary>
		/// Build condition phrase of 'NOT LIKE'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of 'NOT LIKE'.</returns>
		public APSqlConditionPhrase NotLike(String value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotLike, value);
		}


		/// <summary>
		/// Build condition phrase of 'LIKE', auto add wildcard to find, example '%value%'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of 'LIKE'.</returns>
		public APSqlConditionPhrase Match(string value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Like, APDalProvider.Wildcard + value + APDalProvider.Wildcard);
		}


		/// <summary>
		/// Build condition phrase of 'LIKE', auto add wildcard to find, example 'value%'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of 'LIKE'.</returns>
		public APSqlConditionPhrase StartWith(string value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Like, value + APDalProvider.Wildcard);
		}


		/// <summary>
		/// Build condition phrase of 'LIKE', auto add wildcard to find, example '%value'.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of 'LIKE'.</returns>
		public APSqlConditionPhrase EndWith(string value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Like, APDalProvider.Wildcard + value);
		}


		/// <summary>
		/// Build condition phrase of 'IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'IN'.</returns>
		public APSqlConditionPhrase In(params String[] value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.In, value);
		}


		/// <summary>
		/// Build condition phrase of 'NOT IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'NOT IN'.</returns>
		public APSqlConditionPhrase NotIn(params String[] value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotIn, value);
		}


		/// <summary>
		/// Build condition phrase of 'BETWEEN'.
		/// </summary>
		/// <param name="begin">Begin value.</param>
		/// <param name="end">End value.</param>
		/// <returns>Condition phrase of 'BETWEEN'.</returns>
		public APSqlConditionPhrase Between(String begin, String end)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.Between, new String[2] { begin, end });
		}


		/// <summary>
		/// Build condition phrase of 'NOT BETWEEN'.
		/// </summary>
		/// <param name="begin">Begin value.</param>
		/// <param name="end">End value.</param>
		/// <returns>Condition phrase of 'NOT BETWEEN'.</returns>
		public APSqlConditionPhrase NotBetween(String begin, String end)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotBetween, new String[2] { begin, end });
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
