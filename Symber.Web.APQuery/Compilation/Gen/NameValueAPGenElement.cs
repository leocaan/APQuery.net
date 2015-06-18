
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Name value generate element.
	/// </summary>
	public sealed class NameValueAPGenElement : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty _nameProp;
		private static APGenProperty _valueProp;
		private static APGenPropertyCollection _properties;


		#endregion


		#region [ Constructors ]


		static NameValueAPGenElement()
		{
			_nameProp = new APGenProperty("name", typeof(string), "", APGenPropertyOptions.IsKey);
			_valueProp = new APGenProperty("value", typeof(string), "");

			_properties = new APGenPropertyCollection();
			_properties.Add(_nameProp);
			_properties.Add(_valueProp);
		}


		/// <summary>
		/// Create a new name value generate element.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		public NameValueAPGenElement(string name, string value)
		{
			this[_nameProp] = name;
			this[_valueProp] = value;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Name.
		/// </summary>
		[APGenProperty("name", DefaultValue = "", Options = APGenPropertyOptions.IsKey)]
		public string Name
		{
			get { return (string)this[_nameProp]; }
		}


		/// <summary>
		/// Value.
		/// </summary>
		[APGenProperty("value", DefaultValue = "")]
		public string Value
		{
			get { return (string)this[_valueProp]; }
			set { this[_valueProp] = value; }
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APGenPropertyCollection Properties
		{
			get { return _properties; }
		}


		#endregion

	}
}
