using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// Base class of the SQL phrase.
	/// </summary>
	public abstract class APSqlPhrase : IAPSqlPhrase
	{

		#region [ Fields ]


		private IAPSqlPhrase _next;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new phrase.
		/// </summary>
		public APSqlPhrase()
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Next phrase.
		/// </summary>
		public virtual IAPSqlPhrase Next
		{
			get { return _next; }
		}


		/// <summary>
		/// Last phrase.
		/// </summary>
		public virtual IAPSqlPhrase Last
		{
			get
			{
				if (_next == null)
					return this;
				return _next.Last;
			}
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Set next null.
		/// </summary>
		internal protected virtual void SetNextNull()
		{
			_next = null;
		}


		/// <summary>
		/// Set next phrase.
		/// </summary>
		/// <param name="phrase">The next phrase.</param>
		/// <returns>The next phrase.</returns>
		public virtual IAPSqlPhrase SetNext(IAPSqlPhrase phrase)
		{
			_next = phrase;
			return phrase ?? this;
		}


		/// <summary>
		/// Set next phrases.
		/// </summary>
		/// <param name="phrases">The phrases.</param>
		/// <returns>The last phrase.</returns>
		public IAPSqlPhrase SetNext(params IAPSqlPhrase[] phrases)
		{
			IAPSqlPhrase phrase = this;
			foreach (IAPSqlPhrase next in phrases)
				phrase = phrase.SetNext(next);
			return phrase;
		}


		/// <summary>
		/// Set next phrases.
		/// </summary>
		/// <param name="phrases">The IEnumerable phrases.</param>
		/// <returns>The last phrase.</returns>
		public IAPSqlPhrase SetNext(IEnumerable<IAPSqlPhrase> phrases)
		{
			IAPSqlPhrase phrase = this;
			foreach (IAPSqlPhrase next in phrases)
				phrase = phrase.SetNext(next);
			return phrase;
		}


		#endregion

	}

}
