
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configurates namespaces in APGen.
	/// </summary>
	public sealed class APGenNamespace : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty importProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenNamespace()
		{
			importProp = new APGenProperty(
				"import", 
				typeof(string), 
				null, 
				APCVHelper.WhiteSpaceTrimStringConverter, 
				APCVHelper.NonEmptyStringValidator, 
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);

			properties = new APGenPropertyCollection();
			properties.Add(importProp);
		}


		/// <summary>
		/// Create a new namespace config.
		/// </summary>
		public APGenNamespace()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Value.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("import", IsRequired = true, IsKey = true)]
		public string Import
		{
			get { return (string)base[importProp]; }
			set { base[importProp] = value; }
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
