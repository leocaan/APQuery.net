using Symber.Web.Data;
using System;
using System.CodeDom;
using System.ComponentModel;
using System.Data;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Column definition in business model.
	/// </summary>
	public sealed class APGenColumn : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty columnNameProp;
		private static APGenProperty propertyNameProp;
		private static APGenProperty typeProp;
		private static APGenProperty isEnumProp;
		private static APGenProperty defaultValueProp;
		private static APGenProperty overrideProp;

		private static APGenProperty identityTypeProp;
		private static APGenProperty providerIdentityBaseProp;
		private static APGenProperty isNullableProp;
		private static APGenProperty primaryKeyProp;
		private static APGenProperty dbTypeProp;
		private static APGenProperty dbDefaultValueProp;
		private static APGenProperty dataLengthProp;
		private static APGenProperty precisionProp;
		private static APGenProperty scaleProp;
		private static APGenProperty commentProp;
		private static APGenProperty displayProp;
		private static APGenProperty requiredProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Fields ]


		private string _fieldName;
		private string _paramName;
		private Type _parsedType;
		private bool _parsed;
		private CodeTypeReference _typeRef;
		private CodeTypeReference _defRef;


		#endregion


		#region [ Constructors ]


		static APGenColumn()
		{
			nameProp = new APGenProperty(
				"name", 
				typeof(string), 
				null, 
				APCVHelper.WhiteSpaceTrimStringConverter, 
				APCVHelper.NonEmptyStringValidator, 
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			columnNameProp = new APGenProperty("columnName", typeof(string));
			propertyNameProp = new APGenProperty("propertyName", typeof(string));
			typeProp = new APGenProperty(
				"type",
				typeof(string),
				"string",
				TypeDescriptor.GetConverter(typeof(string)),
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.None
				);
			isEnumProp = new APGenProperty("isEnum", typeof(bool), false);
			defaultValueProp = new APGenProperty("defaultValue", typeof(string), "");
			overrideProp = new APGenProperty("override", typeof(bool), false);
			identityTypeProp = new APGenProperty(
				"identityType",
				typeof(APColumnIdentityType),
				APColumnIdentityType.None,
				new GenericEnumAPConverter(typeof(APColumnIdentityType)),
				APCVHelper.DefaultValidator, 
				APGenPropertyOptions.None
				);
			providerIdentityBaseProp = new APGenProperty("providerIdentityBase", typeof(int), 5000);
			isNullableProp = new APGenProperty("isNullable", typeof(bool), false);
			primaryKeyProp = new APGenProperty("primaryKey", typeof(bool), false);
			dbTypeProp = new APGenProperty(
				"dbType",
				typeof(DbType),
				DbType.Object,
				new GenericEnumAPConverter(typeof(DbType)),
				APCVHelper.DefaultValidator, 
				APGenPropertyOptions.None
				);
			dbDefaultValueProp = new APGenProperty("dbDefaultValue", typeof(string));
			dataLengthProp = new APGenProperty("dataLength", typeof(int), 0);
			precisionProp = new APGenProperty("precision", typeof(int), 18);
			scaleProp = new APGenProperty("scale", typeof(int), 0);
			commentProp = new APGenProperty("comment", typeof(string));
			displayProp = new APGenProperty("display", typeof(string));
			requiredProp = new APGenProperty("required", typeof(bool));


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(columnNameProp);
			properties.Add(propertyNameProp);
			properties.Add(typeProp);
			properties.Add(isEnumProp);
			properties.Add(defaultValueProp);
			properties.Add(overrideProp);
			properties.Add(identityTypeProp);
			properties.Add(providerIdentityBaseProp);
			properties.Add(isNullableProp);
			properties.Add(primaryKeyProp);
			properties.Add(dbTypeProp);
			properties.Add(dbDefaultValueProp);
			properties.Add(dataLengthProp);
			properties.Add(precisionProp);
			properties.Add(scaleProp);
			properties.Add(commentProp);
			properties.Add(displayProp);
			properties.Add(requiredProp);
		}


		/// <summary>
		/// Create a new column definition.
		/// </summary>
		public APGenColumn()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Name of the column definition.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Database column name.
		/// </summary>
		[APGenProperty("columnName")]
		public string ColumnName
		{
			get { return (string)base[columnNameProp]; }
			set { base[columnNameProp] = value; }
		}


		/// <summary>
		/// Property name in source code.
		/// </summary>
		[APGenProperty("propertyName")]
		public string PropertyName
		{
			get { return (string)base[propertyNameProp]; }
			set { base[propertyNameProp] = value; }
		}


		/// <summary>
		/// Data type of the column.
		/// </summary>
		[APGenProperty("type", DefaultValue = "string")]
		public string Type
		{
			get { return (string)base[typeProp]; }
			set { base[typeProp] = value; }
		}


		/// <summary>
		/// Data type is enum.
		/// </summary>
		[APGenProperty("isEnum", DefaultValue = false)]
		public bool IsEnum
		{
			get { return (bool)base[isEnumProp]; }
			set { base[isEnumProp] = value; }
		}


		/// <summary>
		/// Default value of the column in source code, used when creating a new object.
		/// </summary>
		[APGenProperty("defaultValue", DefaultValue = "")]
		public string DefaultValue
		{
			get { return (string)base[defaultValueProp]; }
			set { base[defaultValueProp] = value; }
		}


		/// <summary>
		/// Data attribute is override.
		/// </summary>
		[APGenProperty("override", DefaultValue = false)]
		public bool Override
		{
			get { return (bool)base[overrideProp]; }
			set { base[overrideProp] = value; }
		}


		/// <summary>
		/// Whether the column is a identity column.
		/// </summary>
		[APGenProperty("IdentityType", DefaultValue = APColumnIdentityType.None)]
		public APColumnIdentityType IdentityType
		{
			get { return (APColumnIdentityType)base[identityTypeProp]; }
			set { base[identityTypeProp] = value; }
		}


		/// <summary>
		/// Base value when identityType is 'Provider".
		/// </summary>
		[APGenProperty("providerIdentityBase", DefaultValue = 5000)]
		public int ProviderIdentityBase
		{
			get { return (int)base[providerIdentityBaseProp]; }
			set { base[providerIdentityBaseProp] = value; }
		}


		/// <summary>
		/// Whether the column can be null in database.
		/// </summary>
		[APGenProperty("isNullable", DefaultValue = false)]
		public bool IsNullable
		{
			get { return (bool)base[isNullableProp]; }
			set { base[isNullableProp] = value; }
		}


		/// <summary>
		/// Whether the column is a primary key.
		/// </summary>
		[APGenProperty("primaryKey", DefaultValue = false)]
		public bool PrimaryKey
		{
			get { return (bool)base[primaryKeyProp]; }
			set { base[primaryKeyProp] = value; }
		}


		/// <summary>
		/// Data type in database.
		/// </summary>
		[APGenProperty("dbType", DefaultValue = DbType.Object)]
		public DbType DBType
		{
			get { return (DbType)base[dbTypeProp]; }
			set { base[dbTypeProp] = value; }
		}


		/// <summary>
		/// Default value in database, used when insert a new row to the table, and the column value is not specified.
		/// </summary>
		[APGenProperty("dbDefaultValue")]
		public string DBDefaultValue
		{
			get { return (string)base[dbDefaultValueProp]; }
			set { base[dbDefaultValueProp] = value; }
		}


		/// <summary>
		/// Data length of the column.
		/// </summary>
		[APGenProperty("dataLength", DefaultValue = 0)]
		public int DataLength
		{
			get { return (int)base[dataLengthProp]; }
			set { base[dataLengthProp] = value; }
		}


		/// <summary>
		/// Precision of the column.
		/// </summary>
		[APGenProperty("precision", DefaultValue = 18)]
		public int Precision
		{
			get { return (int)base[precisionProp]; }
			set { base[precisionProp] = value; }
		}


		/// <summary>
		/// Scale of the column.
		/// </summary>
		[APGenProperty("scale", DefaultValue = 0)]
		public int Scale
		{
			get { return (int)base[scaleProp]; }
			set { base[scaleProp] = value; }
		}


		/// <summary>
		/// Comment used to create comment in source code and database.
		/// </summary>
		[APGenProperty("comment")]
		public string Comment
		{
			get { return (string)base[commentProp]; }
			set { base[commentProp] = value; }
		}


		/// <summary>
		/// Display of column to show UI string of lable or title.
		/// </summary>
		[APGenProperty("display")]
		public string Display
		{
			get { return (string)base[displayProp]; }
			set { base[displayProp] = value; }
		}


		/// <summary>
		/// Data attribute is required.
		/// </summary>
		[APGenProperty("required", DefaultValue = false)]
		public bool Required
		{
			get { return (bool)base[requiredProp]; }
			set { base[requiredProp] = value; }
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


		internal CodePrimitiveExpression ColumnExp
		{
			get { return new CodePrimitiveExpression(ColumnName); }
		}


		internal string FieldName
		{
			get
			{
				if (_fieldName == null)
					_fieldName = "_" + Char.ToLower(PropertyName[0]) + PropertyName.Substring(1);
				return _fieldName;
			}
		}


		internal string ParamName
		{
			get
			{
				if (_paramName == null)
					_paramName = Char.ToLower(PropertyName[0]) + PropertyName.Substring(1);
				return _paramName;
			}
		}


		internal CodeTypeReference TypeRef
		{
			get
			{
				if (_typeRef == null)
				{
					if (ParsedType == null)
						_typeRef = new CodeTypeReference(Type);
					else
						_typeRef = new CodeTypeReference(ParsedType);
				}
				return _typeRef;
			}
		}


		internal CodeTypeReference DefRef
		{
			get { return _defRef; }
			set { _defRef = value; }
		}


		/// <summary>
		/// Parsed type.
		/// </summary>
		public Type ParsedType
		{
			get
			{
				if (!_parsed)
				{
					_parsedType = APTypeHelper.LoadType(Type);
					_parsed = true;
				}
				return _parsedType;
			}
		}


		#endregion

	}
}
