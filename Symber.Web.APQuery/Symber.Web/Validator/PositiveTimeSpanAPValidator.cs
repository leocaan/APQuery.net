using System;

namespace Symber.Web
{
	/// <summary>
	/// PositiveTimeSpanAPValidator.
	/// </summary>
	public class PositiveTimeSpanAPValidator : APValidatorBase
	{

		#region [ Override Implementation of APValidatorBase ]


		/// <summary>
		/// Check whether can validate.
		/// </summary>
		/// <param name="type">Tyep.</param>
		/// <returns>Whether can validate.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}


		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value.</param>
		public override void Validate(object value)
		{
			TimeSpan s = (TimeSpan)value;
			if (s <= new TimeSpan(0))
				throw new ArgumentException(APResource.APValidate_TimeMustBePositive);
		}


		#endregion

	}
}
