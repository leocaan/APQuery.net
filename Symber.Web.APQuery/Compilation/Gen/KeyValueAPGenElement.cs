
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Key value generate element.
	/// </summary>
	public sealed class KeyValueAPGenElement : APGenElement
	{

		#region [ Static Fields ]


		private static APGenProperty _keyProp;
		private static APGenProperty _valueProp;
		private static APGenPropertyCollection _properties;


		#endregion


		#region [ Constructors ]


		static KeyValueAPGenElement()
		{
			_keyProp = new APGenProperty("key	", typeof(string), "", APGenPropertyOptions.IsKey);
			_valueProp = new APGenProperty("value", typeof(string), "");

			_properties = new APGenPropertyCollection();
			_properties.Add(_keyProp);
			_properties.Add(_valueProp);
		}


		internal KeyValueAPGenElement()
		{
		}


		/// <summary>
		/// Create a new key value generate element.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public KeyValueAPGenElement(string key, string value)
		{
			this[_keyProp] = key;
			this[_valueProp] = value;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Key.
		/// </summary>
		[APGenProperty("key", DefaultValue = "", Options = APGenPropertyOptions.IsKey)]
		public string Key
		{
			get { return (string)this[_keyProp]; }
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
