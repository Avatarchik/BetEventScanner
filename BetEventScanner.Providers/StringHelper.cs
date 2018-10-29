using System.Linq;
using System.Text;

namespace BetEventScanner.Providers
{
    public static class StringHelper
    {
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