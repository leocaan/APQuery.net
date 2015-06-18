using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Alias definition in business mode.
	/// </summary>
	public sealed class APGenAlias : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Fields ]


		private string _fieldName;
		private string _paramName;


		#endregion


		#region [ Constructors ]


		static APGenAlias()
		{
			nameProp = new APGenProperty(
				"name",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
		}


		/// <summary>
		/// Create a new alias definition.
		/// </summary>
		public APGenAlias()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Name of the alias definition.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
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
