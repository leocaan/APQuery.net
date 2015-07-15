using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("APQuery.net")]
[assembly: AssemblyDescription(
@"APQuery.net is a lightweight ORM, open source framework. With the objectization of the database,
SQL expression, database access layout, business logic process and data entity.

Source code : https://github.com/leocaan/APQuery.net

Documents : http://leocaan.github.io/APQuery.net/
")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Symber")]
[assembly: AssemblyProduct("Symber.Web.APQuery")]
[assembly: AssemblyCopyright("Copyright © Symber 2014-2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("31baa97f-440d-4f8d-81f1-68fb6053dd93")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("4.0.2.4")]
[assembly: AssemblyFileVersion("4.0.2.4")]

// Global code run way early in the ASP.NET pipeline as an application starts up. 
// I mean way early, even before Application_Start. 
// First register "APGenBuildProvider" to compily ".apgen" code.
[assembly: SecurityRules(SecurityRuleSet.Level2, SkipVerificationInFullTrust = true)]
[assembly: PreApplicationStartMethod(typeof(Symber.Web.Compilation.APGenBuildProvider), "Register")]
