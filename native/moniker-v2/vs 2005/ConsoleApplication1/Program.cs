using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    { 
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
 
        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);
  
        [DllImport("mrisk.dll", EntryPoint = "fnmrisk", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        private static extern string FnMRisk(int secret);

        public static void Main(string[] args)
        {
            try
            {
                string fullPath = Path.Combine(Environment.CurrentDirectory, "mrisk.dll");
                IntPtr pDll= LoadLibrary(fullPath);
                string text1 = FnMRisk(15280);
                FreeLibrary(pDll);

                Console.WriteLine("DllImport: {0}", text1);

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            } 

            Console.ReadLine();
        }
    }
}
