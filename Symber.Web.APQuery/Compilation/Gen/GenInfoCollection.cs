using System;
using System.Collections;
using System.Collections.Specialized;

namespace Symber.Web.Compilation
{
	internal class GenInfoCollection : NameObjectCollectionBase
	{

		#region [ Constructors ]


		public GenInfoCollection()
			: base (StringComparer.Ordinal)
		{
		}


		#endregion


		#region [ Properties ]


		public ICollection AllKeys
		{
			get { return Keys; }
		}


		public GenInfo this[string name]
		{
			get { return (GenInfo)BaseGet(name); }
			set { BaseSet(name, value); }
		}


		public GenInfo this[int index]
		{
			get { return (GenInfo)BaseGet(index); }
			set { BaseSet(index, value); }
		}


		#endregion


		#region [ Methods ]


		public void Add(string name, GenInfo genInfo)
		{
			BaseAdd(name, genInfo);
		}


		public void Clear()
		{
			BaseClear();
		}


		public string GetKey(int index)
		{
			return BaseGetKey(index);
		}


		public void Remove(string name)
		{
			BaseRemove(name);
		}


		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}


		#endregion

	}
}
