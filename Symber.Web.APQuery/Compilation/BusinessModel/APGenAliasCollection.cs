
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of APGen alias definition.
	/// </summary>
	[APGenCollection(typeof(APGenAlias))]
	public sealed class APGenAliasCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenAliasCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a alias definition by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified alias definition.</returns>
		public APGenAlias this[int index]
		{
			get { return (APGenAlias)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a alias definition by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified alias definition.</returns>
		public new APGenAlias this[string name]
		{
			get { return (APGenAlias)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a alias definition to collection.
		/// </summary>
		/// <param name="alias">The alias definition to add.</param>
		public void Add(APGenAlias alias)
		{
			BaseAdd(alias);
		}


		/// <summary>
		/// Clear all alias definition from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a alias definition by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a alias definition by index.
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
			return new APGenAlias();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenAlias)element).Name;
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
