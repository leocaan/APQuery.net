using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Database index definition in business mode.
	/// </summary>
	public sealed class APGenIndex : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty isDefaultProp;
		private static APGenProperty ordersProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Fields ]


		private string _fieldName;
		private string _paramName;


		#endregion


		#region [ Constructors ]


		static APGenIndex()
		{
			nameProp = new APGenProperty(
				"name",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			isDefaultProp = new APGenProperty("isDefault", typeof(bool), false);
			ordersProp = new APGenProperty("", typeof(APGenOrderCollection), null, null, null, APGenPropertyOptions.IsDefaultCollection);


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(isDefaultProp);
			properties.Add(ordersProp);
		}


		/// <summary>
		/// Create a new database index definition.
		/// </summary>
		public APGenIndex()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Index name.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Whether the index is default.
		/// </summary>
		[APGenProperty("isDefault", DefaultValue = false)]
		public bool IsDefault
		{
			get { return (bool)base[isDefaultProp]; }
			set { base[isDefaultProp] = value; }
		}


		/// <summary>
		/// The columns and it's order in the index.
		/// </summary>
		[APGenProperty("", Options = APGenPropertyOptions.IsDefaultCollection)]
		public APGenOrderCollection Orders
		{
			get { return (APGenOrderCollection)base[ordersProp]; }
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


		#endregion

	}
}
