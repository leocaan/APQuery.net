using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Symber.Web.Data
{

	/// <summary>
	/// Manage data access in SQLServer database.
	/// </summary>
	public partial class SqlAPDalProvider : APDalProvider
	{

		#region [ Fields ]


		private ConnectionStringSettings _connectionString;
		private SqlDbHolder _holder;
		private APQueryParser _parser;


		#endregion


		#region [ Override Implementation of BaseProvider ]


		/// <summary>
		/// Initialize.
		/// </summary>
		/// <param name="name">Provider name.</param>
		/// <param name="config">Config.</param>
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
				throw new ArgumentNullException("config");

			base.Initialize(name, config);

			_connectionString = new ConnectionStringSettings(){
				 ConnectionString = config["connectionString"],
				 ProviderName = config["providerName"] ?? ""
			};

			if (String.IsNullOrEmpty(_connectionString.ConnectionString))
			{
				string connectionStringName = config["connectionStringName"];
				if (connectionStringName == null || connectionStringName.Length == 0)
					ThrowConnectionNameNotSpecified();

				_connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
				if (_connectionString == null)
					ThrowConnectionStringNotFound(connectionStringName);
			}

			_holder = _connectionString == null || String.IsNullOrEmpty(_connectionString.ProviderName)
				? SqlDbHolder.GetSqlDbHolder(SqlClientFactory.Instance, _connectionString.ConnectionString)
				: SqlDbHolder.GetSqlDbHolder(_connectionString.ProviderName, _connectionString.ConnectionString);

			_parser = new SqlAPQueryParser();
		}


		#endregion


		#region [ Override Implementation of APDalProvider ]


		/// <summary>
		/// Create a new connection.
		/// </summary>
		/// <returns>A new connection.</returns>
		public override DbConnection NewConnection()
		{
			return _holder.CreateConnection();
		}


		/// <summary>
		/// Create a new data adapter.
		/// </summary>
		/// <returns>A new data adapter.</returns>
		public override DbDataAdapter NewAdapter()
		{
			return new SqlDataAdapter();
		}


		/// <summary>
		/// Build 'SELECT' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		public override DbCommand BuildSelectCommand(APSqlSelectCommand command)
		{
			return _parser.BuildSelectCommand(command);
		}


		/// <summary>
		/// Build 'SELECT COUNT(*)' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		public override DbCommand BuildSizeOfSelectCommand(APSqlSelectCommand command)
		{
			return _parser.BuildSizeOfSelectCommand(command);
		}


		/// <summary>
		/// Build 'DELETE' command.
		/// </summary>
		/// <param name="command">The 'DELETE' command.</param>
		/// <returns>The DbCommand.</returns>
		public override DbCommand BuildDeleteCommand(APSqlDeleteCommand command)
		{
			return _parser.BuildDeleteCommand(command);
		}


		/// <summary>
		/// Build 'UPDATE' command.
		/// </summary>
		/// <param name="command">The 'UPDATE' command.</param>
		/// <returns>The DbCommand.</returns>
		public override DbCommand BuildUpdateCommand(APSqlUpdateCommand command)
		{
			return _parser.BuildUpdateCommand(command);
		}


		/// <summary>
		/// Build 'INSERT' command.
		/// </summary>
		/// <param name="command">The 'INSERT' command.</param>
		/// <returns>The DbCommand.</returns>
		public override DbCommand BuildInsertCommand(APSqlInsertCommand command)
		{
			return _parser.BuildInsertCommand(command);
		}


		/// <summary>
		/// Gets a new Id in APGen BusinessModel, when identityType="Provider".
		/// </summary>
		/// <param name="tableName">The table name.</param>
		/// <param name="columnName">The column name.</param>
		/// <returns>The DbCommand.</returns>
		public override DbCommand BuildNewIdCommand(string tableName, string columnName)
		{
			SqlCommand dbCmd = new SqlCommand();
			//cmd.CommandType = CommandType.StoredProcedure;
			//cmd.CommandText = currentIdNewIdProc;
			//cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = tableName + "." + columnName;
			dbCmd.CommandText = "update ap_query_mapid set value=value+1 output inserted.value where name=@name";
			dbCmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = tableName + "." + columnName;
			return dbCmd;
		}


		/// <summary>
		/// Build db command to check is a view exist.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public override DbCommand BuildIsViewExistCommand(string name)
		{
			SqlCommand dbCmd = new SqlCommand();
			dbCmd.CommandText = string.Format("select count(*) from sysobjects where xtype = 'V' and name = '{0}'", name);
			return dbCmd;
		}


		/// <summary>
		/// Build db command to create a view.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <param name="command">The command to create view.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public override DbCommand BuildCreateViewCommand(string name, APSqlSelectCommand command)
		{
			DbCommand dbCmd = BuildSelectCommand(command);
			dbCmd.CommandText = string.Format("create view {0}{1}as{1}", name, Environment.NewLine) + dbCmd.CommandText;
			return dbCmd;
		}


		#endregion

	}

}
