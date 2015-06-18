using System;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'SET' phrase.
	/// </summary>
	public sealed class APSqlSetPhrase : APSqlPhrase
	{

		#region [ Fields ]


		private APSqlColumnExpr _assignmentExpr;
		private string _paramName;
		private object _value;


		#endregion


		#region [ Constructors ]
		

		/// <summary>
		/// Create a new 'SET' phrase.
		/// </summary>
		/// <param name="assignmentExpr">SQL assignment Expression.</param>
		/// <param name="value">Value.</param>
		/// <param name="paramName">Parameter name.</param>
		public APSqlSetPhrase(APSqlColumnExpr assignmentExpr, object value, string paramName)
		{
			if (Object.Equals(assignmentExpr, null))
				throw new ArgumentNullException("assignmentExpr");

			_assignmentExpr = assignmentExpr;
			_paramName = String.IsNullOrEmpty(paramName) ? assignmentExpr.ParamName : paramName;
			_value = value;
		}


		/// <summary>
		/// Create a new 'SET' phrase.
		/// </summary>
		/// <param name="assignmentExpr">SQL assignment Expression.</param>
		/// <param name="value">Value.</param>
		public APSqlSetPhrase(APSqlColumnExpr assignmentExpr, object value)
			: this(assignmentExpr, value, null)
		{
		}


		/// <summary>
		/// Create a new 'SET' phrase what user sql expression
		/// </summary>
		/// <param name="assignmentExpr">SQL assignment Expression.</param>
		/// <param name="valueExpr">SQL value of assignment Expression.</param>
		public APSqlSetPhrase(APSqlColumnExpr assignmentExpr, IAPSqlValueExpr valueExpr)
			: this(assignmentExpr, valueExpr, null)
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// SQL assignment Expression.
		/// </summary>
		public APSqlColumnExpr AssignmentExpr
		{
			get { return _assignmentExpr; }
		}


		/// <summary>
		/// Parameter name.
		/// </summary>
		public string ParamName
		{
			get { return _paramName; }
		}


		/// <summary>
		/// Value.
		/// </summary>
		public object Value
		{
			get { return _value; }
		}


		/// <summary>
		/// Whether this is a relation SQL value of assignment Expression.
		/// </summary>
		public bool IsRelationValueExpr
		{
			get
			{
				return _value is APColumnDef
					|| _value is IAPSqlValueExpr;
			}
		}


		/// <summary>
		/// Relation SQL value of assignment Expression.
		/// </summary>
		public IAPSqlValueExpr RelationValueExpr
		{
			get
			{
				if (_value is APColumnDef)
					return new APSqlColumnExpr(_value as APColumnDef);
				else if (_value is IAPSqlValueExpr)
					return _value as IAPSqlValueExpr;
				else
					return null;
			}
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
			if (phrase is APSqlSetPhrase || phrase == null)
				return base.SetNext(phrase);

			throw new APDataException(APResource.GetString(APResource.APData_PhraseNextError,
				GetType().Name, phrase.GetType().Name, typeof(APSqlSetPhrase).Name));
		}


		#endregion

	}

}
