
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of APGen column definition.
	/// </summary>
	[APGenCollection(typeof(APGenColumn))]
	public sealed class APGenColumnCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenColumnCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a column definition by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified column definition.</returns>
		public APGenColumn this[int index]
		{
			get { return (APGenColumn)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a column definition by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified column definition.</returns>
		public new APGenColumn this[string name]
		{
			get { return (APGenColumn)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a column definition to collection.
		/// </summary>
		/// <param name="column">The column definition to add.</param>
		public void Add(APGenColumn column)
		{
			BaseAdd(column);
		}


		/// <summary>
		/// Clear all column definition from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a column definition by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a column definition by index.
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
			return new APGenColumn();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenColumn)element).Name;
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
