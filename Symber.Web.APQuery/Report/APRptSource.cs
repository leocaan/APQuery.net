using Symber.Web.Data;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Source of View and Report.
	/// </summary>
	public abstract class APRptSource
	{

		#region [ Fields ]


		private string _innerKey;
		private string _sourceName;
		private string _description;
		private List<APRptColumnCollection> _references;
		private APRptColumnCollection _allColumns;
		private APRptViewDef _defaultViewDef;
		private APRptReportDef _defaultReportDef;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptSource.
		/// </summary>
		/// <param name="innerKey">View source key.</param>
		/// <param name="sourceName">View source name.</param>
		/// <param name="description">View source description.</param>
		protected APRptSource(string innerKey, string sourceName, string description)
		{
			_innerKey = innerKey;
			_sourceName = sourceName;
			_description = description;
			_references = new List<APRptColumnCollection>();
			_allColumns = new APRptColumnCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// APRptView source key.
		/// </summary>
		public string InnerKey
		{
			get { return _innerKey; }
		}


		/// <summary>
		/// APRptView source name.
		/// </summary>
		public string SourceName
		{
			get { return _sourceName; }
		}


		/// <summary>
		/// APRptView source description.
		/// </summary>
		public string Description
		{
			get { return _description; }
		}


		/// <summary>
		/// All APRptColumn in view source.
		/// </summary>
		public APRptColumnCollection AllColumns
		{
			get { return _allColumns; }
		}


		/// <summary>
		/// Default view defined about this source.
		/// </summary>
		public APRptViewDef DefaultViewDef
		{
			get { return _defaultViewDef; }
			set { _defaultViewDef = value; }
		}


		/// <summary>
		/// Default report defined about this source.
		/// </summary>
		public APRptReportDef DefaultReportDef
		{
			get { return _defaultReportDef; }
			set { _defaultReportDef = value; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add reference.
		/// </summary>
		/// <param name="reference">The reference of APRptColumn.</param>
		public virtual void AddReference(APRptColumnCollection reference)
		{
			_references.Add(reference);

			foreach (APRptColumn ex in reference)
			{
				_allColumns.Add(ex);
			}
		}


		/// <summary>
		/// Gets the filter fields from that can be field columns.
		/// </summary>
		/// <returns>The fields.</returns>
		public virtual IEnumerable<object> GetAvaliableFilterFields()
		{
			foreach (var column in _allColumns)
			{
				if (column.CanBeField)
				{
					yield return column.GetFilterField();
				}
			}
		}


		/// <summary>
		/// Gets the reference list of all APRptColumn collection.
		/// </summary>
		/// <returns>Reference list of all APRptColumn collection.</returns>
		public virtual List<APRptColumnCollection> GetReferences()
		{
			return _references;
		}


		/// <summary>
		/// Gets the prime SQL 'FROM' phrases.
		/// </summary>
		/// <returns>List of APSqlFromPhrase.</returns>
		public abstract List<APSqlFromPhrase> GetPrimeFormPhrases();


		/// <summary>
		/// Gets the prime SQL 'WHERE' phrases.
		/// </summary>
		/// <returns>List of APSqlWherePhrase.</returns>
		public abstract List<APSqlWherePhrase> GetPrimeWherePhrases();


		/// <summary>
		/// Gets the SQL 'WHERE' phrases as fuzzy search.
		/// </summary>
		/// <param name="searchString">The search string.</param>
		/// <returns>The APSqlWherePhrase.</returns>
		public virtual APSqlWherePhrase GetFuzzySearchPhrase(string searchString)
		{
			return null;
		}


		/// <summary>
		/// Gets the prime SQL 'FROM' phrases.
		/// </summary>
		/// <returns>List of APSqlSelectPhrase.</returns>
		public abstract List<APSqlSelectPhrase> GetPrimeSelectPhrases();


		/// <summary>
		/// Gets the prime SQL 'FROM' phrases when group report part.
		/// </summary>
		/// <returns>List of APSqlSelectPhrase.</returns>
		public abstract List<APSqlSelectPhrase> GetGroupSelectPhrases();


		/// <summary>
		/// Gets the prime SQL 'FROM' phrases when group report part.
		/// </summary>
		/// <returns>List of APSqlSelectPhrase.</returns>
		public abstract List<APSqlExprPhrase> GetGroupPhrases();


		/// <summary>
		/// Gets the primary expr in paging query.
		/// </summary>
		/// <returns>The Primary APSqlSelectExpr.</returns>
		public abstract APSqlExpr GetPrimaryExpr();


		#endregion

	}

}
