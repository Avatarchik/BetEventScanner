using System.Text;

namespace BetEventScanner.Providers
{
    public static class StringHelper
    {
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