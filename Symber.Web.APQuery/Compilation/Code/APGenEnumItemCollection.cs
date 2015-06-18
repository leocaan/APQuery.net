
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of enumerate item configs.
	/// </summary>
	[APGenCollection(typeof(APGenEnumItem))]
	public sealed class APGenEnumItemCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenEnumItemCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a enumerate item config by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified enumerate item config.</returns>
		public APGenEnumItem this[int index]
		{
			get { return (APGenEnumItem)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a enumerate item config by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified enumerate item config.</returns>
		public new APGenEnumItem this[string name]
		{
			get { return (APGenEnumItem)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a enumerate item config to collection.
		/// </summary>
		/// <param name="enumItem">The enumerate item config to add.</param>
		public void Add(APGenEnumItem enumItem)
		{
			BaseAdd(enumItem);
		}


		/// <summary>
		/// Clear all enumerate item config from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a enumerate item config by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a enumerate item config by index.
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
			return new APGenEnumItem();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenEnumItem)element).Name;
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
