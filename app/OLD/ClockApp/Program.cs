using SpotApp.Helpers;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ClockApp
{
    internal static class Program
    {
        private static readonly Mutex _mutex = new Mutex(true, "ClockApp");

        [STAThread]
        static void Main()
        {
            if (!_mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Уже запущен!", "Приложение", MessageBoxButtons.OK);
            }
            else
            {
#if !DEBUG
                    //if (SpotHelper.Has())
                    //{
                    //    MessageBox.Show("Не запущен!", "Спот", MessageBoxButtons.OK);

                    //    return;
                    //}
#endif

                var culture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                Application.Run(new MainForm());
            }
        }

    }
}
