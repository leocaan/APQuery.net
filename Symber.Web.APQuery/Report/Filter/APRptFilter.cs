using Symber.Web.Data;
using System;

namespace Symber.Web.Report
{
	/// <summary>
	/// Virtual Report Filter with SQL 'WHERE' Expression.
	/// </summary>
	public class APRptFilter
	{

		#region [ Fields ]


		private APRptColumn _column;
		private APRptFilterComparator _comparator;
		private string[] _values;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptFilter.
		/// </summary>
		/// <param name="column">The column defined</param>
		/// <param name="comparator">Comparator.</param>
		/// <param name="values">Values</param>
		public APRptFilter(APRptColumn column, APRptFilterComparator comparator, params string[] values)
		{
			if (values.Length == 0)
				throw new ArgumentException("values");

			_column = column;
			_comparator = comparator;
			_values = values;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets APRptColumn.
		/// </summary>
		public APRptColumn Column
		{
			get { return _column; }
			set { _column = value; }
		}


		/// <summary>
		/// Gets or sets comparator.
		/// </summary>
		public APRptFilterComparator Comparator
		{
			get { return _comparator; }
			set { _comparator = value; }
		}


		/// <summary>
		/// Gets or sets values
		/// </summary>
		public string[] Values
		{
			get { return _values; }
			set { _values = value; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Parse to query where phrase.
		/// </summary>
		/// <returns>APSqlWherePhrase.</returns>
		public APSqlWherePhrase ParseQueryWherePhrase()
		{
			return _column.ParseQueryWherePhrase(_comparator, _values);
		}


		#endregion

	}
}
