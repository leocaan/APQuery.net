using System;

namespace Symber.Web
{
	/// <summary>
	/// PositiveTimeSpanAPValidatorAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PositiveTimeSpanAPValidatorAttribute : APValidatorAttribute
	{

		#region [ Override Implementation of APValidatorAttribute ]


		/// <summary>
		/// ValidatorInstance.
		/// </summary>
		public override APValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
					instance = new PositiveTimeSpanAPValidator();
				return instance;
			}
		}


		#endregion

	}
}
