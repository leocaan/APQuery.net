using System;

namespace Symber.Web
{
	/// <summary>
	/// StringAPValidator.
	/// </summary>
	public class StringAPValidator : APValidatorBase
	{

		#region [ Fields ]


		private char[] _invalidCharacters;
		private int _maxLength;
		private int _minLength;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Constructors new StringAPValidator.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		public StringAPValidator (int minLength)
		{
			_minLength = minLength;
			_maxLength = int.MaxValue;
		}

		
		/// <summary>
		/// Constructors new StringAPValidator.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		/// <param name="maxLength">Max length.</param>
		public StringAPValidator (int minLength, int maxLength)
		{
			_minLength = minLength;
			_maxLength = maxLength;
		}


		/// <summary>
		/// Constructors new StringAPValidator.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		/// <param name="maxLength">Max length.</param>
		/// <param name="invalidCharacters">Invalid characters.</param>
		public StringAPValidator(int minLength, int maxLength, string invalidCharacters)
		{
			_minLength = minLength;
			_maxLength = maxLength;
			if (invalidCharacters != null)
				this._invalidCharacters = invalidCharacters.ToCharArray ();
		}


		#endregion


		#region [ Override Implementation of APValidatorBase ]


		/// <summary>
		/// Check whether can validate.
		/// </summary>
		/// <param name="type">Tyep.</param>
		/// <returns>Whether can validate.</returns>
		public override bool CanValidate (Type type)
		{
			return type == typeof(string);
		}


		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value.</param>
		public override void Validate(object value)
		{
			if (value == null && _minLength <= 0)
				return;

			string str = (string)value;
			if (str == null || str.Length < _minLength)
				throw new ArgumentException(APResource.GetString(APResource.APValidate_StringAtLeast, _minLength));
			if (str.Length > _maxLength)
				throw new ArgumentException(APResource.GetString(APResource.APValidate_StringMoreThen, _maxLength));
			if (_invalidCharacters != null && str.IndexOfAny(_invalidCharacters) != -1)
				throw new ArgumentException(APResource.GetString(APResource.APValidate_StringInvalidContain, _invalidCharacters));
		}


		#endregion

	}
}
