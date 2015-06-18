using System;
using System.ComponentModel;

namespace Symber.Web.Xml
{

	/// <summary>
	/// Contains meta-information about an individual element within the xml. This class cannot be inherited.
	/// </summary>
	public sealed class APXmlElementInformation
	{

		#region [ Fields ]


		private readonly APXmlPropertyInformation _propertyInfo;
		private readonly APXmlElement _owner;
		private readonly APXmlPropertyInformationCollection _properties;


		#endregion


		#region [ Constructors ]


		internal APXmlElementInformation(APXmlElement owner, APXmlPropertyInformation propertyInfo)
		{
			_propertyInfo = propertyInfo;
			_owner = owner;

			_properties = new APXmlPropertyInformationCollection();
			foreach (APXmlProperty prop in owner.Properties)
				_properties.Add(new APXmlPropertyInformation(owner, prop));
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets a value indicating whether the associated APXmlElement object is a APXmlElementCollection collection.
		/// </summary>
		public bool IsCollection
		{
			get { return _owner is APXmlElementCollection; }
		}


		/// <summary>
		/// Gets a value indicating whether the associated APXmlElement object is in the xml file.
		/// </summary>
		public bool IsPresent
		{
			get { return _propertyInfo != null; }
		}


		/// <summary>
		/// Gets the line number in the xml file where the associated APXmlElement object is defined.
		/// </summary>
		public int LineNumber
		{
			get { return _propertyInfo != null ? _propertyInfo.LineNumber : 0; }
		}


		/// <summary>
		/// Gets the source file where the associated APXmlElement object originated.
		/// </summary>
		public string Source
		{
			get { return _propertyInfo != null ? _propertyInfo.Source : null; }
		}


		/// <summary>
		/// Gets the Type of the associated APXmlElement object.
		/// </summary>
		public Type Type
		{
			get { return _propertyInfo != null ? _propertyInfo.Type : _owner.GetType(); }
		}


		/// <summary>
		/// Gets the object used to validate the associated APXmlElement object.
		/// </summary>
		public APValidatorBase Validator
		{
			get { return _propertyInfo != null ? _propertyInfo.Validator : new DefaultAPValidator(); }
		}


		internal TypeConverter Converter
		{
			get { return _propertyInfo != null ? _propertyInfo.Converter : TypeDescriptor.GetConverter(typeof(String)); }
		}


		/// <summary>
		/// Gets a APXmlPropertyInformationCollection collection of the properties in the associated APXmlElement object.
		/// </summary>
		public APXmlPropertyInformationCollection Properties
		{
			get { return _properties; }
		}


		#endregion

	}

}
