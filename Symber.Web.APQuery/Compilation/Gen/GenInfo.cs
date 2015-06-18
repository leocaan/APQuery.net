using System;
using System.Text;
using System.Xml;

namespace Symber.Web.Compilation
{
	internal abstract class GenInfo
	{

		#region [ Fields ]


		public string Name;
		public string TypeName;
		public GenInfo Parent;
		public IAPGenHost GenHost;
		protected Type Type;
		private string _streamName;


		#endregion


		#region [ Properties ]


		public string XPath
		{
			get
			{
				StringBuilder path = new StringBuilder(Name);
				GenInfo info = Parent;
				while (info != null)
				{
					path.Insert(0, info.Name + "/");
					info = info.Parent;
				}
				return path.ToString();
			}
		}


		public string StreamName
		{
			get { return _streamName; }
			set { _streamName = value; }
		}


		#endregion


		#region [ Methods ]


		public virtual object CreateInstance()
		{
			if (Type == null)
				Type = GenHost.GetGenType(TypeName, true);
			return Activator.CreateInstance(Type, true);
		}


		public abstract bool HasDataContent(APGen gen);


		public abstract void ReadGen(APGen gen, string streamName, XmlTextReader reader);


		public abstract void WriteGen(APGen gen, XmlWriter writer);


		public abstract void ReadData(APGen gen, XmlTextReader reader);


		public abstract void WriteData(APGen gen, XmlWriter writer);


		#endregion


		#region [ Protected Methods ]


		protected void ThrowException(string text, XmlTextReader reader)
		{
			throw new APGenException(text, StreamName, reader.LineNumber);
		}


		#endregion

	}
}
