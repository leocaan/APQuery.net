using System;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL Query parser.
	/// </summary>
	public abstract partial class APQueryParser
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new query parser.
		/// </summary>
		public APQueryParser()
		{
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Build 'SELECT' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		public DbCommand BuildSelectCommand(APSqlSelectCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");
			if (command.Take != null && command.Take <= 0)
				throw new APDataException(APResource.APData_QueryTakeError);
			if (command.Skip != null)
			{
				if (command.Skip < 0)
					throw new APDataException(APResource.APData_QuerySkipError);
			}

			return ParseSelect(command);
		}


		/// <summary>
		/// Build 'SELECT COUNT(*)' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		public DbCommand BuildSizeOfSelectCommand(APSqlSelectCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			return ParseSizeOfSelect(command);
		}


		/// <summary>
		/// Build 'DELETE' command.
		/// </summary>
		/// <param name="command">The 'DELETE' command.</param>
		/// <returns>The DbCommand.</returns>
		public DbCommand BuildDeleteCommand(APSqlDeleteCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			return ParseDelete(command);
		}


		/// <summary>
		/// Build 'UPDATE' command.
		/// </summary>
		/// <param name="command">The 'UPDATE' command.</param>
		/// <returns>The DbCommand.</returns>
		public DbCommand BuildUpdateCommand(APSqlUpdateCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			return ParseUpdate(command);
		}


		/// <summary>
		/// Build 'INSERT' command.
		/// </summary>
		/// <param name="command">The 'INSERT' command.</param>
		/// <returns>The DbCommand.</returns>
		public DbCommand BuildInsertCommand(APSqlInsertCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			return ParseInsert(command);
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Parse 'SELECT' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		protected abstract DbCommand ParseSelect(APSqlSelectCommand command);


		/// <summary>
		/// Parse 'SELECT COUNT(*)' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		protected abstract DbCommand ParseSizeOfSelect(APSqlSelectCommand command);


		/// <summary>
		/// Parse 'DELETE' command.
		/// </summary>
		/// <param name="command">The 'DELETE' command.</param>
		/// <returns>The DbCommand.</returns>
		protected abstract DbCommand ParseDelete(APSqlDeleteCommand command);


		/// <summary>
		/// Parse 'UPDATE' command.
		/// </summary>
		/// <param name="command">The 'UPDATE' command.</param>
		/// <returns>The DbCommand.</returns>
		protected abstract DbCommand ParseUpdate(APSqlUpdateCommand command);


		/// <summary>
		/// Parse 'INSERT' command.
		/// </summary>
		/// <param name="command">The 'INSERT' command.</param>
		/// <returns>The DbCommand.</returns>
		protected abstract DbCommand ParseInsert(APSqlInsertCommand command);


		/// <summary>
		/// Alias name can be unusual, sometime need include in '';
		/// </summary>
		/// <param name="alias">Alias name.</param>
		/// <returns>Suitable alias name.</returns>
		protected string JudgeAliasName(string alias)
		{
			if (Regex.IsMatch(alias, @"^\w+$"))
				return alias;
			return "[" + alias + "]";
		}


		#endregion

	}

}
