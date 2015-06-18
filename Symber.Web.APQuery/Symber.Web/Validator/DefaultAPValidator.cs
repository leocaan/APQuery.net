using System;

namespace Symber.Web
{
	/// <summary>
	/// DefaultAPValidator.
	/// </summary>
	public sealed class DefaultAPValidator : APValidatorBase
	{

		#region [ Constructors ]


		/// <summary>
		/// Constructors new DefaultAPValidator.
		/// </summary>
		public DefaultAPValidator()
		{
		}


		#endregion


		#region [ Override Implementation of APValidatorBase ]


		/// <summary>
		/// Check whether can validate.
		/// </summary>
		/// <param name="type">Tyep.</param>
		/// <returns>Whether can validate.</returns>
		public override bool CanValidate(Type type)
		{
			return true;
		}


		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value</param>
		public override void Validate(object value)
		{
		}


		#endregion

	}
}
