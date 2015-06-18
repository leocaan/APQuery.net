using System;

namespace Symber.Web
{
	/// <summary>
	/// StringAPValidatorAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class StringAPValidatorAttribute : APValidatorAttribute
	{

		#region [ Fields ]


		private string _invalidCharacters;
		private int _maxLength = Int32.MaxValue;
		private int _minLength = 0;


		#endregion


		#region [ Properties ]


		/// <summary>
		/// InvalidCharacters.
		/// </summary>
		public string InvalidCharacters
		{
			get { return _invalidCharacters; }
			set { _invalidCharacters = value; instance = null; }
		}


		/// <summary>
		/// Max length.
		/// </summary>
		public int MaxLength
		{
			get { return _maxLength; }
			set { _maxLength = value; instance = null; }
		}


		/// <summary>
		/// Min length.
		/// </summary>
		public int MinLength
		{
			get { return _minLength; }
			set { _minLength = value; instance = null; }
		}


		#endregion


		#region [ Override Implementation of APValidatorAttribute ]


		/// <summary>
		/// ValidatorInstance.
		/// </summary>
		public override APValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
					instance = new StringAPValidator(_minLength, _maxLength, _invalidCharacters);
				return instance;
			}
		}


		#endregion

	}
}
