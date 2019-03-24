using System;
using System.Linq;
using System.Text;

namespace BetEventScanner.Providers
{
    public static class StringHelper
    {
        public static string ExtractBefore(this string input, char before)
        {
            var startIndex = input.IndexOf(before);
            return input.Substring(0, --startIndex).Trim();
        }

        public static string ExtractAfter(this string input, char after) =>
            input.Substring(input.LastIndexOf(after));

        public static string ExtractBetween(this string input, char first, char last)
        {
            var startIndex = input.IndexOf(first);
            var endIndex = input.IndexOf(last);
            return input.Substring(++startIndex, endIndex - startIndex);
        }

        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

        public static string TakeBetween(this string str, string from, string to)
        {
            var startIndex = str.IndexOf(from);
            var endIndex = str.LastIndexOf(to);
            startIndex = ++startIndex;
            return str.Substring(startIndex, endIndex - startIndex);
        }

        public static string SkipLastN(this string str, int n)
        {
            return new string(str.ToArray().Take(str.Length - n).ToArray());
        }

        public static string SkipFirstAndLast(this string str)
        {
            return new string(str.ToArray().Skip(1).Take(str.Length - 2).ToArray());
        }

        public static string RemoveSpecialSymbols(string origin)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < origin.Length; i++)
            {
                if (char.IsLetterOrDigit(origin[i]))
                {
                    sb.Append(origin[i]);
                }
            }

            return sb.ToString();
        }

        public static string RemoveDashWithTrim(this string str) =>
            str.Replace("-", "").Trim();
    }
}