using System;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Declaratively instructs the .NET Framework to instantiate a xml property. This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class APXmlPropertyAttribute : Attribute
	{

		#region [ Fields ]


		private string _name;
		private object _defaultValue = APXmlProperty.NoDefaultValue;
		private APXmlPropertyOptions _flags;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of APXmlPropertyAttribute class.
		/// </summary>
		/// <param name="name">Name.</param>
		public APXmlPropertyAttribute(string name)
		{
			_name = name;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a value indicating whether this is the default property collection for the decorated xml property.
		/// </summary>
		public bool IsDefaultCollection
		{
			get { return (_flags & APXmlPropertyOptions.IsDefaultCollection) != 0; }
			set
			{
				if (value)
					_flags |= APXmlPropertyOptions.IsDefaultCollection;
				else
					_flags &= ~APXmlPropertyOptions.IsDefaultCollection;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether this is a key property for the decorated element property.
		/// </summary>
		public bool IsKey
		{
			get { return (_flags & APXmlPropertyOptions.IsKey) != 0; }
			set
			{
				if (value)
					_flags |= APXmlPropertyOptions.IsKey;
				else
					_flags &= ~APXmlPropertyOptions.IsKey;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the decorated element property is required.
		/// </summary>
		public bool IsRequired
		{
			get { return (_flags & APXmlPropertyOptions.IsRequired) != 0; }
			set
			{
				if (value)
					_flags |= APXmlPropertyOptions.IsRequired;
				else
					_flags &= ~APXmlPropertyOptions.IsRequired;
			}
		}


		/// <summary>
		/// Gets or sets the name of the decorated xml-element property.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}


		/// <summary>
		/// Gets or sets the default value for the decorated property.
		/// </summary>
		public object DefaultValue
		{
			get { return _defaultValue; }
			set { _defaultValue = value; }
		}


		/// <summary>
		/// Gets or sets the APXmlPropertyOptions for the decorated xml-element property.
		/// </summary>
		public APXmlPropertyOptions Options
		{
			get { return _flags; }
			set { _flags = value; }
		}


		#endregion

	}

}
