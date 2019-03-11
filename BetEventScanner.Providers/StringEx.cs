namespace BetEventScanner.Providers
{
    public static class StringEx
    {
        public static string ExtractBefore(this string input, char before)
        {
            var startIndex = input.IndexOf(before);
            return input.Substring(0, --startIndex).Trim();
        }

        public static string ExtractBetween(this string input, char first, char last)
        {
            var startIndex = input.IndexOf(first);
            var endIndex = input.IndexOf(last);
            return input.Substring(++startIndex, endIndex - startIndex);
        }
    }
}
