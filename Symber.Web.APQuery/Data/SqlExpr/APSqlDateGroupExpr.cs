using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL date grouping Expression.
	/// </summary>
	public class APSqlDateGroupExpr : APSqlOperateExpr
	{

		#region [ Fields ]


		private APSqlExpr _rawExpr;
		private APSqlDateGroupMode _dateGroupMode;


		#endregion
		

		#region [ Constructors ]


		/// <summary>
		/// Create a new APSqlDateGroupExpr.
		/// </summary>
		/// <param name="rowSelectExpr">Row SQL 'SELECT' Expression.</param>
		/// <param name="dateGroupMode">Date group mode.</param>
		public APSqlDateGroupExpr(APSqlExpr rowSelectExpr, APSqlDateGroupMode dateGroupMode)
		{
			_rawExpr = rowSelectExpr;
			_dateGroupMode = dateGroupMode;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Raw APSqlExpr.
		/// </summary>
		public APSqlExpr RawExpr
		{
			get { return _rawExpr; }
		}


		/// <summary>
		/// Date group mode.
		/// </summary>
		public APSqlDateGroupMode DateGroupMode
		{
			get { return _dateGroupMode; }
		}


		/// <summary>
		/// SQL 'SELECT' phrase expression.
		/// </summary>
		public override string SelectExpr
		{
			get
			{
				// support in APQueryParser.

				throw new NotSupportedException();
			}
		}


		/// <summary>
		/// May be about TableDef.
		/// </summary>
		internal override APTableDef MaybeTableDef
		{
			get { return _rawExpr.MaybeTableDef; }
		}


		/// <summary>
		/// Command parameter name.
		/// </summary>
		public override string ParamName
		{
			get
			{
				return RawExpr is APSqlOperateExpr
					? (RawExpr as APSqlOperateExpr).ParamName + DateGroupMode.ToString()
					: DateGroupMode.ToString();
			}
		}


		#endregion

	}

}
