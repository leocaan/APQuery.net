using System;
using System.Configuration.Provider;

namespace Symber.Web.Data
{

	/// <summary>
	/// A collection of DAL provider.
	/// </summary>
	public sealed class APDalProviderCollection : ProviderCollection
	{

		#region [ Methods ]


		/// <summary>
		/// Copy some dal providers to array.
		/// </summary>
		/// <param name="array">The array to copy to.</param>
		/// <param name="index">The first item in the collection to copy.</param>
		public void CopyTo(APDalProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}


		#endregion


		#region [ Override Implementation of ProviderCollection ]


		/// <summary>
		/// Gets a DAL provider in the collection by the specified DAL provider name.
		/// </summary>
		/// <param name="name">DAL provider name.</param>
		/// <returns>DAL provider data.</returns>
		public new APDalProvider this[string name]
		{
			get { return (APDalProvider)base[name]; }
		}


		/// <summary>
		/// Add a DAL provider to the collection.
		/// </summary>
		/// <param name="provider">DAL provider data.</param>
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");

			if (provider is APDalProvider)
				base.Add(provider);
			else
				throw new ArgumentException("provider");
		}


		#endregion

	}

}
