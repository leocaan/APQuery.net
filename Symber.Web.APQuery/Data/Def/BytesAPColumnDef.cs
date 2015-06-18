using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Byte[] column definition.
	/// </summary>
	[Serializable]
	public class BytesAPColumnDef : APColumnDef
	{

		#region [ Fields ]


		private int _length = 0;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new byte[] column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		public BytesAPColumnDef(string tableName, string columnName)
			: base(tableName, columnName)
		{
		}


		/// <summary>
		/// Create a new byte[] column definition.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		/// <param name="length">Data length.</param>
		public BytesAPColumnDef(string tableName, string columnName, bool isNullable, int length)
			: base(tableName, columnName, isNullable)
		{
			_length = length;
		}


		/// <summary>
		/// Create a new byte[] column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database</param>
		/// <param name="columnName">Column name in database.</param>
		public BytesAPColumnDef(APTableDef tableDef, string columnName)
			: base(tableDef, columnName)
		{
		}


		/// <summary>
		/// Create a new byte[] column definition.
		/// </summary>
		/// <param name="tableDef">Table definition in database.</param>
		/// <param name="columnName">Column name in database.</param>
		/// <param name="isNullable">Is nullable.</param>
		/// <param name="length">Data length.</param>
		public BytesAPColumnDef(APTableDef tableDef, string columnName, bool isNullable, int length)
			: base(tableDef, columnName, isNullable)
		{
			_length = length;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Data length.
		/// </summary>
		public int Length
		{
			get { return _length; }
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
