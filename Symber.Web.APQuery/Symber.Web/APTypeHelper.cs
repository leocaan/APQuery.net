using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace Symber.Web
{
	internal class APTypeHelper
	{

		#region [ Fields ]


		internal static Dictionary<string, Type> primaryTypeNames;


		#endregion


		#region [ Constructors ]


		static APTypeHelper()
		{
			primaryTypeNames = new Dictionary<string, Type>();
			primaryTypeNames.Add("Boolean", typeof(Boolean));
			primaryTypeNames.Add("bool", typeof(bool));
			primaryTypeNames.Add("Byte[]", typeof(Byte[]));
			primaryTypeNames.Add("byte[]", typeof(byte[]));
			primaryTypeNames.Add("Byte", typeof(Byte));
			primaryTypeNames.Add("byte", typeof(byte));
			primaryTypeNames.Add("SByte", typeof(SByte));
			primaryTypeNames.Add("sbyte", typeof(sbyte));
			primaryTypeNames.Add("Int16", typeof(Int16));
			primaryTypeNames.Add("short", typeof(short));
			primaryTypeNames.Add("UInt16", typeof(UInt16));
			primaryTypeNames.Add("ushort", typeof(ushort));
			primaryTypeNames.Add("Int32", typeof(Int32));
			primaryTypeNames.Add("int", typeof(int));
			primaryTypeNames.Add("UInt32", typeof(UInt32));
			primaryTypeNames.Add("uint", typeof(uint));
			primaryTypeNames.Add("Int64", typeof(Int64));
			primaryTypeNames.Add("long", typeof(long));
			primaryTypeNames.Add("UInt64", typeof(UInt64));
			primaryTypeNames.Add("ulong", typeof(ulong));
			primaryTypeNames.Add("Char", typeof(Char));
			primaryTypeNames.Add("char", typeof(char));
			primaryTypeNames.Add("Double", typeof(Double));
			primaryTypeNames.Add("double", typeof(double));
			primaryTypeNames.Add("Single", typeof(Single));
			primaryTypeNames.Add("float", typeof(float));
			primaryTypeNames.Add("Guid", typeof(Guid));
			primaryTypeNames.Add("string", typeof(string));
			primaryTypeNames.Add("String", typeof(String));
			primaryTypeNames.Add("Decimal", typeof(decimal));
			primaryTypeNames.Add("decimal", typeof(decimal));
			primaryTypeNames.Add("DateTime", typeof(DateTime));
			primaryTypeNames.Add("datetime", typeof(DateTime));

			primaryTypeNames.Add("Boolean?", typeof(Boolean?));
			primaryTypeNames.Add("bool?", typeof(bool?));
			primaryTypeNames.Add("Byte?", typeof(Byte?));
			primaryTypeNames.Add("byte?", typeof(byte?));
			primaryTypeNames.Add("SByte?", typeof(SByte?));
			primaryTypeNames.Add("sbyte?", typeof(sbyte?));
			primaryTypeNames.Add("Int16?", typeof(Int16?));
			primaryTypeNames.Add("short?", typeof(short?));
			primaryTypeNames.Add("UInt16?", typeof(UInt16?));
			primaryTypeNames.Add("ushort?", typeof(ushort?));
			primaryTypeNames.Add("Int32?", typeof(Int32?));
			primaryTypeNames.Add("int?", typeof(int?));
			primaryTypeNames.Add("UInt32?", typeof(UInt32?));
			primaryTypeNames.Add("uint?", typeof(uint?));
			primaryTypeNames.Add("Int64?", typeof(Int64?));
			primaryTypeNames.Add("long?", typeof(long?));
			primaryTypeNames.Add("UInt64?", typeof(UInt64?));
			primaryTypeNames.Add("ulong?", typeof(ulong?));
			primaryTypeNames.Add("Char?", typeof(Char?));
			primaryTypeNames.Add("char?", typeof(char?));
			primaryTypeNames.Add("Double?", typeof(Double?));
			primaryTypeNames.Add("double?", typeof(double?));
			primaryTypeNames.Add("Single?", typeof(Single?));
			primaryTypeNames.Add("float?", typeof(float?));
			primaryTypeNames.Add("Guid?", typeof(Guid?));
			primaryTypeNames.Add("Decimal?", typeof(decimal?));
			primaryTypeNames.Add("decimal?", typeof(decimal?));
			primaryTypeNames.Add("DateTime?", typeof(DateTime?));
			primaryTypeNames.Add("datetime?", typeof(DateTime?));
		}


		#endregion


		#region [ Methods ]


		public static Type LoadType(string typeName)
		{
			Type type;

			if (null != (type = Type.GetType(typeName)))
				return type;

			if (primaryTypeNames.ContainsKey(typeName))
				return primaryTypeNames[typeName];

			if (null != (type = typeof(Guid).Assembly.GetType(typeName)))
				return type;

			if (null != (type = typeof(HttpApplication).Assembly.GetType(typeName)))
				return type;

			if (null != (type = Assembly.GetExecutingAssembly().GetType(typeName)))
				return type;

			try
			{
				if (BuildManager.CodeAssemblies != null)
				{
					foreach (Assembly asm in BuildManager.CodeAssemblies)
					{
						if (null != (type = asm.GetType(typeName)))
							return type;
					}
				}
			}
			catch
			{
			}

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (null != (type = asm.GetType(typeName)))
					return type;
			}

			return null;
		}


		public static Type LoadControlType(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type != null)
				return type;

			type = typeof(HttpApplication).Assembly.GetType(typeName);
			if (type != null)
				return type;

			foreach (Assembly asm in BuildManager.CodeAssemblies)
			{
				type = asm.GetType(typeName);
				if (type != null)
					return type;
			}

			return null;
		}


		#endregion

	}
}
