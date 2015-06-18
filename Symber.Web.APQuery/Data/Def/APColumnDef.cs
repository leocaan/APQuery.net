using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Represents column information in database.
	/// </summary>
	[Serializable]
	public class APColumnDef
	{

		#region [ Fields ]


		private readonly APTableDef _tableDef;
		private readonly string _columnName;
		private bool _isNullable;
		private string _display = String.Empty;
		private bool _required = false;
		private string _selectExpr;


		#endregion
		

		#region [ Constructors ]

		
		/// <summary>
		/// Create a new column define.
		/// </summary>
		/// <param name="tableName">Table name in database.</param>
		/// <param name="columnName">Column name in database.</param>
		public APColumnDef(string tableName, string columnName)
			: this(tableName, columnName, false)
		{
		}


		/// <summary>
		/// Create a new column define.
		/// </summary>
		/// <param name="tableName">Table name in database.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		public APColumnDef(string tableName, string columnName, bool isNullable)
		{
			if (columnName == null)
				throw new ArgumentNullException("columnName");
			if (columnName == String.Empty)
				throw new ArgumentException("columnName");

			_tableDef = new APTableDef(tableName);
			_columnName = columnName;
			_isNullable = isNullable;
		}
		

		/// <summary>
		/// Create a new column define.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		public APColumnDef(APTableDef tableDef, string columnName)
			: this(tableDef, columnName, false)
		{
		}


		/// <summary>
		/// Create a new column define.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		public APColumnDef(APTableDef tableDef, string columnName, bool isNullable)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");
			if (columnName == null)
				throw new ArgumentNullException("columnName");
			if (columnName == String.Empty)
				throw new ArgumentException("columnName");

			_tableDef = tableDef;
			_columnName = columnName;
			_isNullable = isNullable;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Table defined in database.
		/// </summary>
		public APTableDef TableDef
		{
			get { return _tableDef; }
		}


		/// <summary>
		/// Column name in database.
		/// </summary>
		public string ColumnName
		{
			get { return _columnName; }
		}


		/// <summary>
		/// Is nullable.
		/// </summary>
		public bool IsNullable
		{
			get { return _isNullable; }
		}


		/// <summary>
		/// Gets or sets UI string of UI lable or title.
		/// </summary>
		public string Display
		{
			get { return _display; }
			set { _display = value; }
		}


		/// <summary>
		/// Gets or sets the value is required.
		/// </summary>
		public bool Required
		{
			get { return _required; }
			set { _required = value; }
		}


		/// <summary>
		/// Ascending 'ORDER BY' phrase.
		/// </summary>
		public APSqlOrderPhrase Asc
		{
			get { return new APSqlOrderPhrase(this, APSqlOrderAccording.Asc); }
		}


		/// <summary>
		/// Descending 'ORDER BY' phrase.
		/// </summary>
		public APSqlOrderPhrase Desc
		{
			get { return new APSqlOrderPhrase(this, APSqlOrderAccording.Desc); }
		}


		/// <summary>
		/// 'SELECT' phrase expression.
		/// </summary>
		public string SelectExpr
		{
			get
			{
				if (_selectExpr == null)
				{
					string tn = _tableDef.SelectExpr;
					_selectExpr = String.IsNullOrEmpty(tn) ? _columnName : tn + "." + _columnName;
				}
				return _selectExpr;
			}
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Right column.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(APColumnDef left, APColumnDef right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.Equals, right);
		}


		/// <summary>
		/// Build condition phrase of '!='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Right column.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(APColumnDef left, APColumnDef right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.NotEqual, right);
		}



		/// <summary>
		/// Build condition phrase of '>'.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '>'.</returns>
		public static APSqlConditionPhrase operator >(APColumnDef left, APColumnDef right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Right SQL Expression.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(APColumnDef left, APColumnDef right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Right column.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(APColumnDef left, APColumnDef right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Right column.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(APColumnDef left, APColumnDef right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '=='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Sub query.</param>
		/// <returns>Condition phrase of '=='.</returns>
		public static APSqlConditionPhrase operator ==(APColumnDef left, APSqlCommand right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.Equals, right);
		}


		/// <summary>
		/// Build condition phrase of '!='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Sub query.</param>
		/// <returns>Condition phrase of '!='.</returns>
		public static APSqlConditionPhrase operator !=(APColumnDef left, APSqlCommand right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.NotEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '>'.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Sub query.</param>
		/// <returns>Condition phrase of '>'.</returns>
		public static APSqlConditionPhrase operator >(APColumnDef left, APSqlCommand right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;'.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Sub query.</param>
		/// <returns>Condition phrase of '&lt;'.</returns>
		public static APSqlConditionPhrase operator <(APColumnDef left, APSqlCommand right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThan, right);
		}


		/// <summary>
		/// Build condition phrase of '&gt;='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Sub query.</param>
		/// <returns>Condition phrase of '&gt;='.</returns>
		public static APSqlConditionPhrase operator >=(APColumnDef left, APSqlCommand right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.GreaterThanOrEqual, right);
		}


		/// <summary>
		/// Build condition phrase of '&lt;='.
		/// </summary>
		/// <param name="left">Left column.</param>
		/// <param name="right">Sub query.</param>
		/// <returns>Condition phrase of '&lt;='.</returns>
		public static APSqlConditionPhrase operator <=(APColumnDef left, APSqlCommand right)
		{
			return new APSqlConditionPhrase(left, APSqlConditionOperator.LessThanOrEqual, right);
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


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="def">Source.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlColumnExpr(APColumnDef def)
		{
			return new APSqlColumnExpr(def);
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="def">Source.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlSelectPhrase(APColumnDef def)
		{
			return new APSqlSelectPhrase(def);
		}


		/// <summary>
		/// Implicit conversion operator.
		/// </summary>
		/// <param name="def">Source.</param>
		/// <returns>Target.</returns>
		public static implicit operator APSqlExprPhrase(APColumnDef def)
		{
			return new APSqlExprPhrase(def);
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

			APColumnDef other = obj as APColumnDef;
			if (!Object.ReferenceEquals(other, null))
				return _tableDef == other._tableDef && _columnName == other._columnName;

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
