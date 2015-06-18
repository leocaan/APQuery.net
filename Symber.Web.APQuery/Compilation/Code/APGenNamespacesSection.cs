
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configurates namespaces in APGen.
	/// </summary>
	public sealed partial class APGenNamespacesSection : APGenSection
	{

		#region [ Static Fields ]


		private static APGenProperty namespacesProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenNamespacesSection()
		{
			namespacesProp = new APGenProperty("", typeof(APGenNamespaceCollection), null, null, null, APGenPropertyOptions.IsDefaultCollection);

			properties = new APGenPropertyCollection();
			properties.Add(namespacesProp);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Collection of namespaces config.
		/// </summary>
		[APGenProperty("", Options = APGenPropertyOptions.IsDefaultCollection)]
		public APGenNamespaceCollection Namespaces
		{
			get { return (APGenNamespaceCollection)base[namespacesProp]; }
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
