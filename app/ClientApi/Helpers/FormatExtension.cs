using System.Text.RegularExpressions;

namespace ClientApi.Helpers
{
    public static class FormatExtension
    {
        public static string CleanAsDecimal(this string str)
        {
            return Regex.Replace(str, @"\s+", "").Replace(",", ".");
        }
    }
}
