using Symber.Web.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Web.Configuration;

namespace Symber.Web.Data
{

	/// <summary>
	/// Database commonn functions.
	/// </summary>
	public class APDatabase : IDisposable
	{

		#region [ Static Fields ]


		private static APQuerySection cfgSection;
		private static APDalProviderCollection providersCollection;
		private static APDalProvider defaultProvider;


		#endregion


		#region [ Static Properties ]


		/// <summary>
		/// All DAL providers.
		/// </summary>
		public static APDalProviderCollection Providers
		{
			get { return providersCollection; }
		}


		/// <summary>
		/// Defualt DAL provider.
		/// </summary>
		public static APDalProvider Provider
		{
			get { return defaultProvider; }
		}


		#endregion


		// -------------------------------------------------------------------------------------------------------------------


		#region [ Fields ]


		private APDalProvider _provider;
		private DbConnection _connection;
		private DbTransaction _transaction;


		#endregion


		#region [ Constructors ]


		static APDatabase()
		{
			try
			{
				LoadConfig(null);
			}
			catch
			{

			}
		}


		/// <summary>
		/// Load config file.
		/// Defaults, you can use 'null' parameter, Web project maybe find web.config, winform project maybe find
		/// app.config.
		/// </summary>
		/// <param name="configurationManager">The configurationManager.</param>
		public static void LoadConfig(System.Configuration.Configuration configurationManager)
		{
			if (configurationManager == null)
			{
				//cfgSection = (APQuerySection)WebConfigurationManager.GetSection("APQuery");
				cfgSection = (APQuerySection)ConfigurationManager.GetSection("APQuery");
			}
			else
			{
				cfgSection = (APQuerySection)configurationManager.GetSection("APQuery");
			}
			if (cfgSection == null)
				throw new ConfigurationErrorsException(APResource.GetString(APResource.APConfig_NotFindSection, "APQuery"));

			providersCollection = new APDalProviderCollection();
			ProvidersHelper.InstantiateProviders(cfgSection.Providers, providersCollection, typeof(APDalProvider));
			defaultProvider = Providers[cfgSection.DefaultProvider];
			if (defaultProvider == null)
				throw new ConfigurationErrorsException(APResource.GetString(APResource.APProvider_NotFound, cfgSection.DefaultProvider));
		}


		/// <summary>
		/// Create a new APDatabase object.
		/// </summary>
		public APDatabase()
		{
			_provider = defaultProvider;
		}


		/// <summary>
		/// Create a new APDatabase object with DAL provider name.
		/// </summary>
		/// <param name="providerName">DAL provider name.</param>
		public APDatabase(string providerName)
		{
			if (providerName == null)
				throw new ArgumentNullException("providerName");
			if (providerName == String.Empty)
				throw new ArgumentException("providerName");

			_provider = Providers[providerName];
		}


