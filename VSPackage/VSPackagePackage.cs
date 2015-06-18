using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Symber.Web.Compilation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Symber.Web.APQuery.VSPackage
{
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideAutoLoad(UIContextGuids80.SolutionExists)]
	[Guid(GuidList.guidAPGenPkgString)]
	public sealed class VSPackagePackage : Package
	{
		private DTE2 _dte;
		private string _fullPath;
		private string _fileName;
		private OleMenuCommand _cmdGenerate;
		private EnvDTE.Project _project;

		public VSPackagePackage()
		{
		}


		protected override void Initialize()
		{
			base.Initialize();

			_dte = GetService(typeof(DTE)) as DTE2;

			OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if (null != mcs)
			{
				CommandID menuCommandID = new CommandID(GuidList.guidAPGenCmdSet, (int)PkgCmdIDList.cmdidMyCommand);
				_cmdGenerate = new OleMenuCommand(MenuItemCallback, menuCommandID);
				//_cmdGenerate. = Resources.Resources.APGen_Generate_MenuItem_Text;
				_cmdGenerate.BeforeQueryStatus += BeforeButtonClicked;
				mcs.AddCommand(_cmdGenerate);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_cmdGenerate != null)
				_cmdGenerate.BeforeQueryStatus -= BeforeButtonClicked;

			base.Dispose(disposing);
		}


		void BeforeButtonClicked(object sender, EventArgs e)
		{
			OleMenuCommand button = sender as OleMenuCommand;
			button.Visible = false;


			GetSelectedItemPath();

			if (string.IsNullOrEmpty(_fullPath))
				return;

			if (File.Exists(_fullPath))
			{
				if (Path.GetExtension(_fullPath) == ".apgen")
					button.Visible = true;
			}
		}


		private void MenuItemCallback(object sender, EventArgs e)
		{
			//DynamicTypeService typeService;
			//IVsSolution solution;
			//using (var serviceProvider = new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)_dte.DTE))
			//{
			//	typeService = (DynamicTypeService)serviceProvider.GetService(typeof(DynamicTypeService));
			//	solution = (IVsSolution)serviceProvider.GetService(typeof(SVsSolution));
			//}


			//IVsHierarchy vsHierarchy;
			//var hr = solution.GetProjectOfUniqueName(_dte.SelectedItems.Item(1).ProjectItem.ContainingProject.UniqueName, out vsHierarchy);

			//if (hr != ProjectExtensions.S_OK)
			//{
			//	throw Marshal.GetExceptionForHR(hr);
			//}

			//var resolver = typeService.GetTypeResolutionService(vsHierarchy);




			//ITypeDiscoveryService discovery = typeService.GetTypeDiscoveryService(vsHierarchy);

			//IDictionary<string, Type> availableTypes = new Dictionary<string, Type>();
			//foreach (Type type in discovery.GetTypes(typeof(object), true))
			//{
			//	// We will never allow non-public types selection, as it's terrible practice.
			//	if (type.IsPublic)
			//	{
			//		if (!availableTypes.ContainsKey(type.FullName))
			//		{
			//			availableTypes.Add(type.FullName, type);
			//		}
			//	}
			//}


			string newFileFullPath = _fullPath + ".cs";
			using (Stream stream = File.Open(newFileFullPath, FileMode.OpenOrCreate & FileMode.Truncate))
			{
				using (StreamWriter writer = new StreamWriter(stream))
				{
					CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");
					APGen gen = APGenManager.OpenGenDocument(_fullPath);

					provider.GenerateCodeFromCompileUnit(gen.Generate(), writer, null);
				}
			}

			var newItem = _project.ProjectItems.AddFromFile(_fullPath + ".cs");

			MessageBox.Show(String.Format(Resources.APGen_General_Success, _fileName));

			newItem.Open().Visible = true;
		}


		private void GetSelectedItemPath()
		{
			try
			{
				var items = (Array)_dte.ToolWindows.SolutionExplorer.SelectedItems;
				foreach (UIHierarchyItem selItem in items)
				{
					var item = selItem.Object as ProjectItem;

					if (item != null && item.Properties != null)
					{
						_project = item.ContainingProject;
						_fullPath = item.Properties.Item("FullPath").Value.ToString();
						_fileName = item.Properties.Item("FileName").Value.ToString();
					}
				}
			}
			catch { /* Something weird is happening. Ignore this and return null */}
		}


		private static IEnumerable<CodeElement> FindClassesInCodeModel(CodeElements codeElements)
		{
			foreach (CodeElement codeElement in codeElements)
			{
				if (codeElement.Kind == vsCMElement.vsCMElementClass)
				{
					yield return codeElement;
				}

				foreach (var element in FindClassesInCodeModel(codeElement.Children))
				{
					yield return element;
				}
			}
		}

	}
}
