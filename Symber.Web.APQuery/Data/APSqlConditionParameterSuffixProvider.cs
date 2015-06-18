
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL command parameter suffix provider.
	/// If an APColumnDef need more than one condition, the parameter name must be differented.
	/// Provider can provide not repeat parameter names.
	/// </summary>
	public class APSqlParameterSuffixProvider : IAPSqlParameterNameProvider
	{

		#region [ Fields ]


		private int _index;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlConditionParameterSuffixProvider.
		/// </summary>
		public APSqlParameterSuffixProvider()
		{
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Gets command parameter name of SQL assignment Expression.
		/// </summary>
		/// <param name="assignmentExpr">'SELECT' phrase.</param>
		/// <returns>The parameter name.</returns>
		public virtual string GetParameterName(APSqlOperateExpr assignmentExpr)
		{
			return assignmentExpr.ParamName + _index++;
		}


		#endregion

	}

}
