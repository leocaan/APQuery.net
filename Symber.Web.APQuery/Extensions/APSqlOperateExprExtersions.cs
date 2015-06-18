
namespace Symber.Web.Data
{

	/// <summary>
	/// APSqlOperateExpr Extersions.
	/// </summary>
	public static class APSqlOperateExprExtersions
	{

		#region [ 'AS' Extensions ]


		/// <summary>
		/// Create a new 'SELECT' phrase with alias.
		/// </summary>
		/// <param name="selectExpr">The SQL 'SELECT' Expression, can be alias.</param>
		/// <param name="alias">Alias.</param>
		/// <returns>'SELECT' phrase.</returns>
		public static APSqlSelectPhrase As(this APSqlOperateExpr selectExpr, string alias)
		{
			return new APSqlSelectPhrase(selectExpr, alias);
		}


		#endregion

	}

}
