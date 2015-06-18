using System.CodeDom;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configurates namespaces in APGen.
	/// </summary>
	public sealed partial class APGenNamespacesSection : APGenSection
	{

		#region [ Override Implementation of APGenSection ]


		/// <summary>
		/// Generate code.
		/// </summary>
		/// <param name="gen">The specified APGen generation object.</param>
		public override void Generate(APGen gen)
		{
			CodeNamespace cns = gen.GetCodeNamespace(gen.DefaultNamespace);

			foreach (APGenNamespace ns in Namespaces)
			{
				cns.Imports.Add(new CodeNamespaceImport(ns.Import));
			}
		}


		#endregion

	}
}
