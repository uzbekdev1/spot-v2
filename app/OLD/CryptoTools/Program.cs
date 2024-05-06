using System;
using System.Windows.Forms;

namespace CryptoTools
{
    internal static class Program
    { 

        [STAThread]
        static void Main()
        {
            Application.Run(new RsaForm());
        }

    }
}
