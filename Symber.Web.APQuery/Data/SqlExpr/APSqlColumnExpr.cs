
namespace Symber.Web.Data
{

	/// <summary>
	/// SQL '[table_name.]column_name' Expression.
	/// </summary>
	public class APSqlColumnExpr : APSqlOperateExpr, IAPSqlValueExpr
	{

		#region [ Fields ]


		private APColumnDef _columnDef;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlColumnExpr.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		public APSqlColumnExpr(APColumnDef columnDef)
		{
			_columnDef = columnDef;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Column definition.
		/// </summary>
		public APColumnDef ColumnDef
		{
			get { return _columnDef; }
		}


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get { return _columnDef.SelectExpr; }
		}


		/// <summary>
		/// Command parameter name.
		/// </summary>
		public override string ParamName
		{
			get { return _columnDef.ColumnName; }
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return _columnDef.TableDef; }
		}


		/// <summary>
		/// SQL value of assignment phrase expression.
		/// </summary>
		public string ValueExpr
		{
			get { return SelectExpr; }
		}


		#endregion

	}

}
