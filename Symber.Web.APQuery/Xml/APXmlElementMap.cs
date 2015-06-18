using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace Symber.Web.Xml
{

	internal class APXmlElementMap
	{

		#region [ Static Fields ]


		static readonly Hashtable elementMaps = Hashtable.Synchronized(new Hashtable());


		#endregion


		#region [ Static Methods ]


		public static APXmlElementMap GetMap(Type type)
		{
			APXmlElementMap map = elementMaps[type] as APXmlElementMap;
			if (map != null)
				return map;

			map = new APXmlElementMap(type);
			elementMaps[type] = map;
			return map;
		}


		#endregion


		#region [ Fields ]


		private readonly APXmlPropertyCollection _properties;
		private readonly APXmlCollectionAttribute _collectionAttribute;


		#endregion


		#region [ Constructors ]


		public APXmlElementMap(Type type)
		{
			_properties = new APXmlPropertyCollection();

			_collectionAttribute = Attribute.GetCustomAttribute(type, typeof(APXmlCollectionAttribute)) as APXmlCollectionAttribute;

			PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

			foreach (PropertyInfo prop in props)
			{
				APXmlPropertyAttribute attr = Attribute.GetCustomAttribute(prop, typeof(APXmlPropertyAttribute)) as APXmlPropertyAttribute;
				if (attr == null)
					continue;
				string name = attr.Name != null ? attr.Name : prop.Name;

				APValidatorAttribute validatorAttr = Attribute.GetCustomAttribute(prop, typeof(APValidatorAttribute)) as APValidatorAttribute;
				APValidatorBase validator = validatorAttr != null ? validatorAttr.ValidatorInstance : null;

				TypeConverterAttribute convertAttr = Attribute.GetCustomAttribute(prop, typeof(TypeConverterAttribute)) as TypeConverterAttribute;
				TypeConverter converter = convertAttr != null ? (TypeConverter)Activator.CreateInstance(Type.GetType(convertAttr.ConverterTypeName), true) : null;

				APXmlProperty property = new APXmlProperty(name, prop.PropertyType, attr.DefaultValue, converter, validator, attr.Options);

				property.CollectionAttribute = Attribute.GetCustomAttribute(prop, typeof(APXmlCollectionAttribute)) as APXmlCollectionAttribute;
				_properties.Add(property);
			}
		}


		#endregion


		#region [ Properties ]


		public APXmlCollectionAttribute CollectionAttribute
		{
			get { return _collectionAttribute; }
		}


		public bool HasProperties
		{
			get { return _properties.Count > 0; }
		}


		public APXmlPropertyCollection Properties
		{
			get { return _properties; }
		}


		#endregion

	}

}
