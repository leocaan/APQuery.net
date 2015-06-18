using System.CodeDom.Compiler;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.Compilation;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Gen builde provider.
	/// </summary>
	[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web | BuildProviderAppliesTo.Code)]
	public class APGenBuildProvider : BuildProvider
	{

		#region [ Global Register ]


		/// <summary>
		/// Global code run way early in the ASP.NET pipeline as an application starts up. 
		/// I mean way early, even before Application_Start. 
		/// First register "APGenBuildProvider" to compily ".apgen" code.
		/// </summary>
		public static void Register()
		{
			//
			// Add ".apgen" file build provider
			//

			System.Web.Compilation.BuildProvider.RegisterBuildProvider(".apgen", typeof(Symber.Web.Compilation.APGenBuildProvider));
		}


		#endregion


		#region [ Static Fields ]


		internal static int getProfileMethodCount;
		internal static bool flag;


		#endregion


		#region [ Constructors ]


		/// <summary>
		/// Create a new APGenBuildProvider.
		/// </summary>
		public APGenBuildProvider()
		{
		}


		#endregion


		#region [ Methods ]


		/// <summary>
		/// Generate the code from .apgen file.
		/// </summary>
		/// <param name="assemblyBuilder">AssemblyBuilder</param>
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			if (!flag && BuildManager.CodeAssemblies != null && getProfileMethodCount > 0)
			{
				getProfileMethodCount = 0;
				flag = true;
			}

			string path = Path.Combine(
				HttpRuntime.AppDomainAppPath,
				VirtualPath.Substring(HttpRuntime.AppDomainAppVirtualPath.Length + (HttpRuntime.AppDomainAppVirtualPath.Length == 1 ? 0 : 1)));

			using (TextWriter writer = assemblyBuilder.CreateCodeFile(this))
			{
				APGen gen = APGenManager.OpenGenDocument(path);
				CodeDomProvider provider = assemblyBuilder.CodeDomProvider;
				provider.GenerateCodeFromCompileUnit(gen.Generate(), writer, null);
				gen.SyncData();
			}
		}


		#endregion

	}
}
