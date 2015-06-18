using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Database relation definition.
	/// </summary>
	[Serializable]
	public sealed class APRelationDef
	{

		#region [ Fields ]


		private readonly APColumnDef _master;
		private readonly APColumnDef _slave;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new relation definition.
		/// </summary>
		/// <param name="masterTableName">Master table name.</param>
		/// <param name="masterColumnName">Master column name.</param>
		/// <param name="slaveTableName">Slave table name.</param>
		/// <param name="slaveColumnName">Slave table name.</param>
		public APRelationDef(string masterTableName, string masterColumnName, string slaveTableName, string slaveColumnName)
		{
			if (masterColumnName == null)
				throw new ArgumentNullException("masterColumnName");
			if (masterColumnName == String.Empty)
				throw new ArgumentException("masterColumnName");

			if (slaveColumnName == null)
				throw new ArgumentNullException("slaveColumnName");
			if (slaveColumnName == String.Empty)
				throw new ArgumentException("slaveColumnName");

			_master = new APColumnDef(masterTableName, masterColumnName);
			_slave = new APColumnDef(slaveTableName, slaveColumnName);
		}


		/// <summary>
		/// Create a new relation definition.
		/// </summary>
		/// <param name="master">Master column definition.</param>
		/// <param name="slave">Save column definition.</param>
		public APRelationDef(APColumnDef master, APColumnDef slave)
		{
			if (Object.Equals(master, null))
				throw new ArgumentNullException("master");
			if (Object.Equals(slave, null))
				throw new ArgumentNullException("slave");

			_master = master;
			_slave = slave;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Master column definition.
		/// </summary>
		public APColumnDef Master
		{
			get { return _master; }
		}


		/// <summary>
		/// Slave column definition.
		/// </summary>
		public APColumnDef Slave
		{
			get { return _slave; }
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// Implicit convert operator.
		/// </summary>
		/// <param name="relation">Relation definition.</param>
		/// <returns>Condition phrase.</returns>
		public static implicit operator APSqlConditionPhrase(APRelationDef relation)
		{
			return new APSqlConditionPhrase(relation);
		}


		/// <summary>
		/// And operator.
		/// </summary>
		/// <param name="left">Left relation definition.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>And condition group.</returns>
		public static APSqlConditionAndPhrase operator &(APRelationDef left, APSqlWherePhrase right)
		{
			return new APSqlConditionAndPhrase(left, right);
		}


		/// <summary>
		/// Or operator.
		/// </summary>
		/// <param name="left">Left relation definition.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>Or condition group.</returns>
		public static APSqlConditionOrPhrase operator |(APRelationDef left, APSqlWherePhrase right)
		{
			return new APSqlConditionOrPhrase(left, right);
		}


		/// <summary>
		/// Not operator.
		/// </summary>
		/// <param name="def">Relation definition.</param>
		/// <returns>Condition phrase.</returns>
		public static APSqlConditionPhrase operator !(APRelationDef def)
		{
			APSqlConditionPhrase phrase = new APSqlConditionPhrase(def);
			phrase.IsNot = !phrase.IsNot;
			return phrase;
		}


		#endregion

	}

}
