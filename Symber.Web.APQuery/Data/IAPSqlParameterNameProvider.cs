
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL command parameter name provider.
	/// If an APColumnDef need more than one condition, the parameter name must be differented.
	/// Provider can provide not repeat parameter names.
	/// </summary>
	public interface IAPSqlParameterNameProvider
	{

		/// <summary>
		/// Gets command parameter name of SQL assignment Expression.
		/// </summary>
		/// <param name="assignmentExpr">'SELECT' phrase.</param>
		/// <returns>The parameter name.</returns>
		string GetParameterName(APSqlOperateExpr assignmentExpr);

	}

}
