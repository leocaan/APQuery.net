using System;
using System.Data;

namespace Symber.Web.Data
{

	/// <summary>
	/// APColumnDef Extensions.
	/// </summary>
	public static class APColumnDefExtensions
	{

		#region [ Set & Get Extensions ]


		/// <summary>
		/// Create a 'SET' phrase with specified value.
		/// </summary>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="value">Value.</param>
		/// <returns>Set phrase.</returns>
		public static APSqlSetPhrase SetValue<T>(this APColumnDef columnDef, T value)
		{
			return new APSqlSetPhrase(columnDef, value);
		}


		/// <summary>
		/// Create a 'SET' phrase with specified value.
		/// </summary>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="value">Value.</param>
		/// <param name="paramName">SqlCommand parameter name.</param>
		/// <returns>Set phrase.</returns>
		public static APSqlSetPhrase SetValue<T>(this APColumnDef columnDef, T value, string paramName)
		{
			return new APSqlSetPhrase(columnDef, value, paramName);
		}


		/// <summary>
		/// Create a 'SET' phrase with specified value. When set a column equals another column.
		/// </summary>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="value">Value.</param>
		/// <returns>Set phrase.</returns>
		public static APSqlSetPhrase SetValue(this APColumnDef columnDef, APColumnDef value)
		{
			return new APSqlSetPhrase(columnDef, value);
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(this APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<T>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(this APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<T>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="throwIfValidColumnName">throw if the name specified is not a valid column name.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(this APColumnDef columnDef, IDataReader reader, bool throwIfValidColumnName)
		{
			return GetValue<T>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName, defaultValue: default(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <param name="throwIfValidColumnName">throw if the name specified is not a valid column name.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(this APColumnDef columnDef, IDataReader reader, string columnName, bool throwIfValidColumnName)
		{
			return GetValue<T>(columnDef, reader, columnName, throwIfValidColumnName, defaultValue: default(T));
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="throwIfValidColumnName">throw if the name specified is not a valid column name.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(this APColumnDef columnDef, IDataReader reader, bool throwIfValidColumnName, T defaultValue)
		{
			return GetValue<T>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName, defaultValue);
		}


		/// <summary>
		/// Get value of type T from a data reader.
		/// </summary>
		/// <typeparam name="T">Type to get.</typeparam>
		/// <param name="columnDef">APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <param name="throwIfValidColumnName">throw if the name specified is not a valid column name.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <returns>Value of type T.</returns>
		public static T GetValue<T>(this APColumnDef columnDef, IDataReader reader, string columnName, bool throwIfValidColumnName, T defaultValue)
		{
			int index;
			try
			{
				index = reader.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				if (throwIfValidColumnName)
					throw;
				return defaultValue;
			}

			if (reader.IsDBNull(index))
				return defaultValue;
			if (typeof(T).IsEnum)
				return (T)(object)reader.GetInt32(index);
			if (typeof(T).IsGenericType && typeof(T).IsValueType)
				return (T)Convert.ChangeType(reader[index], Nullable.GetUnderlyingType(typeof(T)));
			if (typeof(T) == typeof(Guid) && reader[index] is Array)
				return (T)(object)(new Guid((byte[])reader[index]));
			return (T)Convert.ChangeType(reader[index], typeof(T));
		}


		#endregion


		#region [ 'AS' Extensions ]


		/// <summary>
		/// Create a new 'SELECT' phrase with alias.
		/// </summary>
		/// <param name="columnDef">The column define.</param>
		/// <param name="alias">Alias.</param>
		/// <returns>'SELECT' phrase.</returns>
		public static APSqlSelectPhrase As(this APColumnDef columnDef, string alias)
		{
			return new APSqlSelectPhrase(columnDef, alias);
		}


		#endregion


		#region [ Aggregation Extensions ]


		/// <summary>
		/// Build a SQL aggregation Expression of average.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of average.</returns>
		public static APSqlAggregationExpr Avg(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.AVG);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of average.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of average.</returns>
		public static APSqlAggregationExpr Avg(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.AVG, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of binary checksum.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of binary checksum.</returns>
		public static APSqlAggregationExpr BinaryChecksum(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.BINARY_CHECKSUM);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of checksum.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of checksum.</returns>
		public static APSqlAggregationExpr Checksum(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.CHECKSUM);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of checksum agg.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of checksum agg.</returns>
		public static APSqlAggregationExpr ChecksumAgg(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.CHECKSUM_AGG);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of checksum agg.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of checksum agg.</returns>
		public static APSqlAggregationExpr ChecksumAgg(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.CHECKSUM_AGG, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of count.</returns>
		public static APSqlAggregationExpr Count(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.COUNT);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of count.</returns>
		public static APSqlAggregationExpr Count(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.COUNT, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count big.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of count big.</returns>
		public static APSqlAggregationExpr CountBig(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.COUNT_BIG);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of count big.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of count big.</returns>
		public static APSqlAggregationExpr CountBig(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.COUNT_BIG, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of grouping.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of grouping.</returns>
		public static APSqlAggregationExpr Grouping(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.GROUPING);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of max.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of max.</returns>
		public static APSqlAggregationExpr Max(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.MAX);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of max.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of max.</returns>
		public static APSqlAggregationExpr Max(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.MAX, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of min.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of min.</returns>
		public static APSqlAggregationExpr Min(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.MIN);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of min.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of min.</returns>
		public static APSqlAggregationExpr Min(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.MIN, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of sum.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of sum.</returns>
		public static APSqlAggregationExpr Sum(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.SUM);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of sum.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="mode">Select mode, 'ALL' or 'DISTINCT'.</param>
		/// <returns>SQL aggregation Expression of sum.</returns>
		public static APSqlAggregationExpr Sum(this APColumnDef columnDef, APSqlSelectMode mode)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.SUM, mode);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical standard deviation.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of statistical standard deviation.</returns>
		public static APSqlAggregationExpr Stdev(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.STDEV);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical standard deviation for the population.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of statistical standard deviation for the population.</returns>
		public static APSqlAggregationExpr Stdevp(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.STDEVP);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical variance.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of statistical variance.</returns>
		public static APSqlAggregationExpr Var(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.VAR);
		}


		/// <summary>
		/// Build a SQL aggregation Expression of statistical variance for the population.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <returns>SQL aggregation Expression of statistical variance for the population.</returns>
		public static APSqlAggregationExpr Varp(this APColumnDef columnDef)
		{
			return new APSqlAggregationExpr(new APSqlColumnExpr(columnDef), APSqlAggregationType.VARP);
		}


		#endregion


		#region [ DateGroup Extensions ]


		/// <summary>
		/// Build a SQL date group Expression.
		/// </summary>
		/// <param name="columnDef">Column definition.</param>
		/// <param name="dateGroupMode">Date group mode.</param>
		/// <returns>SQL date group Expression.</returns>
		public static APSqlDateGroupExpr DateGroup(this DateTimeAPColumnDef columnDef, APSqlDateGroupMode dateGroupMode)
		{
			return new APSqlDateGroupExpr(new APSqlColumnExpr(columnDef), dateGroupMode);
		}


		#endregion


		#region [ ...APColumnDef ]


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">BooleanAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Boolean GetValue(this BooleanAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Boolean>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Boolean));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">BooleanAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Boolean GetValue(this BooleanAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Boolean>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Boolean));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">DateTimeAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static DateTime GetValue(this DateTimeAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<DateTime>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(DateTime));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">DateTimeAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static DateTime GetValue(this DateTimeAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<DateTime>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(DateTime));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">GuidAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Guid GetValue(this GuidAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Guid>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Guid));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">GuidAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Guid GetValue(this GuidAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Guid>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Guid));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <typeparam name="T">Type of enum.</typeparam>
		/// <param name="columnDef">EnumAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static T GetValue<T>(this EnumAPColumnDef<T> columnDef, IDataReader reader)
		{
			return GetValue<T>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(T));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <typeparam name="T">Type of enum.</typeparam>
		/// <param name="columnDef">EnumAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static T GetValue<T>(this EnumAPColumnDef<T> columnDef, IDataReader reader, string columnName)
		{
			return GetValue<T>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(T));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">Int16APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Int16 GetValue(this Int16APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Int16>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Int16));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">Int16APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Int16 GetValue(this Int16APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Int16>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Int16));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">Int32APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Int32 GetValue(this Int32APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Int32>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Int32));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">Int32APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Int32 GetValue(this Int32APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Int32>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Int32));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">Int64APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Int64 GetValue(this Int64APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Int64>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Int64));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">Int64APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Int64 GetValue(this Int64APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Int64>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Int64));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">UInt16APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static UInt16 GetValue(this UInt16APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<UInt16>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(UInt16));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">UInt16APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static UInt16 GetValue(this UInt16APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<UInt16>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(UInt16));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">UInt32APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static UInt32 GetValue(this UInt32APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<UInt32>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(UInt32));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">UInt32APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static UInt32 GetValue(this UInt32APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<UInt32>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(UInt32));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">UInt64APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static UInt64 GetValue(this UInt64APColumnDef columnDef, IDataReader reader)
		{
			return GetValue<UInt64>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(UInt64));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">UInt64APColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static UInt64 GetValue(this UInt64APColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<UInt64>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(UInt64));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">SingleAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Single GetValue(this SingleAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Single>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Single));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">SingleAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Single GetValue(this SingleAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Single>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Single));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">DoubleAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Double GetValue(this DoubleAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Double>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Double));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">DoubleAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Double GetValue(this DoubleAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Double>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Double));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">DecimalAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Decimal GetValue(this DecimalAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Decimal>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Decimal));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">DecimalAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Decimal GetValue(this DecimalAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Decimal>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Decimal));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">ByteAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Byte GetValue(this ByteAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Byte>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Byte));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">ByteAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Byte GetValue(this ByteAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Byte>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Byte));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">CharAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static Char GetValue(this CharAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<Char>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(Char));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">CharAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static Char GetValue(this CharAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<Char>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(Char));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">StringAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static String GetValue(this StringAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<String>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(String));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">StringAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static String GetValue(this StringAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<String>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(String));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">SByteAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static SByte GetValue(this SByteAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<SByte>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(SByte));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">SByteAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static SByte GetValue(this SByteAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<SByte>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(SByte));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">BytesAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <returns>Value</returns>
		public static byte[] GetValue(this BytesAPColumnDef columnDef, IDataReader reader)
		{
			return GetValue<byte[]>(columnDef, reader, columnDef.ColumnName, throwIfValidColumnName: true, defaultValue: default(byte[]));
		}


		/// <summary>
		/// Get value from a data reader.
		/// </summary>
		/// <param name="columnDef">BytesAPColumnDef</param>
		/// <param name="reader">Data reader.</param>
		/// <param name="columnName">Name of column.</param>
		/// <returns>Value</returns>
		public static byte[] GetValue(this BytesAPColumnDef columnDef, IDataReader reader, string columnName)
		{
			return GetValue<byte[]>(columnDef, reader, columnName, throwIfValidColumnName: true, defaultValue: default(byte[]));
		}


		#endregion

	}

}
