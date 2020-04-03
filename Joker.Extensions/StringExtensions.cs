using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

namespace Joker.Extensions
{
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
    }
}
