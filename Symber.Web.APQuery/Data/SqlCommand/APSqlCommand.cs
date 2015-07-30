using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// Base class of all SQL command.
	/// </summary>
	public abstract class APSqlCommand : IAPSqlPhrase
	{

		#region [ Fields ]
	
		
		private bool _commandNameSuitable = true;
		
		
		#endregion


		#region [ Properties ]


		/// <summary>
		/// Suitable command name, if command name conflict. Default is true.
		/// </summary>
		public bool CommandNameSuitable
		{
			get { return _commandNameSuitable; }
			set { _commandNameSuitable = value; }
		}


		#endregion


		#region [ Implementation of IAPSqlPhrase ]


		/// <summary>
		/// Set next phrase.
		/// </summary>
		/// <param name="phrase">The next phrase.</param>
		/// <returns>The next phrase.</returns>
		public IAPSqlPhrase SetNext(IAPSqlPhrase phrase)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// Next phrase.
		/// </summary>
		public IAPSqlPhrase Next
		{
			get { throw new NotImplementedException(); }
		}


		/// <summary>
		/// Last phrase.
		/// </summary>
		public IAPSqlPhrase Last
		{
			get { throw new NotImplementedException(); }
		}


		#endregion

	}

}
