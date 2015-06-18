using Symber.Web.Data;
using System;
using System.ComponentModel;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Database relation definition in business mode.
	/// </summary>
	public sealed class APGenRelation : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty masterTableProp;
		private static APGenProperty masterColumnProp;
		private static APGenProperty slaveTableProp;
		private static APGenProperty slaveColumnProp;
		private static APGenProperty cascadeTypeProp;
		private static APGenProperty commentProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Fields ]


		private APGenTable _masterTableRef;
		private APGenTable _slaveTableRef;
		private APGenColumn _masterColumnRef;
		private APGenColumn _slaveColumnRef;
		private string _fieldName;
		private string _paramName;


		#endregion


		#region [ Constructors ]


		static APGenRelation()
		{
			nameProp = new APGenProperty(
				"name",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			masterTableProp = new APGenProperty(
				"masterTable",
				typeof(string),
				"",
				TypeDescriptor.GetConverter(typeof(string)),
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			masterColumnProp = new APGenProperty(
				"masterColumn",
				typeof(string),
				"",
				TypeDescriptor.GetConverter(typeof(string)),
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			slaveTableProp = new APGenProperty(
				"slaveTable",
				typeof(string),
				"",
				TypeDescriptor.GetConverter(typeof(string)),
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			slaveColumnProp = new APGenProperty(
				"slaveColumn",
				typeof(string),
				"",
				TypeDescriptor.GetConverter(typeof(string)),
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			cascadeTypeProp = new APGenProperty(
				"cascadeType",
				typeof(APRelationCascadeType),
				APRelationCascadeType.None,
				new GenericEnumAPConverter(typeof(APRelationCascadeType)),
				APCVHelper.DefaultValidator,
				APGenPropertyOptions.None
				);
			commentProp = new APGenProperty("comment", typeof(string));


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(masterTableProp);
			properties.Add(masterColumnProp);
			properties.Add(slaveTableProp);
			properties.Add(slaveColumnProp);
			properties.Add(cascadeTypeProp);
			properties.Add(commentProp);
		}

		/// <summary>
		/// Create a new database relation definition.
		/// </summary>
		public APGenRelation()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Relation name.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Master table name.
		/// </summary>
		[APGenProperty("masterTable", DefaultValue = "", IsRequired = true)]
		public string MasterTable
		{
			get { return (string)base[masterTableProp]; }
			set { base[masterTableProp] = value; }
		}


		/// <summary>
		/// Master column name.
		/// </summary>
		[APGenProperty("masterColumn", DefaultValue = "", IsRequired = true)]
		public string MasterColumn
		{
			get { return (string)base[masterColumnProp]; }
			set { base[masterColumnProp] = value; }
		}


		/// <summary>
		/// Slave table name.
		/// </summary>
		[APGenProperty("slaveTable", DefaultValue = "", IsRequired = true)]
		public string SlaveTable
		{
			get { return (string)base[slaveTableProp]; }
			set { base[slaveTableProp] = value; }
		}


		/// <summary>
		/// Slave column name.
		/// </summary>
		[APGenProperty("slaveColumn", DefaultValue = "", IsRequired = true)]
		public string SlaveColumn
		{
			get { return (string)base[slaveColumnProp]; }
			set { base[slaveColumnProp] = value; }
		}


		/// <summary>
		/// Cascade type of the relation.
		/// </summary>
		[APGenProperty("cascadeType", DefaultValue = APRelationCascadeType.None)]
		public APRelationCascadeType CascadeType
		{
			get { return (APRelationCascadeType)base[cascadeTypeProp]; }
			set { base[cascadeTypeProp] = value; }
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
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APGenPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion


		#region [ Internal Properties ]


		internal string FieldName
		{
			get
			{
				if (_fieldName == null)
					_fieldName = "_" + Char.ToLower(Name[0]) + Name.Substring(1);
				return _fieldName;
			}
		}


		internal string ParamName
		{
			get
			{
				if (_paramName == null)
					_paramName = Char.ToLower(Name[0]) + Name.Substring(1);
				return _paramName;
			}
		}


		internal APGenTable MasterTableRef
		{
			get { return _masterTableRef; }
			set { _masterTableRef = value; }
		}


		internal APGenTable SlaveTableRef
		{
			get { return _slaveTableRef; }
			set { _slaveTableRef = value; }
		}


		internal APGenColumn MasterColumnRef
		{
			get { return _masterColumnRef; }
			set { _masterColumnRef = value; }
		}


		internal APGenColumn SlaveColumnRef
		{
			get { return _slaveColumnRef; }
			set { _slaveColumnRef = value; }
		}


		#endregion

	}
}
