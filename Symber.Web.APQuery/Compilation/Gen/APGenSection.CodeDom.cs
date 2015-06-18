using System;
using System.CodeDom;
using System.Reflection;

namespace Symber.Web.Compilation
{
	/// <summary>
	/// Generate config section.
	/// </summary>
	public abstract partial class APGenSection
	{

		internal static string[] NewLine = new string[] { System.Environment.NewLine };


		internal static CodeBinaryOperatorExpression Add(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.Add, exp2); }
		internal static CodeStatementCollection AddStatement(CodeStatementCollection statements, params CodeExpression[] exps) { foreach (CodeExpression exp in exps) statements.Add(exp); return statements; }
		internal static CodeBinaryOperatorExpression And(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.BooleanAnd, exp2); }
		internal static CodeAttributeArgument AttrArg() { return new CodeAttributeArgument(); }
		internal static CodeAttributeArgument AttrArg(CodeExpression val) { return new CodeAttributeArgument(val); }
		internal static CodeAttributeArgument AttrArg(string name, CodeExpression val) { return new CodeAttributeArgument(name, val); }
		internal static CodeAttributeDeclaration AttrDecl(string type) { return new CodeAttributeDeclaration(type); }
		internal static CodeAttributeDeclaration AttrDecl(CodeTypeReference type, params CodeAttributeArgument[] args) { return new CodeAttributeDeclaration(type, args); }


		internal static CodeBaseReferenceExpression Base() { return new CodeBaseReferenceExpression(); }


		internal static CodeCastExpression Cast(string type, CodeExpression exp) { return new CodeCastExpression(type, exp); }
		internal static CodeCastExpression Cast(Type type, CodeExpression exp) { return new CodeCastExpression(type, exp); }
		internal static CodeCastExpression Cast(CodeTypeReference type, CodeExpression exp) { return new CodeCastExpression(type, exp); }
		internal static CodeCatchClause Catch() { return new CodeCatchClause(); }
		internal static CodeCommentStatement Comment(string comment) { return new CodeCommentStatement(comment); }
		internal static CodeCommentStatement Comment(string comment, bool doc) { return new CodeCommentStatement(comment, doc); }
		internal static CodePrimitiveExpression Const(object value) { return new CodePrimitiveExpression(value); }


		internal static CodeBinaryOperatorExpression Equals(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.IdentityEquality, exp2); }
		internal static CodeBinaryOperatorExpression EqualsValue(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.ValueEquality, exp2); }
		internal static CodeExpressionStatement Eval(CodeExpression exp) { return new CodeExpressionStatement(exp); }
		internal static CodeEventReferenceExpression EventRef(string name) { return new CodeEventReferenceExpression(new CodeThisReferenceExpression(), name); }
		internal static CodeEventReferenceExpression EventRef(CodeExpression target, string name) { return new CodeEventReferenceExpression(target, name); }


		internal static CodeFieldReferenceExpression FieldRef(string name) { return new CodeFieldReferenceExpression(null, name); }
		internal static CodeFieldReferenceExpression FieldRef(CodeExpression target, string name) { return new CodeFieldReferenceExpression(target, name); }
		internal static CodeIterationStatement For(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements) { return new CodeIterationStatement(initStatement, testExpression, incrementStatement, statements); }


		internal static CodeBinaryOperatorExpression GreaterThan(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.GreaterThan, exp2); }


		internal static CodeConditionStatement If(CodeExpression cond, params CodeStatement[] statements) { return new CodeConditionStatement(cond, statements); }
		internal static CodeBinaryOperatorExpression Inequals(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.IdentityInequality, exp2); }
		internal static CodeIndexerExpression IndexerRef(CodeExpression target, CodeExpression param) { return new CodeIndexerExpression(target, param); }
		internal static CodeIndexerExpression IndexerRef(CodeExpression param) { return new CodeIndexerExpression(new CodeThisReferenceExpression(), param); }
		internal static CodeBinaryOperatorExpression IsNull(CodeExpression exp) { return new CodeBinaryOperatorExpression(exp, CodeBinaryOperatorType.IdentityEquality, Const(null)); }


		internal static CodeBinaryOperatorExpression LessThan(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.LessThan, exp2); }
		internal static CodeAssignStatement Let(CodeExpression exp, CodeExpression value) { return new CodeAssignStatement(exp, value); }
		internal static CodeVariableReferenceExpression Local(string name) { return new CodeVariableReferenceExpression(name); }


		internal static CodeMethodInvokeExpression MethodInvoke(string name, params CodeExpression[] parameters) { return new CodeMethodInvokeExpression(null, name, parameters); }
		internal static CodeMethodInvokeExpression MethodInvoke(CodeMethodReferenceExpression methodRef, params CodeExpression[] parameters) { return new CodeMethodInvokeExpression(methodRef, parameters); }
		internal static CodeMethodInvokeExpression MethodInvoke(CodeExpression target, string name, params CodeExpression[] parameters) { return new CodeMethodInvokeExpression(target, name, parameters); }
		internal static CodeMethodInvokeExpression MethodInvoke(string target, string name, params CodeExpression[] parameters) { return new CodeMethodInvokeExpression(Local(target), name, parameters); }
		internal static CodeMethodReferenceExpression MethodRef(string name, CodeTypeReference type) { CodeMethodReferenceExpression a = new CodeMethodReferenceExpression(); a.MethodName = name; a.TypeArguments.Add(type); return a; }
		internal static CodeMethodReferenceExpression MethodRef(CodeExpression target, string name, CodeTypeReference type) { CodeMethodReferenceExpression a = new CodeMethodReferenceExpression(); a.TargetObject = target; a.MethodName = name; a.TypeArguments.Add(type); return a; }


		internal static CodeExpression New(Type type, params CodeExpression[] parameters) { return new CodeObjectCreateExpression(type, parameters); }
		internal static CodeExpression New(string type, params CodeExpression[] parameters) { return new CodeObjectCreateExpression(TypeRef(type), parameters); }
		internal static CodeExpression New(CodeTypeReference type, params CodeExpression[] parameters) { return new CodeObjectCreateExpression(type, parameters); }
		internal static CodeArrayCreateExpression NewArray(Type type, params CodeExpression[] parameters) { return new CodeArrayCreateExpression(type, parameters); }
		internal static CodeArrayCreateExpression NewArray(string type, params CodeExpression[] parameters) { return new CodeArrayCreateExpression(type, parameters); }
		internal static CodeArrayCreateExpression NewArray(CodeTypeReference type, params CodeExpression[] parameters) { return new CodeArrayCreateExpression(type, parameters); }
		internal static CodeArrayIndexerExpression ArrayIndex(CodeExpression target, params CodeExpression[] indeces) { return new CodeArrayIndexerExpression(target, indeces); }

		internal static CodeBinaryOperatorExpression NotNull(CodeExpression exp) { return new CodeBinaryOperatorExpression(exp, CodeBinaryOperatorType.IdentityInequality, Const(null)); }
		internal static CodeBinaryOperatorExpression NotEqualsValue(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.IdentityInequality, exp2); }
		internal static CodeExpression Null() { return Const(null); }


		internal static CodeBinaryOperatorExpression Or(CodeExpression exp1, CodeExpression exp2) { return new CodeBinaryOperatorExpression(exp1, CodeBinaryOperatorType.BooleanOr, exp2); }


		internal static CodeParameterDeclarationExpression Param(string type, string name) { return new CodeParameterDeclarationExpression(type, name); }
		internal static CodeParameterDeclarationExpression Param(Type type, string name) { return new CodeParameterDeclarationExpression(type, name); }
		internal static CodeParameterDeclarationExpression Param(CodeTypeReference type, string name) { return new CodeParameterDeclarationExpression(type, name); }
		internal static CodeArgumentReferenceExpression ParamRef(string name) { return new CodeArgumentReferenceExpression(name); }
		internal static CodePropertyReferenceExpression PropRef(string name) { return new CodePropertyReferenceExpression(null, name); }
		internal static CodePropertyReferenceExpression PropRef(CodeExpression target, string name) { return new CodePropertyReferenceExpression(target, name); }


		internal static CodeMethodReturnStatement Return(CodeExpression exp) { return new CodeMethodReturnStatement(exp); }
		internal static CodeMethodReturnStatement ReturnNull() { return new CodeMethodReturnStatement(Const(null)); }
		internal static CodeMethodReturnStatement ReturnTrue() { return new CodeMethodReturnStatement(Const(true)); }
		internal static CodeMethodReturnStatement ReturnFalse() { return new CodeMethodReturnStatement(Const(false)); }


		internal static CodeSnippetExpression Special(string value) { return new CodeSnippetExpression(value); }


		internal static CodeThisReferenceExpression This() { return new CodeThisReferenceExpression(); }
		internal static CodeThrowExceptionStatement Throw(CodeExpression exp) { return new CodeThrowExceptionStatement(exp); }
		internal static CodeThrowExceptionStatement Throw() { return new CodeThrowExceptionStatement(); }
		internal static CodeTryCatchFinallyStatement Try() { return new CodeTryCatchFinallyStatement(); }
		internal static CodeTypeOfExpression Typeof(string type) { return new CodeTypeOfExpression(type); }
		internal static CodeTypeOfExpression Typeof(Type type) { return new CodeTypeOfExpression(type); }
		internal static CodeTypeOfExpression Typeof(CodeTypeReference type) { return new CodeTypeOfExpression(type); }
		internal static CodeTypeReference TypeRef(Type type) { return new CodeTypeReference(type); }
		internal static CodeTypeReference TypeRef(string name) { return new CodeTypeReference(name); }
		internal static CodeTypeReference TypeTRef(string name, CodeTypeReference T) { return new CodeTypeReference(name, T); }
		internal static CodeTypeReference TypeTRef(string name, params CodeTypeReference[] TArguments) { return new CodeTypeReference(name, TArguments); }
		internal static CodeTypeReferenceExpression TypeRefExp(Type type) { return new CodeTypeReferenceExpression(type); }
		internal static CodeTypeReferenceExpression TypeRefExp(string type) { return new CodeTypeReferenceExpression(type); }
		internal static CodeTypeReferenceExpression TypeRefExp(CodeTypeReference type) { return new CodeTypeReferenceExpression(type); }


		internal static CodePropertySetValueReferenceExpression Value() { return new CodePropertySetValueReferenceExpression(); }
		internal static CodeVariableDeclarationStatement VarDecl(Type type, string name, CodeExpression init) { return new CodeVariableDeclarationStatement(type, name, init); }
		internal static CodeVariableDeclarationStatement VarDecl(string type, string name, CodeExpression init) { return new CodeVariableDeclarationStatement(type, name, init); }
		internal static CodeVariableDeclarationStatement VarDecl(CodeTypeReference type, string name, CodeExpression init) { return new CodeVariableDeclarationStatement(type, name, init); }


		internal static void CreateCommont(CodeCommentStatementCollection statements, string comment)
		{
			if (comment == null)
				return;

			statements.Add(new CodeCommentStatement("<summary>", true));
			if (comment == String.Empty)
			{
				statements.Add(new CodeCommentStatement(String.Empty, true));
			}
			else
			{
				string[] ss = comment.Split(NewLine, StringSplitOptions.None);
				foreach (string s in ss)
				{
					statements.Add(new CodeCommentStatement(s, true));
				}
			}
			statements.Add(new CodeCommentStatement("</summary>", true));
		}


		internal static CodeTypeDeclaration NewEnum(string name, TypeAttributes typeAttributes, string comment)
		{
			CodeTypeDeclaration em = new CodeTypeDeclaration(name);
			em.TypeAttributes = typeAttributes;
			em.IsEnum = true;
			CreateCommont(em.Comments, comment);
			return em;
		}


		internal static CodeMemberField NewEnumItem(string name, string comment)
		{
			CodeMemberField item = new CodeMemberField();
			item.Name = name;
			CreateCommont(item.Comments, comment);
			return item;
		}


		internal static CodeMemberField NewEnumItem(string name, string comment, string value)
		{
			CodeMemberField item = new CodeMemberField();
			item.Name = name;
			if (!String.IsNullOrEmpty(value))
				item.InitExpression = Local(value);
			CreateCommont(item.Comments, comment);
			return item;
		}


		internal static CodeTypeDeclaration NewClass(string name, TypeAttributes typeAttributes, string comment)
		{
			CodeTypeDeclaration cls = new CodeTypeDeclaration(name);
			cls.TypeAttributes = typeAttributes;
			cls.IsClass = true;
			CreateCommont(cls.Comments, comment);
			return cls;
		}


		internal static CodeMemberField NewMemberField(string name, CodeTypeReference type, MemberAttributes attribute, string comment)
		{
			CodeMemberField field = new CodeMemberField();
			field.Name = name;
			field.Type = type;
			field.Attributes = attribute;
			CreateCommont(field.Comments, comment);
			return field;
		}


		internal static CodeMemberProperty NewMemberProperty(string name, CodeTypeReference type, MemberAttributes attribute, string comment)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = type;
			property.Attributes = attribute;
			CreateCommont(property.Comments, comment);
			return property;
		}


		internal static CodeConstructor NewCtor(MemberAttributes attribute, string comment, params CodeParameterDeclarationExpression[] parameters)
		{
			CodeConstructor ctor = new CodeConstructor();
			ctor.Attributes = attribute;
			CreateCommont(ctor.Comments, comment);
			foreach (CodeParameterDeclarationExpression parameter in parameters)
				ctor.Parameters.Add(parameter);
			return ctor;
		}


		internal static CodeTypeConstructor NewStaticCtor(string comment)
		{
			CodeTypeConstructor ctor = new CodeTypeConstructor();
			return ctor;
		}


		internal static CodeMemberMethod NewMemberMethod(string name, MemberAttributes attribute, string comment, params CodeParameterDeclarationExpression[] parameters)
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = name;
			method.Attributes = attribute;
			CreateCommont(method.Comments, comment);
			foreach (CodeParameterDeclarationExpression parameter in parameters)
				method.Parameters.Add(parameter);
			return method;
		}


		internal static CodeMemberMethod NewMemberMethod(string name, MemberAttributes attribute, CodeTypeReference returnType, string comment, params CodeParameterDeclarationExpression[] parameters)
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = name;
			method.Attributes = attribute;
			method.ReturnType = returnType;
			CreateCommont(method.Comments, comment);
			foreach (CodeParameterDeclarationExpression parameter in parameters)
				method.Parameters.Add(parameter);
			return method;
		}


		internal static CodeTypeDelegate NewDelegate(string name, string comment, params CodeParameterDeclarationExpression[] parameters)
		{
			CodeTypeDelegate dlgt = new CodeTypeDelegate(name);
			CreateCommont(dlgt.Comments, comment);
			foreach (CodeParameterDeclarationExpression parameter in parameters)
				dlgt.Parameters.Add(parameter);
			return dlgt;
		}


		internal static CodeTypeDelegate NewDelegate(string name, CodeTypeReference returnType, string comment, params CodeParameterDeclarationExpression[] parameters)
		{
			CodeTypeDelegate dlgt = new CodeTypeDelegate(name);
			dlgt.ReturnType = returnType;
			CreateCommont(dlgt.Comments, comment);
			foreach (CodeParameterDeclarationExpression parameter in parameters)
				dlgt.Parameters.Add(parameter);
			return dlgt;
		}

	}
}
