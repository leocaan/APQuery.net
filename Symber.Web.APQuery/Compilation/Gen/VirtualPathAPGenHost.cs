using System.IO;
using System.Web;

namespace Symber.Web.Compilation
{
	class VirtualPathAPGenHost : APGenHost
	{

		#region [ Implementation of IAPGenHost ]


		public override string GetStreamName(string path)
		{
			return MapPath(path);
		}


		#endregion


		#region [ Methods ]


		public string MapPath(string virtualPath)
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.Server.MapPath(virtualPath);
			}
			else if (HttpRuntime.AppDomainAppVirtualPath != null && virtualPath.StartsWith(HttpRuntime.AppDomainAppVirtualPath))
			{
				if (virtualPath == HttpRuntime.AppDomainAppVirtualPath)
					return HttpRuntime.AppDomainAppPath;
				return Path.Combine(HttpRuntime.AppDomainAppPath, virtualPath.Substring(HttpRuntime.AppDomainAppVirtualPath.Length));
			}

			return virtualPath;
		}


		#endregion

	}
}
