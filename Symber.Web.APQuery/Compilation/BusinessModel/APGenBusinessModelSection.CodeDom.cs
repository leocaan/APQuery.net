using Symber.Web.Data;
using System;
using System.CodeDom;
using System.Reflection;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configures business mode for APGen.
	/// </summary>
	public sealed partial class APGenBusinessModelSection : APGenSection
	{

		#region [ Fields ]


		private CodeTypeReference TableDefType = TypeRef("APTableDef");
		private CodeTypeReference ColumnDefType = TypeRef("APColumnDef");
		private CodeTypeReference AsteriskColumnDefType = TypeRef("AsteriskAPColumnDef");
		private CodeTypeReference RelationDefType = TypeRef("APRelationDef");
		private CodeTypeReference DalType = TypeRef("APDal");
		private CodeTypeReference CacheType = TypeRef("APDataCache");
		private CodeTypeReference QueryType = TypeRef("APQuery");
		private CodeTypeReference WherePhraseType = TypeRef("APSqlWherePhrase");
		private CodeTypeReference OrderPhraseType = TypeRef("APSqlOrderPhrase");
		private CodeTypeReference OrderByClauseType = TypeRef("APSqlOrderByClause");
		private CodeTypeReference SelectPhraseType = TypeRef("APSqlSelectPhrase");
		private CodeTypeReference SetPhraseType = TypeRef("APSqlSetPhrase");
		private CodeTypeReference CondPhraseType = TypeRef("APSqlConditionPhrase");
		private CodeTypeReference AggregationPhraseType = TypeRef("APSqlAggregationPhrase");
		private CodeTypeReference SetPhraseSelectorType = TypeRef("APSqlSetPhraseSelector");


		private CodeTypeReference OrderPhraseDictionaryTType = TypeTRef("Dictionary", TypeRef(typeof(string)), TypeRef("APSqlOrderPhrase"));

		private CodeTypeReference DataReaderType = TypeRef("IDataReader");
		private CodeTypeReference VarType = TypeRef("var");
		private CodeTypeReference ObjectType = TypeRef("Object");
		private CodeTypeReference IntType = TypeRef(typeof(int));
		private CodeTypeReference ProviderType = TypeRef("APDalProvider");
		private CodeTypeReference DatabaseType = TypeRef("APDatabase");
		private CodeTypeReference ExceptionType = TypeRef("APDataException");
		private CodeTypeReference MapHandlerType = TypeRef("MapHandler");

		private CodeTypeReferenceExpression QueryTypeExp = TypeRefExp("APQuery");


		private string assignmentMethodName = "Assignment";
		private string compareEqualsMethodName = "CompareEquals";
		private string insertMethodName = "Insert";
		private string updateMethodName = "Update";
		private string primaryDeleteMethodName = "PrimaryDelete";
		private string conditionDeleteMethodName = "ConditionDelete";
		private string conditionQueryCountMethodName = "ConditionQueryCount";
		private string primaryGetMethodName = "PrimaryGet";
		private string mapMethodName = "Map";
		private string tolerantMapMethodName = "TolerantMap";
		private string fullupMethodName = "Fullup";
		private string mapListMethodName = "MapList";
		private string tolerantMapListMethodName = "TolerantMapList";
		private string conditionQueryMethodName = "ConditionQuery";
		private string getAllMethodName = "GetAll";
		private string getInitDataMethodName = "GetInitData";
		private string initDataMethodName = "InitData";



		private string DBDefName = "APDBDef";
		private string BplDefName = "APBplDef";
		private string DalDefName = "APDalDef";
		private CodeTypeReference DBType;
		private CodeSnippetExpression DBDef;


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Dal provider object.
		/// </summary>
		public APDalProvider ProviderInstance
		{
			get
			{
				if (Provider != null && !String.IsNullOrEmpty(Provider.Name))
					return APDalProviderHelper.InstantiateProvider(Provider);

				if (string.IsNullOrEmpty(ProviderName))
					return APDatabase.Provider;
				else
					return APDatabase.Providers[ProviderName];
			}
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Check whether the column can be a identity column rely on Provider.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <returns>true means the column can be a identity column, otherwise false.</returns>
		public bool CanIdentityRelyOnProvider(APGenColumn column)
		{
			return
				column.IsEnum
				|| column.ParsedType == typeof(sbyte)
				|| column.ParsedType == typeof(short)
				|| column.ParsedType == typeof(int)
				|| column.ParsedType == typeof(long)
				|| column.ParsedType == typeof(byte)
				|| column.ParsedType == typeof(ushort)
				|| column.ParsedType == typeof(uint)
				|| column.ParsedType == typeof(ulong)
				|| column.ParsedType == typeof(decimal)
				|| column.ParsedType == typeof(float)
				|| column.ParsedType == typeof(double)
				|| column.ParsedType == typeof(Guid);
		}


		/// <summary>
		/// Check whether the column can be a identity column rely on Database.
		/// </summary>
		/// <param name="column">Column definition.</param>
		/// <returns>true means the column can be a identity column, otherwise false.</returns>
		public bool CanIdentityRelyOnDatabase(APGenColumn column)
		{
			return
				column.IsEnum
				|| column.ParsedType == typeof(sbyte)
				|| column.ParsedType == typeof(short)
				|| column.ParsedType == typeof(int)
				|| column.ParsedType == typeof(long)
				|| column.ParsedType == typeof(byte)
				|| column.ParsedType == typeof(ushort)
				|| column.ParsedType == typeof(uint)
				|| column.ParsedType == typeof(ulong)
				|| column.ParsedType == typeof(decimal);
		}


		#endregion


		#region [ Private Methods ]


		private void InitPrefix()
		{
			string prefix = String.IsNullOrEmpty(DBPrefix) ? null : DBPrefix;

			foreach (APGenRelation relation in Relations)
			{
				if (MainPart)
				{
					relation.MasterTableRef = Tables[relation.MasterTable];
					if (relation.MasterTableRef == null)
						throw new APGenException(APResource.GetString(APResource.APBusiness_RelationTargetMissing, relation.Name));
					relation.SlaveTableRef = Tables[relation.SlaveTable];
					if (relation.SlaveTableRef == null)
						throw new APGenException(APResource.GetString(APResource.APBusiness_RelationTargetMissing, relation.Name));
					relation.MasterColumnRef = relation.MasterTableRef.Columns[relation.MasterColumn];
					if (relation.MasterColumnRef == null)
						throw new APGenException(APResource.GetString(APResource.APBusiness_RelationTargetMissing, relation.Name));
					relation.SlaveColumnRef = relation.SlaveTableRef.Columns[relation.SlaveColumn];
					if (relation.SlaveColumnRef == null)
						throw new APGenException(APResource.GetString(APResource.APBusiness_RelationTargetMissing, relation.Name));
				}
				else
				{
					relation.MasterTableRef = Tables[relation.MasterTable];
					if (relation.MasterTableRef == null)
					{
						relation.MasterTableRef = new APGenTable() { TableName = relation.MasterTable };
						relation.MasterColumnRef = new APGenColumn() { ColumnName = relation.MasterColumn };
					}
					else
					{
						relation.MasterColumnRef = relation.MasterTableRef.Columns[relation.MasterColumn];
						if (relation.MasterColumnRef == null)
							throw new APGenException(APResource.GetString(APResource.APBusiness_RelationTargetMissing, relation.Name));
					}

					relation.SlaveTableRef = Tables[relation.SlaveTable];
					if (relation.SlaveTableRef == null)
					{
						relation.SlaveTableRef = new APGenTable() { TableName = relation.SlaveTable };
						relation.SlaveColumnRef = new APGenColumn() { ColumnName = relation.SlaveColumn };
					}
					else
					{
						relation.SlaveColumnRef = relation.SlaveTableRef.Columns[relation.SlaveColumn];
						if (relation.SlaveColumnRef == null)
							throw new APGenException(APResource.GetString(APResource.APBusiness_RelationTargetMissing, relation.Name));
					}
				}
			}

			foreach (APGenTable table in Tables)
			{
				if (String.IsNullOrEmpty(table.ClassName))
					table.ClassName = table.Name;
				if (String.IsNullOrEmpty(table.TableName))
					table.TableName = table.Name;
				if (prefix != null)
					table.TableName = prefix + table.TableName;

				foreach (APGenColumn column in table.Columns)
				{
					if (String.IsNullOrEmpty(column.PropertyName))
						column.PropertyName = column.Name;
					if (String.IsNullOrEmpty(column.ColumnName))
						column.ColumnName = column.Name;
					if (column.IdentityType == APColumnIdentityType.Database && !CanIdentityRelyOnDatabase(column))
						column.IdentityType = APColumnIdentityType.None;
					else if (column.IdentityType == APColumnIdentityType.Provider && !CanIdentityRelyOnProvider(column))
						column.IdentityType = APColumnIdentityType.None;
				}
			}

			foreach (APGenTable view in Views)
			{
				if (String.IsNullOrEmpty(view.ClassName))
					view.ClassName = view.Name;
				if (String.IsNullOrEmpty(view.TableName))
					view.TableName = view.Name;
				if (prefix != null)
					view.TableName = prefix + view.TableName;

				foreach (APGenColumn column in view.Columns)
				{
					if (String.IsNullOrEmpty(column.PropertyName))
						column.PropertyName = column.Name;
					if (String.IsNullOrEmpty(column.ColumnName))
						column.ColumnName = column.Name;
					if (column.IdentityType == APColumnIdentityType.Database && !CanIdentityRelyOnDatabase(column))
						column.IdentityType = APColumnIdentityType.None;
					else if (column.IdentityType == APColumnIdentityType.Provider && !CanIdentityRelyOnProvider(column))
						column.IdentityType = APColumnIdentityType.None;
				}
			}

			// If defPrefix is not empty, rename defs
			if (DefPrefix != null)
			{
				DBDefName = DefPrefix + "DBDef";
				BplDefName = DefPrefix + "BplDef";
				DalDefName = DefPrefix + "DalDef";
			}
			DBType = TypeRef(DBDefName);
			DBDef = Special(DBDefName);
		}


		private CodeTypeDeclaration CreateDatabaseDef(CodeNamespace cns)
		{
			CodeTypeDeclaration ctd;
			ctd = NewClass(DBDefName, TypeAttributes.Public, APResource.APBusiness_DBDefComment);
			ctd.IsPartial = true;
			ctd.BaseTypes.Add(TypeRef("APDatabase"));
			cns.Types.Add(ctd);

			foreach (APGenTable table in Tables)
			{
				CreateTableDef(ctd, table, false);
			}

			foreach (APGenTable view in Views)
			{
				CreateTableDef(ctd, view, true);
			}

			foreach (APGenRelation relation in Relations)
			{
				CodeMemberField field = NewMemberField(relation.FieldName, RelationDefType, MemberAttributes.Private | MemberAttributes.Static, null);
				ctd.Members.Add(field);

				CodeMemberProperty prop = NewMemberProperty(relation.Name, RelationDefType, MemberAttributes.Public | MemberAttributes.Static, relation.Comment + " RelationDef");
				prop.GetStatements.Add(If(Equals(FieldRef(relation.FieldName), Null()), Let(FieldRef(relation.FieldName), New(RelationDefType, relation.MasterTableRef.TableExp, relation.MasterColumnRef.ColumnExp, relation.SlaveTableRef.TableExp, relation.SlaveColumnRef.ColumnExp))));
				prop.GetStatements.Add(Return(FieldRef(relation.FieldName)));
				ctd.Members.Add(prop);
			}


			// static provider
			//
			if (MainPart)
			{
				CodeMemberField staticProvider = NewMemberField("staticProvider", ProviderType, MemberAttributes.Private | MemberAttributes.Static, null);

				if (Provider != null && !String.IsNullOrEmpty(Provider.Name))
				{
					staticProvider.InitExpression = MethodInvoke(TypeRefExp("APDalProviderHelper"), "InstantiateProvider",
						Const(Provider.Name),
						Const(Provider.Type),
						Const(Provider.ConnectionString),
						Const(Provider.ProviderName));
				}
				else
				{
					if (String.IsNullOrEmpty(ProviderName))
						staticProvider.InitExpression = PropRef("Provider");
					else
						staticProvider.InitExpression = IndexerRef(PropRef("Providers"), Const(ProviderName));
				}
				ctd.Members.Add(staticProvider);
			}


			// constructors
			//
			if (MainPart)
			{
				CodeConstructor ctor1 = NewCtor(MemberAttributes.Public, null);
				ctor1.BaseConstructorArgs.Add(FieldRef("staticProvider"));
				ctd.Members.Add(ctor1);

				CodeConstructor ctor2 = NewCtor(MemberAttributes.Public, null, Param(TypeRef(typeof(string)), "providerName"));
				ctor2.BaseConstructorArgs.Add(ParamRef("providerName"));
				ctd.Members.Add(ctor2);
			}


			foreach (APGenTable table in Tables)
			{
				CreateDalProperty(ctd, table, false);
			}

			foreach (APGenTable view in Views)
			{
				CreateDalProperty(ctd, view, true);
			}


			// override Rollback and Close
			//
			if (MainPart)
			{
				CodeMemberMethod rollback = NewMemberMethod("Rollback", MemberAttributes.Public | MemberAttributes.Override, null);
				CodeMemberMethod close = NewMemberMethod("Close", MemberAttributes.Public | MemberAttributes.Override, null);
				ctd.Members.Add(rollback);
				ctd.Members.Add(close);
				rollback.Statements.Add(MethodInvoke(Base(), "Rollback"));
				close.Statements.Add(MethodInvoke(Base(), "Close"));
			}


			// init data
			if (MainPart)
			{
				CodeMemberMethod initData = NewMemberMethod("InitData", MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_InitDataComment);
				ctd.Members.Add(initData);
				initData.Statements.Add(VarDecl(DBType, "db", New(DBType)));
				initData.Statements.Add(MethodInvoke("db", "BeginTrans"));
				CodeTryCatchFinallyStatement _try;
				initData.Statements.Add(_try = Try());
				foreach (APGenTable table in Tables)
				{
					_try.TryStatements.Add(MethodInvoke(PropRef(Local("db"), table.Name + "Dal"), initDataMethodName, ParamRef("db")));
				}
				foreach (APGenTable view in Views)
				{
					_try.TryStatements.Add(MethodInvoke(PropRef(Local("db"), view.Name + "Dal"), initDataMethodName, ParamRef("db")));
				}
				_try.TryStatements.Add(MethodInvoke("db", "Commit"));
				CodeCatchClause catchClause = new CodeCatchClause();
				catchClause.Statements.Add(MethodInvoke("db", "Rollback"));
				catchClause.Statements.Add(Throw());
				_try.CatchClauses.Add(catchClause);
				_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
			}

			return ctd;
		}


		private void CreateTableDef(CodeTypeDeclaration ctd, APGenTable table, bool isView)
		{
			// add tableDef
			CodeTypeDeclaration tableDel = NewClass(table.TableDefName, TypeAttributes.Public, null);
			tableDel.IsPartial = true;
			tableDel.BaseTypes.Add(TypeRef("APTableDef"));
			tableDel.CustomAttributes.Add(AttrDecl(TypeRef("Serializable")));
			ctd.Members.Add(tableDel);

			CodeConstructor ctor = NewCtor(MemberAttributes.Public, null, Param(TypeRef(typeof(string)), "tableName"));
			ctor.BaseConstructorArgs.Add(ParamRef("tableName"));
			tableDel.Members.Add(ctor);

			ctor = NewCtor(MemberAttributes.Family, null, Param(TypeRef(typeof(string)), "tableName"), Param(TypeRef(typeof(string)), "alias"));
			ctor.BaseConstructorArgs.Add(ParamRef("tableName"));
			ctor.BaseConstructorArgs.Add(ParamRef("alias"));
			tableDel.Members.Add(ctor);



			// all APColumnDef
			//
			foreach (APGenColumn column in table.Columns)
			{
				CodeTypeReference tr = ColumnDefType;
				Type type = column.ParsedType;
				bool needLength = false;
				bool needPrecision = false;

				if (type != null)
				{
					if (type.IsGenericType && type.IsValueType)
						type = Nullable.GetUnderlyingType(type);

					if (type.IsPrimitive)
					{
						tr = TypeRef(type.Name + "APColumnDef");
						if (type == typeof(string) || type == typeof(byte[]))
							needLength = true;
						else if (type == typeof(decimal))
							needPrecision = true;
					}
					else if (type.IsEnum)
					{
						tr = TypeTRef("EnumAPColumnDef", column.TypeRef);
					}
					else if (type == typeof(Guid))
					{
						tr = TypeRef("GuidAPColumnDef");
					}
					else if (type == typeof(String))
					{
						tr = TypeRef("StringAPColumnDef");
						needLength = true;
					}
					else if (type == typeof(DateTime))
					{
						tr = TypeRef("DateTimeAPColumnDef");
					}
					else if (type == typeof(Decimal))
					{
						tr = TypeRef("DecimalAPColumnDef");
						needPrecision = true;
					}
					else if (type == typeof(byte[]))
					{
						tr = TypeRef("BytesAPColumnDef");
						needLength = true;
					}
				}
				else if (column.IsEnum)
				{
					tr = TypeTRef("EnumAPColumnDef", column.TypeRef);
				}
				column.DefRef = tr;
				CodeMemberField fieldColumnDef = NewMemberField(column.FieldName, tr, MemberAttributes.Private, null);
				tableDel.Members.Add(fieldColumnDef);

				CodeMemberProperty columnProp = NewMemberProperty(column.PropertyName, tr, MemberAttributes.Public, column.Name + " ColumnDef");
				CodeConditionStatement condColumn = If(MethodInvoke("Object", "ReferenceEquals", FieldRef(column.FieldName), Null()));
				CodeExpression initColumn = New(tr, This(), column.ColumnExp, Const(column.IsNullable));
				if (needLength)
				{
					(initColumn as CodeObjectCreateExpression).Parameters.Add(Const(column.DataLength));
				}
				else if (needPrecision)
				{
					(initColumn as CodeObjectCreateExpression).Parameters.Add(Const(column.Precision));
					(initColumn as CodeObjectCreateExpression).Parameters.Add(Const(column.Scale));
				}
				condColumn.TrueStatements.Add(Let(FieldRef(column.FieldName), initColumn));
				condColumn.TrueStatements.Add(Let(PropRef(FieldRef(column.FieldName), "Display"),
					Const(column.Display == String.Empty ? column.ColumnName : column.Display)));
				if (column.Required)
					condColumn.TrueStatements.Add(Let(PropRef(FieldRef(column.FieldName), "Required"), Const(true)));
				columnProp.GetStatements.Add(condColumn);
				columnProp.GetStatements.Add(Return(FieldRef(column.FieldName)));
				tableDel.Members.Add(columnProp);
			}

	

			CodeExpression init = null;
			foreach (APGenIndex index in table.Indexes)
			{
				CodeExpression[] array = new CodeExpression[index.Orders.Count];

				for (int i = 0; i < index.Orders.Count; i++)
				{
					APGenOrder order = index.Orders[i];
					array[i] = PropRef(PropRef(order.Name), order.According == APSqlOrderAccording.Asc ? "Asc" : "Desc");
				 }


				CodeMemberProperty propOrder = NewMemberProperty(index.Name, OrderPhraseType, MemberAttributes.Public, index.Name + " OrderByDef");
				propOrder.GetStatements.Add(Return(
					PropRef(New(OrderByClauseType, array), "Next")
					));
				tableDel.Members.Add(propOrder);


				if (index.IsDefault && init == null)
				{
					init = PropRef(index.Name);
				}
			}

			CodeMemberProperty defaultOrder = NewMemberProperty("DefaultOrder", OrderPhraseType, MemberAttributes.Public, "Default Index");
			if (init == null)
				defaultOrder.GetStatements.Add(Return(Null()));
			else
				defaultOrder.GetStatements.Add(Return(init));
			tableDel.Members.Add(defaultOrder);



			CodeMemberMethod as_ = NewMemberMethod("As", MemberAttributes.Public, "Create a alias table", Param(typeof(string), "name"));
			tableDel.Members.Add(as_);
			as_.ReturnType = TypeRef(table.TableDefName);
			as_.Statements.Add(Return(New(TypeRef(table.TableDefName), Const(table.TableName), ParamRef("name"))));

			foreach (APGenAlias alias in table.Aliases)
			{
				CodeMemberField aliasField = NewMemberField(alias.FieldName, TypeRef(table.TableDefName), MemberAttributes.Private | MemberAttributes.Static, null);
				tableDel.Members.Add(aliasField);

				CodeMemberProperty aliasProp = NewMemberProperty(alias.Name, TypeRef(table.TableDefName), MemberAttributes.Public, "Alias table " + alias.Name);
				aliasProp.GetStatements.Add(If(Equals(FieldRef(alias.FieldName), Null()), Let(FieldRef(alias.FieldName), MethodInvoke("As", Const(alias.Name)))));
				aliasProp.GetStatements.Add(Return(FieldRef(alias.FieldName)));
				tableDel.Members.Add(aliasProp);
			}

			CodeMemberField fieldTableDef = NewMemberField(table.FieldName, TypeRef(table.TableDefName), MemberAttributes.Private | MemberAttributes.Static, null);
			ctd.Members.Add(fieldTableDef);

			CodeMemberProperty propTableDef = NewMemberProperty(table.ClassName, TypeRef(table.TableDefName), MemberAttributes.Public | MemberAttributes.Static, table.Comment + " TableDef");
			propTableDef.GetStatements.Add(If(Equals(FieldRef(table.FieldName), Null()), Let(FieldRef(table.FieldName), New(TypeRef(table.TableDefName), table.TableExp))));
			propTableDef.GetStatements.Add(Return(FieldRef(table.FieldName)));
			ctd.Members.Add(propTableDef);



			#region [ Map ]


			CodeParameterDeclarationExpression dataDef = Param(table.DataType, "data");
			CodeArgumentReferenceExpression dataRef = ParamRef("data");
			CodeTypeReference boolDef = TypeRef(typeof(bool));
			CodeParameterDeclarationExpression throwDef = Param(boolDef, "throwIfValidColumnName");
			CodeArgumentReferenceExpression throwRef = ParamRef("throwIfValidColumnName");
			CodeTypeReference listType = TypeTRef("List", table.DataType);


			// Fullup
			CodeMemberMethod mapFull = NewMemberMethod(fullupMethodName, MemberAttributes.Public, APResource.APBusiness_MapComment, Param(DataReaderType, "reader"), dataDef, throwDef);
			tableDel.Members.Add(mapFull);


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			foreach (APGenColumn column in table.Columns)
			{
				if (column.DefaultValue != "")
				{
					mapFull.Statements.Add(Let(FieldRef(dataRef, column.PropertyName),
						MethodInvoke(
						MethodRef(PropRef(column.PropertyName), "GetValue", column.TypeRef),
						ParamRef("reader"),
						throwRef,
						Special(column.DefaultValue))));
				}
				else
				{
					mapFull.Statements.Add(Let(FieldRef(dataRef, column.PropertyName),
						MethodInvoke(
						MethodRef(PropRef(column.PropertyName), "GetValue", column.TypeRef),
						ParamRef("reader"),
						throwRef)));
				}
			}

			// Map
			CodeMemberMethod map = NewMemberMethod(mapMethodName, MemberAttributes.Public, APResource.APBusiness_MapComment, Param(DataReaderType, "reader"));
			tableDel.Members.Add(map);
			map.ReturnType = table.DataType;
			map.Statements.Add(VarDecl(table.DataType, "data", New(table.DataType)));
			map.Statements.Add(MethodInvoke(fullupMethodName, ParamRef("reader"), dataRef, Const(true)));
			map.Statements.Add(Return(dataRef));

			// TolerantMap
			CodeMemberMethod tolerantMap = NewMemberMethod(tolerantMapMethodName, MemberAttributes.Public, APResource.APBusiness_MapComment, Param(DataReaderType, "reader"));
			tableDel.Members.Add(tolerantMap);
			tolerantMap.ReturnType = table.DataType;
			tolerantMap.Statements.Add(VarDecl(table.DataType, "data", New(table.DataType)));
			tolerantMap.Statements.Add(MethodInvoke(fullupMethodName, ParamRef("reader"), dataRef, Const(false)));
			tolerantMap.Statements.Add(Return(dataRef));

			// MapList
			{
				CodeMemberMethod mapList = NewMemberMethod(mapListMethodName, MemberAttributes.Public, APResource.APBusiness_MapComment, Param(DataReaderType, "reader"));
				tableDel.Members.Add(mapList);
				mapList.ReturnType = listType;

				mapList.Statements.Add(VarDecl(listType, "list", New(listType)));

				// try ... finally ...
				CodeVariableReferenceExpression reader = Local("reader");
				CodeTryCatchFinallyStatement _try;
				mapList.Statements.Add(_try = Try());
				CodeIterationStatement s = new CodeIterationStatement();
				_try.TryStatements.Add(
					For(Eval(Special("")), MethodInvoke(reader, "Read"), Eval(Special("")),
					Eval(MethodInvoke(Local("list"), "Add", MethodInvoke(mapMethodName, reader)))));
				_try.FinallyStatements.Add(MethodInvoke(reader, "Close"));

				mapList.Statements.Add(Return(Local("list")));
			}

			// TolerantMapList
			{
				CodeMemberMethod tolerantMapList = NewMemberMethod(tolerantMapListMethodName, MemberAttributes.Public, APResource.APBusiness_MapComment, Param(DataReaderType, "reader"));
				tableDel.Members.Add(tolerantMapList);
				tolerantMapList.ReturnType = listType;

				tolerantMapList.Statements.Add(VarDecl(listType, "list", New(listType)));

				// try ... finally ...
				CodeVariableReferenceExpression reader = Local("reader");
				CodeTryCatchFinallyStatement _try;
				tolerantMapList.Statements.Add(_try = Try());
				CodeIterationStatement s = new CodeIterationStatement();
				_try.TryStatements.Add(
					For(Eval(Special("")), MethodInvoke(reader, "Read"), Eval(Special("")),
					Eval(MethodInvoke(Local("list"), "Add", MethodInvoke(tolerantMapMethodName, reader)))));
				_try.FinallyStatements.Add(MethodInvoke(reader, "Close"));

				tolerantMapList.Statements.Add(Return(Local("list")));
			}

			#endregion

		}


		private void CreateDalProperty(CodeTypeDeclaration ctd, APGenTable table, bool isView)
		{
			CodeTypeReference dalType = TypeRef(DalDefName + "." + table.DalName);
			string fieldName = table.FieldName + "Dal";
			CodeMemberField field = NewMemberField(fieldName, dalType, MemberAttributes.Private, null);
			ctd.Members.Add(field);
			CodeMemberProperty prop = NewMemberProperty(table.ClassName + "Dal", dalType, MemberAttributes.Public, table.Comment + " Dal");
			prop.GetStatements.Add(If(Equals(FieldRef(fieldName), Null()), Let(FieldRef(fieldName), New(dalType, This()))));
			prop.GetStatements.Add(Return(FieldRef(fieldName)));
			ctd.Members.Add(prop);
		}


		private CodeTypeDeclaration CreateDalDef(CodeNamespace cns)
		{
			CodeTypeDeclaration ctd;
			ctd = NewClass(DalDefName, TypeAttributes.Public, APResource.APBusiness_DalDefComment);
			ctd.IsPartial = true;
			cns.Types.Add(ctd);

			return ctd;
		}


		private CodeTypeDeclaration CreateBplDef(CodeNamespace cns)
		{
			CodeTypeDeclaration ctd;
			ctd = NewClass(BplDefName, TypeAttributes.Public, APResource.APBusiness_BplDefComment);
			ctd.IsPartial = true;
			cns.Types.Add(ctd);

			return ctd;
		}


		private void CreateData(CodeNamespace cns, APGenTable table, bool isView)
		{
			CodeTypeDeclaration ctd;
			ctd = NewClass(table.DataInheritFromBase ? table.DataBaseName : table.DataName, TypeAttributes.Public, table.Comment + (table.DataInheritFromBase ? " Base" : ""));
			ctd.CustomAttributes.Add(AttrDecl(TypeRef("Serializable")));
			ctd.IsPartial = true;
			if (table.DataInheritFromBase)
			{
				ctd.TypeAttributes |= TypeAttributes.Abstract;
			}
			if (table.Inherits != "")
			{
				ctd.BaseTypes.Add(TypeRef(table.Inherits));
			}
			cns.Types.Add(ctd);

			// constructor
			//
			CodeConstructor constructor = NewCtor(MemberAttributes.Public, APResource.APBusiness_DefaultConstructorComment);
			ctd.Members.Add(constructor);

			constructor = NewCtor(MemberAttributes.Public, APResource.APBusiness_InitFieldsConstructorComment);
			ctd.Members.Add(constructor);

			// update self
			CodeMemberMethod assignment = NewMemberMethod(assignmentMethodName, MemberAttributes.Public, APResource.APBusiness_AssignmentComment, Param(table.DataType, "data"));
			ctd.Members.Add(assignment);

			// compare equals
			CodeMemberMethod compareEqulas = NewMemberMethod(compareEqualsMethodName, MemberAttributes.Public, TypeRef(typeof(bool)), APResource.APBusiness_CompareEqualsComment, Param(table.DataType, "data"));
			ctd.Members.Add(compareEqulas);

			foreach (APGenColumn column in table.Columns)
			{
				string comment = string.IsNullOrEmpty(column.Comment) ? column.Name : column.Comment;

				// field
				//
				CodeMemberField filed = NewMemberField(column.FieldName, column.TypeRef, MemberAttributes.Private, comment);
				if (!string.IsNullOrEmpty(column.DefaultValue))
				{
					filed.InitExpression = Special(column.DefaultValue);
				}
				else
				{
					if (column.ParsedType == typeof(string) && !column.IsNullable)
					{
						filed.InitExpression = PropRef(TypeRefExp(typeof(String)), "Empty");
					}
				}
				ctd.Members.Add(filed);


				// property
				//
				CodeMemberProperty prop = NewMemberProperty(column.PropertyName, column.TypeRef, MemberAttributes.Public, comment);
				if (column.Override)
				{
					prop.Attributes |= MemberAttributes.Override;
				}
				ctd.Members.Add(prop);
				if (!String.IsNullOrEmpty(column.Display))
				{
					prop.CustomAttributes.Add(AttrDecl(TypeRef("Display"), AttrArg("Name", Const(column.Display))));
				}
				if (column.Required)
				{
					prop.CustomAttributes.Add(AttrDecl(TypeRef("Required")));
				}
				if (column.ParsedType == typeof(String))
				{
					prop.CustomAttributes.Add(AttrDecl(TypeRef("StringLength"), AttrArg(Const(column.DataLength))));
				}


				prop.GetStatements.Add(Return(FieldRef(column.FieldName)));
				prop.SetStatements.Add(Let(FieldRef(column.FieldName), Value()));


				// APColumnDef property
				CodeMemberProperty defProp = NewMemberProperty(column.PropertyName + "Def", column.DefRef, MemberAttributes.Public | MemberAttributes.Static, comment + " APColumnDef");
				ctd.Members.Add(defProp);
				defProp.GetStatements.Add(Return(
					PropRef(PropRef(Special(DBDefName), table.DataName), column.PropertyName)
					));


				// constructor
				constructor.Parameters.Add(Param(column.TypeRef, column.ParamName));
				constructor.Statements.Add(Let(FieldRef(column.FieldName), ParamRef(column.ParamName)));

				// update self
				assignment.Statements.Add(Let(PropRef(column.PropertyName), PropRef(ParamRef("data"), column.PropertyName)));

				// compare equals
				compareEqulas.Statements.Add(If(NotEqualsValue(PropRef(column.PropertyName), PropRef(ParamRef("data"), column.PropertyName)), ReturnFalse()));
			}
			compareEqulas.Statements.Add(ReturnTrue());


			// APTableDef property
			CodeMemberProperty tablefProp = NewMemberProperty("TableDef", TypeRef(DBDefName + "." + table.TableDefName), MemberAttributes.Public | MemberAttributes.Static, table.TableDefName + " APTableDef");
			ctd.Members.Add(tablefProp);
			tablefProp.GetStatements.Add(Return(
				PropRef(Special(DBDefName), table.DataName)
				));

			// APSqlAsteriskExpr property
			CodeMemberProperty asteriskProp = NewMemberProperty("Asterisk", TypeRef("APSqlAsteriskExpr"), MemberAttributes.Public | MemberAttributes.Static, table.TableDefName + " APSqlAsteriskExpr");
			ctd.Members.Add(asteriskProp);
			asteriskProp.GetStatements.Add(Return(
				PropRef(PropRef(Special(DBDefName), table.DataName), "Asterisk")
				));



			#region [ associate with Bpl ]


			CodePropertyReferenceExpression bpl = PropRef(Special(BplDefName), table.BplName);
			CodeParameterDeclarationExpression whereDef = Param(WherePhraseType, "condition");
			CodeArgumentReferenceExpression whereRef = ParamRef("condition");

			// insert
			{
				CodeMemberMethod method = NewMemberMethod(insertMethodName, MemberAttributes.Public, APResource.APBusiness_InsertComment);
				method.Statements.Add(MethodInvoke(bpl, insertMethodName, Cast(table.DataType, This())));
				ctd.Members.Add(method);
			}

			// update
			{
				CodeMemberMethod method = NewMemberMethod(updateMethodName, MemberAttributes.Public, APResource.APBusiness_UpdateComment);
				method.Statements.Add(MethodInvoke(bpl, updateMethodName, Cast(table.DataType, This())));
				ctd.Members.Add(method);
			}

			// primary delete
			{
				CodeMemberMethod method = NewMemberMethod(primaryDeleteMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_PrimaryDeleteComment);
				CodeMethodInvokeExpression call = MethodInvoke(bpl, primaryDeleteMethodName);
				method.Statements.Add(call);
				ctd.Members.Add(method);
				foreach (APGenColumn pk in table.PrimaryKeyColumns)
				{
					method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
					call.Parameters.Add(ParamRef(pk.ParamName));
				}
			}

			// condition delete
			{
				CodeMemberMethod method = NewMemberMethod(conditionDeleteMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionDeleteComment, whereDef);
				method.Statements.Add(MethodInvoke(bpl, conditionDeleteMethodName, whereRef));
				ctd.Members.Add(method);
			}

			// condition query count
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryCountMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryCountComment, whereDef);
				method.ReturnType = IntType;
				method.Statements.Add(Return(MethodInvoke(bpl, conditionQueryCountMethodName, whereRef)));
				ctd.Members.Add(method);
			}

			// primary get
			{
				CodeMemberMethod method = NewMemberMethod(primaryGetMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_PrimaryGetComment);
				method.ReturnType = table.DataType;
				CodeMethodInvokeExpression call = MethodInvoke(bpl, primaryGetMethodName);
				method.Statements.Add(Return(call));
				ctd.Members.Add(method);
				foreach (APGenColumn pk in table.PrimaryKeyColumns)
				{
					method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
					call.Parameters.Add(ParamRef(pk.ParamName));
				}
			}

			CodeTypeReference listType = TypeTRef("List", table.DataType);
			// condition query
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryComment, whereDef, Param(OrderPhraseType, "orderBy"), Param(IntType, "take"), Param(IntType, "skip"));
				method.ReturnType = listType;
				method.Statements.Add(Return(MethodInvoke(bpl, conditionQueryMethodName, whereRef, ParamRef("orderBy"), ParamRef("take"), ParamRef("skip"))));
				ctd.Members.Add(method);
			}
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryComment, whereDef, Param(OrderPhraseType, "orderBy"), Param(IntType, "take"));
				method.ReturnType = listType;
				method.Statements.Add(Return(MethodInvoke(bpl, conditionQueryMethodName, whereRef, ParamRef("orderBy"), ParamRef("take"))));
				ctd.Members.Add(method);
			}
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryComment, whereDef, Param(OrderPhraseType, "orderBy"));
				method.ReturnType = listType;
				method.Statements.Add(Return(MethodInvoke(bpl, conditionQueryMethodName, whereRef, ParamRef("orderBy"))));
				ctd.Members.Add(method);
			}

			// get all
			{
				CodeMemberMethod method = NewMemberMethod(getAllMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_GetAllComment);
				method.ReturnType = listType;
				method.Statements.Add(Return(MethodInvoke(bpl, getAllMethodName)));
				ctd.Members.Add(method);
			}

			#endregion


			// if FromBase
			//
			if (table.DataInheritFromBase)
			{
				CodeTypeDeclaration ctd_ex;
				ctd_ex = NewClass(table.DataName, TypeAttributes.Public, table.Comment);
				ctd_ex.CustomAttributes.Add(AttrDecl(TypeRef("Serializable")));
				ctd_ex.IsPartial = true;
				ctd_ex.BaseTypes.Add(TypeRef(table.DataBaseName));
				cns.Types.Add(ctd_ex);


				// constructor
				//
				constructor = NewCtor(MemberAttributes.Public, APResource.APBusiness_DefaultConstructorComment);
				ctd_ex.Members.Add(constructor);

				constructor = NewCtor(MemberAttributes.Public, APResource.APBusiness_InitFieldsConstructorComment);
				ctd_ex.Members.Add(constructor);

				foreach (APGenColumn column in table.Columns)
				{
					constructor.Parameters.Add(Param(column.TypeRef, column.ParamName));
					constructor.BaseConstructorArgs.Add(ParamRef(column.ParamName));
				}
			}
		}


		#endregion


		#region [ Dal Gen ]


		private void CreateDal(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			CodeTypeDeclaration ctd;
			ctd = NewClass(table.DalInheritFromBase ? table.DalBaseName : table.DalName, TypeAttributes.Public, table.Comment + (table.DalInheritFromBase ? " DalBase" : " Dal"));
			ctd.IsPartial = true;
			ctd.BaseTypes.Add(DalType);
			cns.Members.Add(ctd);

			// constructors
			//
			CodeConstructor ctor1 = NewCtor(MemberAttributes.Public, null);
			ctd.Members.Add(ctor1);
			CodeConstructor ctor2 = NewCtor(MemberAttributes.Public, null, Param(DatabaseType, "db"));
			ctor2.BaseConstructorArgs.Add(ParamRef("db"));
			ctd.Members.Add(ctor2);



			// if FromBase
			//
			if (table.DalInheritFromBase)
			{
				CodeTypeDeclaration ctd_ex;
				ctd_ex = NewClass(table.DalName, TypeAttributes.Public, table.Comment + " Dal");
				ctd_ex.IsPartial = true;
				ctd_ex.BaseTypes.Add(TypeRef(table.DalBaseName));
				cns.Members.Add(ctd_ex);

				// constructors
				//
				CodeConstructor ctor_ex1 = NewCtor(MemberAttributes.Public, null);
				ctd_ex.Members.Add(ctor_ex1);
				CodeConstructor ctor_ex2 = NewCtor(MemberAttributes.Public, null, Param(DatabaseType, "db"));
				ctor_ex2.BaseConstructorArgs.Add(ParamRef("db"));
				ctd_ex.Members.Add(ctor_ex2);
			}

			if (!isView)
			{
				// insert method
				//
				CreateDalInsert(ctd, table, isView);

				// update method
				//
				CreateDalUpdate(ctd, table, isView);
				// update method
				//
				CreateDalUpdate2(ctd, table, isView);

				// primary delete method
				//
				CreateDalPrimaryDelete(ctd, table, isView);

				// condition delete method
				//
				CreateDalConditionDelete(ctd, table, isView);
			}

			// condition query count
			//
			CreateDalConditionQueryCount(ctd, table, isView);

			// primary get method
			//
			CreateDalPrimaryGet(ctd, table, isView);

			// condition query
			//
			CreateDalConditionQuery(ctd, table, isView);

			// misc methods
			//
			CreateDalMisc(ctd, table, isView);
		}


		private void CreateDalInsert(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			if (isView) return;

			CodeMemberMethod method = NewMemberMethod(insertMethodName, MemberAttributes.Public, APResource.APBusiness_InsertComment, Param(table.DataType, "data"));
			cns.Members.Add(method);

			CodeArgumentReferenceExpression dataRef = ParamRef("data");

			APGenColumn dbIdentity = null;

			foreach (APGenColumn column in table.Columns)
			{
				if (column.IdentityType == APColumnIdentityType.Provider)
				{
					if (column.ParsedType == typeof(Guid))
					{
						method.Statements.Add(
							If(Equals(PropRef(dataRef, column.PropertyName), PropRef(TypeRefExp(typeof(Guid)), "Empty")),
							Let(PropRef(dataRef, column.PropertyName), MethodInvoke(TypeRefExp(typeof(Guid)), "NewGuid"))));
					}
					else
					{
						method.Statements.Add(
							If(Equals(PropRef(dataRef, column.PropertyName), Const(0)),
							Let(PropRef(dataRef, column.PropertyName), 
							Cast(TypeRef(column.ParsedType),
							MethodInvoke("GetNewId", PropRef(PropRef(DBDef, table.ClassName), column.PropertyName))))));
					}
				}
				else if (column.IdentityType == APColumnIdentityType.Database && dbIdentity == null)
				{
					dbIdentity = column;
				}
			}


			CodePropertyReferenceExpression tableRef
				= PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression insert
				= MethodInvoke(QueryTypeExp, "insert", tableRef);
			CodeMethodInvokeExpression set
				= MethodInvoke(insert, "values");
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", set);
			method.Statements.Add(query);

			if (dbIdentity == null)
			{
				method.Statements.Add(MethodInvoke("ExecuteNonQuery", Local("query")));
			}
			else
			{
				method.Statements.Add(Let(PropRef(dataRef, dbIdentity.PropertyName),
					Cast(dbIdentity.TypeRef,
						MethodInvoke(
						TypeRefExp("Convert"),
						"ChangeType",
						MethodInvoke("ExecuteScalar", Local("query")),
						Typeof(dbIdentity.TypeRef)
						)
					)));
			}


			foreach (APGenColumn column in table.Columns)
			{
				if (column.IdentityType != APColumnIdentityType.Database)
					set.Parameters.Add(MethodInvoke(PropRef(tableRef, column.PropertyName), "SetValue", PropRef(dataRef, column.PropertyName)));
			}
		}


		private void CreateDalUpdate(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			if (isView) return;

			CodeMemberMethod method = NewMemberMethod(updateMethodName, MemberAttributes.Public, APResource.APBusiness_UpdateComment, Param(table.DataType, "data"));
			cns.Members.Add(method);

			CodeArgumentReferenceExpression dataRef = ParamRef("data");


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression update
				= MethodInvoke(QueryTypeExp, "update", tableRef);
			CodeMethodInvokeExpression set
				= MethodInvoke(update, "values");
			CodeMethodInvokeExpression where
				= MethodInvoke(set, "where");
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", where);
			method.Statements.Add(query);
			method.Statements.Add(MethodInvoke("ExecuteNonQuery", Local("query")));

			foreach (APGenColumn column in table.Columns)
			{
				if (column.PrimaryKey)
					where.Parameters.Add(Equals(PropRef(tableRef, column.PropertyName), PropRef(dataRef, column.PropertyName)));
				else if (column.IdentityType != APColumnIdentityType.Database)
					set.Parameters.Add(MethodInvoke(PropRef(tableRef, column.PropertyName), "SetValue", PropRef(dataRef, column.PropertyName)));
			}
		}


		private void CreateDalUpdate2(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			if (isView) return;

			CodeMemberMethod method = NewMemberMethod(updateMethodName, MemberAttributes.Public, APResource.APBusiness_UpdateComment);
			cns.Members.Add(method);
			foreach (APGenColumn pk in table.PrimaryKeyColumns)
			{
				method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
			}
			method.Parameters.Add(Param(ObjectType, "metadata"));


			CodeArgumentReferenceExpression dataRef = ParamRef("metadata");


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression update
				= MethodInvoke(QueryTypeExp, "update", tableRef);
			CodeMethodInvokeExpression set
				= MethodInvoke(update, "values");
			CodeMethodInvokeExpression where
				= MethodInvoke(set, "where");
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", where);
			method.Statements.Add(query);
			method.Statements.Add(MethodInvoke("ExecuteNonQuery", Local("query")));

			set.Parameters.Add(MethodInvoke(TypeRefExp(SetPhraseSelectorType), "Select", tableRef, dataRef));


			foreach (APGenColumn pk in table.PrimaryKeyColumns)
			{
				where.Parameters.Add(Equals(PropRef(tableRef, pk.PropertyName), PropRef(pk.ParamName)));
			}
		}


		private void CreateDalPrimaryDelete(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			if (isView) return;

			CodeMemberMethod method = NewMemberMethod(primaryDeleteMethodName, MemberAttributes.Public, APResource.APBusiness_PrimaryDeleteComment);
			cns.Members.Add(method);
			foreach (APGenColumn pk in table.PrimaryKeyColumns)
			{
				method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
			}


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression delete
				= MethodInvoke(QueryTypeExp, "delete", tableRef);
			CodeMethodInvokeExpression where
				= MethodInvoke(delete, "where");
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", where);
			method.Statements.Add(query);
			method.Statements.Add(MethodInvoke("ExecuteNonQuery", Local("query")));

			foreach (APGenColumn pk in table.PrimaryKeyColumns)
			{
				where.Parameters.Add(Equals(PropRef(tableRef, pk.PropertyName), PropRef(pk.ParamName)));
			}
		}


		private void CreateDalConditionDelete(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			if (isView) return;

			CodeMemberMethod method = NewMemberMethod(conditionDeleteMethodName, MemberAttributes.Public, APResource.APBusiness_ConditionDeleteComment, Param(WherePhraseType, "condition"));
			cns.Members.Add(method);


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression delete
				= MethodInvoke(QueryTypeExp, "delete", tableRef);
			CodeMethodInvokeExpression where
				= MethodInvoke(delete, "where", ParamRef("condition"));
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", where);
			method.Statements.Add(query);
			method.Statements.Add(MethodInvoke("ExecuteNonQuery", Local("query")));
		}


		private void CreateDalConditionQueryCount(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			CodeMemberMethod method = NewMemberMethod(conditionQueryCountMethodName, MemberAttributes.Public, APResource.APBusiness_ConditionQueryCountComment, Param(WherePhraseType, "condition"));
			cns.Members.Add(method);
			method.ReturnType = IntType;


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression select
				= MethodInvoke(QueryTypeExp, "select", MethodInvoke(PropRef(tableRef, "Asterisk"), "Count"));
			CodeMethodInvokeExpression from
				= MethodInvoke(select, "from", tableRef);
			CodeMethodInvokeExpression where
				= MethodInvoke(from, "where", ParamRef("condition"));
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", where);
			method.Statements.Add(query);
			method.Statements.Add(Return(MethodInvoke("ExecuteCount", Local("query"))));
		}


		private void CreateDalPrimaryGet(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			CodeMemberMethod method = NewMemberMethod(primaryGetMethodName, MemberAttributes.Public, APResource.APBusiness_PrimaryGetComment);
			cns.Members.Add(method);
			method.ReturnType = table.DataType;
			foreach (APGenColumn pk in table.PrimaryKeyColumns)
			{
				method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
			}


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression select
				= MethodInvoke(QueryTypeExp, "select", PropRef(tableRef, "Asterisk"));
			CodeMethodInvokeExpression from
				= MethodInvoke(select, "from", tableRef);
			CodeMethodInvokeExpression where
				= MethodInvoke(from, "where");
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", where);
			method.Statements.Add(query);
			method.Statements.Add(VarDecl(DataReaderType, "reader", MethodInvoke("ExecuteReader", Local("query"))));


			foreach (APGenColumn pk in table.PrimaryKeyColumns)
			{
				where.Parameters.Add(Equals(PropRef(tableRef, pk.PropertyName), PropRef(pk.ParamName)));
			}

			// try ... finally ...
			CodeVariableReferenceExpression reader = Local("reader");
			CodeFieldReferenceExpression mapFull = FieldRef(fullupMethodName);
			CodeTryCatchFinallyStatement _try;
			method.Statements.Add(_try = Try());
			_try.TryStatements.Add(
				If(MethodInvoke(reader, "Read"),
					Return(MethodInvoke(tableRef, mapMethodName, reader))
				));
			_try.TryStatements.Add(ReturnNull());

			_try.FinallyStatements.Add(MethodInvoke(reader, "Close"));
		}


		private void CreateDalConditionQuery(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public, APResource.APBusiness_ConditionQueryComment, Param(WherePhraseType, "condition"), Param(OrderPhraseType, "orderBy"), Param(typeof(int?), "take"), Param(typeof(int?), "skip"));
			cns.Members.Add(method);
			CodeTypeReference listType = TypeTRef("List", table.DataType);
			method.ReturnType = listType;


			CodePropertyReferenceExpression tableRef = PropRef(DBDef, table.ClassName);
			CodeMethodInvokeExpression select
				= MethodInvoke(QueryTypeExp, "select", PropRef(tableRef, "Asterisk"));
			CodeMethodInvokeExpression from
				= MethodInvoke(select, "from", tableRef);
			CodeVariableDeclarationStatement query = VarDecl(VarType, "query", from);
			method.Statements.Add(query);

			{
				var _if = If(NotEqualsValue(ParamRef("condition"), Null()));
				_if.TrueStatements.Add(MethodInvoke(FieldRef("query"), "where", ParamRef("condition")));
				method.Statements.Add(_if);
			}
			{
				var _if = If(NotEqualsValue(ParamRef("orderBy"), Null()));
				_if.TrueStatements.Add(MethodInvoke(FieldRef("query"), "order_by", ParamRef("orderBy")));
				method.Statements.Add(_if);
			}
			{
				var _if = If(NotEqualsValue(ParamRef("take"), Null()));
				_if.TrueStatements.Add(MethodInvoke(FieldRef("query"), "take", ParamRef("take")));
				method.Statements.Add(_if);
			}
			{
				var _if = If(NotEqualsValue(ParamRef("skip"), Null()));
				_if.TrueStatements.Add(MethodInvoke(FieldRef("query"), "skip", ParamRef("skip")));
				method.Statements.Add(_if);
			}
			method.Statements.Add(MethodInvoke(FieldRef("query"), "primary", PropRef(tableRef, table.PrimaryKeyColumns[0].PropertyName)));

			method.Statements.Add(VarDecl(DataReaderType, "reader", MethodInvoke("ExecuteReader", Local("query"))));


			CodeVariableReferenceExpression reader = Local("reader");
			CodeFieldReferenceExpression mapFull = FieldRef(fullupMethodName);
			method.Statements.Add(Return(MethodInvoke(tableRef, mapListMethodName, reader)));
		}


		private void CreateDalConditionQueryOrderByIndex(CodeTypeDeclaration cns, APGenTable table, APGenIndex index, bool isView)
		{
			CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName + "OrderBy" + index.Name, MemberAttributes.Public, APResource.APBusiness_ConditionQueryComment, Param(WherePhraseType, "condition"), Param(typeof(int), "maxReturnCount"));
			cns.Members.Add(method);
			CodeTypeReference listType = TypeTRef("List", table.DataType);
			method.ReturnType = listType;

			method.Statements.Add(
				Return(MethodInvoke(conditionQueryMethodName,
				ParamRef("condition"),
				PropRef(FieldRef(DBDef, table.Name), index.Name),
				ParamRef("maxReturnCount")))
				);
		}


		private void CreateDalMisc(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			CodeParameterDeclarationExpression dataDef = Param(table.DataType, "data");
			CodeParameterDeclarationExpression whereDef = Param(WherePhraseType, "condition");
			CodeArgumentReferenceExpression dataRef = ParamRef("data");


			if (!isView)
			{
				// GetInitData
				CodeMemberMethod getInitData = NewMemberMethod(getInitDataMethodName, MemberAttributes.Public, APResource.APBusiness_GetInitDataComment);
				cns.Members.Add(getInitData);
				getInitData.ReturnType = TypeTRef("List", table.DataType);
				getInitData.Statements.Add(Return(New(TypeTRef("List", table.DataType))));
			}

			// initData
			CodeMemberMethod initData = NewMemberMethod(initDataMethodName, MemberAttributes.Public, APResource.APBusiness_InitDataComment, Param(DBType, "db"));
			cns.Members.Add(initData);
			if (!isView)
			{
				initData.Statements.Add(VarDecl(TypeTRef("List", table.DataType), "list", MethodInvoke(getInitDataMethodName)));
				CodeIterationStatement iteration = For(
					VarDecl(typeof(int), "i", Const(0)),
					LessThan(Local("i"), PropRef(Local("list"), "Count")),
					Let(Local("i"), Add(Local("i"), Const(1))));
				iteration.Statements.Add(VarDecl(table.DataType, "data", IndexerRef(Local("list"), Local("i"))));
				CodeMethodInvokeExpression methodInvoke = MethodInvoke(primaryGetMethodName);
				foreach (APGenColumn pk in table.PrimaryKeyColumns)
				{
					methodInvoke.Parameters.Add(PropRef(Local("data"), pk.PropertyName));
				}
				CodeConditionStatement condition = If(EqualsValue(methodInvoke, Null()));
				condition.TrueStatements.Add(MethodInvoke(insertMethodName, Local("data")));
				iteration.Statements.Add(condition);
				initData.Statements.Add(iteration);
			}
		}


		#endregion


		#region [ Bpl Gen ]


		private void CreateBpl(CodeTypeDeclaration cns, APGenTable table, bool isView)
		{
			CodeTypeDeclaration ctd = NewClass(table.BplInheritFromBase ? table.BplBaseName : table.BplName, TypeAttributes.Public, table.Comment + (table.BplInheritFromBase ? " BplBase" : " Bpl"));
			ctd.IsPartial = true;
			cns.Members.Add(ctd);

			if (table.DalInheritFromBase)
			{
				CodeTypeDeclaration ctd_ex;
				ctd_ex = NewClass(table.BplName, TypeAttributes.Public, table.Comment + " Dal");
				ctd_ex.IsPartial = true;
				ctd_ex.BaseTypes.Add(TypeRef(table.BplBaseName));
				cns.Members.Add(ctd_ex);
			}

			CodeParameterDeclarationExpression dataDef = Param(table.DataType, "data");

			CodeArgumentReferenceExpression dataRef = ParamRef("data");
			CodeParameterDeclarationExpression whereDef = Param(WherePhraseType, "condition");
			CodeArgumentReferenceExpression whereRef = ParamRef("condition");
			CodeTypeReference listType = TypeTRef("List", table.DataType);


			if (!isView)
			{
				#region [ Insert(...) ]
				{
					CodeMemberMethod method = NewMemberMethod(insertMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_InsertComment, dataDef);
					ctd.Members.Add(method);
					method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
					CodeTryCatchFinallyStatement _try;
					method.Statements.Add(_try = Try());


					_try.TryStatements.Add(MethodInvoke(PropRef(Local("db"), table.DalName), insertMethodName, dataRef));
					_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
				}
				#endregion


				#region [ Update(...) ]
				{
					CodeMemberMethod method = NewMemberMethod(updateMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_UpdateComment, dataDef);
					ctd.Members.Add(method);
					method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
					CodeTryCatchFinallyStatement _try;
					method.Statements.Add(_try = Try());


					_try.TryStatements.Add(MethodInvoke(PropRef(Local("db"), table.DalName), updateMethodName, dataRef));
					_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
				}
				#endregion


				#region [ PrimaryDelete(...) ]
				{
					CodeMemberMethod method = NewMemberMethod(primaryDeleteMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_PrimaryDeleteComment);
					ctd.Members.Add(method);

					foreach (APGenColumn pk in table.PrimaryKeyColumns)
					{
						method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
					}

					method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
					CodeTryCatchFinallyStatement _try = Try();


					CodeMethodInvokeExpression primaryDelete = MethodInvoke(PropRef(Local("db"), table.DalName), primaryDeleteMethodName);
					foreach (APGenColumn pk in table.PrimaryKeyColumns)
					{
						primaryDelete.Parameters.Add(ParamRef(pk.ParamName));
					}
					_try.TryStatements.Add(primaryDelete);

					_try.FinallyStatements.Add(MethodInvoke(Local("db"), "Close"));

					method.Statements.Add(_try);
				}
				#endregion


				#region [ ConditionDelete(...) ]
				{
					CodeMemberMethod method = NewMemberMethod(conditionDeleteMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionDeleteComment, whereDef);
					ctd.Members.Add(method);
					method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
					CodeTryCatchFinallyStatement _try;
					method.Statements.Add(_try = Try());
					_try.TryStatements.Add(MethodInvoke(PropRef(Local("db"), table.DalName), conditionDeleteMethodName, whereRef));
					_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
				}
				#endregion
			}


			#region [ ConditionQueryCount(...) ]
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryCountMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryCountComment, whereDef);
				method.ReturnType = IntType;
				ctd.Members.Add(method);
				method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
				CodeTryCatchFinallyStatement _try;
				method.Statements.Add(_try = Try());
				_try.TryStatements.Add(Return(MethodInvoke(PropRef(Local("db"), table.DalName), conditionQueryCountMethodName, whereRef)));
				_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
			}
			#endregion


			#region [ PrimaryGet(...) ]
			{
				CodeMemberMethod method = NewMemberMethod(primaryGetMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_PrimaryGetComment);
				ctd.Members.Add(method);
				method.ReturnType = table.DataType;

				CodeMethodInvokeExpression baseCall = MethodInvoke(PropRef(Local("db"), table.DalName), primaryGetMethodName);
				foreach (APGenColumn pk in table.PrimaryKeyColumns)
				{
					method.Parameters.Add(Param(pk.TypeRef, pk.ParamName));
					baseCall.Parameters.Add(ParamRef(pk.ParamName));
				}

				method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
				CodeTryCatchFinallyStatement _try;
				method.Statements.Add(_try = Try());
				_try.TryStatements.Add(Return(baseCall));
				_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
			}
			#endregion


			#region [ ConditionQuery(...) #1 ]
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryComment, whereDef, Param(OrderPhraseType, "orderBy"), Param(typeof(int?), "take"), Param(typeof(int?), "skip"));
				method.ReturnType = listType;
				ctd.Members.Add(method);
				method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
				CodeTryCatchFinallyStatement _try;
				method.Statements.Add(_try = Try());
				_try.TryStatements.Add(Return(MethodInvoke(PropRef(Local("db"), table.DalName), conditionQueryMethodName, whereRef, FieldRef("orderBy"), ParamRef("take"), ParamRef("skip"))));
				_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
			}
			#endregion

			#region [ ConditionQuery(...) #2 ]
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryComment, whereDef, Param(OrderPhraseType, "orderBy"), Param(typeof(int?), "take"));
				method.ReturnType = listType;
				ctd.Members.Add(method);
				method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
				CodeTryCatchFinallyStatement _try;
				method.Statements.Add(_try = Try());
				_try.TryStatements.Add(Return(MethodInvoke(PropRef(Local("db"), table.DalName), conditionQueryMethodName, whereRef, FieldRef("orderBy"), ParamRef("take"), Null())));
				_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
			}
			#endregion

			#region [ ConditionQuery(...) #3 ]
			{
				CodeMemberMethod method = NewMemberMethod(conditionQueryMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_ConditionQueryComment, whereDef, Param(OrderPhraseType, "orderBy"));
				method.ReturnType = listType;
				ctd.Members.Add(method);
				method.Statements.Add(VarDecl(DBType, "db", New(DBType)));
				CodeTryCatchFinallyStatement _try;
				method.Statements.Add(_try = Try());
				_try.TryStatements.Add(Return(MethodInvoke(PropRef(Local("db"), table.DalName), conditionQueryMethodName, whereRef, FieldRef("orderBy"), Null(), Null())));
				_try.FinallyStatements.Add(MethodInvoke("db", "Close"));
			}
			#endregion


			#region [ GetAll() ]
			{
				CodeMemberMethod method = NewMemberMethod(getAllMethodName, MemberAttributes.Public | MemberAttributes.Static, APResource.APBusiness_GetAllComment);
				method.ReturnType = listType;
				ctd.Members.Add(method);
				method.Statements.Add(Return(MethodInvoke(conditionQueryMethodName, Null(), Null())));
			}
			#endregion
		}


		#endregion


		#region [ Override Implementation of APGenSection ]


		/// <summary>
		/// Generate source code and database with the business modes.
		/// </summary>
		/// <param name="gen">The specified APGen generation object.</param>
		public override void Generate(APGen gen)
		{
			if (!Enabled)
				return;

			InitPrefix();



			CodeNamespace cns = gen.GetCodeNamespace(Namespace ?? gen.DefaultNamespace);
			cns.Imports.Add(new CodeNamespaceImport("System"));
			cns.Imports.Add(new CodeNamespaceImport("System.Collections"));
			cns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
			cns.Imports.Add(new CodeNamespaceImport("System.Data"));
			cns.Imports.Add(new CodeNamespaceImport("System.ComponentModel.DataAnnotations"));
			cns.Imports.Add(new CodeNamespaceImport("Symber.Web.Data"));



			// class APDBDef
			CreateDatabaseDef(cns);
			// class APDalDef
			CodeTypeDeclaration dalDef = CreateDalDef(cns);
			// class APBplDef
			CodeTypeDeclaration bplDef = CreateBplDef(cns);

			foreach (APGenTable table in Tables)
			{
				// class a Data
				CreateData(cns, table, false);

				// class a Dal
				CreateDal(dalDef, table, false);

				// class a Bpl
				CreateBpl(bplDef, table, false);
			}

			foreach (APGenTable view in Views)
			{
				// class a Data
				CreateData(cns, view, true);

				// class a Dal
				CreateDal(dalDef, view, true);

				// class a Bpl
				CreateBpl(bplDef, view, true);
			}
		}


		/// <summary>
		/// Synchronize data.
		/// </summary>
		/// <param name="gen">The specified APGen object.</param>
		public override void SyncData(APGen gen)
		{
			if (Enabled && AutoSyncDatabase)
			{
				ProviderInstance.Sync(this);
			}
		}


		/// <summary>
		/// Initialize data.
		/// </summary>
		/// <param name="gen">The specified APGen object.</param>
		public override void InitData(APGen gen)
		{
			if (Enabled && AutoInitDatabase)
			{
				string ns = Namespace ?? gen.DefaultNamespace;
				Type type = APTypeHelper.LoadType(string.IsNullOrEmpty(ns) ? DBDefName : ns + "." + DBDefName);
				if (type != null)
				{
					type.InvokeMember("InitData", BindingFlags.InvokeMethod, null, null, new object[] { });
				}
			}
		}


		#endregion

	}
}
