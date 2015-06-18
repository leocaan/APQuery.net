
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of namespace configs.
	/// </summary>
	[APGenCollection(typeof(APGenNamespace))]
	public sealed class APGenNamespaceCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenNamespaceCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a namespace config config by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified namespace config.</returns>
		public APGenNamespace this[int index]
		{
			get { return (APGenNamespace)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a namespace config config by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified namespace config.</returns>
		public new APGenNamespace this[string name]
		{
			get { return (APGenNamespace)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a namespace config to collection.
		/// </summary>
		/// <param name="namespace_">The namespace config to add.</param>
		public void Add(APGenNamespace namespace_)
		{
			BaseAdd(namespace_);
		}


		/// <summary>
		/// Clear all namespace config from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a namespace config by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a namespace config by index.
		/// </summary>
		/// <param name="index">The index to remove</param>
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
			return new APGenNamespace();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenNamespace)element).Import;
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
