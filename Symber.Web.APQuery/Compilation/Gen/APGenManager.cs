using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate manager.
	/// </summary>
	public static class APGenManager
	{

		#region [ Static Fields ]


		private static APGenFactory factory = new APGenFactory();
		private static List<APGen> gens;
		private static bool initData;
		private static bool syncData;


		#endregion


		#region [ Static Methods ]


		/// <summary>
		/// Synchronize database and initialize data.
		/// <param name="pathOfFolderOrFile">
		/// Path of folder or file.
		/// 1. If the value is null, find .apgen file in '~/App_Code' virtual path.
		/// 2. If the value is suffix '.apgen' file, find the file.
		/// 3. If the value is folder, find .apgen file in the folder.
		/// </param>
		/// </summary>
		public static void SyncAndInitData(string pathOfFolderOrFile = null)
		{
			if (!initData)
			{
				foreach (APGen gen in APGenManager.Gens(pathOfFolderOrFile))
				{
					gen.SyncData();
					gen.InitData();
				}
			}
			initData = true;
		}


		#endregion


		#region [ Methods ]


		internal static List<APGen> Gens(string pathOfFolderOrFile)
		{
			if (gens == null)
			{
				gens = new List<APGen>();
				if (pathOfFolderOrFile == null)
				{
					pathOfFolderOrFile = HttpContext.Current.Server.MapPath("~");
				}
				else if (pathOfFolderOrFile.StartsWith("~"))
				{
					pathOfFolderOrFile = HttpContext.Current.Server.MapPath(pathOfFolderOrFile);
				}
				
				if (pathOfFolderOrFile.EndsWith(".apgen"))
				{
					gens.Add(OpenGenDocument(pathOfFolderOrFile));
				}
				else
				{
					foreach (FileInfo fi in new DirectoryInfo(pathOfFolderOrFile).GetFiles("*.apgen", SearchOption.AllDirectories))
					{
						gens.Add(OpenGenDocument(fi.FullName));
					}
				}
			}

			return gens;
		}


		/// <summary>
		/// Open generate document.
		/// </summary>
		/// <param name="path">The path of the document.</param>
		/// <returns>APGen object.</returns>
		public static APGen OpenGenDocument(string path)
		{
			APGen gen;
			if (path.StartsWith("~"))
				gen = factory.Create(typeof(VirtualPathAPGenHost));
			else
				gen = factory.Create(typeof(APGenHost));

			gen.Init(path);
			return gen;
		}


		#endregion

	}
}
