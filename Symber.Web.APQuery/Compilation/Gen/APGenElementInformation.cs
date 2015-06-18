using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Contains meta-information about an individual element within the configuration.
	/// This class cannot be inherited.
	/// </summary>
	public sealed class APGenElementInformation
	{

		#region [ Fields ]


		private readonly APGenPropertyInformation _propertyInfo;
		private readonly APGenElement _owner;
		private readonly APGenPropertyInformationCollection _properties;


		#endregion


		#region [ Constructors ]


		internal APGenElementInformation(APGenElement owner, APGenPropertyInformation propertyInfo)
		{
			_propertyInfo = propertyInfo;
			_owner = owner;

			_properties = new APGenPropertyInformationCollection();
			foreach (APGenProperty prop in owner.Properties)
				_properties.Add(new APGenPropertyInformation(owner, prop));
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets a value indicating whether the associated APGenElement object is a APGenElementCollection collection.
		/// </summary>
		public bool IsCollection
		{
			get { return _owner is APGenElementCollection; }
		}


		/// <summary>
		/// Gets a value indicating whether the associated APGenElement object is in the configuration file.
		/// </summary>
		public bool IsPresent
		{
			get { return _propertyInfo != null; }
		}


		/// <summary>
		/// Gets the line number in the configuration file where the associated APGenElement object is defined.
		/// </summary>
		public int LineNumber
		{
			get { return _propertyInfo != null ? _propertyInfo.LineNumber : 0; }
		}


		/// <summary>
		/// Gets the source file where the associated APGenElement object originated.
		/// </summary>
		public string Source
		{
			get { return _propertyInfo != null ? _propertyInfo.Source : null; }
		}


		/// <summary>
		/// Gets the type of the associated APGenElement object.
		/// </summary>
		public Type Type
		{
			get { return _propertyInfo != null ? _propertyInfo.Type : _owner.GetType(); }
		}


		/// <summary>
		/// Gets the object used to validate the associated APGenElement object.
		/// </summary>
		public APValidatorBase Validator
		{
			get { return _propertyInfo != null ? _propertyInfo.Validator : new DefaultAPValidator(); }
		}


		/// <summary>
		/// Gets a APGenPropertyInformationCollection collection of the properties in the associated APGenElement object.
		/// </summary>
		public APGenPropertyInformationCollection Properties
		{
			get { return _properties; }
		}


		#endregion

	}
}
