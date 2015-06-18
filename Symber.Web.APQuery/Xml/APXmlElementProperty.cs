
namespace Symber.Web.Xml
{

	/// <summary>
	/// Specifies the property of a xml element. This class cannot be inherited.
	/// </summary>
	public sealed class APXmlElementProperty
	{

		#region [ Fields ]


		private APValidatorBase _validator;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes an instance of the APXmlElementProperty class based on a supplied parameter.
		/// </summary>
		/// <param name="validator">The validator.</param>
		public APXmlElementProperty(APValidatorBase validator)
		{
			_validator = validator;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets a APValidatorBase object used to validate the APXmlElementProperty object.
		/// </summary>
		public APValidatorBase Validator
		{
			get { return _validator; }
		}


		#endregion

	}

}