		/// <summary>
		/// Create a new APDatabase object with DAL provider.
		/// </summary>
		/// <param name="provider">DAL providers.</param>
		public APDatabase(APDalProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");

			_provider = provider;
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Starts a database transaction.
		/// </summary>
		public virtual void BeginTrans()
		{
			if (_transaction == null)
				_transaction = Connection.BeginTransaction();
		}


		/// <summary>
		/// Commits the database transaction.
		/// </summary>
		public virtual void Commit()
		{
			if (_transaction != null)
			{
				_transaction.Commit();
				_transaction.Dispose();
				_transaction = null;
			}
		}


		/// <summary>
		/// Rolls back a transaction from a pending state.
		/// </summary>
		public virtual void Rollback()
		{
			if (_transaction != null)
			{
				_transaction.Rollback();
				_transaction.Dispose();
				_transaction = null;
			}
		}


		/// <summary>
		/// Close the database connection
		/// </summary>
		public virtual void Close()
		{
			Dispose();
		}


		/// <summary>
		/// Executes an SQL statement against the Connection and returns the number of rows affected.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The number of rows affected.</returns>
		public virtual int ExecuteNonQuery(APSqlCommand command)
		{
			DbCommand dbCmd = _provider.BuildCommand(command);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			return dbCmd.ExecuteNonQuery();
		}


		/// <summary>
		/// Executes the query, and returns the first column of the first row in the result set returned
		/// by the query. Additional columns or rows are ignored.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public virtual object ExecuteScalar(APSqlCommand command)
		{
			if (command is APSqlInsertCommand)
				(command as APSqlInsertCommand).NeedReturnAutoIncrement = true;
			DbCommand dbCmd = _provider.BuildCommand(command);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			return dbCmd.ExecuteScalar();
		}


		/// <summary>
		/// Executes the CommandText against the Connection and builds an IDataReader.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>An IDataReader object.</returns>
		public virtual IDataReader ExecuteReader(APSqlCommand command)
		{
			DbCommand dbCmd = _provider.BuildCommand(command);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			return dbCmd.ExecuteReader();
		}


		/// <summary>
		/// Executes the CommandText against the Connection and builds an IDataReader. Return the count of SELECT command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>Count of SELECT command records.</returns>
		public virtual int ExecuteSizeOfSelect(APSqlSelectCommand command)
		{
			DbCommand dbCmd = _provider.BuildSizeOfSelectCommand(command);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			return (int)dbCmd.ExecuteScalar();
		}


		/// <summary>
		/// Get a new auto-increment ID.
		/// </summary>
		/// <param name="tableName">Table name.</param>
		/// <param name="columnName">Column name.</param>
		/// <returns>A new ID.</returns>
		public virtual long GetNewId(string tableName, string columnName)
		{
			DbCommand dbCmd = _provider.BuildNewIdCommand(tableName, columnName);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			return Convert.ToInt64(dbCmd.ExecuteScalar());
		}


		/// <summary>
		/// Create a database command from a SQL expression.
		/// </summary>
		/// <param name="sql">SQL expression.</param>
		/// <returns>Database command object.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public virtual DbCommand CreateSqlCommand(string sql)
		{
			DbCommand dbCmd = Connection.CreateCommand();
			dbCmd.Transaction = Transaction;
			dbCmd.CommandType = CommandType.Text;
			dbCmd.CommandText = sql;
			return dbCmd;
		}


		/// <summary>
		/// Create a database command from a SQL expression.
		/// </summary>
		/// <param name="sql">SQL expression.</param>
		/// <param name="args">arguments.</param>
		/// <returns>Database command object.</returns>
		public virtual DbCommand CreateSqlCommand(string sql, params object[] args)
		{
			return CreateSqlCommand(String.Format(sql, args));
		}


		/// <summary>
		/// Adds or refreshes rows in the DataSet.
		/// </summary>
		/// <param name="dbCmd">Database command.</param>
		/// <param name="ds">A DataSet to fill with records and, if necessary, schema.</param>
		/// <param name="tableNames">Array of table names in dataset.</param>
		public void FillDataSet(DbCommand dbCmd, DataSet ds, params string[] tableNames)
		{
			if (dbCmd == null) throw new ArgumentNullException("cmd");
			if (ds == null) throw new ArgumentNullException("ds");

			using (DbDataAdapter adapter = Provider.NewAdapter())
			{
				adapter.SelectCommand = dbCmd;

				string tableName = "Table";
				for (int i = 0; i < tableNames.Length; i++)
				{
					if ((tableNames[i] == null) || (tableNames[i] == string.Empty))
						throw new ArgumentException("A value of tableNames was provided as null or empty string. ", "tableNames");

					adapter.TableMappings.Add(tableName, tableNames[i]);
					tableName += (i + 1).ToString();
				}

				adapter.Fill(ds);
			}
		}


		/// <summary>
		/// Is view exist.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <returns>Whether the view is exist.</returns>
		public virtual bool IsViewExist(string name)
		{
			DbCommand dbCmd = _provider.BuildIsViewExistCommand(name);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			return Convert.ToInt32(dbCmd.ExecuteScalar()) > 0;
		}


		/// <summary>
		/// Create view.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <param name="command">The command to create view.</param>
		public virtual void CreateView(string name, APSqlSelectCommand command)
		{
			DbCommand dbCmd = _provider.BuildCreateViewCommand(name, command);
			dbCmd.Connection = Connection;
			dbCmd.Transaction = _transaction;
			dbCmd.ExecuteNonQuery();
		}


		#endregion


		#region [ Protected Properties ]


		/// <summary>
		/// Current provider.
		/// </summary>
		protected APDalProvider ThisProvider
		{
			get { return _provider; }
		}


		#endregion


		#region [ Public Properties ]


		/// <summary>
		/// Database connection.
		/// </summary>
		public DbConnection Connection
		{
			get
			{
				if (_connection == null)
					_connection = _provider.NewConnection();
				return _connection;
			}
		}


		/// <summary>
		/// Current database transaction.
		/// </summary>
		public DbTransaction Transaction
		{
			get { return _transaction; }
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
				if (_connection != null)
				{
					_connection.Close();
					_connection = null;
				}
			}
		}


		#endregion

	}

}
