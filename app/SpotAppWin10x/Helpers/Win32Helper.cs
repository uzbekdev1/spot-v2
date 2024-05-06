using Microsoft.Win32;
using SpotApp.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SpotApp
{
    internal static class Win32Helper
    {  

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool InternetIsConnected()
        {
            return InternetGetConnectedState(out _, 0);
        }

        private static IEnumerable<ManagementObject> Query(string query)
        {
            try
            {
                var searcher = new ManagementObjectSearcher(query);
                var processes = searcher.Get();
                var results = processes.GetEnumerator();
                var items = new List<ManagementObject>();

                while (results.MoveNext())
                {
                    items.Add((ManagementObject)results.Current);
                }

                return items;
            }
            catch
            {
                return new ManagementObject[0];
            }
        }
         
        public static bool DetectVirtualMachine()
        {
            foreach (var mo in Query("Select * from Win32_ComputerSystem"))
            {
                var model = (string)mo["Model"];

                if (model == "VirtualBox" ||
                    model == "Virtual Machine" ||
                    model == "Parallels Virtual Platform" ||
                    model.StartsWith("VMware"))
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetStartup()
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            var register = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            try
            {
                var value = register.GetValue(AppSettings.AppName);

                if (value != null)
                {
                    register.DeleteValue(AppSettings.AppName);
                }

                register.SetValue(AppSettings.AppName, Application.ExecutablePath);
            }
            catch
            {
            }
        }

    }
}
