
namespace Symber.Web.Compilation
{
	/// <summary>
	/// Specifies the property of a configuration element. This class cannot be inherited.
	/// </summary>
	public sealed class APGenElementProperty
	{

		#region [ Fields ]


		APValidatorBase _validator;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Initializes a new instance of the APGenElementProperty class, based on a supplied parameter.
		/// </summary>
		/// <param name="validator">A APValidatorBase object.</param>
		public APGenElementProperty(APValidatorBase validator)
		{
			_validator = validator;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets a APValidatorBase object used to validate the APGenElementProperty object.
		/// </summary>
		public APValidatorBase Validator
		{
			get { return _validator; }
		}


		#endregion

	}
}
