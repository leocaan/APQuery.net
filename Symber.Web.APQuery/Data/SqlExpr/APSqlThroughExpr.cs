
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL not parser through Expression.
	/// </summary>
	public class APSqlThroughExpr : APSqlOperateExpr, IAPSqlValueExpr
	{

		#region [ Static ]


		/// <summary>
		/// Create a new APSqlConstExpr;
		/// </summary>
		/// <param name="format">The Format.</param>
		/// <returns>APSqlConstExpr.</returns>
		public static APSqlThroughExpr Expr(string format)
		{
			return new APSqlThroughExpr(format);
		}


		/// <summary>
		/// Create a new APSqlConstExpr;
		/// </summary>
		/// <param name="format">The Format.</param>
		/// <param name="paramName">Command parameter name.</param>
		/// <returns>APSqlConstExpr.</returns>
		public static APSqlThroughExpr Expr(string format, string paramName)
		{
			return new APSqlThroughExpr(format, paramName);
		}


		/// <summary>
		/// Create a new APSqlConstExpr;
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Format.</param>
		/// <returns>APSqlConstExpr.</returns>
		public static APSqlThroughExpr Expr(APTableDef maybyTableDef, string format)
		{
			return new APSqlThroughExpr(maybyTableDef, format);
		}


		/// <summary>
		/// Create a new APSqlConstExpr;
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Format.</param>
		/// <param name="paramName">Command parameter name.</param>
		/// <returns>APSqlConstExpr.</returns>
		public static APSqlThroughExpr Expr(APTableDef maybyTableDef, string format, string paramName)
		{
			return new APSqlThroughExpr(maybyTableDef, format, paramName);
		}


		#endregion


		#region [ Fields ]


		private string _through;
		private string _paramName;
		private APTableDef _maybeTableDef;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlThroughExpr
		/// </summary>
		/// <param name="format">The Fromat.</param>
		public APSqlThroughExpr(string format)
			: this(null, format, null)
		{
		}


		/// <summary>
		/// Create a new APSqlThroughExpr
		/// </summary>
		/// <param name="format">The Fromat.</param>
		/// <param name="paramName">Command parameter name.</param>
		public APSqlThroughExpr(string format, string paramName)
			: this(null, format, paramName)
		{
		}


		/// <summary>
		/// Create a new APSqlThroughExpr
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Fromat.</param>
		public APSqlThroughExpr(APTableDef maybyTableDef, string format)
			: this(maybyTableDef, format, null)
		{
		}


		/// <summary>
		/// Create a new APSqlThroughExpr
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Fromat.</param>
		/// <param name="paramName">Command parameter name.</param>
		public APSqlThroughExpr(APTableDef maybyTableDef, string format, string paramName)
		{
			_maybeTableDef = maybyTableDef;
			_through = format;
			_paramName = paramName;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get
			{
				return _through;
			}
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return _maybeTableDef; }
		}


		/// <summary>
		/// Command parameter name.
		/// </summary>
		public override string ParamName
		{
			get
			{
				return _paramName;
			}
		}


		/// <summary>
		/// SQL value of assignment phrase expression.
		/// </summary>
		public string ValueExpr
		{
			get
			{
				return SelectExpr;
			}
		}


		#endregion

	}

}
