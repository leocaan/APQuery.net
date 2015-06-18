using Symber.Web.Data;
using System;
using System.Collections.Generic;

namespace Symber.Web.Report
{

	/// <summary>
	/// Composite APRptColumn.
	/// </summary>
	public abstract class CompositeAPRptColumn : APRptColumn
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new TextAPRptColumn.
		/// </summary>
		/// <param name="id">Column unique ID.</param>
		/// <param name="title">Title.</param>
		public CompositeAPRptColumn(string id, string title)
			: base(id, title)
		{
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add an APSqlSelectPhrase to list with the column.
		/// </summary>
		/// <param name="phrases">The list of APSqlSelectPhrase.</param>
		public override void AddToQuerySelectPhrases(List<APSqlSelectPhrase> phrases)
		{
			throw new NotImplementedException();
		}


		#endregion

	}

}
