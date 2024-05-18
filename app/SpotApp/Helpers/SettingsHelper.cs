using Newtonsoft.Json;
using SpotApp.Core;
using System.IO;
using System.Windows.Forms;

namespace SpotApp.Helpers
{
    internal class SettingsHelper
    {

        private static readonly string _root;

        static SettingsHelper()
        {
            _root = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings");
        }

        public static void SetForm(Control control)
        {
            if (control == null)
                return;

            if (string.IsNullOrEmpty(control.Name))
                return;

            var json = JsonConvert.SerializeObject(new FormSettings
            {
                Location = control.Location,
                Size = control.Size,
                Name = GetCleanName(control.Name),
            }, Formatting.Indented);

            var path = Path.Combine(_root, $"{GetCleanName(control.Name)}.json");

            File.WriteAllText(path, json);
        }

        public static FormSettings GetForm(Control control)
        {
            if (control == null)
                return null;

            if (string.IsNullOrEmpty(control.Name))
                return null;

            var path = Path.Combine(_root, $"{GetCleanName(control.Name)}.json");

            if (!File.Exists(path))
            {
                return null;
            }

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<FormSettings>(json);
        }

        public static string GenerateOrderName(string name, int? order = null)
        {
            if (order != null && order.HasValue)
                return $"{name}_{order.Value}";
            return name;
        }

        private static string GetCleanName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            if (name.IndexOf("_") < 0)
                return name;

            var clean = name.Substring(0, name.LastIndexOf("_"));

            return clean;
        }
    }
}
