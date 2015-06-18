
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of enumerate configs.
	/// </summary>
	[APGenCollection(typeof(APGenEnum), AddItemName = "enum")]
	public sealed class APGenEnumCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenEnumCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a enumerate config by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified enumerate config.</returns>
		public APGenEnum this[int index]
		{
			get { return (APGenEnum)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a enumerate config by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified enumerate config.</returns>
		public new APGenEnum this[string name]
		{
			get { return (APGenEnum)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a enumerate config to collection.
		/// </summary>
		/// <param name="aEnum">The enumerate config to add.</param>
		public void Add(APGenEnum aEnum)
		{
			BaseAdd(aEnum);
		}


		/// <summary>
		/// Clear all enumerate config from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a enumerate config by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a enumerate config by index.
		/// </summary>
		/// <param name="index">The index to remove.</param>
		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}


		#endregion


		#region [ Override Implementation of APGenElementCollection ]


		/// <summary>
		/// Create a new configuration element.
		/// </summary>
		/// <returns>The created configuration element.</returns>
		protected override APGenElement CreateNewElement()
		{
			return new APGenEnum();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenEnum)element).Name;
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
