using System;

namespace Symber.Web
{
	/// <summary>
	/// IntegerAPValidator.
	/// </summary>
	public class IntegerAPValidator : APValidatorBase
	{

		#region [ Fields ]


		private bool _rangeIsExclusive;
		private int _minValue;
		private int _maxValue;
		private int _resolution;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Constructors new IntegerAPValidator.
		/// </summary>
		/// <param name="minValue">Min value.</param>
		/// <param name="maxValue">Max value.</param>
		/// <param name="rangeIsExclusive">Ranges is exclusive.</param>
		/// <param name="resolution">Resolution.</param>
		public IntegerAPValidator(int minValue, int maxValue, bool rangeIsExclusive, int resolution)
		{
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._rangeIsExclusive = rangeIsExclusive;
			this._resolution = resolution;
		}


		/// <summary>
		/// Constructors new IntegerAPValidator.
		/// </summary>
		/// <param name="minValue">Min value.</param>
		/// <param name="maxValue">Max value.</param>
		/// <param name="rangeIsExclusive">Ranges is exclusive.</param>
		public IntegerAPValidator(int minValue, int maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0)
		{
		}


		/// <summary>
		/// Constructors new IntegerAPValidator.
		/// </summary>
		/// <param name="minValue">Min value.</param>
		/// <param name="maxValue">Max value.</param>
		public IntegerAPValidator(int minValue, int maxValue)
			: this(minValue, maxValue, false, 0)
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
			return type == typeof(int);
		}


		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value.</param>
		public override void Validate(object value)
		{
			int l = (int)value;

			if (!_rangeIsExclusive)
			{
				if (l < _minValue || l > _maxValue)
					throw new ArgumentException(APResource.GetString(APResource.APValidate_NumberOutOfRange, _minValue, _maxValue));
			}
			else
			{
				if (l >= _minValue && l <= _maxValue)
					throw new ArgumentException(APResource.GetString(APResource.APValidate_NumberInTheRange, _minValue, _maxValue));
			}
			if (_resolution != 0 && l % _resolution != 0)
				throw new ArgumentException(APResource.GetString(APResource.APValidate_NumberInTheRange, _resolution));
		}


		#endregion

	}
}
