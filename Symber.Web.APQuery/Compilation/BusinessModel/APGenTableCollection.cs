
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of table definition.
	/// </summary>
	[APGenCollection(typeof(APGenTable), AddItemName = "table")]
	public sealed class APGenTableCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenTableCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a table definition by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified table definition.</returns>
		public APGenTable this[int index]
		{
			get { return (APGenTable)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a table definition by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified table definition.</returns>
		public new APGenTable this[string name]
		{
			get { return (APGenTable)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a table definition to collection.
		/// </summary>
		/// <param name="table">The table definition to add.</param>
		public void Add(APGenTable table)
		{
			BaseAdd(table);
		}


		/// <summary>
		/// Clear all table definition from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a table definition by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a table definition by index.
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
			return new APGenTable();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenTable)element).Name;
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
