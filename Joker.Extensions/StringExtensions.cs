using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

namespace Joker.Extensions;

public static class StringExtensions
{
    public static string CamelCase(this string value)
    {
        return char.ToLowerInvariant(value[0]) + value.Substring(1);
    }

    public static Guid ToGuid(this string value)
    {
        var isValid = Guid.TryParse(value, out var guid);
        if (!isValid)
            throw new Exception("Invalid Guid");

        return guid;
    }

    public static string RemoveSpecialCharacters(this string value)
    {
        var dictionary = new Dictionary<char, char[]>
        {
            {'a', new[] {'à', 'á', 'ä', 'â', 'ã'}},
            {'A', new[] {'À', 'Á', 'Ä', 'Â', 'Ã'}},
            {'c', new[] {'ç'}},
            {'C', new[] {'Ç'}},
            {'e', new[] {'è', 'é', 'ë', 'ê'}},
            {'E', new[] {'È', 'É', 'Ë', 'Ê'}},
            {'i', new[] {'ì', 'í', 'ï', 'î'}},
            {'I', new[] {'Ì', 'Í', 'Ï', 'Î'}},
            {'o', new[] {'ò', 'ó', 'ö', 'ô', 'õ'}},
            {'O', new[] {'Ò', 'Ó', 'Ö', 'Ô', 'Õ'}},
            {'u', new[] {'ù', 'ú', 'ü', 'û'}},
            {'U', new[] {'Ù', 'Ú', 'Ü', 'Û'}}
        };

        value = dictionary.Keys.Aggregate(value, (x, y) => dictionary[y].Aggregate(x, (z, c) => z.Replace(c, y)));

        return new Regex("[^0-9a-zA-Z._ ]+?").Replace(value, string.Empty);
    }
        
    public static HtmlString ToHtmlString(this string str)
    {
        return new HtmlString(str);
    }
        
    public static string CreateUniqueCode()
    {
        Guid g = Guid.NewGuid();
        string s = Convert.ToBase64String(g.ToByteArray());
        s = s.Replace("=", "");
        s = s.Replace("+", "");
        return s;
    }

    public static string GenerateSlug(this string phrase)
    {
        string str = phrase.RemoveAccent().ToLower();
        // invalid chars           
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        // convert multiple spaces into one space   
        str = Regex.Replace(str, @"\s+", " ").Trim();
        // cut and trim 
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        str = Regex.Replace(str, @"\s", "-"); // hyphens   
        return str;
    }

    public static string RemoveAccent(this string txt)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }
        
    public static bool IsValidJson(this string text)
    {
        text = text.Trim();
        if ((text.StartsWith("{") && text.EndsWith("}")) ||
            (text.StartsWith("[") && text.EndsWith("]")))
        {
            try
            {
                JsonDocument.Parse(text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}