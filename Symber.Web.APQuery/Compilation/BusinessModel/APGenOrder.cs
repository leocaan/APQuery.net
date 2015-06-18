using Symber.Web.Data;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Database index order information in business mode.
	/// </summary>
	public sealed class APGenOrder : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty nameProp;
		private static APGenProperty accordingProp;
		private static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenOrder()
		{
			nameProp = new APGenProperty(
				"name",
				typeof(string),
				null,
				APCVHelper.WhiteSpaceTrimStringConverter,
				APCVHelper.NonEmptyStringValidator,
				APGenPropertyOptions.IsRequired | APGenPropertyOptions.IsKey
				);
			accordingProp = new APGenProperty(
				"according",
				typeof(APSqlOrderAccording),
				APSqlOrderAccording.Asc,
				new GenericEnumAPConverter(typeof(APSqlOrderAccording)),
				APCVHelper.DefaultValidator,
				APGenPropertyOptions.None
				);


			properties = new APGenPropertyCollection();
			properties.Add(nameProp);
			properties.Add(accordingProp);
		}


		/// <summary>
		/// Create a new index order.
		/// </summary>
		public APGenOrder()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Column name.
		/// </summary>
		[StringAPValidator(MinLength = 1)]
		[APGenProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)base[nameProp]; }
			set { base[nameProp] = value; }
		}


		/// <summary>
		/// According type.
		/// </summary>
		[APGenProperty("according", DefaultValue = APSqlOrderAccording.Asc)]
		public APSqlOrderAccording According
		{
			get { return (APSqlOrderAccording)base[accordingProp]; }
			set { base[accordingProp] = value; }
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
