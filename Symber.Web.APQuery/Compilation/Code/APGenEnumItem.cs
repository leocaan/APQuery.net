
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Enumerate item config.
	/// </summary>
	public sealed class APGenEnumItem : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty defaultValueProp;
		private static APGenProperty dictionaryNameProp;
		private static APGenProperty commentProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenEnumItem()
		{
			nameProp = new APGenProperty(
				"name", 
				typeof(string), 
				null, 
				APCVHelper.WhiteSpaceTrimStringConverter, 
				APCVHelper.PropertyNameValidator, 
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			defaultValueProp = new APGenProperty("defaultValue", typeof(string), null);
			dictionaryNameProp = new APGenProperty("dictionaryName", typeof(string), "");
			commentProp = new APGenProperty("comment", typeof(string));

			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(defaultValueProp);
			properties.Add(dictionaryNameProp);
			properties.Add(commentProp);
		}


		/// <summary>
		/// Create a new enumerate item config.
		/// </summary>
		public APGenEnumItem()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Item name.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Default value.
		/// </summary>
		[APGenProperty("defaultValue", DefaultValue = null)]
		public string DefaultValue
		{
			get { return (string)base[defaultValueProp]; }
			set { base[defaultValueProp] = value; }
		}


		/// <summary>
		/// Name in dictionary.
		/// </summary>
		[APGenProperty("dictionaryName", DefaultValue = "")]
		public string DictionaryName
		{
			get { return (string)base[dictionaryNameProp]; }
			set { base[dictionaryNameProp] = value; }
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

	}
}
