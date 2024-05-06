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

            var json = JsonConvert.SerializeObject(new FormSettings
            {
                Location = control.Location,
                Size = control.Size,
                Name = control.Name,
            }, Formatting.Indented);
            var path = Path.Combine(_root, $"{control.Name}.json");

            File.WriteAllText(path, json);
        }

        public static FormSettings GetForm(Control control)
        {
            if (control == null)
                return null;

            var path = Path.Combine(_root, $"{control.Name}.json");

            if (!File.Exists(path))
            {
                return null;
            }

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<FormSettings>(json);
        }

    }
}
