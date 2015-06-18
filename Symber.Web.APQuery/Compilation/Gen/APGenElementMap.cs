using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace Symber.Web.Compilation
{
	internal class APGenElementMap
	{

		#region [ Static Fields ]


		static readonly Hashtable elementMaps = Hashtable.Synchronized(new Hashtable());


		#endregion


		#region [ Static Methods ]


		public static APGenElementMap GetMap(Type type)
		{
			APGenElementMap map = elementMaps[type] as APGenElementMap;
			if (map != null)
				return map;

			map = new APGenElementMap(type);
			elementMaps[type] = map;
			return map;
		}


		#endregion


		#region [ Fields ]


		private readonly APGenPropertyCollection _properties;
		private readonly APGenCollectionAttribute _collectionAttribute;


		#endregion


		#region [ Constructors ]


		public APGenElementMap(Type type)
		{
			_properties = new APGenPropertyCollection();

			_collectionAttribute = Attribute.GetCustomAttribute(type, typeof(APGenCollectionAttribute)) as APGenCollectionAttribute;

			PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

			foreach (PropertyInfo prop in props)
			{
				APGenPropertyAttribute attr = Attribute.GetCustomAttribute(prop, typeof(APGenPropertyAttribute)) as APGenPropertyAttribute;
				if (attr == null)
					continue;
				string name = attr.Name != null ? attr.Name : prop.Name;

				APValidatorAttribute validatorAttr = Attribute.GetCustomAttribute(prop, typeof(APValidatorAttribute)) as APValidatorAttribute;
				APValidatorBase validator = validatorAttr != null ? validatorAttr.ValidatorInstance : null;

				TypeConverterAttribute convertAttr = Attribute.GetCustomAttribute(prop, typeof(TypeConverterAttribute)) as TypeConverterAttribute;
				TypeConverter converter = convertAttr != null ? (TypeConverter)Activator.CreateInstance(Type.GetType(convertAttr.ConverterTypeName), true) : null;

				APGenProperty property = new APGenProperty(name, prop.PropertyType, attr.DefaultValue, converter, validator, attr.Options);

				property.CollectionAttribute = Attribute.GetCustomAttribute(prop, typeof(APGenCollectionAttribute)) as APGenCollectionAttribute;
				_properties.Add(property);
			}
		}


		#endregion


		#region [ Properties ]


		public APGenCollectionAttribute CollectionAttribute
		{
			get { return _collectionAttribute; }
		}


		public bool HasProperties
		{
			get { return _properties.Count > 0; }
		}


		public APGenPropertyCollection Properties
		{
			get { return _properties; }
		}


		#endregion

	}
}
