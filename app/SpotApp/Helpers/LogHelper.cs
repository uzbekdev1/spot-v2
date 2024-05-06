using System;
using System.IO;
using System.Windows.Forms;

namespace SpotApp.Helpers
{
    internal class LogHelper
    {
        private static readonly string _root;

        static LogHelper()
        {
            _root = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LogFiles");
            if (!Directory.Exists(_root))
            {
                Directory.CreateDirectory(_root);
            }
        }

        public static void WriteLog(LogLevel level, string message = "")
        {
            try
            {
                var path = Path.Combine(_root, $"log{DateTime.Now:yyyyMMdd}.txt");

                if (!Directory.Exists(_root))
                    Directory.CreateDirectory(_root);

                File.AppendAllText(path, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {Enum.GetName(typeof(LogLevel), level)} - {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Fatal
    }
}
