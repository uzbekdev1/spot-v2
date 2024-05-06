using System.Diagnostics;

namespace SpotApp.Helpers
{
    internal class SpotHelper
    {

        public static bool Has()
        {  
            try
            {
                var process = Process.GetProcessesByName("SpotApp");

                return process.Length > 0 && !process[0].HasExited;
            }
            catch
            {
            }

            return false;
        }

        public static void Close()
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("SpotApp"))
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
