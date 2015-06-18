using System;
using System.Collections.Generic;
using System.Data;

namespace Symber.Web.Data
{
	/// <summary>
	/// APDatabase Extensions.
	/// </summary>
	public static class APDatabaseExtensions
	{

		/// <summary>
		/// Query extensions.
		/// </summary>
		/// <typeparam name="T">T.</typeparam>
		/// <param name="db">APDatabase.</param>
		/// <param name="command">The 'SELECT' command.</param>
		/// <param name="fillup">Fill function.</param>
		/// <returns>IEnumerable.</returns>
		public static IEnumerable<T> Query<T>(this APDatabase db, APSqlSelectCommand command, Func<IDataReader, T> fillup)
		{
			using (IDataReader reader = db.ExecuteReader(command))
			{
				while (reader.Read())
				{
					yield return fillup(reader);
				}
			}
		}


		/// <summary>
		/// Query extensions.
		/// </summary>
		/// <typeparam name="T">T.</typeparam>
		/// <param name="db">APDatabase.</param>
		/// <param name="rawSql">The Raw SQL expression.</param>
		/// <param name="fillup">Fill function.</param>
		/// <returns>IEnumerable.</returns>
		public static IEnumerable<T> Query<T>(this APDatabase db, string rawSql, Func<IDataReader, T> fillup)
		{
			using (IDataReader reader = db.CreateSqlCommand(rawSql).ExecuteReader())
			{
				while (reader.Read())
				{
					yield return fillup(reader);
				}
			}
		}


		/// <summary>
		/// Query extensions.
		/// </summary>
		/// <param name="db">APDatabase.</param>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The count of "SELECT COUNT(*)" command.</returns>
		public static int SizeOf(this APDatabase db, APSqlSelectCommand command)
		{
			return db.ExecuteSizeOfSelect(command);
		}


		/// <summary>
		/// Map extensions.
		/// </summary>
		/// <typeparam name="T">T.</typeparam>
		/// <param name="db">APDatabase.</param>
		/// <param name="reader">IDataReader.</param>
		/// <param name="fillup">Fill function.</param>
		/// <returns>IEnumerable.</returns>
		public static IEnumerable<T> Map<T>(this APDatabase db, IDataReader reader, Func<IDataReader, T> fillup)
		{
			while (reader.Read())
			{
				yield return fillup(reader);
			}
		}

	}

}
