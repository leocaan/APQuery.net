using Symber.Web.Compilation;
using System;
using System.Collections.Specialized;
using System.Configuration.Provider;

namespace Symber.Web.Data
{

	/// <summary>
	/// Dal provider helper.
	/// </summary>
	public static class APDalProviderHelper
	{

		/// <summary>
		/// Get instance of APDalProvider from gen settings.
		/// </summary>
		/// <param name="gen">The gen settings.</param>
		/// <returns>The instance of APDalProvider.</returns>
		public static APDalProvider InstantiateProvider(APGenDalProvider gen)
		{
			return InstantiateProvider(gen.Name, gen.Type, gen.ConnectionString, gen.ProviderName);
		}


		/// <summary>
		/// Get instance of APDalProvider from settigns.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="providerName">The provider name.</param>
		/// <returns>The instance of APDalProvider.</returns>
		public static APDalProvider InstantiateProvider(string name, string type, string connectionString, string providerName)
		{
			Type settingsType = null;

			if (settingsType == null)
				settingsType = APTypeHelper.LoadType(type);
			if (settingsType == null)
				throw new ProviderException(String.Format("Can not load type '{0}'.", type));

			APDalProvider provider = Activator.CreateInstance(settingsType) as APDalProvider;
			if (provider == null)
				throw new ProviderException(String.Format("Can not create instance of type '{0}'.", type));

			NameValueCollection config = new NameValueCollection()
			{
				{ "connectionString", connectionString },
				{ "providerName", providerName }
			};
			provider.Initialize(name, config);

			return provider;
		}

	}

}
