using System;
using System.Collections;
using System.Xml;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Represents a xml element within a xml file.
	/// </summary>
	public abstract class APXmlElement
	{

		#region [ Fields ]


		private string _rawXml;
		private APXmlElementMap _map;
		private APXmlPropertyCollection _keyProps;
		private APXmlElementCollection _defaultCollection;
		private APXmlElementInformation _elementInfo;
		private APXmlElementProperty _elementProperty;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Creates a new instance of the APXmlElement class.
		/// </summary>
		protected APXmlElement()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets an ElementInformation object that contains the non-customizable information and functionality
		/// of the APXmlElement object.
		/// </summary>
		public APXmlElementInformation ElementInformation
		{
			get
			{
				if (_elementInfo == null)
					_elementInfo = new APXmlElementInformation(this, null);
				return _elementInfo;
			}
		}


		#endregion


		#region [ Internal Properties ]


		internal string RawXml
		{
			get { return _rawXml; }
			set { if (_rawXml == null || value != null) _rawXml = value; }
		}


		#endregion


		#region [ Internal Methods ]


		internal virtual void InitFromProperty(APXmlPropertyInformation propertyInfo)
		{
			_elementInfo = new APXmlElementInformation(this, propertyInfo);
			Init();
		}


		internal APXmlPropertyCollection GetKeyProperties()
		{
			if (_keyProps != null)
				return _keyProps;

			APXmlPropertyCollection tmpkeyProps = new APXmlPropertyCollection();

			foreach (APXmlProperty prop in Properties)
			{
				if (prop.IsKey)
					tmpkeyProps.Add(prop);
			}

			return _keyProps = tmpkeyProps;
		}


		internal APXmlElementCollection GetDefaultCollection()
		{
			if (_defaultCollection != null)
				return _defaultCollection;

			APXmlProperty defaultCollectionProp = null;

			foreach (APXmlProperty prop in Properties)
			{
				if (prop.IsDefaultCollection)
				{
					defaultCollectionProp = prop;
					break;
				}
			}

			if (defaultCollectionProp != null)
				_defaultCollection = this[defaultCollectionProp] as APXmlElementCollection;
			return _defaultCollection;
		}


		internal virtual bool HasValues()
		{
			foreach (APXmlPropertyInformation pi in ElementInformation.Properties)
			{
				if (pi.ValueOrigin != APXmlPropertyValueOrigin.Default)
					return true;
			}
			return false;
		}


		#endregion


		#region [ Protected Properties ]


		/// <summary>
		/// Gets the APXmlElementProperty object that represents the APXmlElement object itself.
		/// </summary>
		protected internal virtual APXmlElementProperty ElementProperty
		{
			get
			{
				if (_elementProperty == null)
					_elementProperty = new APXmlElementProperty(ElementInformation.Validator);
				return _elementProperty;
			}
		}


		/// <summary>
		/// Gets or sets a property or attribute of this element.
		/// </summary>
		/// <param name="property">The property to access.</param>
		/// <returns>The specified property, attribute, or child element.</returns>
		protected internal object this[APXmlProperty property]
		{
			get { return this[property.Name]; }
			set { this[property.Name] = value; }
		}


		/// <summary>
		/// Gets or sets a property, attribute, or child element of this element.
		/// </summary>
		/// <param name="propertyName">The name of the APXmlElementProperty to access.</param>
		/// <returns>The specified property, attribute, or child element.</returns>
		protected internal object this[string propertyName]
		{
			get
			{
				APXmlPropertyInformation property = ElementInformation.Properties[propertyName];
				if (property == null)
					throw new InvalidOperationException(APResource.GetString(APResource.APXml_PropertyNotExist, propertyName));
				return property.Value;
			}
			set
			{
				APXmlPropertyInformation property = ElementInformation.Properties[propertyName];
				if (property == null)
					throw new InvalidOperationException(APResource.GetString(APResource.APXml_PropertyNotExist, propertyName));

				SetPropertyValue(property.Property, value);

				property.Value = value;
			}
		}


		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		protected internal virtual APXmlPropertyCollection Properties
		{
			get
			{
				if (_map == null)
					_map = APXmlElementMap.GetMap(GetType());
				return _map.Properties;
			}
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Sets the APXmlElement object to its initial state.
		/// </summary>
		protected internal virtual void Init()
		{
		}


		/// <summary>
		/// Sets a property to the specified value.
		/// </summary>
		/// <param name="prop">The element property to set.</param>
		/// <param name="value">The value to assign to the property.</param>
		protected void SetPropertyValue(APXmlProperty prop, object value)
		{
			try
			{
				prop.Validate(value);
			}
			catch (Exception e)
			{
				throw new APXmlException(APResource.GetString(APResource.APXml_PropertyValueInvalid, prop.Name, e.Message), e);
			}
		}


		/// <summary>
		/// Reads XML from the xml file.
		/// </summary>
		/// <param name="reader">The XmlReader that reads from the xml file.</param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			Hashtable readProps = new Hashtable();

			reader.MoveToContent();

			while (reader.MoveToNextAttribute())
			{
				APXmlPropertyInformation prop = ElementInformation.Properties[reader.LocalName];
				if (prop == null || (serializeCollectionKey && !prop.IsKey))
				{
					if (reader.LocalName == "xmlns")
					{
						// Ignore
					}
					else if (!OnDeserializeUnrecognizedAttribute(reader.LocalName, reader.Value))
					{
						throw new APXmlException(APResource.GetString(APResource.APXml_UnrecognizedAttribute, reader.LocalName));
					}

					continue;
				}

				if (readProps.ContainsKey(prop))
					throw new APXmlException(APResource.GetString(APResource.APXml_DuplicateAttribute, prop.Name));

				string value = null;
				try
				{
					value = reader.Value;
					ValidateValue(prop.Property, value);
					prop.SetStringValue(value);
				}
				catch (APXmlException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new APXmlException(APResource.GetString(APResource.APXml_PropertyCannotBeParsed, prop.Name), ex, reader);
				}
				readProps[prop] = prop.Name;
			}

			reader.MoveToElement();

			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				int depth = reader.Depth;

				reader.ReadStartElement();
				reader.MoveToContent();

				do
				{
					if (reader.NodeType != XmlNodeType.Element)
					{
						reader.Skip();
						continue;
					}

					APXmlPropertyInformation prop = ElementInformation.Properties[reader.LocalName];
					if (prop == null || (serializeCollectionKey && !prop.IsKey))
					{
						if (!OnDeserializeUnrecognizedElement(reader.LocalName, reader))
						{
							if (prop == null)
							{
								APXmlElementCollection collection = GetDefaultCollection();
								if (collection != null && collection.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
									continue;
							}
							throw new APXmlException(APResource.GetString(APResource.APXml_UnrecognizedElement, reader.LocalName));
						}
						continue;
					}

					if (!prop.IsElement)
						throw new APXmlException(APResource.GetString(APResource.APXml_NotAElement, prop.Name));

					if (readProps.Contains(prop))
						throw new APXmlException(APResource.GetString(APResource.APXml_DuplicateElement, prop.Name));

					APXmlElement val = prop.Value as APXmlElement;
					val.DeserializeElement(reader, serializeCollectionKey);
					readProps[prop] = prop.Name;
				} while (depth < reader.Depth);

				if (reader.NodeType == XmlNodeType.EndElement)
					reader.Read();
			}

			foreach (APXmlPropertyInformation prop in ElementInformation.Properties)
			{
				if (!String.IsNullOrEmpty(prop.Name) && prop.IsRequired && !readProps.ContainsKey(prop))
				{
					APXmlPropertyInformation property = ElementInformation.Properties[prop.Name];
					if (property == null)
					{
						object val = OnRequiredPropertyNotFound(prop.Name);
						if (!object.Equals(val, prop.DefaultValue))
							prop.Value = val;
					}
				}
			}

			PostDeserialize();
		}


		/// <summary>
		/// Gets a value indicating whether an unknown attribute is encountered during deserialization.
		/// </summary>
		/// <param name="name">The name of the unrecognized attribute.</param>
		/// <param name="value">The value of the unrecognized attribute.</param>
		/// <returns>true when an unknown attribute is encountered while deserializing.</returns>
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}


		/// <summary>
		/// Gets a value indicating whether an unknown element is encountered during deserialization.
		/// </summary>
		/// <param name="element">The name of the unknown subelement.</param>
		/// <param name="reader">The XmlReader object being used for deserialization.</param>
		/// <returns>true when an unknown element is encountered while deserializing.</returns>
		protected virtual bool OnDeserializeUnrecognizedElement(string element, XmlReader reader)
		{
			return false;
		}


		/// <summary>
		/// Gets a value indicating whether a required property is not found.
		/// </summary>
		/// <param name="name">The name of the required attribute that was not found.</param>
		/// <returns>true when a required property is not found while deserializing.</returns>
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new APXmlException(APResource.GetString(APResource.APXml_RequiredAttribute, name));
		}


		/// <summary>
		/// Called before serialization.
		/// </summary>
		/// <param name="writer">The XmlWriter object that will be used to serialize the APXmlElement object.</param>
		protected virtual void PreSerialize(XmlWriter writer)
		{
		}


		/// <summary>
		/// Called after deserialization.
		/// </summary>
		protected virtual void PostDeserialize()
		{
		}


		/// <summary>
		/// Used to initialize a default set of values for the APXmlElement object.
		/// </summary>
		protected internal virtual void InitializeDefault()
		{
		}


		/// <summary>
		/// Writes the contents of this element to the file when implemented in a derived class.
		/// </summary>
		/// <param name="writer">The XmlWriter that writes to the file.</param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
		/// <returns>true if any data was actually serialized; otherwise, false.</returns>
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			PreSerialize(writer);

			if (serializeCollectionKey)
			{
				APXmlPropertyCollection props = GetKeyProperties();
				foreach (APXmlProperty prop in props)
					writer.WriteAttributeString(prop.Name, prop.ConvertToString(this[prop.Name]));
				return props.Count > 0;
			}

			bool wroteData = false;

			foreach (APXmlPropertyInformation prop in ElementInformation.Properties)
			{
				if (prop.IsElement || prop.ValueOrigin == APXmlPropertyValueOrigin.Default)
					continue;

				if (!object.Equals(prop.Value, prop.DefaultValue))
				{
					writer.WriteAttributeString(prop.Name, prop.GetStringValue());
					wroteData = true;
				}
			}

			foreach (APXmlPropertyInformation prop in ElementInformation.Properties)
			{
				if (!prop.IsElement)
					continue;

				APXmlElement val = prop.Value as APXmlElement;
				if (val != null)
					wroteData = val.SerializeToXmlElement(writer, prop.Name) || wroteData;
			}

			return wroteData;
		}


		/// <summary>
		/// Writes the outer tags of this element to the file when implemented in a derived class.
		/// </summary>
		/// <param name="writer">The XmlWriter that writes to the file.</param>
		/// <param name="elementName">The name of the APXmlElement to be written.</param>
		/// <returns>true if writing was successful.</returns>
		protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (!HasValues())
				return false;

			if (!String.IsNullOrEmpty(elementName))
				writer.WriteStartElement(elementName);

			bool res = SerializeElement(writer, false);

			if (!String.IsNullOrEmpty(elementName))
				writer.WriteEndElement();

			return res;
		}


		#endregion


		#region [ Private Methods ]


		private void ValidateValue(APXmlProperty property, string value)
		{
			APValidatorBase validator;

			if (property == null || (validator = property.Validator) == null)
				return;

			if (!validator.CanValidate(property.Type))
				throw new APXmlException(APResource.GetString(APResource.APXml_ValidatorNotSupportType, property.Type));
			validator.Validate(property.ConvertFromString(value));
		}


		#endregion


		#region [ Override Implementation of Object ]


		/// <summary>
		/// Determines whether two Object instances are equal.
		/// </summary>
		/// <param name="compareTo">The Object to compare with the current Object.</param>
		/// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
		public override bool Equals(object compareTo)
		{
			APXmlElement other = compareTo as APXmlElement;

			if (other == null)
				return false;
			
			if (GetType() != other.GetType())
				return false;

			foreach (APXmlProperty prop in Properties)
			{
				if (!object.Equals(this[prop], other[prop]))
					return false;
			}
			return true;
		}


		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current Object.</returns>
		public override int GetHashCode()
		{
			int code = 0;
			object obj;

			foreach (APXmlProperty prop in Properties)
			{
				obj = this[prop];
				if (obj == null)
					continue;
				code += obj.GetHashCode();
			}
			return code;
		}


		#endregion

	}

}
