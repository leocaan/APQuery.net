using System;
using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// Help build 'SET' phrases.
	/// </summary>
	public static class APSqlSetPhraseSelector
	{

		/// <summary>
		/// Select 'SET' phrase from metadata.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="data">The metadata.</param>
		/// <returns>The phrases.</returns>
		public static IEnumerable<APSqlSetPhrase> Select(APTableDef tableDef, object data)
		{
			Type typeColumnDef = typeof(APColumnDef);
			Type typeData = data.GetType();

			foreach (var piColumn in tableDef.GetType().GetProperties())
			{
				if (typeColumnDef.IsAssignableFrom(piColumn.PropertyType))
				{
					var columnDef = (APColumnDef)piColumn.GetValue(tableDef, null);

					var piProp = typeData.GetProperty(columnDef.ColumnName,
						System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
					if (piProp != null)
						yield return new APSqlSetPhrase(columnDef, piProp.GetValue(data, null));
				}
			}
		}


		/// <summary>
		/// Select 'SET' phrase from metadata.
		/// </summary>
		/// <typeparam name="Entity">Entity type.</typeparam>
		/// <param name="data">The metadata.</param>
		/// <returns>The phrases.</returns>
		public static IEnumerable<APSqlSetPhrase> Select<Entity>(object data)
		{
			Type typeColumnDef = typeof(APColumnDef);
			Type typeData = data.GetType();

			foreach (var piColumn in typeof(Entity).GetProperties(
				System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy))
			{
				if (typeColumnDef.IsAssignableFrom(piColumn.PropertyType))
				{
					var columnDef = (APColumnDef)piColumn.GetValue(null, null);

					var piProp = typeData.GetProperty(columnDef.ColumnName,
						System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
					if (piProp != null)
						yield return new APSqlSetPhrase(columnDef, piProp.GetValue(data, null));
				}
			}
		}

	}

}
