using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Collection attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class APGenCollectionAttribute : Attribute
	{

		#region [ Fields ]


		private string _addItemName = "add";
		private Type _itemType;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new collection attribute.
		/// </summary>
		/// <param name="itemType">Item type.</param>
		public APGenCollectionAttribute(Type itemType)
		{
			_itemType = itemType;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Add item name.
		/// </summary>
		public string AddItemName
		{
			get { return _addItemName; }
			set { _addItemName = value; }
		}


		/// <summary>
		/// Item type.
		/// </summary>
		public Type ItemType
		{
			get { return _itemType; }
		}


		#endregion

	}
}
