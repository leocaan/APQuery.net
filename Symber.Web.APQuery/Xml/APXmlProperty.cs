using System;
using System.ComponentModel;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Represents an attribute or a child of a xml element. This class cannot be inherited.
	/// </summary>
	public sealed class APXmlProperty
	{

		#region [ Static Methods ]


		internal static readonly object NoDefaultValue = new object();


		#endregion


		#region [ Fields ]


		private string _name;
		private Type _type;
		private object _defaultValue;
		private TypeConverter _converter;
		private APValidatorBase _validation;
		private APXmlPropertyOptions _flags;
		private string _description;
		private APXmlCollectionAttribute _collectionAttribute;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Creates a new instance of the ConfigurationProperty class.
		/// </summary>
		/// <param name="name">The name of the xml entity.</param>
		/// <param name="type">The type of the xml entity.</param>
		public APXmlProperty(string name, Type type)
			: this(name, type, NoDefaultValue,
			TypeDescriptor.GetConverter(type), new DefaultAPValidator(), APXmlPropertyOptions.None, null)
		{
		}


		/// <summary>
		/// Creates a new instance of the ConfigurationProperty class.
		/// </summary>
		/// <param name="name">The name of the xml entity.</param>
		/// <param name="type">The type of the xml entity.</param>
		/// <param name="defaultValue">The default value of the xml entity.</param>
		public APXmlProperty(string name, Type type, object defaultValue)
			: this(name, type, defaultValue,
			TypeDescriptor.GetConverter(type), new DefaultAPValidator(), APXmlPropertyOptions.None, null)
		{
		}


		/// <summary>
		/// Creates a new instance of the ConfigurationProperty class.
		/// </summary>
		/// <param name="name">The name of the xml entity.</param>
		/// <param name="type">The type of the xml entity.</param>
		/// <param name="defaultValue">The default value of the xml entity.</param>
		/// <param name="flags">One of the APXmlPropertyOptions enumeration values.</param>
		public APXmlProperty(string name, Type type, object defaultValue, APXmlPropertyOptions flags)
			: this(name, type, defaultValue,
			TypeDescriptor.GetConverter(type), new DefaultAPValidator(), flags, null)
		{
		}


		/// <summary>
		/// Creates a new instance of the ConfigurationProperty class.
		/// </summary>
		/// <param name="name">The name of the xml entity.</param>
		/// <param name="type">The type of the xml entity.</param>
		/// <param name="defaultValue">The default value of the xml entity.</param>
		/// <param name="converter">The type of the converter to apply.</param>
		/// <param name="validation">The validator to use.</param>
		/// <param name="flags">One of the APXmlPropertyOptions enumeration values.</param>
		public APXmlProperty(string name, Type type, object defaultValue,
			TypeConverter converter, APValidatorBase validation, APXmlPropertyOptions flags)
			: this(name, type, defaultValue, converter, validation, flags, null)
		{
		}


		/// <summary>
		/// Creates a new instance of the ConfigurationProperty class.
		/// </summary>
		/// <param name="name">The name of the xml entity.</param>
		/// <param name="type">The type of the xml entity.</param>
		/// <param name="defaultValue">The default value of the xml entity.</param>
		/// <param name="converter">The type of the converter to apply.</param>
		/// <param name="validation">The validator to use.</param>
		/// <param name="flags">One of the APXmlPropertyOptions enumeration values.</param>
		/// <param name="description">The description of the xml entity.</param>
		public APXmlProperty(string name, Type type, object defaultValue,
			TypeConverter converter, APValidatorBase validation, APXmlPropertyOptions flags, string description)
		{
			_name = name;

			_converter = converter != null ? converter : TypeDescriptor.GetConverter(type);

			if (defaultValue != null)
			{
				if (defaultValue == NoDefaultValue)
				{
					switch (Type.GetTypeCode(type))
					{
						case TypeCode.Object:
							defaultValue = null;
							break;
						case TypeCode.String:
							defaultValue = String.Empty;
							break;
						default:
							defaultValue = Activator.CreateInstance(type);
							break;
					}
				}
				else
				{
					if (!type.IsAssignableFrom(defaultValue.GetType()))
					{
						if (!_converter.CanConvertFrom(defaultValue.GetType()))
							throw new APXmlException(APResource.GetString(APResource.APXml_DefaultValueTypeError, name, type, defaultValue.GetType()));
						defaultValue = _converter.ConvertFrom(defaultValue);
					}
				}
			}

			_defaultValue = defaultValue;
			_flags = flags;
			_type = type;
			_validation = validation != null ? validation : new DefaultAPValidator();
			_description = description;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets whether the property is the default collection of an element.
		/// </summary>
		public bool IsDefaultCollection
		{
			get { return (_flags & APXmlPropertyOptions.IsDefaultCollection) != 0; }
		}


		/// <summary>
		/// Gets a value indicating whether this APXmlProperty is the key for the containing APXmlElement object.
		/// </summary>
		public bool IsKey
		{
			get { return (_flags & APXmlPropertyOptions.IsKey) != 0; }
		}


		/// <summary>
		/// Gets a value indicating whether this APXmlProperty is required.
		/// </summary>
		public bool IsRequired
		{
			get { return (_flags & APXmlPropertyOptions.IsRequired) != 0; }
		}


		/// <summary>
		/// Gets the name of this APXmlProperty.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}


		/// <summary>
		/// Gets the type of this APXmlProperty object.
		/// </summary>
		public Type Type
		{
			get { return _type; }
		}


		/// <summary>
		/// Gets the default value for this APXmlProperty property.
		/// </summary>
		public object DefaultValue
		{
			get { return _defaultValue; }
		}


		/// <summary>
		/// Gets the TypeConverter used to convert this APXmlProperty into an XML representation for writing to the xml file.
		/// </summary>
		public TypeConverter Converter
		{
			get { return _converter; }
		}


		/// <summary>
		/// Gets the APValidatorAttribute, which is used to validate this APXmlProperty object.
		/// </summary>
		public APValidatorBase Validator
		{
			get { return _validation; }
		}


		/// <summary>
		/// Gets the description associated with the APXmlProperty.
		/// </summary>
		public string Description
		{
			get { return _description; }
		}


		#endregion


		#region [ Internal Properties ]


		internal bool IsElement
		{
			get { return (typeof(APXmlElement).IsAssignableFrom(_type)); }
		}


		internal APXmlCollectionAttribute CollectionAttribute
		{
			get { return _collectionAttribute; }
			set { _collectionAttribute = value; }
		}


		#endregion


		#region [ Internal Methods ]


		internal object ConvertFromString(string value)
		{
			if (_converter != null)
				return _converter.ConvertFromInvariantString(value);
			else
				throw new NotImplementedException();
		}


		internal string ConvertToString(object value)
		{
			if (_converter != null)
				return _converter.ConvertToInvariantString(value);
			else
				throw new NotImplementedException();
		}


		internal void Validate(object value)
		{
			if (_validation != null)
				_validation.Validate(value);
		}


		#endregion

	}

}
