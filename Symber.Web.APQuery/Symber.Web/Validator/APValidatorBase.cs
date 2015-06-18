using System;

namespace Symber.Web
{
	/// <summary>
	/// APValidatorBase.
	/// </summary>
	public abstract class APValidatorBase
	{

		#region [ Constructors ]


		/// <summary>
		/// Constructors.
		/// </summary>
		protected APValidatorBase()
		{
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Check whether can validate.
		/// </summary>
		/// <param name="type">Tyep.</param>
		/// <returns>Whether can validate.</returns>
		public virtual bool CanValidate(Type type)
		{
			return false;
		}


		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value.</param>
		public abstract void Validate(object value);


		#endregion

	}
}
