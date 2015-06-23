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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Symber.Web.APQuery.VSPackage
{
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideAutoLoad(UIContextGuids80.SolutionExists)]
	[Guid(GuidList.guidPkgString)]
	public sealed class VSPackagePackage : Package
	{
		private DTE2 _dte;
		private string _fullPath;
		private string _fileName;
		private OleMenuCommand _cmdGenerate;
		private OleMenuCommand _cmdNewGen;
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
				_cmdGenerate = new OleMenuCommand(ItemNodeCallback, new CommandID(GuidList.guidCmdSet, (int)PkgCmdIDList.cmdidGenerate));
				_cmdGenerate.BeforeQueryStatus += BeforeItemNodeClicked;
				mcs.AddCommand(_cmdGenerate);


				_cmdNewGen = new OleMenuCommand(FolderNodeCallback, new CommandID(GuidList.guidCmdSet, (int)PkgCmdIDList.cmdidNewGen));
				_cmdNewGen.BeforeQueryStatus += BeforeFolderNodeClicked;
				mcs.AddCommand(_cmdNewGen);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_cmdGenerate != null)
				_cmdGenerate.BeforeQueryStatus -= BeforeItemNodeClicked;

			base.Dispose(disposing);
		}


		void BeforeItemNodeClicked(object sender, EventArgs e)
		{
			OleMenuCommand button = sender as OleMenuCommand;
			button.Visible = false;


			GetSelectedItemPath();

			if (string.IsNullOrEmpty(_fullPath))
				return;

			else if (File.Exists(_fullPath))
			{
				if (Path.GetExtension(_fullPath) == ".apgen")
					button.Visible = true;
			}
		}


		void BeforeFolderNodeClicked(object sender, EventArgs e)
		{
			OleMenuCommand button = sender as OleMenuCommand;
			button.Visible = false;


			GetSelectedItemPath();

			if (string.IsNullOrEmpty(_fullPath))
				return;

			if (Directory.Exists(_fullPath))
			{
				button.Visible = true;
			}
		}


		private void ItemNodeCallback(object sender, EventArgs e)
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
			using (Stream stream = File.Open(newFileFullPath, FileMode.Create))
			{
				StreamWriter writer = new StreamWriter(stream);
				CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");
				APGen gen = APGenManager.OpenGenDocument(_fullPath);

				provider.GenerateCodeFromCompileUnit(gen.Generate(), writer, null);
			}

			var newItem = _project.ProjectItems.AddFromFile(newFileFullPath);
			newItem.Open().Visible = true;
		}


		private void FolderNodeCallback(object sender, EventArgs e)
		{
			string newFileFullPath = _fullPath + "Business.apgen";

			CopyResourceFile("Business.apgen", newFileFullPath);
			InsureSchema();

			var newItem = _project.ProjectItems.AddFromFile(newFileFullPath);
			newItem.Open().Visible = true;
		}


		private void InsureSchema()
		{
			var rootPath = Path.GetDirectoryName(_project.FullName);
			var schemaPath = Path.Combine(rootPath, "xml.schema.definition");


			if (!Directory.Exists(schemaPath))
				Directory.CreateDirectory(schemaPath);
			_project.ProjectItems.AddFromDirectory(schemaPath);


			var schemaName = Path.Combine(schemaPath, "apgen.xsd");
			CopyResourceFile("apgen.xsd", schemaName);
			_project.ProjectItems.AddFromFile(schemaName);

			schemaName = Path.Combine(schemaPath, "apgen.xsx");
			CopyResourceFile("apgen.xsx", schemaName);
			_project.ProjectItems.AddFromFile(schemaName);
		}

		private void CopyResourceFile(string resourceName, string path)
		{
			var ns = "Symber.Web.APQuery.VSPackage.Resources.";
			var resource = GetType().Assembly.GetManifestResourceStream(ns + resourceName);

			string content = new StreamReader(resource).ReadToEnd();
		
			using (Stream stream = File.Open(path, FileMode.Create))
			{
				StreamWriter writer = new StreamWriter(stream);

				writer.Write(content);
				writer.Flush();
				writer.Close();
			}
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
