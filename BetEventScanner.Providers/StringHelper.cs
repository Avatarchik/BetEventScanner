using System.Linq;
using System.Text;

namespace BetEventScanner.Providers
{
    public static class StringHelper
    {
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
    }
}