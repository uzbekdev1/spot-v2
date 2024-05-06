using System;
using System.Runtime.InteropServices;

namespace StdCallDemo
{
    internal class Program
    {

        [DllImport("moniker.dll", EntryPoint = "box", CallingConvention = CallingConvention.StdCall)]
        private static extern string Box(int secret);
         
        public static void Main(string[] args)
        { 
            var secret = 15280;
            var result = Box(secret);
             
            Console.WriteLine("RSA key: {0}", result);
            Console.ReadLine();
        }
    }
}
