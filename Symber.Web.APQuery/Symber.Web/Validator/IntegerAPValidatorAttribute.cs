using System;

namespace Symber.Web
{
	/// <summary>
	/// IntegerAPValidatorAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerAPValidatorAttribute : APValidatorAttribute
	{

		#region [ Fields ]


		private bool _excludeRange = false;
		private int _maxValue = 0;
		private int _minValue = 0;


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Gets or sets the exclude range.
		/// </summary>
		public bool ExcludeRange
		{
			get { return _excludeRange; }
			set { _excludeRange = value; instance = null; }
		}


		/// <summary>
		/// Gets or sets max value.
		/// </summary>
		public int MaxValue
		{
			get { return _maxValue; }
			set { _maxValue = value; instance = null; }
		}


		/// <summary>
		/// Gets or set min value.
		/// </summary>
		public int MinValue
		{
			get { return _minValue; }
			set { _minValue = value; instance = null; }
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
					instance = new IntegerAPValidator(_minValue, _maxValue, _excludeRange);

				return instance;
			}
		}


		#endregion

	}
}
