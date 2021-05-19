using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle("Tital.AutoConfig.MVC")]
[assembly: AssemblyDescription("针对MVC自动配置")]
[assembly: ComVisible(false)]
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Tital.AutoConfig.Bootstrapper), "Initialise")]