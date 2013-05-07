using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using XNATools.Attributes;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("On Screen Keyboard Display")]
[assembly: AssemblyProduct("OSKD")]
[assembly: AssemblyDescription("Display a keyboard and a mouse. Highlight pressed key")]
[assembly: AssemblyCompany("Hâthor")]
[assembly: AssemblyCopyright("Copyright © Hâthor 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type. Only Windows
// assemblies support COM.
[assembly: ComVisible(false)]

// On Windows, the following GUID is for the ID of the typelib if this
// project is exposed to COM. On other platforms, it unique identifies the
// title storage container when deploying this assembly to the device.
[assembly: Guid("621116be-59ed-447a-9368-5fe569fbd7de")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//

#if DEBUG
[assembly: AssemblyCodeName("Skull Kid")]
[assembly: AssemblyVersion("0.0.0.0")]
#else
[assembly: AssemblyCodeName("Morpheel")]
[assembly: AssemblyVersion("2.2.*")]
#endif