using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClockApp.Helpers
{
    internal static class UIHelper
    {
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

        public static void RunAsync(Form form, Action<Form> start, Action<Form> done)
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
    }
}
