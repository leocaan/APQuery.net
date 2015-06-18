
namespace Symber.Web.Data
{

	/// <summary>
	/// A SQL Expression, can be value of assignment phrase.
	/// </summary>
	public interface IAPSqlValueExpr
	{

		/// <summary>
		/// SQL value of assignment phrase expression.
		/// </summary>
		string ValueExpr { get; }

	}

}
