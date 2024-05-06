using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using SpotApp.Core;
using SpotApp.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SpotApp
{
    internal static class Program
    {

        private static readonly Mutex _mutex = new Mutex(true, AppSettings.AppName);

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void ConfigureLogging()
        {
            var root = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Logs");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var version = Path.Combine(root, $"v{AppSettings.AppVersion}");
            if (!Directory.Exists(version))
            {
                Directory.CreateDirectory(version);
            }

            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date [%thread] %-5level %logger [%property{NDC}] - %message%newline"
            };
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = Path.Combine(version, "log"),
                Layout = patternLayout,
                DatePattern = "yyyyMMdd", 
                RollingStyle = RollingFileAppender.RollingMode.Date,
                StaticLogFileName = false
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }

        private static void ShowExceptionDetails(Exception exp)
        {
            MessageBox.Show(exp.Message, "Ошибка");

            _logger.Error(exp.ToString());
        }

        [STAThread]
        public static void Main()
        {

            if (Win32Helper.DetectVirtualMachine())
            {
                MessageBox.Show("Это приложение не может работать на виртуальной машине", "Ошибка", MessageBoxButtons.OK);

                return;
            }

            if (!Win32Helper.InternetIsConnected())
            {
                MessageBox.Show("Нет соединения!", "Интернет", MessageBoxButtons.OK);

                return;
            }

            if (!_mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Уже запущен!", "Приложение", MessageBoxButtons.OK);
            }
            else
            {

                var culture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                ConfigureLogging();
                Win32Helper.SetStartup();

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ApplicationExit += Application_ApplicationExit;

                var service = new SpotServiceV2();
                var version = service.GetVersion();

                if (version == AppSettings.AppVersion || Debugger.IsAttached)
                {
                    Application.Run(new LoginForm());
                }
                else
                {
                    if (MessageBox.Show($"Приложение устарело , текущая версия: {AppSettings.AppVersion} -  новый версия: {version}. Пожалуйста, обновите сейчас!", "Проверка", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        if (service.DownloadLatest(out var executionPath))
                        {
                            Process.Start(executionPath);

                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show("Приложение не обновляется, попробуйте в следующий раз!", "Проверка");

                            Application.Run(new LoginForm());
                        }
                    }
                }

            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowExceptionDetails(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowExceptionDetails(e.ExceptionObject as Exception);
        }

    }
}
