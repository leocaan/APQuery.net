using System;
using System.CodeDom;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Table definition in business model.
	/// </summary>
	public sealed class APGenTable : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty tableNameProp;
		private static APGenProperty classNameProp;
		private static APGenProperty dataInheritFromBaseProp;
		private static APGenProperty dalInheritFromBaseProp;
		private static APGenProperty bplInheritFromBaseProp;
		private static APGenProperty inheritsProp;
		private static APGenProperty commentProp;
		private static APGenProperty columnsProp;
		private static APGenProperty indexesProp;
		private static APGenProperty uniquesProp;
		private static APGenProperty aliasesProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Fields ]


		private APGenColumnCollection _primaryKeyColumns;
		private string _fieldName;
		private string _paramName;


		#endregion


		#region [ Constructors ]


		static APGenTable()
		{
			nameProp = new APGenProperty(
				"name",
				typeof(string),
				null, 
				APCVHelper.WhiteSpaceTrimStringConverter, 
				APCVHelper.NonEmptyStringValidator, 
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			tableNameProp = new APGenProperty("tableName", typeof(string));
			classNameProp = new APGenProperty("className", typeof(string));
			dataInheritFromBaseProp = new APGenProperty("dataInheritFromBase", typeof(bool), true);
			dalInheritFromBaseProp = new APGenProperty("dalInheritFromBase", typeof(bool), true);
			bplInheritFromBaseProp = new APGenProperty("bplInheritFromBase", typeof(bool), true);
			inheritsProp = new APGenProperty("inherits", typeof(string), "");
			commentProp = new APGenProperty("comment", typeof(string));
			columnsProp = new APGenProperty("columns", typeof(APGenColumnCollection));
			indexesProp = new APGenProperty("indexes", typeof(APGenIndexCollection));
			uniquesProp = new APGenProperty("uniques", typeof(APGenIndexCollection));
			aliasesProp = new APGenProperty("aliases", typeof(APGenAliasCollection));


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(tableNameProp);
			properties.Add(classNameProp);
			properties.Add(dataInheritFromBaseProp);
			properties.Add(dalInheritFromBaseProp);
			properties.Add(bplInheritFromBaseProp);
			properties.Add(inheritsProp);
			properties.Add(commentProp);
			properties.Add(columnsProp);
			properties.Add(indexesProp);
			properties.Add(uniquesProp);
			properties.Add(aliasesProp);
		}


		/// <summary>
		/// Create a new table definition.
		/// </summary>
		public APGenTable()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Name of the table definition.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Table name in database.
		/// </summary>
		[APGenProperty("tableName")]
		public string TableName
		{
			get { return (string)base[tableNameProp]; }
			set { base[tableNameProp] = value; }
		}


		/// <summary>
		/// Class name for source code.
		/// </summary>
		[APGenProperty("className")]
		public string ClassName
		{
			get { return (string)base[classNameProp]; }
			set { base[classNameProp] = value; }
		}


		/// <summary>
		/// Generate a base class and a inherited class for data presentation.
		/// </summary>
		[APGenProperty("dataInheritFromBase", DefaultValue = true)]
		public bool DataInheritFromBase
		{
			get { return (bool)base[dataInheritFromBaseProp]; }
			set { base[dataInheritFromBaseProp] = value; }
		}


		/// <summary>
		/// Generate a base class and a inherited class for data access level.
		/// </summary>
		[APGenProperty("dalInheritFromBase", DefaultValue = true)]
		public bool DalInheritFromBase
		{
			get { return (bool)base[dalInheritFromBaseProp]; }
			set { base[dalInheritFromBaseProp] = value; }
		}


		/// <summary>
		/// Generate a base class and inherited class for business process level.
		/// </summary>
		[APGenProperty("bplInheritFromBase", DefaultValue = true)]
		public bool BplInheritFromBase
		{
			get { return (bool)base[bplInheritFromBaseProp]; }
			set { base[bplInheritFromBaseProp] = value; }
		}


		/// <summary>
		/// Inherits.
		/// </summary>
		[APGenProperty("inherits", DefaultValue = "")]
		public string Inherits
		{
			get { return (string)base[inheritsProp]; }
			set { base[inheritsProp] = value; }
		}


		/// <summary>
		/// Comment.
		/// </summary>
		[APGenProperty("comment")]
		public string Comment
		{
			get { return (string)base[commentProp]; }
			set { base[commentProp] = value; }
		}


		/// <summary>
		/// All columns in the table.
		/// </summary>
		[APGenProperty("columns")]
		public APGenColumnCollection Columns
		{
			get { return (APGenColumnCollection)base[columnsProp]; }
		}


		/// <summary>
		/// All indexes in the table.
		/// </summary>
		[APGenProperty("indexes")]
		public APGenIndexCollection Indexes
		{
			get { return (APGenIndexCollection)base[indexesProp]; }
		}


		/// <summary>
		/// All uniques in the table.
		/// </summary>
		[APGenProperty("uniques")]
		public APGenIndexCollection Uniques
		{
			get { return (APGenIndexCollection)base[uniquesProp]; }
		}


		/// <summary>
		/// All aliases for the table.
		/// </summary>
		[APGenProperty("aliases")]
		public APGenAliasCollection Aliases
		{
			get { return (APGenAliasCollection)base[aliasesProp]; }
		}


		/// <summary>
		/// Default index.
		/// </summary>
		public APGenIndex DefaultIndex
		{
			get
			{
				foreach (APGenIndex index in Indexes)
				{
					if (index.IsDefault) return index;
				}
				return null;
			}
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APGenPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion


		#region [ Internal Properties ]


		internal CodePrimitiveExpression TableExp
		{
			get { return new CodePrimitiveExpression(TableName); }
		}


		internal CodeTypeReference DataType
		{
			get { return new CodeTypeReference(ClassName); }
		}


		internal string TableDefName
		{
			get { return ClassName + "TableDef"; }
		}


		internal string DalBaseName
		{
			get { return ClassName + "DalBase"; }
		}


		internal string DalName
		{
			get { return ClassName + "Dal"; }
		}


		internal string BplBaseName
		{
			get { return ClassName + "BplBase"; }
		}


		internal string BplName
		{
			get { return ClassName + "Bpl"; }
		}


		internal string DataBaseName
		{
			get { return ClassName + "Base"; }
		}


		internal string DataName
		{
			get { return ClassName; }
		}


		/// <summary>
		/// Primary key columns.
		/// </summary>
		public APGenColumnCollection PrimaryKeyColumns
		{
			get
			{
				if (_primaryKeyColumns == null)
				{
					initPrimaryKeyColumns();
				}
				return _primaryKeyColumns;
			}
		}


		internal string FieldName
		{
			get
			{
				if (_fieldName == null)
					_fieldName = "_" + Char.ToLower(ClassName[0]) + ClassName.Substring(1);
				return _fieldName;
			}
		}


		internal string ParamName
		{
			get
			{
				if (_paramName == null)
					_paramName = Char.ToLower(ClassName[0]) + ClassName.Substring(1);
				return _paramName;
			}
		}


		#endregion


		#region [ Private Methods ]


		private void initPrimaryKeyColumns()
		{
			_primaryKeyColumns = new APGenColumnCollection();
			foreach (APGenColumn column in Columns)
			{
				if (column.PrimaryKey)
					_primaryKeyColumns.Add(column);
			}
			if (_primaryKeyColumns.Count == 0)
			{
				_primaryKeyColumns.Add(Columns[0]);
				Columns[0].PrimaryKey = true;
			}
		}


		#endregion


		#region [ Override Implementation of APGenElement ]


		/// <summary>
		/// Post deserialize.
		/// </summary>
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
			initPrimaryKeyColumns();
		}


		#endregion

	}
}
