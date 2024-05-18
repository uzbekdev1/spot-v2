using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SpotApp.Helpers
{
    internal static class UIHelper
    {

        public static string CleanNumber(string str)
        {
            return Regex.Replace(str, @"\s+", "").Replace(".", ",");
        }

        public static string NumberFormat(decimal val)
        {
            return val.ToString("n2");
        }

        public static string NumberFormat(decimal? val)
        {
            return val > 0 ? val.Value.ToString("n2") : "";
        }

        public static string NumberFormat(int val)
        {
            return val.ToString("n2");
        }

        public static string NumberFormat(int? val)
        {
            return val > 0 ? val.Value.ToString("n2") : "";
        }

        public static void RunAsync(Form form, Action<Form> action, int waiting = 100)
        {
            var runner = new BackgroundWorker();

            runner.DoWork += (object sender1, DoWorkEventArgs e1) =>
            {
                Thread.Sleep(waiting);
            };
            runner.RunWorkerCompleted += (object sender1, RunWorkerCompletedEventArgs e1) =>
            {
                action(form);
            };

            runner.RunWorkerAsync();
        }

        public static void RunForce(Form form, Action<Form> action)
        {
            var runner = new BackgroundWorker();

            runner.DoWork += (object sender1, DoWorkEventArgs e1) =>
            {
            };
            runner.RunWorkerCompleted += (object sender1, RunWorkerCompletedEventArgs e1) =>
            {
                action(form);
            };

            runner.RunWorkerAsync();
        }

        public static void SafeInvoke(Control form, Action<Control> action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(action);
            }
            else
            {
                action(form);
            }
        }

        public static void RunAsync(Control form, Action<Control> start, Action<Control> done)
        {
            var runner = new BackgroundWorker();

            runner.DoWork += (object sender1, DoWorkEventArgs e1) =>
            {
                start(form);
            };

            runner.RunWorkerCompleted += (object sender1, RunWorkerCompletedEventArgs e1) =>
            {
                done(form);
            };

            runner.RunWorkerAsync();
        }


        public static DateTime ToVersionTime(string s)
        {
            return DateTime.ParseExact(s, "ddMMyyyy", null, DateTimeStyles.None);
        }

        public static void SafeInvokeForm(Form form, Action<Form> action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(action);
            }
            else
            {
                action(form);
            }
        }
    }
}
