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
[assembly: AssemblyTitle("Arunav.Net.AppHost")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Arunav")]
[assembly: AssemblyProduct("Arunav.Net.AppHost")]
[assembly: AssemblyCopyright("Copyright © Arunav Gupta 2020")]
[assembly: AssemblyTrademark("www.arunav.net")]
[assembly: AssemblyCulture("")]
[assembly: System.CLSCompliant(true)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("79a014ce-8d26-4615-bc85-73fa9b3a4401")]

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
