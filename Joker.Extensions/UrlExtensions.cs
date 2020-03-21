//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Web;

//namespace Joker.Extensions
//{
//    public static class UrlExtensions
//    {
//        public static string GetUrlWithQueryObject(string url, object obj)
//        {
//            var properties = from p in obj.GetType().GetProperties()
//                where p.GetValue(obj, null) != null
//                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

//            var queryString = String.Join("&", properties.ToArray());

//            if (string.IsNullOrEmpty(queryString))
//                return url;
//            else
//                return $"{url}?{queryString}";
//        }

//        public static string GetUrlWithPagingQueryString(string url, int? page, int? pageSize, string q)
//        {
//            List<string> qs = new List<string>();

//            if (page.HasValue)
//                qs.Add($"Page={page}");

//            if (pageSize.HasValue)
//                qs.Add($"PageSize={pageSize}");

//            if (!string.IsNullOrEmpty(q))
//                qs.Add($"q={q}");

//            string queryString = qs.Join("&");

//            if (string.IsNullOrEmpty(queryString))
//                return url;
//            else
//                return $"{url}?{queryString}";
//        }

//        public static string UrlFriendly(string text, int maxLength = 0)
//        {
//            // Return empty value if text is null
//            if (text == null) return "";
//            var normalizedString = text
//                // Make lowercase
//                .ToLowerInvariant()
//                // Normalize the text
//                .Normalize(NormalizationForm.FormD);
//            var stringBuilder = new StringBuilder();
//            var stringLength = normalizedString.Length;
//            var prevdash = false;
//            var trueLength = 0;
//            char c;
//            for (int i = 0; i < stringLength; i++)
//            {
//                c = normalizedString[i];
//                switch (CharUnicodeInfo.GetUnicodeCategory(c))
//                {
//                    // Check if the character is a letter or a digit if the character is a
//                    // international character remap it to an ascii valid character
//                    case UnicodeCategory.LowercaseLetter:
//                    case UnicodeCategory.UppercaseLetter:
//                    case UnicodeCategory.DecimalDigitNumber:
//                        if (c < 128)
//                            stringBuilder.Append(c);
//                        else
//                            stringBuilder.Append(RemapInternationalCharToAscii(c));
//                        prevdash = false;
//                        trueLength = stringBuilder.Length;
//                        break;
//                    // Check if the character is to be replaced by a hyphen but only if the last character wasn't
//                    case UnicodeCategory.SpaceSeparator:
//                    case UnicodeCategory.ConnectorPunctuation:
//                    case UnicodeCategory.DashPunctuation:
//                    case UnicodeCategory.OtherPunctuation:
//                    case UnicodeCategory.MathSymbol:
//                        if (!prevdash)
//                        {
//                            stringBuilder.Append('-');
//                            prevdash = true;
//                            trueLength = stringBuilder.Length;
//                        }

//                        break;
//                }

//                // If we are at max length, stop parsing
//                if (maxLength > 0 && trueLength >= maxLength)
//                    break;
//            }

//            // Trim excess hyphens
//            var result = stringBuilder.ToString().Trim('-');
//            // Remove any excess character to meet maxlength criteria
//            return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
//        }

//        public static string RemapInternationalCharToAscii(char c)
//        {
//            string s = c.ToString().ToLowerInvariant();
//            if ("àåáâäãåą".Contains(s))
//            {
//                return "a";
//            }
//            else if ("èéêëę".Contains(s))
//            {
//                return "e";
//            }
//            else if ("ìíîïı".Contains(s))
//            {
//                return "i";
//            }
//            else if ("òóôõöøőð".Contains(s))
//            {
//                return "o";
//            }
//            else if ("ùúûüŭů".Contains(s))
//            {
//                return "u";
//            }
//            else if ("çćčĉ".Contains(s))
//            {
//                return "c";
//            }
//            else if ("żźž".Contains(s))
//            {
//                return "z";
//            }
//            else if ("śşšŝ".Contains(s))
//            {
//                return "s";
//            }
//            else if ("ñń".Contains(s))
//            {
//                return "n";
//            }
//            else if ("ýÿ".Contains(s))
//            {
//                return "y";
//            }
//            else if ("ğĝ".Contains(s))
//            {
//                return "g";
//            }
//            else if (c == 'ř')
//            {
//                return "r";
//            }
//            else if (c == 'ł')
//            {
//                return "l";
//            }
//            else if (c == 'đ')
//            {
//                return "d";
//            }
//            else if (c == 'ß')
//            {
//                return "ss";
//            }
//            else if (c == 'þ')
//            {
//                return "th";
//            }
//            else if (c == 'ĥ')
//            {
//                return "h";
//            }
//            else if (c == 'ĵ')
//            {
//                return "j";
//            }
//            else
//            {
//                return "";
//            }
//        }
//    }
//}