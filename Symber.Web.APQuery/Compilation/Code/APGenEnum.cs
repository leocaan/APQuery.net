
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configurates enumerates.
	/// </summary>
	public sealed class APGenEnum : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty enabledProp;
		private static APGenProperty isFlagsProp;
		private static APGenProperty commentProp;
		private static APGenProperty enumItemsProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenEnum()
		{
			nameProp = new APGenProperty(
				"name", 
				typeof(string), 
				null, 
				APCVHelper.WhiteSpaceTrimStringConverter, 
				APCVHelper.PropertyNameValidator, 
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			enabledProp = new APGenProperty("enabled", typeof(bool), true);
			isFlagsProp = new APGenProperty("isFlags", typeof(bool), false);
			commentProp = new APGenProperty("comment", typeof(string));
			enumItemsProp = new APGenProperty("", typeof(APGenEnumItemCollection), null, null, null, APGenPropertyOptions.IsDefaultCollection);

			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(enabledProp);
			properties.Add(isFlagsProp);
			properties.Add(commentProp);
			properties.Add(enumItemsProp);
		}


		/// <summary>
		/// Create a new enumerate config.
		/// </summary>
		public APGenEnum()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Name.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// Whether the enumerate config is enabled.
		/// </summary>
		[APGenProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get { return (bool)base[enabledProp]; }
			set { base[enabledProp] = value; }
		}


		/// <summary>
		/// Whether the enumerate has a flags attribute.
		/// </summary>
		[APGenProperty("isFlags", DefaultValue = false)]
		public bool IsFlags
		{
			get { return (bool)base[isFlagsProp]; }
			set { base[isFlagsProp] = value; }
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
		/// Enumerate items.
		/// </summary>
		[APGenProperty("", Options = APGenPropertyOptions.IsDefaultCollection)]
		public APGenEnumItemCollection EnumItems
		{
			get { return (APGenEnumItemCollection)base[enumItemsProp]; }
		}


		/// <summary>
		/// Properties.
		/// </summary>
		protected internal override APGenPropertyCollection Properties
		{
			get { return properties; }
		}


		#endregion

	}
}
