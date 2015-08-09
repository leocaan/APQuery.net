
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL raw string Expression.
	/// </summary>
	public class APSqlRawExpr : APSqlOperateExpr, IAPSqlValueExpr
	{

		#region [ Static ]


		/// <summary>
		/// Create a new APSqlRawExpr;
		/// </summary>
		/// <param name="format">The Format.</param>
		/// <returns>APSqlRawExpr.</returns>
		public static APSqlRawExpr Expr(string format)
		{
			return new APSqlRawExpr(format);
		}


		/// <summary>
		/// Create a new APSqlRawExpr;
		/// </summary>
		/// <param name="format">The Format.</param>
		/// <param name="paramName">Command parameter name.</param>
		/// <returns>APSqlRawExpr.</returns>
		public static APSqlRawExpr Expr(string format, string paramName)
		{
			return new APSqlRawExpr(format, paramName);
		}


		/// <summary>
		/// Create a new APSqlRawExpr;
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Format.</param>
		/// <returns>APSqlRawExpr.</returns>
		public static APSqlRawExpr Expr(APTableDef maybyTableDef, string format)
		{
			return new APSqlRawExpr(maybyTableDef, format);
		}


		/// <summary>
		/// Create a new APSqlRawExpr;
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Format.</param>
		/// <param name="paramName">Command parameter name.</param>
		/// <returns>APSqlRawExpr.</returns>
		public static APSqlRawExpr Expr(APTableDef maybyTableDef, string format, string paramName)
		{
			return new APSqlRawExpr(maybyTableDef, format, paramName);
		}


		#endregion


		#region [ Fields ]


		private string _through;
		private string _paramName;
		private APTableDef _maybeTableDef;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlRawExpr
		/// </summary>
		/// <param name="format">The Fromat.</param>
		public APSqlRawExpr(string format)
			: this(null, format, null)
		{
		}


		/// <summary>
		/// Create a new APSqlRawExpr
		/// </summary>
		/// <param name="format">The Fromat.</param>
		/// <param name="paramName">Command parameter name.</param>
		public APSqlRawExpr(string format, string paramName)
			: this(null, format, paramName)
		{
		}


		/// <summary>
		/// Create a new APSqlRawExpr
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Fromat.</param>
		public APSqlRawExpr(APTableDef maybyTableDef, string format)
			: this(maybyTableDef, format, null)
		{
		}


		/// <summary>
		/// Create a new APSqlRawExpr
		/// </summary>
		/// <param name="maybyTableDef">May be about Table defined.</param>
		/// <param name="format">The Fromat.</param>
		/// <param name="paramName">Command parameter name.</param>
		public APSqlRawExpr(APTableDef maybyTableDef, string format, string paramName)
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
