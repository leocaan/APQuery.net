
namespace Symber.Web
{
	/// <summary>
	/// NullableStringAPValidator.
	/// </summary>
	public class NullableStringAPValidator : StringAPValidator
	{

		#region [ Constructors ]


		/// <summary>
		/// Constructors new PropertyNameAPValidator.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		public NullableStringAPValidator(int minLength)
			: base(minLength)
		{
		}


		/// <summary>
		/// Constructors new PropertyNameAPValidator.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		/// <param name="maxLength">Max length.</param>
		public NullableStringAPValidator(int minLength, int maxLength)
			: base(minLength, maxLength)
		{
		}


		/// <summary>
		/// Constructors new PropertyNameAPValidator.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		/// <param name="maxLength">Max length.</param>
		/// <param name="invalidCharacters">Invalid characters string.</param>
		public NullableStringAPValidator(int minLength, int maxLength, string invalidCharacters)
			: base(minLength, maxLength, invalidCharacters)
		{
		}


		#endregion


		#region [ Override Implementation of APValidatorBase ]


		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value.</param>
		public override void Validate(object value)
		{
			if (value == null)
				return;
			base.Validate(value);
		}


		#endregion

	}
}
