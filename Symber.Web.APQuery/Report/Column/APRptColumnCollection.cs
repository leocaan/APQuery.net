using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Symber.Web.Report
{

	/// <summary>
	/// A collection of APRptColumn.
	/// </summary>
	[Serializable]
	public class APRptColumnCollection : Dictionary<string, APRptColumn>, IEnumerable<APRptColumn>
	{

		#region [ Inner Class APRptColumnEnumerator ]

		/// <summary>
		/// Enumerator.
		/// </summary>
		[Serializable]
		public struct APRptColumnEnumerator : IEnumerator<APRptColumn>, IDisposable, IEnumerator
		{

			#region [ Fields ]


			private APRptColumnCollection _host;
			private APRptColumnCollection.KeyCollection.Enumerator _hostEnumerator;


			#endregion


			#region [ Constructors ]


			internal APRptColumnEnumerator(APRptColumnCollection host)
			{
				_host = host;
				_hostEnumerator = host.Keys.GetEnumerator();
			}


			#endregion


			#region [ Methods ]


			/// <summary>
			/// Dispose.
			/// </summary>
			public void Dispose()
			{
				_host = null;
				_hostEnumerator.Dispose();
			}


			/// <summary>
			/// Move Next.
			/// </summary>
			/// <returns></returns>
			public bool MoveNext()
			{
				return _hostEnumerator.MoveNext();
			}


			/// <summary>
			/// Current.
			/// </summary>
			public APRptColumn Current
			{
				get { return _host[_hostEnumerator.Current]; }
			}


			/// <summary>
			/// Current.
			/// </summary>
			object IEnumerator.Current
			{
				get { return Current; }
			}


			/// <summary>
			/// Reset.
			/// </summary>
			void IEnumerator.Reset()
			{
				((IEnumerator)_hostEnumerator).Reset();
			}


			#endregion

		}

		#endregion


		#region [ Fields ]


		private string _id;
		private string _title;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		public APRptColumnCollection()
			: base()
		{
		}



		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected APRptColumnCollection(SerializationInfo info, StreamingContext context)
			:base(info, context)
		{

		}


		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		/// <param name="collection">Other collection.</param>
		public APRptColumnCollection(APRptColumnCollection collection)
			: base(collection)
		{
		}


		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		/// <param name="comparer">String comparer.</param>
		public APRptColumnCollection(StringComparer comparer)
			: base(comparer)
		{
		}


		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		/// <param name="capacity">Default capacity of collection.</param>
		public APRptColumnCollection(int capacity)
			: base(capacity)
		{
		}


		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		/// <param name="collection">Other collection.</param>
		/// <param name="comparer">String comparer.</param>
		public APRptColumnCollection(APRptColumnCollection collection, StringComparer comparer)
			: base(collection, comparer)
		{
		}


		/// <summary>
		/// Create a new APRptColumnCollection.
		/// </summary>
		/// <param name="capacity">Default capacity of collection.</param>
		/// <param name="comparer">String comparer.</param>
		public APRptColumnCollection(int capacity, StringComparer comparer)
			: base(capacity, comparer)
		{
		}


		#endregion


		#region [ Properties ]


		/// <summary>
		/// ID of the collection.
		/// </summary>
		public string ID
		{
			get { return _id; }
			set { _id = value; }
		}


		/// <summary>
		/// Title of the collection.
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Add a column to collection.
		/// </summary>
		/// <param name="column">Column.</param>
		public void Add(APRptColumn column)
		{
			if (!ContainsKey(column.ID))
				Add(column.ID, column);
		}


		/// <summary>
		/// Gets a list of APRptColumn by specified ids.
		/// </summary>
		/// <param name="columnIds">The ids.</param>
		/// <returns>The list.</returns>
		public List<APRptColumn> SelectAvailableColumns(params string[] columnIds)
		{
			List<APRptColumn> list = new List<APRptColumn>();
			foreach (string id in columnIds)
			{
				list.Add(this[id]);
			}
			return list;
		}


		#endregion


		#region [ Implementation of IEnumerable<APRptColumn> ]


		/// <summary>
		/// Get enumerator.
		/// </summary>
		/// <returns>IEnumerator.</returns>
		public new IEnumerator<APRptColumn> GetEnumerator()
		{
			return new APRptColumnEnumerator(this);
		}


		/// <summary>
		/// GetObjectData.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}


		#endregion

	}

}
