using System;
using System.IO;

namespace Symber.Web.Compilation
{
	class APGenHost : IAPGenHost
	{

		#region [ Implementation of IAPGenHost ]


		public void Init()
		{
		}


		public string DecryptSection(string encryptedXml)
		{
			return encryptedXml;
		}


		public string EncryptSection(string rowXml)
		{
			return rowXml;
		}


		public virtual Type GetGenType(string typeName, bool throwOnError)
		{
			Type type = Type.GetType(typeName);

			if (type == null && throwOnError)
				throw new APGenException(APResource.GetString(APResource.APGen_TypeNotFound, typeName));
			return type;
		}


		public virtual string GetGenTypeName(Type type)
		{
			return type.AssemblyQualifiedName;
		}


		public virtual string GetStreamName(string path)
		{
			return path;
		}


		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (!File.Exists(streamName))
				throw new APGenException(APResource.APGen_FileNotFound);
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}


		#endregion

	}
}
