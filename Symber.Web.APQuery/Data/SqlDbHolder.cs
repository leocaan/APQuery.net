using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQLServer helper class.
	/// </summary>
	public sealed class SqlDbHolder : DbHolderBase
	{

		#region [ Static Methods ]


		/// <summary>
		/// Get SQLServer help object.
		/// </summary>
		/// <param name="providerName">Provider name.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>A SqlDbHolder object.</returns>
		public static SqlDbHolder GetSqlDbHolder(string providerName, string connectionString)
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
					return new SqlDbHolder(factory, connectionString);
			}
			return new SqlDbHolder(SqlClientFactory.Instance, connectionString);
		}


		/// <summary>
		/// Get SQLServer help object
		/// </summary>
		/// <param name="factory">Factory object</param>
		/// <param name="connectionString">Connection string</param>
		/// <returns>A SqlDbHolder object.</returns>
		public static SqlDbHolder GetSqlDbHolder(DbProviderFactory factory, string connectionString)
		{
			return new SqlDbHolder(factory, connectionString);
		}


		#endregion


		#region [ Fields ]


		private DbProviderFactory _factory = null;
		private string _connectionString;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new instance of SqlDbHolder.
		/// </summary>
		/// <param name="factory">Factory object.</param>
		/// <param name="connectionString">Connection string.</param>
		public SqlDbHolder(DbProviderFactory factory, string connectionString)
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
			dbp.Value = parameterValue ?? DBNull.Value;
			dbp.Direction = direction;
			dbp.DbType = type;
			command.Parameters.Add(dbp);
			return dbp;
		}


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


		#endregion

	}

}
