using System;

namespace Symber.Web
{
	/// <summary>
	/// PropertyNameAPValidator.
	/// </summary>
	internal class PropertyNameAPValidator : StringAPValidator
	{

		#region [ Constructors ]


		/// <summary>
		/// Constructors new PropertyNameAPValidator.
		/// </summary>
		public PropertyNameAPValidator()
			: base(1)
		{
		}


		#endregion


		#region [ Override Implementation of APValidator ]
		

		/// <summary>
		/// Validate value.
		/// </summary>
		/// <param name="value">Value.</param>
		public override void Validate(object value)
		{
			base.Validate(value);

			string val = value as string;
			if (value == null)
				throw new ArgumentNullException("value");
			val = val.Trim();
			if (String.IsNullOrEmpty(val))
				throw new ArgumentException(APResource.APValidate_NameEmpty);
			if (val.Contains("."))
				throw new ArgumentException(APResource.APValidate_NameContainPeriod);
		}


		#endregion

	}
}
