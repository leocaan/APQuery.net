namespace Symber.Web.APQuery.VSPackage
{
	using EnvDTE;
	using Microsoft.VisualStudio.Shell;
	using Microsoft.VisualStudio.Shell.Interop;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Runtime.InteropServices;
	using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

	internal static class StringExtensions
	{
		public static bool EqualsIgnoreCase(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}
	}

	internal static class ProjectExtensions
	{
		public const int S_OK = 0;
		public const string WebApplicationProjectTypeGuid = "{349C5851-65DF-11DA-9384-00065B846F21}";
		public const string WebSiteProjectTypeGuid = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";

		public static string GetProjectDir(this Project project)
		{
			return project.GetPropertyValue<string>("FullPath");
		}

		public static string GetTargetDir(this Project project)
		{
			var fullPath = project.GetProjectDir();
			string outputPath;

			outputPath = project.GetConfigurationPropertyValue<string>("OutputPath");

			return Path.Combine(fullPath, outputPath);
		}


		public static bool IsWebProject(this Project project)
		{
			return project.GetProjectTypes().Any(
					  g => g.EqualsIgnoreCase(WebApplicationProjectTypeGuid)
							|| g.EqualsIgnoreCase(WebSiteProjectTypeGuid));
		}

		public static string GetRootNamespace(this Project project)
		{
			return project.GetPropertyValue<string>("RootNamespace");
		}

		private static T GetPropertyValue<T>(this Project project, string propertyName)
		{
			var property = project.Properties.Item(propertyName);

			if (property == null)
			{
				return default(T);
			}

			return (T)property.Value;
		}

		private static T GetConfigurationPropertyValue<T>(this Project project, string propertyName)
		{
			var property = project.ConfigurationManager.ActiveConfiguration.Properties.Item(propertyName);

			if (property == null)
			{
				return default(T);
			}

			return (T)property.Value;
		}

		private static IEnumerable<string> GetProjectTypes(this Project project)
		{
			IVsSolution solution;
			using (var serviceProvider = new ServiceProvider((IServiceProvider)project.DTE))
			{
				solution = (IVsSolution)serviceProvider.GetService(typeof(IVsSolution));
			}

			IVsHierarchy hierarchy;
			var hr = solution.GetProjectOfUniqueName(project.UniqueName, out hierarchy);

			if (hr != S_OK)
			{
				Marshal.ThrowExceptionForHR(hr);
			}

			string projectTypeGuidsString;

			var aggregatableProject = (IVsAggregatableProject)hierarchy;
			hr = aggregatableProject.GetAggregateProjectTypeGuids(out projectTypeGuidsString);

			if (hr != S_OK)
			{
				Marshal.ThrowExceptionForHR(hr);
			}

			return projectTypeGuidsString.Split(';');
		}
	}
}
