using System;
using System.Data;

namespace Symber.Web.Data
{

	/// <summary>
	/// Data Access Level class.
	/// </summary>
	public class APDal : IDisposable
	{

		#region [ Static Converter ]


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="reader">Data reader.</param>
		/// <param name="index">Index to read from reader.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(IDataReader reader, int index)
		{
			return GetValue(reader, index, default(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="reader">Data reader.</param>
		/// <param name="index">Index to read from reader.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(IDataReader reader, int index, T defaultValue)
		{
			if (reader.IsDBNull(index))
				return defaultValue;
			if (typeof(T).IsEnum)
				return (T)reader[index];
			if (typeof(T).IsGenericType && typeof(T).IsValueType)
				return (T)Convert.ChangeType(reader[index], Nullable.GetUnderlyingType(typeof(T)));
			return (T)Convert.ChangeType(reader[index], typeof(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">column name to read from reader.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(IDataReader reader, string columnName)
		{
			return GetValue<T>(reader, columnName, true, default(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">column name to read from reader.</param>
		/// <param name="throwIfValidColumnName">throw if the name specified is not a valid column name.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(IDataReader reader, string columnName, bool throwIfValidColumnName, T defaultValue)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				if (throwIfValidColumnName)
					throw;
				return defaultValue;
			}

			if (reader.IsDBNull(index))
				return defaultValue;
			if (typeof(T).IsEnum)
				return (T)reader[index];
			if (typeof(T).IsGenericType && typeof(T).IsValueType)
				return (T)Convert.ChangeType(reader[index], Nullable.GetUnderlyingType(typeof(T)));
			return (T)Convert.ChangeType(reader[index], typeof(T));
		}


		#endregion


		#region [ Fields ]


		private APDatabase _database;
		private bool _isOwnerDatabase;


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Database connection.
		/// </summary>
		protected APDatabase Database
		{
			get { return _database; }
		}


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APDal instance.
		/// </summary>
		public APDal()
		{
			_database = new APDatabase();
			_isOwnerDatabase = true;
		}


		/// <summary>
		/// Create a new APDal instance with a database object.
		/// </summary>
		/// <param name="db">APDatabase.</param>
		public APDal(APDatabase db)
		{
			_database = db;
			_isOwnerDatabase = false;
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Close the database.
		/// </summary>
		public void Close()
		{
			Dispose();
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Executes an SQL statement against the Connection and returns the number of rows affected.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The number of rows affected.</returns>
		protected int ExecuteNonQuery(APSqlCommand command)
		{
			return _database.ExecuteNonQuery(command);
		}


		/// <summary>
		/// Executes the query, and returns the first column of the first row in the result set
		/// returned by the query. Additional columns or rows are ignored.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if
		/// the result set is empty.</returns>
		protected object ExecuteScalar(APSqlCommand command)
		{
			return _database.ExecuteScalar(command);
		}


		/// <summary>
		/// Convert return value of ExecuteScalar to int type.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The count of query execute.</returns>
		protected int ExecuteCount(APSqlCommand command)
		{
			return Convert.ToInt32(ExecuteScalar(command));
		}


		/// <summary>
		/// Executes the CommandText against the Connection and builds an IDataReader.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>An IDataReader object.</returns>
		protected IDataReader ExecuteReader(APSqlCommand command)
		{
			return _database.ExecuteReader(command);
		}


		/// <summary>
		/// Get a new auto-increment id.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns>A new id value.</returns>
		public virtual long GetNewId(string tableName, string columnName)
		{
			return _database.GetNewId(tableName, columnName);
		}


		/// <summary>
		/// Get a new auto-increment id.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns>A new id value.</returns>
		public virtual long GetNewId(APColumnDef column)
		{
			return _database.GetNewId(column.TableDef.TableName, column.ColumnName);
		}


		/// <summary>
		/// Is view exist.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <returns>Whether the view is exist.</returns>
		public virtual bool IsViewExist(string name)
		{
			return _database.IsViewExist(name);
		}


		/// <summary>
		/// Create view.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <param name="command">The command to create view.</param>
		public virtual void CreateView(string name, APSqlSelectCommand command)
		{
			_database.CreateView(name, command);
		}


		#endregion


		#region [ Implementation of IDisposable ]


		/// <summary>
		/// Dispose.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Dispose.
		/// </summary>
		/// <param name="disposing">The disposing.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_isOwnerDatabase)
					_database.Close();
			}
		}


		#endregion

	}

}
