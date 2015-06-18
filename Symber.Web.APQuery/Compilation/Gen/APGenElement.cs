using System;
using System.Collections;
using System.Xml;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Config element.
	/// </summary>
	public abstract class APGenElement
	{

		#region [ Fields ]


		private string _rawXml;
		private APGenElementMap _map;
		private APGenPropertyCollection _keyProps;
		private APGenElementCollection _defaultCollection;
		private APGenElementInformation _elementInfo;
		private APGenElementProperty _elementProperty;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new config element.
		/// </summary>
		protected APGenElement()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Element information.
		/// </summary>
		public APGenElementInformation ElementInformation
		{
			get
			{
				if (_elementInfo == null)
					_elementInfo = new APGenElementInformation(this, null);
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


		internal virtual void InitFromProperty(APGenPropertyInformation propertyInfo)
		{
			_elementInfo = new APGenElementInformation(this, propertyInfo);
			Init();
		}


		internal APGenPropertyCollection GetKeyProperties()
		{
			if (_keyProps != null)
				return _keyProps;

			APGenPropertyCollection tmpkeyProps = new APGenPropertyCollection();

			foreach (APGenProperty prop in Properties)
			{
				if (prop.IsKey)
					tmpkeyProps.Add(prop);
			}

			return _keyProps = tmpkeyProps;
		}


		internal APGenElementCollection GetDefaultCollection()
		{
			if (_defaultCollection != null)
				return _defaultCollection;

			APGenProperty defaultCollectionProp = null;

			foreach (APGenProperty prop in Properties)
			{
				if (prop.IsDefaultCollection)
				{
					defaultCollectionProp = prop;
					break;
				}
			}

			if (defaultCollectionProp != null)
				_defaultCollection = this[defaultCollectionProp] as APGenElementCollection;
			return _defaultCollection;
		}


		internal virtual bool HasValues()
		{
			foreach (APGenPropertyInformation pi in ElementInformation.Properties)
			{
				if (pi.ValueOrigin != APGenPropertyValueOrigin.Default)
					return true;
			}
			return false;
		}


		#endregion


		#region [ Protected Properties ]


		/// <summary>
		/// Element property.
		/// </summary>
		protected internal virtual APGenElementProperty ElementProperty
		{
			get
			{
				if (_elementProperty == null)
					_elementProperty = new APGenElementProperty(ElementInformation.Validator);
				return _elementProperty;
			}
		}


		/// <summary>
		/// Get or set value by property.
		/// </summary>
		/// <param name="property">Property.</param>
		/// <returns>Value</returns>
		protected internal object this[APGenProperty property]
		{
			get { return this[property.Name]; }
			set { this[property.Name] = value; }
		}


		/// <summary>
		/// Get or set value by property name.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		/// <returns>Value</returns>
		protected internal object this[string propertyName]
		{
			get
			{
				APGenPropertyInformation property = ElementInformation.Properties[propertyName];
				if (property == null)
					throw new InvalidOperationException(APResource.GetString(APResource.APGen_PropertyNotExist, propertyName));
				return property.Value;
			}
			set
			{
				APGenPropertyInformation property = ElementInformation.Properties[propertyName];
				if (property == null)
					throw new InvalidOperationException(APResource.GetString(APResource.APGen_PropertyNotExist, propertyName));

				SetPropertyValue(property.Property, value);

				property.Value = value;
			}
		}


		/// <summary>
		/// Property collection.
		/// </summary>
		protected internal virtual APGenPropertyCollection Properties
		{
			get
			{
				if (_map == null)
					_map = APGenElementMap.GetMap(GetType());
				return _map.Properties;
			}
		}


		#endregion


		#region [ Protected Methods ]


		/// <summary>
		/// Initialize.
		/// </summary>
		protected internal virtual void Init()
		{
		}


		/// <summary>
		/// Set property value.
		/// </summary>
		/// <param name="prop">Property</param>
		/// <param name="value">Value</param>
		protected void SetPropertyValue(APGenProperty prop, object value)
		{
			try
			{
				prop.Validate(value);
			}
			catch (Exception e)
			{
				throw new APGenException(APResource.GetString(APResource.APGen_PropertyValueInvalid, prop.Name, e.Message), e);
			}
		}


		/// <summary>
		/// Reads XML from the configuration file.
		/// </summary>
		/// <param name="reader">The XmlReader that reads from the configuration file.</param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			Hashtable readProps = new Hashtable();

			reader.MoveToContent();

			while (reader.MoveToNextAttribute())
			{
				APGenPropertyInformation prop = ElementInformation.Properties[reader.LocalName];
				if (prop == null || (serializeCollectionKey && !prop.IsKey))
				{
					if (reader.LocalName == "xmlns")
					{
						// Ignore
					}
					else
					{
						if (!OnDeserializeUnrecognizedAttribute(reader.LocalName, reader.Value))
							throw new APGenException(APResource.GetString(APResource.APGen_UnrecognizedAttribute, reader.LocalName), reader);
					}

					continue;
				}

				if (readProps.ContainsKey(prop))
					throw new APGenException(APResource.GetString(APResource.APGen_DuplicateAttribute, prop.Name), reader);

				string value = null;
				try
				{
					value = reader.Value;
					ValidateValue(prop.Property, value);
					prop.SetStringValue(value);
				}
				catch (APGenException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new APGenException(APResource.GetString(APResource.APGen_PropertyCannotBeParsed, prop.Name), ex, reader);
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

					APGenPropertyInformation prop = ElementInformation.Properties[reader.LocalName];
					if (prop == null || (serializeCollectionKey && !prop.IsKey))
					{
						if (!OnDeserializeUnrecognizedElement(reader.LocalName, reader))
						{
							if (prop == null)
							{
								APGenElementCollection collection = GetDefaultCollection();
								if (collection != null && collection.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
									continue;
							}
							throw new APGenException(APResource.GetString(APResource.APGen_UnrecognizedElement, reader.LocalName), reader);
						}
						continue;
					}

					if (!prop.IsElement)
						throw new APGenException(APResource.GetString(APResource.APGen_NotElement, prop.Name), reader);

					if (readProps.Contains(prop))
						throw new APGenException(APResource.GetString(APResource.APGen_DuplicateElement, prop.Name), reader);

					APGenElement val = prop.Value as APGenElement;
					val.DeserializeElement(reader, serializeCollectionKey);
					readProps[prop] = prop.Name;
				} while (depth < reader.Depth);

				if (reader.NodeType == XmlNodeType.EndElement)
					reader.Read();
			}

			foreach (APGenPropertyInformation prop in ElementInformation.Properties)
			{
				if (!String.IsNullOrEmpty(prop.Name) && prop.IsRequired && !readProps.ContainsKey(prop))
				{
					APGenPropertyInformation property = ElementInformation.Properties[prop.Name];
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
		/// <returns>true when an unknown attribute is encountered while deserializing; otherwise, false.</returns>
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}


		/// <summary>
		/// Gets a value indicating whether an unknown element is encountered during deserialization.
		/// </summary>
		/// <param name="element">The name of the unknown subelement.</param>
		/// <param name="reader">The XmlReader being used for deserialization.</param>
		/// <returns>true when an unknown element is encountered while deserializing; otherwise, false.</returns>
		protected virtual bool OnDeserializeUnrecognizedElement(string element, XmlReader reader)
		{
			return false;
		}


		/// <summary>
		/// Throws an exception when a required property is not found.
		/// </summary>
		/// <param name="name">The name of the required attribute that was not found.</param>
		/// <returns>None.</returns>
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new APGenException(APResource.GetString(APResource.APGen_RequiredAttribute, name));
		}


		/// <summary>
		/// Called before serialization.
		/// </summary>
		/// <param name="writer">The XmlWriter that will be used to serialize the ConfigurationElement.</param>
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
		/// Used to initialize a default set of values for the ConfigurationElement object.
		/// </summary>
		protected internal virtual void InitializeDefault()
		{
		}


		/// <summary>
		/// Writes the contents of this configuration element to the configuration file when implemented in a derived class.
		/// </summary>
		/// <param name="writer">The XmlWriter that writes to the configuration file.</param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
		/// <returns>true if any data was actually serialized; otherwise, false.</returns>
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			PreSerialize(writer);

			if (serializeCollectionKey)
			{
				APGenPropertyCollection props = GetKeyProperties();
				foreach (APGenProperty prop in props)
					writer.WriteAttributeString(prop.Name, prop.ConvertToString(this[prop.Name]));
				return props.Count > 0;
			}

			bool wroteData = false;

			foreach (APGenPropertyInformation prop in ElementInformation.Properties)
			{
				if (prop.IsElement || prop.ValueOrigin == APGenPropertyValueOrigin.Default)
					continue;

				if (!object.Equals(prop.Value, prop.DefaultValue))
				{
					writer.WriteAttributeString(prop.Name, prop.GetStringValue());
					wroteData = true;
				}
			}

			foreach (APGenPropertyInformation prop in ElementInformation.Properties)
			{
				if (!prop.IsElement)
					continue;

				APGenElement val = prop.Value as APGenElement;
				if (val != null)
					wroteData = val.SerializeToXmlElement(writer, prop.Name) || wroteData;
			}

			return wroteData;
		}


		/// <summary>
		/// Writes the outer tags of this configuration element to the configuration file when implemented in a derived class.
		/// </summary>
		/// <param name="writer">The XmlWriter that writes to the configuration file.</param>
		/// <param name="elementName">The name of the ConfigurationElement to be written.</param>
		/// <returns>true if writing was successful; otherwise, false.</returns>
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


		private void ValidateValue(APGenProperty property, string value)
		{
			APValidatorBase validator;

			if (property == null || (validator = property.Validator) == null)
				return;

			if (!validator.CanValidate(property.Type))
				throw new APGenException(APResource.GetString(APResource.APGen_ValidatorNotSupportType, property.Type));
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
			APGenElement other = compareTo as APGenElement;

			if (other == null)
				return false;
			
			if (GetType() != other.GetType())
				return false;

			foreach (APGenProperty prop in Properties)
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

			foreach (APGenProperty prop in Properties)
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
