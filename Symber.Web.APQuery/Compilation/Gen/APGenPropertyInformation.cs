using System;
using System.ComponentModel;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Contains meta-information on an individual property within the configuration.
	/// This type cannot be inherited.
	/// </summary>
	public sealed class APGenPropertyInformation
	{

		#region [ Fields ]


		private int _lineNumber;
		private string _source;
		private object _value;
		private APGenPropertyValueOrigin _origin;
		private readonly APGenElement _owner;
		private readonly APGenProperty _property;


		#endregion


		#region [ Constructors ]


		internal APGenPropertyInformation(APGenElement owner, APGenProperty property)
		{
			_owner = owner;
			_property = property;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets the TypeConverter object related to the configuration attribute.
		/// </summary>
		public TypeConverter Converter
		{
			get { return _property.Converter; }
		}


		/// <summary>
		/// Gets an object containing the default value related to a configuration attribute.
		/// </summary>
		public object DefaultValue
		{
			get { return _property.DefaultValue; }
		}


		/// <summary>
		/// Gets the description of the object that corresponds to a configuration attribute.
		/// </summary>
		public string Description
		{
			get { return _property.Description; }
		}


		/// <summary>
		/// Gets a value specifying whether the configuration attribute is a key.
		/// </summary>
		public bool IsKey
		{
			get { return _property.IsKey; }
		}


		/// <summary>
		/// Gets a value specifying whether the configuration attribute is required.
		/// </summary>
		public bool IsRequired
		{
			get { return _property.IsRequired; }
		}


		/// <summary>
		/// Gets the line number in the configuration file related to the configuration attribute.
		/// </summary>
		public int LineNumber
		{
			get { return _lineNumber; }
			internal set { _lineNumber = value; }
		}


		/// <summary>
		/// Gets the source file that corresponds to a configuration attribute.
		/// </summary>
		public string Source
		{
			get { return _source; }
			internal set { _source = value; }
		}


		/// <summary>
		/// Gets the name of the object that corresponds to a configuration attribute.
		/// </summary>
		public string Name
		{
			get { return _property.Name; }
		}


		/// <summary>
		/// Gets the Type of the object that corresponds to a configuration attribute.
		/// </summary>
		public Type Type
		{
			get { return _property.Type; }
		}


		/// <summary>
		/// Gets a APValidatorBase object related to the configuration attribute.
		/// </summary>
		public APValidatorBase Validator
		{
			get { return _property.Validator; }
		}


		/// <summary>
		/// Gets or sets an object containing the value related to a configuration attribute.
		/// </summary>
		public object Value
		{
			get {
				if (_origin == APGenPropertyValueOrigin.Default)
				{
					if (_property.IsElement)
					{
						APGenElement element = (APGenElement)Activator.CreateInstance(Type, true);
						element.InitFromProperty(this);
						_value = element;
						_origin = APGenPropertyValueOrigin.Inherited;
					}
					else
					{
						return DefaultValue;
					}
				}
				return _value;
			}
			set
			{
				_value = value;
				_origin = APGenPropertyValueOrigin.SetHere;
			}
		}


		/// <summary>
		/// Gets a PropertyValueOrigin object related to the configuration attribute.
		/// </summary>
		public APGenPropertyValueOrigin ValueOrigin
		{
			get { return _origin; }
		}


		#endregion


		#region [ Internal Properties ]


		internal APGenProperty Property
		{
			get { return _property; }
		}


		internal bool IsElement
		{
			get { return _property.IsElement; }
		}


		#endregion


		#region [ Internal Methods ]


		internal string GetStringValue()
		{
			return _property.ConvertToString(Value);
		}


		internal void SetStringValue(string value)
		{
			_value = _property.ConvertFromString(value);
			if (!object.Equals(_value, DefaultValue))
				_origin = APGenPropertyValueOrigin.SetHere;
		}


		#endregion

	}
}
