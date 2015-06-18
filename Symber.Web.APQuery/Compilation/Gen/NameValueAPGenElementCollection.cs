using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Represents a collection of NameValueAPGenElementCollection objects.
	/// </summary>
	[APGenCollectionAttribute(typeof(NameValueAPGenElement))]
	public sealed class NameValueAPGenElementCollection : APGenElementCollection
	{

		#region [ Static Fields ]


		static APGenPropertyCollection properties;


		#endregion


		#region [ Constructors ]


		static NameValueAPGenElementCollection()
		{
			properties = new APGenPropertyCollection();
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets all the keys in the NameValueAPGenElementCollection.
		/// </summary>
		public string[] AllKeys
		{
			get { return (string[])BaseGetAllKeys(); }
		}


		/// <summary>
		/// Gets or sets the entry with the specified key in the NameValueAPGenElementCollection.
		/// </summary>
		/// <param name="name">The String key of the entry to locate.</param>
		/// <returns>A NameValueAPGenElement object associated with the specified key</returns>
		public new NameValueAPGenElement this[string name]
		{
			get { return (NameValueAPGenElement)BaseGet(name); }
			set { throw new NotImplementedException(); }
		}


		#endregion 


		#region [ Methods ]


		/// <summary>
		/// Adds an entry with the specified NameValueAPGenElement.
		/// </summary>
		/// <param name="nameValue">The specified NameValueAPGenElement</param>
		public void Add(NameValueAPGenElement nameValue)
		{
			BaseAdd(nameValue, false);
		}


		/// <summary>
		/// Removes all entries from the NameValueAPGenElementCollection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}


		/// <summary>
		/// Removes the specified element from the NameValueAPGenElementCollection instance.
		/// </summary>
		/// <param name="nameValue">The specified element to remove.</param>
		public void Remove(NameValueAPGenElement nameValue)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// Removes the entries with the specified key from the NameValueAPGenElementCollection instance.
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
		/// <returns>The created configuration element.</returns>
		protected override APGenElement CreateNewElement()
		{
			return new NameValueAPGenElement("", "");
		}


		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The ConfigurationElement to return the key for.</param>
		/// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
		protected override object GetElementKey(APGenElement element)
		{
			return ((NameValueAPGenElement)element).Name;
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
