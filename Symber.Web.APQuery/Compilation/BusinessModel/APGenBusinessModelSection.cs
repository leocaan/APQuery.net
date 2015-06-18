
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configures business mode for APGen.
	/// </summary>
	public sealed partial class APGenBusinessModelSection : APGenSection
	{

		#region [ Static Fields ]


		private static APGenProperty enabledProp;
		private static APGenProperty namespaceProp;
		private static APGenProperty dbPrefixProp;
		private static APGenProperty defPrefixProp;
		private static APGenProperty providerNameProp;
		private static APGenProperty mainPartProp;
		private static APGenProperty autoSyncDatabaseProp;
		private static APGenProperty autoInitDatabaseProp;

		private static APGenProperty providerProp;
		private static APGenProperty tablesProp;
		private static APGenProperty viewsProp;
		private static APGenProperty relationsProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenBusinessModelSection()
		{
			enabledProp = new APGenProperty("enabled", typeof(bool), true);
			namespaceProp = new APGenProperty("namespace", typeof(string), null);
			dbPrefixProp = new APGenProperty("dbPrefix", typeof(string), null);
			defPrefixProp = new APGenProperty("defPrefix", typeof(string), null);
			providerNameProp = new APGenProperty("providerName", typeof(string), null);
			mainPartProp = new APGenProperty("mainPart", typeof(bool), true);
			autoSyncDatabaseProp = new APGenProperty("autoSyncDatabase", typeof(bool), false);
			autoInitDatabaseProp = new APGenProperty("autoInitDatabase", typeof(bool), false);

			providerProp = new APGenProperty("provider", typeof(APGenDalProvider), null);
			tablesProp = new APGenProperty("tables", typeof(APGenTableCollection));
			viewsProp = new APGenProperty("views", typeof(APGenTableCollection));
			relationsProp = new APGenProperty("relations", typeof(APGenRelationCollection));


			properties = new APGenPropertyCollection();
			properties.Add(enabledProp);
			properties.Add(namespaceProp);
			properties.Add(dbPrefixProp);
			properties.Add(defPrefixProp);
			properties.Add(providerNameProp);
			properties.Add(mainPartProp);
			properties.Add(autoSyncDatabaseProp);
			properties.Add(autoInitDatabaseProp);

			properties.Add(providerProp);
			properties.Add(tablesProp);
			properties.Add(viewsProp);
			properties.Add(relationsProp);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Whether business mode is enabled.
		/// </summary>
		[APGenProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get { return (bool)base[enabledProp]; }
			set { base[enabledProp] = value; }
		}


		/// <summary>
		/// Namespace used to generate code.
		/// </summary>
		[APGenProperty("namespace", DefaultValue = null)]
		public string Namespace
		{
			get { return (string)base[namespaceProp]; }
			set { base[namespaceProp] = value; }
		}


		/// <summary>
		/// Database table prefix.
		/// </summary>
		[APGenProperty("dbPrefix", DefaultValue = null)]
		public string DBPrefix
		{
			get { return (string)base[dbPrefixProp]; }
			set { base[dbPrefixProp] = value; }
		}


		/// <summary>
		/// BplDef, DalDef and DBDef prefix, default is APBplDef, APDalDef and APDBDef.
		/// </summary>
		[APGenProperty("defPrefix", DefaultValue = null)]
		public string DefPrefix
		{
			get { return (string)base[defPrefixProp]; }
			set { base[defPrefixProp] = value; }
		}


		/// <summary>
		/// Provider name.
		/// </summary>
		[APGenProperty("providerName", DefaultValue = null)]
		public string ProviderName
		{
			get { return (string)base[providerNameProp]; }
			set { base[providerNameProp] = value; }
		}


		/// <summary>
		/// Whether business mode is mainPart.
		/// </summary>
		[APGenProperty("mainPart", DefaultValue = true)]
		public bool MainPart
		{
			get { return (bool)base[mainPartProp]; }
			set { base[mainPartProp] = value; }
		}


		/// <summary>
		/// Whether auto synchronize database.
		/// </summary>
		[APGenProperty("autoSyncDatabase", DefaultValue = false)]
		public bool AutoSyncDatabase
		{
			get { return (bool)base[autoSyncDatabaseProp]; }
			set { base[autoSyncDatabaseProp] = value; }
		}


		/// <summary>
		/// Whether auto initialize database.
		/// </summary>
		[APGenProperty("autoInitDatabase", DefaultValue = false)]
		public bool AutoInitDatabase
		{
			get { return (bool)base[autoInitDatabaseProp]; }
			set { base[autoInitDatabaseProp] = value; }
		}


		/// <summary>
		/// The Provider.
		/// </summary>
		[APGenProperty("provider", DefaultValue=null, Options=APGenPropertyOptions.None)]
		public APGenDalProvider Provider
		{
			get { return (APGenDalProvider)base[providerProp]; }
			set { base[providerProp] = value; }
		}


		/// <summary>
		/// All table definition in business mode.
		/// </summary>
		[APGenProperty("tables")]
		public APGenTableCollection Tables
		{
			get { return (APGenTableCollection)base[tablesProp]; }
		}


		/// <summary>
		/// All view definition in business mode.
		/// </summary>
		[APGenProperty("views")]
		public APGenTableCollection Views
		{
			get { return (APGenTableCollection)base[viewsProp]; }
		}


		/// <summary>
		/// All table relations in business mode.
		/// </summary>
		[APGenProperty("relations")]
		public APGenRelationCollection Relations
		{
			get { return (APGenRelationCollection)base[relationsProp]; }
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
