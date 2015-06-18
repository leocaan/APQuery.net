using Symber.Web.Data;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Lookup APRptColumn.
	/// </summary>
	public class LookupAPRptColumn : APRptColumn
	{

		#region [ Fields ]


		private APRelationDef _relationDef;
		private APRptColumn _relationShowColumn;
		private APTableDef _joinTable;
		private APSqlJoinType _joinType = APSqlJoinType.Inner;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new LookupAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="joinTable">Join Table.</param>
		/// <param name="joinType">Join Type.</param>
		/// <param name="relationDef">Relation define.</param>
		/// <param name="relationShowColumn">Show relation column.</param>
		public LookupAPRptColumn(APSqlOperateExpr selectExpr,
			APTableDef joinTable,
			APSqlJoinType joinType,
			APRelationDef relationDef,
			APRptColumn relationShowColumn)
			: base(selectExpr)
		{
			_joinTable = joinTable;
			_joinType = joinType;
			_relationDef = relationDef;
			_relationShowColumn = relationShowColumn;
		}


		/// <summary>
		/// Create a new LookupAPRptColumn.
		/// </summary>
		/// <param name="selectExpr">SQL 'SELECT' Expression.</param>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		/// <param name="joinTable">Join Table.</param>
		/// <param name="joinType">Join Type.</param>
		/// <param name="relationDef">Relation define.</param>
		/// <param name="relationShowColumn">Show relation column.</param>
		public LookupAPRptColumn(APSqlOperateExpr selectExpr, string id, string title,
			APTableDef joinTable,
			APSqlJoinType joinType,
			APRelationDef relationDef,
			APRptColumn relationShowColumn)
			: base(selectExpr, id, title)
		{
			_joinTable = joinTable;
			_joinType = joinType;
			_relationDef = relationDef;
			_relationShowColumn = relationShowColumn;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets relation define.
		/// </summary>
		public virtual APRelationDef RelationDef
		{
			get { return _relationDef; }
		}


		/// <summary>
		/// Gets show relation column.
		/// </summary>
		public virtual APRptColumn RelationShowColumn
		{
			get { return _relationShowColumn; }
		}


		#endregion


		#region [ Override Implementation of APRptColumn ]


		/// <summary>
		/// Gets the filter type of column.
		/// </summary>
		public override APRptFilterType FilterType
		{
			get { return _relationShowColumn.FilterType; }
		}


		/// <summary>
		/// Gets or sets client render formatter.
		/// </summary>
		public override string RenderFormatter
		{
			get { return base.RenderFormatter ?? "Lookup"; }
			set { base.RenderFormatter = value; }
		}

	
		/// <summary>
		/// Add an APSqlSelectPhrase to list with the column.
		/// </summary>
		/// <param name="phrases">The list of APSqlSelectPhrase.</param>
		public override void AddToQuerySelectPhrases(List<APSqlSelectPhrase> phrases)
		{
			base.AddToQuerySelectPhrases(phrases);
			_relationShowColumn.AddToQuerySelectPhrases(phrases);
		}


		/// <summary>
		/// Add an APSqlFromPhrase to list with the column.
		/// </summary>
		/// <param name="phrases">This list of APSqlFromPhrase.</param>
		public override void AddToQueryFromPhrases(List<APSqlFromPhrase> phrases)
		{
			base.AddToQueryFromPhrases(phrases);
			phrases.Add(new APSqlFromPhrase(_joinTable, _joinType, _relationDef));
		}


		/// <summary>
		/// Parse query where phrase.
		/// </summary>
		/// <param name="comparator">Comparator.</param>
		/// <param name="values">Values.</param>
		/// <returns>An APSqlWherePhrase.</returns>
		/// <exception cref="APRptFilterParseException">Throw exception on parse error.</exception>
		public override APSqlWherePhrase ParseQueryWherePhrase(APRptFilterComparator comparator, params string[] values)
		{
			return _relationShowColumn.ParseQueryWherePhrase(comparator, values);
		}


		#endregion

	}

}
