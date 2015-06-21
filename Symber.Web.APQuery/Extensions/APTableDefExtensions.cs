using Symber.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symber.Web.Data
{

	/// <summary>
	/// APTableDef Extensions.
	/// </summary>
	public static class APTableDefExtensions
	{

		#region [ 'JOIN' Extensions ]


		/// <summary>
		/// Create 'JOIN' phrase.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="joinType">Join Type.</param>
		/// <param name="wherePhrase">Join 'ON' phrase.</param>
		/// <returns>A 'FROM' phrase.</returns>
		public static APSqlFromPhrase Join(this APTableDef tableDef, APSqlJoinType joinType, APSqlWherePhrase wherePhrase)
		{
			return new APSqlFromPhrase(tableDef, joinType, wherePhrase);
		}


		/// <summary>
		/// Create 'LEFT JOIN' phrase.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="wherePhrase">Join 'ON' phrase.</param>
		/// <returns>A 'FROM' phrase.</returns>
		public static APSqlFromPhrase JoinLeft(this APTableDef tableDef, APSqlWherePhrase wherePhrase)
		{
			return new APSqlFromPhrase(tableDef, APSqlJoinType.Left, wherePhrase);
		}


		/// <summary>
		/// Create 'RIGHT JOIN' phrase.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="wherePhrase">Join 'ON' phrase.</param>
		/// <returns>A 'FROM' phrase.</returns>
		public static APSqlFromPhrase JoinRight(this APTableDef tableDef, APSqlWherePhrase wherePhrase)
		{
			return new APSqlFromPhrase(tableDef, APSqlJoinType.Right, wherePhrase);
		}


		/// <summary>
		/// Create 'INNERT JOIN' phrase.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="wherePhrase">Join 'ON' phrase.</param>
		/// <returns>A 'FROM' phrase.</returns>
		public static APSqlFromPhrase JoinInner(this APTableDef tableDef, APSqlWherePhrase wherePhrase)
		{
			return new APSqlFromPhrase(tableDef, APSqlJoinType.Inner, wherePhrase);
		}


		/// <summary>
		/// Create 'Cross JOIN' phrase.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="wherePhrase">Join 'ON' phrase.</param>
		/// <returns>A 'FROM' phrase.</returns>
		public static APSqlFromPhrase JoinCross(this APTableDef tableDef, APSqlWherePhrase wherePhrase)
		{
			return new APSqlFromPhrase(tableDef, APSqlJoinType.Cross, wherePhrase);
		}


		/// <summary>
		/// Create 'Full JOIN' phrase.
		/// </summary>
		/// <param name="tableDef">The table defined.</param>
		/// <param name="wherePhrase">Join 'ON' phrase.</param>
		/// <returns>A 'FROM' phrase.</returns>
		public static APSqlFromPhrase JoinFull(this APTableDef tableDef, APSqlWherePhrase wherePhrase)
		{
			return new APSqlFromPhrase(tableDef, APSqlJoinType.Full, wherePhrase);
		}


		#endregion

	}

}
