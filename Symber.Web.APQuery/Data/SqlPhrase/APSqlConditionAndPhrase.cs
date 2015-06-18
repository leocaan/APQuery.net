using System.Collections.Generic;

namespace Symber.Web.Data
{

	/// <summary>
	/// SQL 'AND' condition group.
	/// </summary>
	public sealed class APSqlConditionAndPhrase : APSqlWherePhrase
	{

		#region [ Fields ]


		private APSqlWherePhrase _child;


		#endregion


		#region [ Constructs ]


		/// <summary>
		/// Create a new 'AND' condition group.
		/// </summary>
		/// <param name="phrase1">'WHERE' phrase1.</param>
		/// <param name="phrase2">'WHERE' phrase2.</param>
		public APSqlConditionAndPhrase(APSqlWherePhrase phrase1, APSqlWherePhrase phrase2)
		{
			_child = phrase1;
			_child.SetNext(phrase2);
		}


		/// <summary>
		/// Create a new 'AND' condition group.
		/// </summary>
		/// <param name="phrase1">'WHERE' phrase1.</param>
		/// <param name="phrases">'WHERE' phrases.</param>
		public APSqlConditionAndPhrase(APSqlWherePhrase phrase1, params APSqlWherePhrase[] phrases)
		{
			_child = phrase1;
			_child.SetNext(phrases);
		}


		/// <summary>
		/// Create a new 'AND' condition group.
		/// </summary>
		/// <param name="phrases">The IEnumerable phrases.</param>
		public APSqlConditionAndPhrase(IEnumerable<APSqlWherePhrase> phrases)
		{
			IAPSqlPhrase phrase = null;
			foreach (APSqlWherePhrase next in phrases)
			{
				if (_child == null)
					phrase = _child = next;
				else
					phrase = phrase.SetNext(next);
			}
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// Child 'WHERE' phrase.
		/// </summary>
		public APSqlWherePhrase Child
		{
			get { return _child as APSqlWherePhrase; }
		}


		#endregion


		#region [ Override Implementation of Operator ]


		/// <summary>
		/// And operator.
		/// </summary>
		/// <param name="left">Left and condition group.</param>
		/// <param name="right">Right condition phrase.</param>
		/// <returns>And condition group.</returns>
		public static APSqlConditionAndPhrase operator &(APSqlConditionAndPhrase left, APSqlConditionPhrase right)
		{
			left.Child.Last.SetNext(right);
			return left;
		}


		/// <summary>
		/// And operator.
		/// </summary>
		/// <param name="left">Left and condition group.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>And condition group.</returns>
		public static APSqlConditionAndPhrase operator &(APSqlConditionAndPhrase left, APSqlWherePhrase right)
		{
			return new APSqlConditionAndPhrase(left, right);
		}


		/// <summary>
		/// Or operator.
		/// </summary>
		/// <param name="left">Left and condition group.</param>
		/// <param name="right">Right where phrase.</param>
		/// <returns>Or condition group.</returns>
		public static APSqlConditionOrPhrase operator |(APSqlConditionAndPhrase left, APSqlWherePhrase right)
		{
			return new APSqlConditionOrPhrase(left, right);
		}


		/// <summary>
		/// Not operator.
		/// </summary>
		/// <param name="phrase">And condition group.</param>
		/// <returns>And condition group.</returns>
		public static APSqlConditionAndPhrase operator !(APSqlConditionAndPhrase phrase)
		{
			phrase.IsNot = !phrase.IsNot;
			return phrase;
		}


		#endregion

	}

}
