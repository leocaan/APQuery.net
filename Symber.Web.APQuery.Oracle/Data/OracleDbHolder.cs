using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace Symber.Web.Data
{

	/// <summary>
	/// Oracle helper class.
	/// </summary>
	public sealed class OracleDbHolder : DbHolderBase
	{

		#region [ Static Methods ]


		/// <summary>
		/// Get Oracle help object.
		/// </summary>
		/// <param name="providerName">Provider name.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>A OracleDbHolder object.</returns>
		public static OracleDbHolder GetOracleDbHolder(string providerName, string connectionString)
		{
			DbProviderFactory factory = null;

			if (!String.IsNullOrEmpty(providerName))
			{
				try
				{
					factory = DbProviderFactories.GetFactory(providerName);
				}
				catch (Exception)
				{
				}

				if (factory != null)
					return new OracleDbHolder(factory, connectionString);
			}
			return new OracleDbHolder(OracleClientFactory.Instance, connectionString);
		}


		/// <summary>
		/// Get Oracle help object
		/// </summary>
		/// <param name="factory">Factory object</param>
		/// <param name="connectionString">Connection string</param>
		/// <returns>A OracleDbHolder object.</returns>
		public static OracleDbHolder GetOracleDbHolder(DbProviderFactory factory, string connectionString)
		{
			return new OracleDbHolder(factory, connectionString);
		}


		#endregion


		#region [ Fields ]


		private DbProviderFactory _factory = null;
		private string _connectionString;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new instance of OracleDbHolder.
		/// </summary>
		/// <param name="factory">Factory object.</param>
		/// <param name="connectionString">Connection string.</param>
		public OracleDbHolder(DbProviderFactory factory, string connectionString)
		{
			_factory = factory;
			_connectionString = connectionString;
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Create a new connection.
		/// </summary>
		/// <returns>Connection object.</returns>
		public DbConnection CreateConnection()
		{
			DbConnection connection = _factory.CreateConnection();
			connection.ConnectionString = _connectionString;
			connection.Open();
			return connection;
		}


		/// <summary>
		/// Create a new store precedure command.
		/// </summary>
		/// <param name="connection">Connection object.</param>
		/// <param name="trans">Transaction object.</param>
		/// <param name="storedProcessString">Store precedure name.</param>
		/// <returns>Command object.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public DbCommand CreateSpCommand(DbConnection connection, DbTransaction trans, string storedProcessString)
		{
			DbCommand command = _factory.CreateCommand();
			command.Connection = connection;
			command.Transaction = trans;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcessString;
			return command;
		}


		/// <summary>
		/// Create a new store precedure command.
		/// </summary>
		/// <param name="connection">Connection object.</param>
		/// <param name="storedProcessString">Store precedure name.</param>
		/// <returns>Command object.</returns>
		public DbCommand CreateSpCommand(DbConnection connection, string storedProcessString)
		{
			return CreateSpCommand(connection, null, storedProcessString);
		}


		/// <summary>
		/// Create a new store precedure command.
		/// </summary>
		/// <param name="storedProcessString">Store precedure name.</param>
		/// <returns>Command object.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public DbCommand CreateSpCommand(string storedProcessString)
		{
			DbCommand command = _factory.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcessString;
			return command;
		}


		/// <summary>
		/// Create a new sql command.
		/// </summary>
		/// <param name="connection">Connection object.</param>
		/// <param name="trans">Transaction object.</param>
		/// <param name="sqlString">Sql script.</param>
		/// <returns>Command object.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public DbCommand CreateSqlCommand(DbConnection connection, DbTransaction trans, string sqlString)
		{
			DbCommand command = _factory.CreateCommand();
			command.Connection = connection;
			command.Transaction = trans;
			command.CommandText = sqlString;
			return command;
		}


		/// <summary>
		/// Create a new sql command.
		/// </summary>
		/// <param name="connection">Connection object.</param>
		/// <param name="sqlString">Sql script.</param>
		/// <returns>Command object.</returns>
		public DbCommand CreateSqlCommand(DbConnection connection, string sqlString)
		{
			return CreateSqlCommand(connection, null, sqlString);
		}


		/// <summary>
		/// Create a new sql command.
		/// </summary>
		/// <param name="sqlString">Sql script.</param>
		/// <returns>Command object.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public DbCommand CreateSqlCommand(string sqlString)
		{
			DbCommand command = _factory.CreateCommand();
			command.CommandText = sqlString;
			return command;
		}


		/// <summary>
		/// Add parameter to command.
		/// </summary>
		/// <param name="command">Command object.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="parameterValue">Parameter value.</param>
		/// <returns>Parameter value.</returns>
		public DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
		{
			return AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
		}


		/// <summary>
		/// Add parameter to command.
		/// </summary>
		/// <param name="command">Command object.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="direction">Parameter direction.</param>
		/// <param name="parameterValue">Parameter value.</param>
		/// <returns>Parameter value.</returns>
		public DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbp = command.CreateParameter();
			dbp.ParameterName = parameterName;
			dbp.Value = parameterValue ?? DBNull.Value;
			dbp.Direction = direction;
			command.Parameters.Add(dbp);
			return dbp;
		}


		/// <summary>
		/// Add parameter to command.
		/// </summary>
		/// <param name="command">Command object.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="direction">Parameter direction.</param>
		/// <param name="type">Type.</param>
		/// <param name="parameterValue">Parameter value.</param>
		/// <returns>Parameter value.</returns>
		public DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, DbType type, object parameterValue)
		{
			DbParameter dbp = command.CreateParameter();
			dbp.ParameterName = parameterName;
			dbp.Direction = direction;
			if (type == DbType.Guid)
			{
				dbp.DbType = DbType.Binary;
				dbp.Size = 16;
				if (parameterValue == null) dbp.Value = DBNull.Value;
				else dbp.Value = ((Guid)parameterValue).ToByteArray();
			}
			else
			{
				dbp.DbType = type;
				dbp.Value = parameterValue ?? DBNull.Value;
			}
			command.Parameters.Add(dbp);
			return dbp;

		}


		//public DbParameter AddReturn(DbCommand command)
		//{
		//	return AddParameter(command, "v_ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		//}


		//public DbParameter AddRefReturn(DbCommand command)
		//{
		//	OracleParameter op = new OracleParameter();
		//	op.ParameterName = "v_ReturnVal";
		//	op.OracleDbType = OracleDbType.RefCursor;
		//	op.Direction = ParameterDirection.ReturnValue;
		//	command.Parameters.Add(op);
		//	return op;
		//}


		//public DbParameter AddRefCursor(DbCommand command, string parameterName)
		//{
		//	OracleParameter op = new OracleParameter();
		//	op.ParameterName = parameterName;
		//	op.OracleDbType = OracleDbType.RefCursor;
		//	op.Direction = ParameterDirection.Output;
		//	command.Parameters.Add(op);
		//	return op;
		//}


		/// <summary>
		/// Get return value from a parameter.
		/// </summary>
		/// <param name="returnValue">Parameter contains return value.</param>
		/// <returns>Return value.</returns>
		public int GetReturnValue(DbParameter returnValue)
		{
			object value = returnValue.Value;
			return value is int ? (int)value : -1;
		}


		//public Guid GetGuid(DbDataReader reader, int index)
		//{
		//	return new Guid((byte[])reader[index]);
		//}


		//public bool GetBoolean(DbDataReader reader, int index)
		//{
		//	return reader.GetDecimal(index) == 1 ? true : false;
		//}


		#endregion

	}

}
