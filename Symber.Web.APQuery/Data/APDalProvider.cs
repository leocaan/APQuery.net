using Symber.Web.Compilation;
using System;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;

namespace Symber.Web.Data
{

	/// <summary>
	/// Defines the contract to provide DAL services using custom dal providers.
	/// </summary>
	public abstract class APDalProvider : ProviderBase
	{

		#region [ Static ]


		/// <summary>
		/// SQL wildcard '%'.
		/// </summary>
		public const string Wildcard = "%";


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new DAL provider.
		/// </summary>
		protected APDalProvider()
		{

		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Create a new connection.
		/// </summary>
		/// <returns>A new connection.</returns>
		public abstract DbConnection NewConnection();


		/// <summary>
		/// Create a new data adapter.
		/// </summary>
		/// <returns>A new data adapter</returns>
		public abstract DbDataAdapter NewAdapter();


		/// <summary>
		/// Build 'SELECT' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildSelectCommand(APSqlSelectCommand command);


		/// <summary>
		/// Build 'SELECT COUNT(*)' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildSizeOfSelectCommand(APSqlSelectCommand command);


		/// <summary>
		/// Build 'DELETE' command.
		/// </summary>
		/// <param name="command">The 'DELETE' command.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildDeleteCommand(APSqlDeleteCommand command);


		/// <summary>
		/// Build 'UPDATE' command.
		/// </summary>
		/// <param name="command">The 'UPDATE' command.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildUpdateCommand(APSqlUpdateCommand command);


		/// <summary>
		/// Build 'INSERT' command.
		/// </summary>
		/// <param name="command">The 'INSERT' command.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildInsertCommand(APSqlInsertCommand command);


		/// <summary>
		/// Build command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The DbCommand.</returns>
		public DbCommand BuildCommand(APSqlCommand command)
		{
			if (command is APSqlSelectCommand)
				return BuildSelectCommand(command as APSqlSelectCommand);
			else if (command is APSqlDeleteCommand)
				return BuildDeleteCommand(command as APSqlDeleteCommand);
			else if (command is APSqlUpdateCommand)
				return BuildUpdateCommand(command as APSqlUpdateCommand);
			else if (command is APSqlInsertCommand)
				return BuildInsertCommand(command as APSqlInsertCommand);
			else
				throw new APDataException(APResource.APData_UnknownQueryCommand);
		}


		/// <summary>
		/// Synchronize database.
		/// </summary>
		/// <param name="businessModel">Business model.</param>
		public abstract void Sync(APGenBusinessModelSection businessModel);


		/// <summary>
		/// Build DB command to Get a new auto-increment id.
		/// </summary>
		/// <param name="tableName">The table name.</param>
		/// <param name="columnName">The column name.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildNewIdCommand(string tableName, string columnName);


		/// <summary>
		/// Build DB command to check is a view exist.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildIsViewExistCommand(string name);


		/// <summary>
		/// Build DB command to create a view.
		/// </summary>
		/// <param name="name">View name.</param>
		/// <param name="command">The command to create view.</param>
		/// <returns>The DbCommand.</returns>
		public abstract DbCommand BuildCreateViewCommand(string name, APSqlSelectCommand command);


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Throw an exception indicate connection name not specified.
		/// </summary>
		protected virtual void ThrowConnectionNameNotSpecified()
		{
			throw new ProviderException(APResource.APProvider_ConnectionNameNotSpecified);
		}


		/// <summary>
		/// Throw an exception indicate connection string not found.
		/// </summary>
		/// <param name="connectionStringName">Connection string name.</param>
		protected virtual void ThrowConnectionStringNotFound(string connectionStringName)
		{
			throw new ProviderException(APResource.GetString(APResource.APProvider_ConnectionStringNotFound, connectionStringName));
		}


		/// <summary>
		/// Throw an exception indicate parsed type not implemented.
		/// </summary>
		/// <param name="type">Type.</param>
		protected virtual void ThrowColumnParsedTypeNotImplemented(Type type)
		{
			throw new NotSupportedException(APResource.GetString(APResource.APDal_ColumnParsedTypeNotImplemented, type.Name));
		}


		/// <summary>
		/// Throw an exception indicate parsed type not implemented.
		/// </summary>
		/// <param name="type">DbType.</param>
		protected virtual void ThrowColumnParsedTypeNotImplemented(DbType type)
		{
			throw new NotSupportedException(APResource.GetString(APResource.APDal_ColumnParsedTypeNotImplemented, type.ToString()));
		}

		
		/// <summary>
		/// Throw an exception indicate unsupport.
		/// </summary>
		/// <param name="message">The message.</param>
		protected virtual void ThrowDBUnsupport(string message)
		{
			throw new NotSupportedException(APResource.GetString(APResource.APDal_DBUnsupport, message));
		}


		#endregion

	}

}
