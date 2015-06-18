using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Guid column definition.
	/// </summary>
	[Serializable]
	public class GuidAPColumnDef : APColumnDef
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new guid column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		public GuidAPColumnDef(string tableName, string columnName)
			: base(tableName, columnName)
		{
		}


		/// <summary>
		/// Create a new guid column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		public GuidAPColumnDef(string tableName, string columnName, bool isNullable)
			: base(tableName, columnName, isNullable)
		{
		}


		/// <summary>
		/// Create a new guid column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		public GuidAPColumnDef(APTableDef tableDef, string columnName)
			: base(tableDef, columnName)
		{
		}


		/// <summary>
		/// Create a new guid column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		public GuidAPColumnDef(APTableDef tableDef, string columnName, bool isNullable)
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
		public static APSqlConditionPhrase operator ==(GuidAPColumnDef column, Guid value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(Guid value, GuidAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(GuidAPColumnDef column, Guid value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(Guid value, GuidAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(GuidAPColumnDef column, Guid? value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(Guid? value, GuidAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.Equals, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <param name="value">Value.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(GuidAPColumnDef column, Guid? value)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of '!='
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="column">Column definition.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(Guid? value, GuidAPColumnDef column)
		{
			return new APSqlConditionPhrase(column, APSqlConditionOperator.NotEqual, value);
		}


		/// <summary>
		/// Build condition phrase of 'IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'IN'.</returns>
		public APSqlConditionPhrase In(params Guid[] value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.In, value);
		}


		/// <summary>
		/// Build condition phrase of 'NOT IN'.
		/// </summary>
		/// <param name="value">Array of values.</param>
		/// <returns>Condition phrase of 'NOT IN'.</returns>
		public APSqlConditionPhrase NotIn(params Guid[] value)
		{
			return new APSqlConditionPhrase(this, APSqlConditionOperator.NotIn, value);
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
