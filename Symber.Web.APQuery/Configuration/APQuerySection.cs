using System.ComponentModel;
using System.Configuration;

namespace Symber.Web.Configuration
{
	/// <summary>
	/// Configures APQuery for Symber.
	/// </summary>
	public sealed class APQuerySection : ConfigurationSection
	{

		#region [ Static Fields ]


		private static ConfigurationProperty defaultProviderProp;
		private static ConfigurationProperty providersProp;
		private static ConfigurationPropertyCollection properties;


		#endregion


		#region [ Constructor ]


		static APQuerySection()
		{
			defaultProviderProp = new ConfigurationProperty(
				"defaultProvider", 
				typeof(string), 
				"SqlAPDalProvider", 
				TypeDescriptor.GetConverter(typeof(string)), 
				APPropertyHelper.NonEmptyStringValidator, 
				ConfigurationPropertyOptions.None);
			providersProp = new ConfigurationProperty(
				"providers",
				typeof(ProviderSettingsCollection),
				null, 
				null, 
				null, 
				ConfigurationPropertyOptions.None);
			
			properties = new ConfigurationPropertyCollection();
			properties.Add(defaultProviderProp);
			properties.Add(providersProp);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Default APDal provider.
		/// </summary>
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "SqlAPDalProvider")]
		public string DefaultProvider
		{
			get { return (string)base[defaultProviderProp]; }
			set { base[defaultProviderProp] = value; }
		}


		/// <summary>
		/// All APDal providers.
		/// </summary>
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get { return (ProviderSettingsCollection)base[providersProp]; }
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected override ConfigurationPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion

	}
}
