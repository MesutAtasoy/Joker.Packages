using System.Text.RegularExpressions;

namespace Joker.Shared.Helper
{
    public static class UrlHelper
    {
        public static string GetFriendlyTitle(string title, bool remapToAscii = false, int maxlength = 250)
        {
            if (string.IsNullOrEmpty(title)) return "";
            title = title.ToLower();
            title = title.Trim();
            if (title.Length > 100)
            {
                title = title.Substring(0, 100);
            }
            title = title.Replace("İ", "I");
            title = title.Replace("ı", "i");
            title = title.Replace("ğ", "g");
            title = title.Replace("Ğ", "G");
            title = title.Replace("ç", "c");
            title = title.Replace("Ç", "C");
            title = title.Replace("ö", "o");
            title = title.Replace("Ö", "O");
            title = title.Replace("ş", "s");
            title = title.Replace("Ş", "S");
            title = title.Replace("ü", "u");
            title = title.Replace("Ü", "U");
            title = title.Replace("'", "");
            title = title.Replace("\"", "");
            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (title.Contains(strChr))
                {
                    title = title.Replace(strChr, string.Empty);
                }
            }
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            title = r.Replace(title, "-");
            while (title.IndexOf("--") > -1)
                title = title.Replace("--", "-");
            return title;
        }
    }

}
