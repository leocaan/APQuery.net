
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of database index definition.
	/// </summary>
	[APGenCollection(typeof(APGenIndex), AddItemName = "index")]
	public sealed class APGenIndexCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenIndexCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a database index definition by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified database index definition.</returns>
		public APGenIndex this[int index]
		{
			get { return (APGenIndex)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a database index definition by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified database index definition.</returns>
		public new APGenIndex this[string name]
		{
			get { return (APGenIndex)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a database index definition to collection.
		/// </summary>
		/// <param name="index">The database index definition to add.</param>
		public void Add(APGenIndex index)
		{
			BaseAdd(index);
		}


		/// <summary>
		/// Clear all database index definition from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a database index definition by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a database index definition by index.
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
			return new APGenIndex();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenIndex)element).Name;
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
