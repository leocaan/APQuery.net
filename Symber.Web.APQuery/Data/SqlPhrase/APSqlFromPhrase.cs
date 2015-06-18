using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'FROM' phrase.
	/// </summary>
	public sealed class APSqlFromPhrase : APSqlPhrase
	{

		#region [ Fields ]


		private APTableDef _tableDef;
		private APSqlJoinType _joinType;
		private APSqlWherePhrase _joinOnPhrase;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a 'FROM' phrase.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		public APSqlFromPhrase(APTableDef tableDef)
			: this(tableDef, APSqlJoinType.None, null)
		{
		}


		/// <summary>
		/// Create a 'FROM' phrase.
		/// </summary>
		/// <param name="tableDef">Table definition.</param>
		/// <param name="joinType">SQL 'JOIN' type.</param>
		/// <param name="joinOnPhrase">SQL 'JOIN' condition.</param>
		public APSqlFromPhrase(APTableDef tableDef, APSqlJoinType joinType, APSqlWherePhrase joinOnPhrase)
		{
			if (tableDef == null)
				throw new ArgumentNullException("tableDef");

			_tableDef = tableDef;
			_joinType = joinType;
			_joinOnPhrase = joinOnPhrase;
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Table definition.
		/// </summary>
		public APTableDef TableDef
		{
			get { return _tableDef; }
		}


		/// <summary>
		/// Table name.
		/// </summary>
		public string TableName
		{
			get { return _tableDef.TableName; }
		}


		/// <summary>
		/// Table alias.
		/// </summary>
		public string AliasName
		{
			get { return _tableDef.AliasName; }
		}


		/// <summary>
		/// SQL 'JOIN' type.
		/// </summary>
		public APSqlJoinType JoinType
		{
			get { return _joinType; }
		}


		/// <summary>
		/// SQL 'JOIN' condition.
		/// </summary>
		public APSqlWherePhrase JoinOnPhrase
		{
			get { return _joinOnPhrase; }
		}


		#endregion


		#region [ Override Implementation of APSqlPhrase ]


		/// <summary>
		/// Set next phrase.
		/// </summary>
		/// <param name="phrase">The next phrase.</param>
		/// <returns>The next phrase.</returns>
		public override IAPSqlPhrase SetNext(IAPSqlPhrase phrase)
		{
			if (phrase is APSqlFromPhrase || phrase == null)
				return base.SetNext(phrase);

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlFromPhrase).Name));
		}


		#endregion

	}

}
