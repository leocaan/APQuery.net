using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Symber.Web.Data
{

	/// <summary>
	/// Query parser for Oracle database.
	/// </summary>
	public class OracleAPQueryParser : APQueryParser
	{

		#region [ Constructors ]


		/// <summary>
		/// Create a new Oracle query parser.
		/// </summary>
		public OracleAPQueryParser()
		{
		}


		#endregion


		#region [ Private Methods ]


		private void AddParameter(OracleCommand dbCmd, string paramName, object paramValue, ParameterDirection direction)
		{
			if (dbCmd == null)
				return;

			OracleParameter param = dbCmd.CreateParameter();
			param.Direction = direction;
			param.ParameterName = paramName;

			if (paramValue == null)
			{
				param.Value = DBNull.Value;
			}
			else
			{
				Type valueType = paramValue.GetType();

				if (valueType.IsEnum)
					param.Value = (int)paramValue;
				else if (valueType == typeof(Boolean))
					param.Value = (bool)paramValue ? 1 : 0;
				else if (valueType == typeof(DateTime) && DateTime.MinValue.Equals(paramValue))
					param.Value = DBNull.Value;
				else if (valueType == typeof(Guid))
					param.Value = ((Guid)paramValue).ToByteArray();
				else
					param.Value = paramValue;
			}

			dbCmd.Parameters.Add(param);
		}


		private void WriteSelect(ParserWriter writer, APSqlSelectCommand command)
		{
			writer.WriteDirect("SELECT");

			if (command.SelectMode == APSqlSelectMode.DISTINCT)
				writer.Write("DISTINCT");

			APSqlSelectPhrase phrase = command.SelectExprClause.Next as APSqlSelectPhrase;
			bool isFirst = true;
			while (phrase != null)
			{
				if (!isFirst)
					writer.Write(',');
				else
					isFirst = false;

				WriteSelectExpression(writer, phrase.Expr);
				if (!string.IsNullOrEmpty(phrase.Alias))
					writer.Write("AS " + JudgeAliasName(phrase.Alias));


				phrase = phrase.Next as APSqlSelectPhrase;
			}
		}


		private void WriteSelectExpression(ParserWriter writer, APSqlExpr selectExpr)
		{
			if (selectExpr is APSqlAggregationExpr)
			{
				APSqlAggregationExpr expr = selectExpr as APSqlAggregationExpr;
				writer.Write(expr.AggregationType.ToString());
				writer.Write("(");
				if (expr.SelectMode == APSqlSelectMode.DISTINCT)
					writer.Write("DISTINCT");

				if (expr.RowSelectExpr is APSqlAsteriskExpr)
				{
					writer.Write("*");
				}
				else
				{
					WriteSelectExpression(writer, expr.RowSelectExpr);
				}

				writer.Write(")");
			}
			else if (selectExpr is APSqlDateGroupExpr)
			{
				APSqlDateGroupExpr expr = selectExpr as APSqlDateGroupExpr;

				string datepart = "";
				switch (expr.DateGroupMode)
				{
					case APSqlDateGroupMode.Day: datepart = "yyyy-mm-dd"; break;
					case APSqlDateGroupMode.Week: datepart = "yyyy-IW"; break;
					case APSqlDateGroupMode.Month: datepart = "yyyy-mm"; break;
					case APSqlDateGroupMode.Quarter: datepart = "yyyy-Q"; break;
					case APSqlDateGroupMode.Year: datepart = "yyyy"; break;
				}
				writer.Write("to_char(");
				WriteSelectExpression(writer, expr.RawExpr);
				writer.Write(", '{0}')", datepart);
			}
			else
			{
				writer.Write(selectExpr.SelectExpr);
			}
		}


		private void WriteDelete(ParserWriter writer, APSqlDeleteCommand command)
		{
			writer.WriteDirect("DELETE FROM");
			writer.Write(command.TableDef.TableName);
		}


		private void WriteUpdate(ParserWriter writer, APSqlUpdateCommand command)
		{
			writer.WriteDirect("UPDATE");
			writer.Write(command.TableDef.TableName);
		}


		private void WriteInsert(ParserWriter writer, APSqlInsertCommand command, APSqlValuesClause clause)
		{
			writer.WriteDirect("INSERT INTO");
			writer.Write(command.TableDef.TableName);

			if (clause != null && clause.Next != null)
			{
				APSqlSetPhrase phrase = clause.Next;
				bool isFirst = true;

				writer.Write("(");
				while (phrase != null)
				{
					if (!isFirst)
						writer.Write(',');
					else
						isFirst = false;

					writer.Write(phrase.AssignmentExpr.SelectExpr);

					phrase = phrase.Next as APSqlSetPhrase;
				}
				writer.Write(")");
			}
		}


		private void WriteInsertWithSubQuery(ParserWriter writer, APSqlInsertCommand command)
		{
			writer.WriteDirect("INSERT INTO");
			writer.Write(command.TableDef.TableName);

			if (command.SelectClause != null)
			{
				writer.Write("(");

				APSqlSelectPhrase phrase = command.SelectClause.Next as APSqlSelectPhrase;
				bool isFirst = true;
				while (phrase != null)
				{
					if (!isFirst)
						writer.Write(',');
					else
						isFirst = false;

					WriteSelectExpression(writer, phrase.Expr);

					phrase = phrase.Next as APSqlSelectPhrase;
				}

				writer.Write(")");
			}
		}


		private void WriteFrom(ParserWriter writer, APSqlFromClause clause, OracleCommand dbCmd)
		{
			if (clause != null && clause.Next != null)
			{
				writer.WriteLine();
				writer.WriteDirect("FROM");
				APSqlFromPhrase phrase = clause.Next;
				bool isFirst = true;

				while (phrase != null)
				{
					if (phrase.JoinType == APSqlJoinType.None)
					{
						if (!isFirst)
							writer.Write(',');
						else
							isFirst = false;

						writer.Write(phrase.TableName);
						if (phrase.AliasName != null)
							writer.Write(phrase.AliasName);
					}
					else
					{
						writer.Write(GetJoinTypeString(phrase.JoinType));
						writer.Write(phrase.TableName);
						if (phrase.AliasName != null)
							writer.Write(phrase.AliasName);
						if (phrase.JoinType != APSqlJoinType.Cross && phrase.JoinOnPhrase != null)
						{
							WriteJoinOn(writer, phrase.JoinOnPhrase, dbCmd);
						}
					}

					phrase = phrase.Next as APSqlFromPhrase;
				}
			}
		}


		private void WriteWhere(ParserWriter writer, APSqlWhereClause clause, OracleCommand dbCmd, int? maxReturnCount = null)
		{
			bool standardWhere = clause != null && clause.Next != null;
			if (standardWhere || maxReturnCount != null)
			{
				writer.WriteLine();
				writer.WriteDirect("WHERE");

				if (standardWhere)
				{
					APSqlWherePhrase phrase = clause.Next as APSqlWherePhrase;

					if (phrase.Next == null)
					{
						if (phrase is APSqlConditionAndPhrase)
						{
							WriteConditionPhrase(writer, (phrase as APSqlConditionAndPhrase).Child, APSqlConditionJoinType.AND, dbCmd);
							return;
						}
						else if (phrase is APSqlConditionOrPhrase)
						{
							WriteConditionPhrase(writer, (phrase as APSqlConditionOrPhrase).Child, APSqlConditionJoinType.OR, dbCmd);
							return;
						}
					}
					WriteConditionPhrase(writer, phrase, APSqlConditionJoinType.AND, dbCmd);
				}
				if (maxReturnCount != null)
				{
					if (standardWhere)
						writer.Write("AND");
					writer.Write("ROWNUM <= " + maxReturnCount.Value);
				}
			}
		}


		private void WriteConditionPhrase(ParserWriter writer, APSqlWherePhrase phrase, APSqlConditionJoinType join, OracleCommand dbCmd)
		{
			bool isFirst = true;
			writer.Write("(");

			while (phrase != null)
			{
				if (!isFirst)
					writer.Write(join.ToString());

				if (phrase.IsNot)
					writer.Write("NOT (");

				if (phrase is APSqlConditionAndPhrase)
				{
					WriteConditionPhrase(writer, (phrase as APSqlConditionAndPhrase).Child, APSqlConditionJoinType.AND, dbCmd);
				}
				else if (phrase is APSqlConditionOrPhrase)
				{
					WriteConditionPhrase(writer, (phrase as APSqlConditionOrPhrase).Child, APSqlConditionJoinType.OR, dbCmd);
				}
				else
				{
					APSqlConditionPhrase cond = phrase as APSqlConditionPhrase;

					// for this, if columnDef is null, mean this is a 'EXISTS ( subquery )' phrase
					// do not 
					// change to check ConditionOperator.
					if (cond.ConditionOperator != APSqlConditionOperator.Exists && cond.ConditionOperator != APSqlConditionOperator.NotExists)
						WriteSelectExpression(writer, cond.Expr);
					

					if (cond.ConditionOperator == APSqlConditionOperator.Equals /*&& !cond.IsRelationDef*/ && (cond.Value == null || cond.Value == DBNull.Value))
					{
						writer.Write("IS NULL");
					}
					else if (cond.ConditionOperator == APSqlConditionOperator.NotEqual /*&& !cond.IsRelationDef*/ && (cond.Value == null || cond.Value == DBNull.Value))
					{
						writer.Write("IS NOT NULL");
					}
					else
					{
						switch (cond.ConditionOperator)
						{
							case APSqlConditionOperator.Equals: writer.Write("="); break;
							case APSqlConditionOperator.NotEqual: writer.Write("<>"); break;
							case APSqlConditionOperator.GreaterThan: writer.Write(">"); break;
							case APSqlConditionOperator.GreaterThanOrEqual: writer.Write(">="); break;
							case APSqlConditionOperator.LessThan: writer.Write("<"); break;
							case APSqlConditionOperator.LessThanOrEqual: writer.Write("<="); break;
							case APSqlConditionOperator.Between: writer.Write("BETWEEN"); break;
							case APSqlConditionOperator.NotBetween: writer.Write("NOT BETWEEN"); break;
							case APSqlConditionOperator.Like: writer.Write("LIKE"); break;
							case APSqlConditionOperator.NotLike: writer.Write("NOT LIKE"); break;
							case APSqlConditionOperator.In: writer.Write("IN"); break;
							case APSqlConditionOperator.NotIn: writer.Write("NOT IN"); break;
							case APSqlConditionOperator.Exists: writer.Write("EXISTS"); break;
							case APSqlConditionOperator.NotExists: writer.Write("NOT EXISTS"); break;
						}

						object value = cond.Value;

						if (value == null)
						{
							writer.Write("NULL");
						}
						else if (value is APColumnDef)
						{
							writer.Write(new APSqlColumnExpr(value as APColumnDef).SelectExpr);
						}
						else if (value is APSqlExpr)
						{
							writer.Write((value as APSqlExpr).SelectExpr);
						}
						else if (value is APQuery)
						{
							switch (cond.SubQueryScalarRestrict)
							{
								case APSqlSubQueryScalarRestrict.All: writer.Write("ALL"); break;
								case APSqlSubQueryScalarRestrict.Some: writer.Write("SOME"); break;
								case APSqlSubQueryScalarRestrict.Any: writer.Write("ANY"); break;
							}

							writer.Write("(");
							int idented = writer.Idented++;
							writer.WriteLine();
							ParseSelectInternal(value as APSqlSelectCommand, dbCmd, writer);
							writer.Idented = idented;
							writer.Write(")");
						}
						else
						{
							if (cond.ConditionOperator == APSqlConditionOperator.In || cond.ConditionOperator == APSqlConditionOperator.NotIn)
							{
								writer.Write("(");
								int i = 0;
								foreach (object val in value as Array)
								{
									if (i != 0)
										writer.Write(',');
									string paramName = writer.GetSuitableParameterName(cond.ParamName);
									paramName = String.Format("v_{0}${1}", paramName, i);
									writer.Write(":" + paramName);
									AddParameter(dbCmd, paramName, val, ParameterDirection.Input);
									i++;
								}
								writer.Write(")");
							}
							else if (cond.ConditionOperator == APSqlConditionOperator.Between || cond.ConditionOperator == APSqlConditionOperator.NotBetween)
							{
								object begin = (value as Array).GetValue(0);
								object end = (value as Array).GetValue(1);

								if (begin == null)
								{
									writer.Write("NULL");
								}
								else if (begin is APColumnDef)
								{
									writer.Write(new APSqlColumnExpr(begin as APColumnDef).SelectExpr);
								}
								else if (begin is APSqlExpr)
								{
									writer.Write((begin as APSqlExpr).SelectExpr);
								}
								else
								{
									string paramName = writer.GetSuitableParameterName(cond.ParamName);
									writer.Write(":" + paramName);
									AddParameter(dbCmd, paramName, begin, ParameterDirection.Input);
								}

								writer.Write("AND");

								if (end == null)
								{
									writer.Write("NULL");
								}
								else if (end is APColumnDef)
								{
									writer.Write(new APSqlColumnExpr(end as APColumnDef).SelectExpr);
								}
								else if (end is APSqlExpr)
								{
									writer.Write((end as APSqlExpr).SelectExpr);
								}
								else
								{
									string paramName = writer.GetSuitableParameterName(cond.ParamName);
									writer.Write(":" + paramName);
									AddParameter(dbCmd, paramName, end, ParameterDirection.Input);
								}
							}
							else
							{
								string paramName = writer.GetSuitableParameterName(cond.ParamName);
								writer.Write(":" + paramName);
								AddParameter(dbCmd, paramName, value, ParameterDirection.Input);
							}
						}
					}
				}

				if (phrase.IsNot)
					writer.Write(")");

				isFirst = false;
				phrase = phrase.Next as APSqlWherePhrase;
			}

			writer.Write(")");
		}


		private void WriteOrderBy(ParserWriter writer, APSqlOrderByClause clause)
		{
			if (clause != null && clause.Next != null)
			{
				writer.WriteLine();
				writer.WriteDirect("ORDER BY");
				APSqlOrderPhrase phrase = clause.Next;
				bool isFirst = true;

				while (phrase != null)
				{
					if (!isFirst)
						writer.Write(',');
					else
						isFirst = false;

					WriteSelectExpression(writer, phrase.Expr);
					if (phrase.OrderAccording == APSqlOrderAccording.Desc)
						writer.Write("DESC");

					phrase = phrase.Next as APSqlOrderPhrase;
				}
			}
		}


		private void WriteSet(ParserWriter writer, APSqlValuesClause clause, OracleCommand dbCmd)
		{
			if (clause != null && clause.Next != null)
			{
				writer.WriteLine();
				writer.WriteDirect("SET");
				APSqlSetPhrase phrase = clause.Next;
				bool isFirst = true;

				while (phrase != null)
				{
					if (!isFirst)
						writer.Write(',');
					else
						isFirst = false;

					writer.Write(phrase.AssignmentExpr.SelectExpr);
					writer.Write("=");

					if (phrase.IsRelationValueExpr)
					{
						writer.Write(phrase.RelationValueExpr.ValueExpr);
					}
					else
					{
						string paramName = "v_" + writer.GetSuitableParameterName(phrase.ParamName);
						writer.Write(":" + paramName);
						AddParameter(dbCmd, paramName, phrase.Value, ParameterDirection.Input);
					}

					phrase = phrase.Next as APSqlSetPhrase;
				}
			}
		}


		private void WriteValues(ParserWriter writer, APSqlValuesClause clause, OracleCommand dbCmd)
		{
			if (clause != null && clause.Next != null)
			{
				writer.WriteLine();
				writer.WriteDirect("VALUES");
				APSqlSetPhrase phrase = clause.Next;
				bool isFirst = true;

				writer.Write('(');
				while (phrase != null)
				{
					if (!isFirst)
						writer.Write(',');
					else
						isFirst = false;

					if (phrase.IsRelationValueExpr)
					{
						writer.Write(phrase.RelationValueExpr.ValueExpr);
					}
					else
					{
						string paramName = "v_" + writer.GetSuitableParameterName(phrase.ParamName);
						writer.Write(":" + paramName);
						AddParameter(dbCmd, paramName, phrase.Value, ParameterDirection.Input);
					}

					phrase = phrase.Next as APSqlSetPhrase;
				}
				writer.Write(')');
			}
		}


		private void WriteJoinOn(ParserWriter writer, APSqlWherePhrase phrase, OracleCommand dbCmd)
		{
			if (phrase != null)
			{
				writer.Write("ON");


				if (phrase.Next == null)
				{
					if (phrase is APSqlConditionAndPhrase)
					{
						WriteConditionPhrase(writer, (phrase as APSqlConditionAndPhrase).Child, APSqlConditionJoinType.AND, dbCmd);
						return;
					}
					else if (phrase is APSqlConditionOrPhrase)
					{
						WriteConditionPhrase(writer, (phrase as APSqlConditionOrPhrase).Child, APSqlConditionJoinType.OR, dbCmd);
						return;
					}
				}
				WriteConditionPhrase(writer, phrase, APSqlConditionJoinType.AND, dbCmd);
			}
		}


		private void WriteGroupBy(ParserWriter writer, APSqlGroupByClause clause, OracleCommand dbCmd)
		{
			if (clause != null && clause.Next != null)
			{
				writer.WriteLine();
				writer.WriteDirect("GROUP BY");

				APSqlExprPhrase phrase = clause.Next as APSqlExprPhrase;
				bool isFirst = true;
				while (phrase != null)
				{
					if (!isFirst)
						writer.Write(',');
					else
						isFirst = false;

					WriteSelectExpression(writer, phrase.Expr);
					//writer.Write(phrase.Expr.SelectExpr);

					phrase = phrase.Next as APSqlExprPhrase;
				}
			}
		}


		private void WriteHaving(ParserWriter writer, APSqlWhereClause clause, OracleCommand dbCmd)
		{
			if (clause != null && clause.Next != null)
			{
				writer.WriteLine();
				writer.WriteDirect("HAVING");

				APSqlWherePhrase phrase = clause.Next as APSqlWherePhrase;

				if (phrase.Next == null)
				{
					if (phrase is APSqlConditionAndPhrase)
					{
						WriteConditionPhrase(writer, (phrase as APSqlConditionAndPhrase).Child, APSqlConditionJoinType.AND, dbCmd);
						return;
					}
					else if (phrase is APSqlConditionOrPhrase)
					{
						WriteConditionPhrase(writer, (phrase as APSqlConditionOrPhrase).Child, APSqlConditionJoinType.OR, dbCmd);
						return;
					}
				}
				WriteConditionPhrase(writer, phrase, APSqlConditionJoinType.AND, dbCmd);
			}
		}


		private string GetJoinTypeString(APSqlJoinType joinType)
		{
			if (joinType == APSqlJoinType.Inner)
				return "INNER JOIN";
			else if (joinType == APSqlJoinType.Left)
				return "LEFT JOIN";
			else if (joinType == APSqlJoinType.Right)
				return "RIGHT JOIN";
			else if (joinType == APSqlJoinType.Full)
				return "FULL JOIN";
			else if (joinType == APSqlJoinType.Cross)
				return "CROSS JOIN";
			// default is inner join
			return "INNER JOIN";
		}


		#endregion


		#region [ Override Implementation of APQueryParser ]


		private void ParseSelectInternal(APSqlSelectCommand command, OracleCommand dbCmd, ParserWriter writer, int? maxReturnCount = null)
		{
			WriteSelect(writer, command);
			writer.Idented++;
			WriteFrom(writer, command.FromClause, dbCmd);
			WriteWhere(writer, command.WhereClause, dbCmd, maxReturnCount);
			WriteGroupBy(writer, command.GroupByClause, dbCmd);
			WriteHaving(writer, command.HavingClause, dbCmd);
			WriteOrderBy(writer, command.OrderByClause);
		}


		/// <summary>
		/// Parse 'SELECT' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		protected override DbCommand ParseSelect(APSqlSelectCommand command)
		{
			StringBuilder sb = new StringBuilder();
			OracleCommand dbCmd = new OracleCommand();
			using (ParserWriter writer = new ParserWriter(sb, command.CommandNameSuitable))
			{
				if ((command.Skip ?? 0) > 0)
				{
					writer.WriteDirect("SELECT * FROM ( SELECT query_alias.*, ROWNUM query_rownum FROM (");
					writer.WriteLine();
					ParseSelectInternal(command, dbCmd, writer);
					writer.Idented = 0;
					writer.WriteLine();
					writer.WriteDirect(String.Format(") query_alias {0} ) WHERE query_rownum > {1}",
						(command.Take ?? 0) > 0 ? "WHERE ROWNUM <= " + (command.Take.Value + command.Skip.Value) : "",
						command.Skip.Value));
				}
				else
				{
					ParseSelectInternal(command, dbCmd, writer, command.Take);
				}
			}

			dbCmd.CommandText = sb.ToString();
			return dbCmd;
		}


		/// <summary>
		/// Parse 'SELECT COUNT(*)' command.
		/// </summary>
		/// <param name="command">The 'SELECT' command.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		protected override DbCommand ParseSizeOfSelect(APSqlSelectCommand command)
		{
			StringBuilder sb = new StringBuilder();
			OracleCommand dbCmd = new OracleCommand();
			using (ParserWriter writer = new ParserWriter(sb, command.CommandNameSuitable))
			{
				writer.Write("SELECT COUNT(*)");
				writer.Idented++;
				WriteFrom(writer, command.FromClause, dbCmd);
				WriteWhere(writer, command.WhereClause, dbCmd);
				WriteGroupBy(writer, command.GroupByClause, dbCmd);
				WriteHaving(writer, command.HavingClause, dbCmd);
				//WriteOrderBy(writer, command.OrderByClause);

			}

			dbCmd.CommandText = sb.ToString();
			return dbCmd;
		}


		/// <summary>
		/// Parse 'DELETE' command.
		/// </summary>
		/// <param name="command">The 'DELETE' command.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		protected override DbCommand ParseDelete(APSqlDeleteCommand command)
		{
			StringBuilder sb = new StringBuilder();
			OracleCommand dbCmd = new OracleCommand();

			using (ParserWriter writer = new ParserWriter(sb, command.CommandNameSuitable))
			{
				WriteDelete(writer, command);
				writer.Idented++;
				WriteFrom(writer, command.FromClause, dbCmd);
				WriteWhere(writer, command.WhereClause, dbCmd);
			}

			dbCmd.CommandText = sb.ToString();
			return dbCmd;
		}


		/// <summary>
		/// Parse 'UPDATE' command.
		/// </summary>
		/// <param name="command">The 'UPDATE' command.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		protected override DbCommand ParseUpdate(APSqlUpdateCommand command)
		{
			StringBuilder sb = new StringBuilder();
			OracleCommand dbCmd = new OracleCommand();

			using (ParserWriter writer = new ParserWriter(sb, command.CommandNameSuitable))
			{
				WriteUpdate(writer, command);
				writer.Idented++;
				WriteSet(writer, command.ValuesClause, dbCmd);
				WriteFrom(writer, command.FromClause, dbCmd);
				WriteWhere(writer, command.WhereClause, dbCmd);
			}

			dbCmd.CommandText = sb.ToString();
			return dbCmd;
		}


		/// <summary>
		/// Parse 'INSERT' command.
		/// </summary>
		/// <param name="command">The 'INSERT' command.</param>
		/// <returns>The DbCommand.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		protected override DbCommand ParseInsert(APSqlInsertCommand command)
		{
			StringBuilder sb = new StringBuilder();
			OracleCommand dbCmd = new OracleCommand();

			using (ParserWriter writer = new ParserWriter(sb, command.CommandNameSuitable))
			{
				APSqlInsertCommand insert = command as APSqlInsertCommand;

				if (insert.SubQuery != null)
				{
					WriteInsertWithSubQuery(writer, insert);
					writer.WriteLine();
					ParseSelectInternal(insert.SubQuery, dbCmd, writer);
				}
				else
				{
					WriteInsert(writer, insert, command.ValuesClause);
					writer.Idented++;
					WriteValues(writer, command.ValuesClause, dbCmd);

					// oracle unsupport provider="Database", so
					// do not return value.
				}
			}

			dbCmd.CommandText = sb.ToString();
			return dbCmd;
		}


		#endregion

	}

}
