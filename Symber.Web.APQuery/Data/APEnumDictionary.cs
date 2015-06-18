using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Symber.Web.Data
{

	/// <summary>
	/// Enumerate dictionary.
	/// </summary>
	[Serializable]
	public class APEnumDictionary<T> : Dictionary<T, string>
	{

		/// <summary>
		/// Create a enumerate dictionary.
		/// </summary>
		/// <param name="keyValues">The enumerate items and it's display string.</param>
		/// <returns>Created enumerate dictionary.</returns>
		public static APEnumDictionary<T> Create(params KeyValuePair<T, string>[] keyValues)
		{
			if (!typeof(T).IsEnum)
				throw new InvalidOperationException();

			APEnumDictionary<T> dictionary = new APEnumDictionary<T>();
			foreach (KeyValuePair<T, string> k in keyValues)
			{
				dictionary.Add(k.Key, k.Value);
			}

			return dictionary;
		}


		#region [ Constructors ]


		/// <summary>
		/// Create a new instance.
		/// </summary>
		public APEnumDictionary()
			: base()
		{
		}


		/// <summary>
		/// Create a new instance.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="context">Context.</param>
		protected APEnumDictionary(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}


		#endregion

	}

}
