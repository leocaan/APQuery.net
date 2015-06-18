
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of index order information.
	/// </summary>
	[APGenCollection(typeof(APGenOrder))]
	public sealed class APGenOrderCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenOrderCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a index order information by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified index order information.</returns>
		public APGenOrder this[int index]
		{
			get { return (APGenOrder)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a index order information by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified index order information.</returns>
		public new APGenOrder this[string name]
		{
			get { return (APGenOrder)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a index order information to collection.
		/// </summary>
		/// <param name="order">The index order information to add.</param>
		public void Add(APGenOrder order)
		{
			BaseAdd(order);
		}


		/// <summary>
		/// Clear all index order information from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a index order information by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a index order information by index.
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
			return new APGenOrder();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenOrder)element).Name;
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
