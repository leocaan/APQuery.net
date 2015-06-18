
namespace Symber.Web.Compilation
{

	/// <summary>
	/// Gen Dal Provider definition in business mode.
	/// </summary>
	public sealed class APGenDalProvider : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty typeProp;
		private static APGenProperty connectionStringProp;
		private static APGenProperty providerNameProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenDalProvider()
		{
			nameProp = new APGenProperty(
				"name",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			typeProp = new APGenProperty(
				"type",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			connectionStringProp = new APGenProperty(
				"connectionString",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired
				);
			providerNameProp = new APGenProperty(
				"providerName",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.None
				);


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(typeProp);
			properties.Add(connectionStringProp);
			properties.Add(providerNameProp);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Name of APDalProvider.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Type of APDalProvider.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("type", IsRequired = true)]
		public string Type
		{
			get { return (string)base[typeProp]; }
			set { base[typeProp] = value; }
		}


		/// <summary>
		/// Database connection string.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("connectionString", IsRequired = true, IsKey = true)]
		public string ConnectionString
		{
			get { return (string)base[connectionStringProp]; }
			set { base[connectionStringProp] = value; }
		}


		/// <summary>
		/// Database provider name.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("providerName")]
		public string ProviderName
		{
			get { return (string)base[providerNameProp]; }
			set { base[providerNameProp] = value; }
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
