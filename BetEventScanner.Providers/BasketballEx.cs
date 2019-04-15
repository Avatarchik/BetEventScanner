using BetEventScanner.DataModel;
using System.Linq;

namespace BetEventScanner.Providers
{
    public static class BasketballEx
    {
        public static BasketballMetchResult GetResult(this string str)
        {
            var scores = str.GetScores();
            var periods = str.GetPeriods();

            return new BasketballMetchResult
            {
                HomeScore = scores.homeScore,
                AwayScore = scores.awayScore,
                Periods = periods
            };
        }

        public static BasketballPeriod[] GetPeriods(this string str) =>
            string.IsNullOrEmpty(str) ? new BasketballPeriod[0]:
            str.ExtractAfter('(')
            .Replace("(", "")
            .Replace(")", "")
            .Trim()
            .Split(',')
            .Take(4)
            .Select(BasketballPeriod.FromString)
            .ToArray();

        public static (int homeScore, int awayScore) GetScores(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return (0, 0);

            var s = str.GetUntilOrEmpty("(").Split('-').Select(int.Parse).ToArray();
            return (s[0], s[1]);
        }
    }
}
