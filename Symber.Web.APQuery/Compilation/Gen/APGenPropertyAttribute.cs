using System;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Declaratively instructs the APGen to instantiate a configuration property.
	/// This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class APGenPropertyAttribute : Attribute
	{

		#region [ Fields ]


		private string _name;
		private object _defaultValue = APGenProperty.NoDefaultValue;
		private APGenPropertyOptions _flags;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of APGenPropertyAttribute class.
		/// </summary>
		/// <param name="name">Name of the APGenProperty object defined.</param>
		public APGenPropertyAttribute(string name)
		{
			_name = name;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets a value indicating whether this is the default property collection for the
		/// decorated configuration property.
		/// </summary>
		public bool IsDefaultCollection
		{
			get { return (_flags & APGenPropertyOptions.IsDefaultCollection) != 0; }
			set
			{
				if (value)
					_flags |= APGenPropertyOptions.IsDefaultCollection;
				else
					_flags &= ~APGenPropertyOptions.IsDefaultCollection;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether this is a key property for the decorated element property.
		/// </summary>
		public bool IsKey
		{
			get { return (_flags & APGenPropertyOptions.IsKey) != 0; }
			set
			{
				if (value)
					_flags |= APGenPropertyOptions.IsKey;
				else
					_flags &= ~APGenPropertyOptions.IsKey;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the decorated element property is required.
		/// </summary>
		public bool IsRequired
		{
			get { return (_flags & APGenPropertyOptions.IsRequired) != 0; }
			set
			{
				if (value)
					_flags |= APGenPropertyOptions.IsRequired;
				else
					_flags &= ~APGenPropertyOptions.IsRequired;
			}
		}


		/// <summary>
		/// Gets or sets the name of the decorated configuration-element property.
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
		/// Gets or sets the APGenPropertyOptions for the decorated configuration-element property.
		/// </summary>
		public APGenPropertyOptions Options
		{
			get { return _flags; }
			set { _flags = value; }
		}


		#endregion

	}
}
