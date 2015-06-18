
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configurates enums in APGen.
	/// </summary>
	public sealed partial class APGenEnumsSection : APGenSection
	{

		#region [ Static Fields ]


		private static APGenProperty enabledProp;
		private static APGenProperty namespaceProp;
		private static APGenProperty enumsProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenEnumsSection()
		{
			enabledProp = new APGenProperty("enabled", typeof(bool), true);
			namespaceProp = new APGenProperty("namespace", typeof(string), null);
			enumsProp = new APGenProperty(
				"", 
				typeof(APGenEnumCollection), 
				null, 
				null, 
				null, 
				APGenPropertyOptions.IsDefaultCollection
				);


			properties = new APGenPropertyCollection();
			properties.Add(enabledProp);
			properties.Add(enumsProp);
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Whether the section is enabled.
		/// </summary>
		[APGenProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get { return (bool)base[enabledProp]; }
			set { base[enabledProp] = value; }
		}


		/// <summary>
		/// Namespace of the enumerates.
		/// </summary>
		[APGenProperty("namespace", DefaultValue = null)]
		public string Namespace
		{
			get { return (string)base[namespaceProp]; }
			set { base[namespaceProp] = value; }
		}


		/// <summary>
		/// Enumerates collection.
		/// </summary>
		[APGenProperty("", Options = APGenPropertyOptions.IsDefaultCollection)]
		public APGenEnumCollection Enums
		{
			get { return (APGenEnumCollection)base[enumsProp]; }
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
