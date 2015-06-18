
namespace Symber.Web.Data
{

	/// <summary>
	/// Base class of all SQL command.
	/// </summary>
	public abstract class APSqlCommand
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

	}

}
