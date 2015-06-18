using System;
using System.Collections;

namespace Symber.Web.Data
{

	/// <summary>
	/// Database helper class.
	/// </summary>
	public class DbHolderBase
	{

		#region [ Methods ]


		/// <summary>
		/// Check parameter.
		/// </summary>
		/// <param name="param">Parameter.</param>
		/// <param name="checkForNull">Check for null.</param>
		/// <param name="checkIfEmpty">Check if empty.</param>
		/// <param name="checkForCommas">Check for commas.</param>
		/// <param name="maxSize">Max size.</param>
		/// <param name="paramName">Parameter name.</param>
		public static void CheckParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
		{
			if (param == null)
			{
				if (checkForNull)
					throw new ArgumentNullException(paramName);
				return;
			}

			param = param.Trim();
			if (checkIfEmpty && param.Length == 0)
				throw new ArgumentException(APResource.GetString(APResource.APDB_ParameterEmpty, paramName), paramName);
			if (maxSize > 0 && param.Length > maxSize)
				throw new ArgumentException(APResource.GetString(APResource.APDB_ParameterTooLong, paramName, maxSize), paramName);
			if (checkForCommas && param.Contains(","))
				throw new ArgumentException(APResource.GetString(APResource.APDB_ParameterContainComma, paramName), paramName);
		}


		/// <summary>
		/// Validate parameter.
		/// </summary>
		/// <param name="param">Parameter.</param>
		/// <param name="checkForNull">Check for null.</param>
		/// <param name="checkIfEmpty">Check if empty.</param>
		/// <param name="checkForCommas">Check for commas.</param>
		/// <param name="maxSize">Max size.</param>
		/// <returns>True if parameter is valid.</returns>
		public static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
		{
			if (param == null)
				return !checkForNull;

			param = param.Trim();
			if ((checkIfEmpty && param.Length < 1) || (maxSize > 0 && param.Length > maxSize) || (checkForCommas && param.Contains(",")))
				return false;
			return true;
		}


		/// <summary>
		/// Check array parameter.
		/// </summary>
		/// <param name="param">Parameter.</param>
		/// <param name="checkForNull">Check for null.</param>
		/// <param name="checkIfEmpty">Check if empty.</param>
		/// <param name="checkForCommas">Check for commas.</param>
		/// <param name="maxSize">Max size.</param>
		/// <param name="paramName">Parameter name.</param>
		public static void CheckArrayParameter(ref string[] param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
		{
			if (param == null)
				throw new ArgumentNullException(paramName);

			if (param.Length < 1)
				throw new ArgumentException(APResource.GetString(APResource.APDB_ParameterArrayEmpty, paramName));

			Hashtable values = new Hashtable(param.Length);
			for (int i = param.Length - 1; i >= 0; i--)
			{
				CheckParameter(ref param[i], checkForNull, checkIfEmpty, checkForCommas, maxSize, paramName + "[ " + i.ToString() + " ]");
				if (values.Contains(param[i]))
					throw new ArgumentException(APResource.GetString(APResource.APDB_ParameterArrayDuplicateElement, paramName), paramName);
				else
					values.Add(param[i], param[i]);
			}
		}


		#endregion

	}

}
