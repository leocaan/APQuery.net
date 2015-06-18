using System;
using System.CodeDom;
using System.Reflection;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Configurates enums in APGen.
	/// </summary>
	public sealed partial class APGenEnumsSection : APGenSection
	{

		#region [ Override Implementation of APGenSection ]


		/// <summary>
		/// Generate code.
		/// </summary>
		/// <param name="gen">The specified APGen generation object.</param>
		public override void Generate(APGen gen)
		{
			if (!Enabled)
				return;



			CodeNamespace cns = gen.GetCodeNamespace(gen.DefaultNamespace);
			cns.Imports.Add(new CodeNamespaceImport("Symber.Web.Data"));



			foreach (APGenEnum em in Enums)
			{
				if (em.Enabled)
				{
					// Create a enum
					string enumComment = String.IsNullOrEmpty(em.Comment) ? em.Name : em.Comment;

					CodeTypeDeclaration enumImp = NewEnum(em.Name, TypeAttributes.Public, enumComment);


					if (em.IsFlags)
						enumImp.CustomAttributes.Add(AttrDecl(TypeRef("Flags")));


					// Fields

					int value = 1;
					foreach (APGenEnumItem item in em.EnumItems)
					{
						string comment = String.IsNullOrEmpty(item.Comment) ? item.Name : item.Comment;
						if (em.IsFlags && item.DefaultValue == null)
							enumImp.Members.Add(NewEnumItem(item.Name, comment, string.Format("0x{0:x2}", value)));
						else
							enumImp.Members.Add(NewEnumItem(item.Name, comment, item.DefaultValue));
						value = value * 2;
					}
					cns.Types.Add(enumImp);


					// Create dictionary

					CodeTypeDeclaration dicImp = NewClass(em.Name + "APEnumDictionary", TypeAttributes.Public, enumComment + " APEnumDictionary");
					dicImp.IsPartial = true;
					CodeMemberField field = NewMemberField("Dictionary", TypeTRef("APEnumDictionary", TypeRef(em.Name)), MemberAttributes.Public | MemberAttributes.Static, null);
					CodeMethodInvokeExpression create = MethodInvoke(new CodeTypeReferenceExpression(TypeTRef("APEnumDictionary", TypeRef(em.Name))), "Create");
					foreach (APGenEnumItem item in em.EnumItems)
					{
						string comment = String.IsNullOrEmpty(item.Comment) ? item.Name : item.Comment;
						CodeMemberField nameField = NewMemberField(item.Name + "Name", TypeRef(typeof(string)), MemberAttributes.Public | MemberAttributes.Const, comment);
						nameField.InitExpression = Const(item.DictionaryName == "" ? item.Name : item.DictionaryName);
						dicImp.Members.Add(nameField);
						create.Parameters.Add(New(new CodeTypeReference("KeyValuePair", TypeRef(em.Name), TypeRef(typeof(string))), PropRef(TypeRefExp(em.Name), item.Name), FieldRef(item.Name + "Name") /*Const(item.DictionaryName)*/));
					}
					field.InitExpression = create;
					dicImp.Members.Add(field);
					cns.Types.Add(dicImp);
				}
			}
		}


		#endregion

	}
}
