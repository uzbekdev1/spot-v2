using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using SpotLauncher.Properties;
using IWshRuntimeLibrary;
using System.Collections.Generic;

namespace SpotLauncher
{
    public partial class StartForm : Form
    {

#if DEBUG
        private const string AppName = "SpotClient v2 (Test)";
#else
        private const string AppName = "SpotClient v2";
#endif

        private int _counter = 0;

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void CreateShortcut(string root)
        {
            var shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var targetFileLocation = Path.Combine(root, "SpotApp.exe");
#if DEBUG
            var shortcutLocation = Path.Combine(shortcutPath, "Spot-клиент v2 (Test).lnk");
#else
            var shortcutLocation = Path.Combine(shortcutPath, "Spot-клиент v2.lnk");
#endif
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.TargetPath = targetFileLocation;
            shortcut.Save();
        }

        private void ExtractApp(string root)
        {
            var path = Path.Combine(root, "client");

            try
            {
                var fast = new FastZip();
                fast.ExtractZip(path, root, "");

                var app = Path.Combine(root, "SpotApp.exe");
                Process.Start(app);

                System.IO.File.Delete(path);
            }
            catch (Exception exp)
            {
                PrintLine(exp.Message);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void PrintLine(string msg, params object[] args)
        {
            rtbLine.AppendText(string.Format(msg, args) + "\r\n");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _counter++;

            var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), AppName);

            switch (_counter)
            {
                case 1:
                    {
                        PrintLine("Запуск установщика...");
                    }
                    break;
                case 2:
                    {
                        PrintLine("Проверка оборудования");
                    }
                    break;
                case 3:
                    {
                        PrintLine("Операционная система: {0}", Environment.OSVersion);
                        PrintLine("Персональная папка: {0}", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    } break;
                case 4:
                    {
                        var processes1 = Process.GetProcessesByName("SpotApp");
                        if (processes1.Length > 0)
                        {
                            processes1[0].Kill();
                        }
                        var processes2 = Process.GetProcessesByName("ClockApp");
                        if (processes2.Length > 0)
                        {
                            processes2[0].Kill();
                        }
                        PrintLine("Процесс очищен");
                        PrintLine("Проверяем версию: {0}", Resources.AppVersion);
                    } break;
                case 5:
                    {
                        if (Directory.Exists(root))
                        {
                            foreach (var dir in Directory.GetDirectories(root))
                            {
                                if (dir.Contains("Logs"))
                                {
                                    continue;
                                }

                                System.IO.Directory.Delete(dir, true);
                            }

                            foreach (var file in Directory.GetFiles(root))
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                        else
                        {
                            Directory.CreateDirectory(root);
                        }

                        PrintLine("Копирование файлов в Мои Документы");
                        var path = Path.Combine(root, "client");
                        System.IO.File.WriteAllBytes(path, Resources.client);
                    } break;
                default:
                    {

                        ExtractApp(root);

                        CreateShortcut(root);
#if DEBUG
                        PrintLine("Тестовое приложение");
#else
                        PrintLine("Реальное приложение");
#endif
                        PrintLine("Готово!");
                        timer1.Stop();
                    }
                    break;
            }
        }

    }
}
