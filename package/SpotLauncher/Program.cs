using System;
using System.Windows.Forms;
using System.Reflection;
using SpotLauncher.Properties;

namespace SpotLauncher
{
    internal static class Program
    {

        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.Run(new StartForm());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("SharpZipLib"))
            {
                return Assembly.Load(Resources.ICSharpCode_SharpZipLib);
            }

            if (args.Name.Contains("IWshRuntimeLibrary"))
            {
                return Assembly.Load(Resources.Interop_IWshRuntimeLibrary);
            }

            return null;
        }

    }
}
