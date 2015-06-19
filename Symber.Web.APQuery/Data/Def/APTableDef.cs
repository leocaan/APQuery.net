using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Table definition, represent a table in database.
	/// </summary>
	[Serializable]
	public class APTableDef
	{

		#region [ Fields ]


		private readonly string _tableName;
		private readonly string _aliasName;
		[NonSerialized]
		private APSqlAsteriskExpr _asterisk;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new table definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		public APTableDef(string tableName)
		{
			if (tableName == null)
				throw new ArgumentNullException("tableName");
			if (tableName == String.Empty)
				throw new ArgumentException("tableName");

			_tableName = tableName;
		}
		

		/// <summary>
		/// Create a new alias table definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="aliasName">Alias name.</param>
		public APTableDef(string tableName, string aliasName)
			: this(tableName)
		{
			_aliasName = aliasName;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Table name.
		/// </summary>
		public string TableName
		{
			get { return _tableName; }
		}


		/// <summary>
		/// Alias.
		/// </summary>
		protected internal string AliasName
		{
			get { return _aliasName; }
		}


		/// <summary>
		/// 'SELECT' expression.
		/// </summary>
		internal string SelectExpr
		{
			get { return String.IsNullOrEmpty(_aliasName) ? _tableName : _aliasName; }
		}


		/// <summary>
		/// SQL '*' expression.
		/// </summary>
		public APSqlAsteriskExpr Asterisk
		{
			get
			{
				if (_asterisk == null)
					_asterisk = new APSqlAsteriskExpr(this);
				return _asterisk;
			}
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Implicit conversion operator, convert a table definiton to a 'FORM' phrase.
		/// </summary>
		/// <param name="def">Source, a table definition.</param>
		/// <returns>Target, a 'FORM' phrase.</returns>
		public static implicit operator APSqlFromPhrase(APTableDef def)
		{
			return new APSqlFromPhrase(def);
		}


		/// <summary>
		/// Implicit conversion operator, convert a table definiton to a 'DELETE' command.
		/// </summary>
		/// <param name="def">Source, a table definition.</param>
		/// <returns>Target, a 'DELETE' command.</returns>
		public static implicit operator APSqlDeleteCommand(APTableDef def)
		{
			return new APSqlDeleteCommand(def);
		}


		/// <summary>
		/// Implicit conversion operator, convert a table definiton to a 'INSERT INTO' command.
		/// </summary>
		/// <param name="def">Source, a table definition.</param>
		/// <returns>Target, a 'INSERT INTO' command.</returns>
		public static implicit operator APSqlInsertCommand(APTableDef def)
		{
			return new APSqlInsertCommand(def);
		}


		/// <summary>
		/// Implicit conversion operator, convert a table definiton to a 'UPDATE' command.
		/// </summary>
		/// <param name="def">Source, a table definition.</param>
		/// <returns>Target, a 'UPDATE' command.</returns>
		public static implicit operator APSqlUpdateCommand(APTableDef def)
		{
			return new APSqlUpdateCommand(def);
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

			APTableDef other = obj as APTableDef;
			if (obj != null)
				return _tableName == other._tableName && _aliasName == other._aliasName;

			return false;
		}


		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current Object.</returns>
		public override int GetHashCode()
		{
			if (String.IsNullOrEmpty(_aliasName))
			{
				return _tableName.GetHashCode();
			}
			return _aliasName.GetHashCode();
		}


		#endregion

	}

}
