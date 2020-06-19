using System.Reflection;
using System.Runtime.InteropServices;

// Delay Signing assembly attributes
#if DSIGN
[assembly:AssemblyKeyFile(@"..\..\..\SigningKey\_ArunavNet.snk")]
[assembly:AssemblyDelaySign(true)]
#endif

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Arunav.Net.AppBase")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Arunav")]
[assembly: AssemblyProduct("Arunav.Net.AppBase")]
[assembly: AssemblyCopyright("Copyright © Arunav Gupta 2020")]
[assembly: AssemblyTrademark("www.arunav.net")]
[assembly: AssemblyCulture("")]
[assembly: System.CLSCompliant(true)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("180b2c18-7c24-4028-a2c7-98f845d8490e")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyFileVersion("1.2.0.0")]
