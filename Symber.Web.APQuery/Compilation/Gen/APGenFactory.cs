using System;

namespace Symber.Web.Compilation
{
	internal class APGenFactory
	{

		#region [ Static Field ]


		public static IAPGenHost CurrentHost;


		#endregion


		#region [ Methods ]


		public APGen Create(Type hostType)
		{
			CurrentHost = Activator.CreateInstance(hostType, true) as IAPGenHost;
			return new APGen(CurrentHost);
		}


		#endregion

	}
}
