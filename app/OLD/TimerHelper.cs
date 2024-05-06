using System;
using System.Diagnostics;
using System.IO;

namespace SpotApp.Helpers
{
    internal class TimerHelper
    {


        public static void Open()
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                var app = Path.Combine(root, "ClockApp.exe");

                Process.Start(app); 
            }
            catch 
            { 
            }
        }

        public static void Close()
        {

            try
            {
                foreach (var process in Process.GetProcessesByName("ClockApp"))
                {
                    process.Kill();
                }
            }
            catch
            {
            }
        }

    }
}
