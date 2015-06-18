using System;

namespace Symber.Web
{
	/// <summary>
	/// APValidatorAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class APValidatorAttribute : Attribute
	{

		#region [ Fields ]


		private Type _validatorType;


		/// <summary>
		/// Instance
		/// </summary>
		protected APValidatorBase instance;
		

		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Constructors new APValidatorAttribute.
		/// </summary>
		protected APValidatorAttribute()
		{
		}


		/// <summary>
		/// Constructors new APValidatorAttribute.
		/// </summary>
		/// <param name="validatorType">type.</param>
		public APValidatorAttribute(Type validatorType)
		{
			_validatorType = validatorType;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Validator instance.
		/// </summary>
		public virtual APValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
					instance = (APValidatorBase)Activator.CreateInstance(_validatorType);
				return instance;
			}
		}


		/// <summary>
		/// Validator type.
		/// </summary>
		public Type ValidatorType
		{
			get { return _validatorType; }
		}


		#endregion

	}
}
