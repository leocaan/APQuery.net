
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Represents a collection of KeyValueAPGenElement objects.
	/// </summary>
	[APGenCollectionAttribute(typeof(KeyValueAPGenElement))]
	public sealed class KeyValueAPGenElementCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static KeyValueAPGenElementCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets all the keys in the KeyValueAPGenElementCollection.
		/// </summary>
		public string[] AllKeys
		{
			get
			{
				string[] keys = new string[Count];
				int i = 0;
				foreach (KeyValueAPGenElement kv in this)
					keys[i++] = kv.Key;
				return keys;
			}
		}


		/// <summary>
		/// Gets or sets the entry with the specified key in the KeyValueAPGenElementCollection.
		/// </summary>
		/// <param name="key">The String key of the entry to locate.</param>
		/// <returns>A KeyValueAPGenElement object associated with the specified key</returns>
		public new KeyValueAPGenElement this[string key]
		{
			get { return (KeyValueAPGenElement)BaseGet(key); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Adds an entry with the specified KeyValueAPGenElement.
		/// </summary>
		/// <param name="keyValue">The specified KeyValueAPGenElement.</param>
		public void Add(KeyValueAPGenElement keyValue)
		{
			BaseAdd(keyValue, false);
		}


		/// <summary>
		/// Adds an entry with the specified name and value to the KeyValueAPGenElementCollection.
		/// </summary>
		/// <param name="key">The String key of the entry to add.</param>
		/// <param name="value">The String value of the entry to add.</param>
		public void Add(string key, string value)
		{
			Add(new KeyValueAPGenElement(key, value));
		}


		/// <summary>
		/// Removes all entries from the KeyValueAPGenElementCollection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Removes the entries with the specified key from the KeyValueAPGenElementCollection instance.
		/// </summary>
		/// <param name="name">The String key of the entry to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		#endregion


		#region [ Override Implementation of APGenElementCollection ]


		/// <summary>
		/// Create a new configuration element.
		/// </summary>
		/// <returns>The created configuration element</returns>
		protected override APGenElement CreateNewElement()
		{
			return new KeyValueAPGenElement();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((KeyValueAPGenElement)element).Key;
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal override APGenPropertyCollection Properties
		{
			get { return properties; }
		}

		/// <summary>
		/// whether to throw an exception when duplicate name found.
		/// </summary>
		protected override bool ThrowOnDuplicate
		{
			get { return false; }
		}


		#endregion

	}
}
