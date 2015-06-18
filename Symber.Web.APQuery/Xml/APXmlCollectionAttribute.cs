using System;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Declaratively instructs the .NET Framework to instantiate a xml element collection.
	/// This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class APXmlCollectionAttribute : Attribute
	{

		#region [ Fields ]


		private string _addItemName = "add";
		private Type _itemType;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of the APXmlCollectionAttribute class.
		/// </summary>
		/// <param name="itemType">Type of item.</param>
		public APXmlCollectionAttribute(Type itemType)
		{
			_itemType = itemType;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets the name of the add element.
		/// </summary>
		public string AddItemName
		{
			get { return _addItemName; }
			set { _addItemName = value; }
		}


		/// <summary>
		/// Gets the type of the collection element.
		/// </summary>
		public Type ItemType
		{
			get { return _itemType; }
		}


		#endregion

	}

}
