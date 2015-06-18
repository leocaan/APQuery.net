
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection of database relation definition.
	/// </summary>
	[APGenCollection(typeof(APGenRelation))]
	public sealed class APGenRelationCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static APGenRelationCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a relation definition by index.
		/// </summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The specified relation definition.</returns>
		public APGenRelation this[int index]
		{
			get { return (APGenRelation)BaseGet(index); }
			set { if (BaseGet(index) != null) BaseRemoveAt(index); BaseAdd(index, value); }
		}


		/// <summary>
		/// Gets or sets a relation definition by name.
		/// </summary>
		/// <param name="name">The name to access.</param>
		/// <returns>The specified relation definition.</returns>
		public new APGenRelation this[string name]
		{
			get { return (APGenRelation)BaseGet(name); }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a relation definition to collection.
		/// </summary>
		/// <param name="relation">The relation definition to add.</param>
		public void Add(APGenRelation relation)
		{
			BaseAdd(relation);
		}


		/// <summary>
		/// Clear all relation definition from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Remove a relation definition by name.
		/// </summary>
		/// <param name="name">The name to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}


		/// <summary>
		/// Remove a relation definition by index.
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
			return new APGenRelation();
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((APGenRelation)element).Name;
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
