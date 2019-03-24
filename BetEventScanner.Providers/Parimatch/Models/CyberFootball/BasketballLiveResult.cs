using System.Linq;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class Period
    {
        public int Total { get; set; }
        public string Score { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }

        public static Period FromString(string r)
        {
            var p = new Period
            {
                Score = r
            };

            var scores = r.Split('-').Select(int.Parse).ToArray();
            p.Total = scores.Sum();
            p.Score1 = scores[0];
            p.Score2 = scores[1];

            return p;
        }
    }

    public class BasketballLiveResult
    {
        public SportLiveMatch Match { get; set; }

        public int CurrentPeriod { get; set; }

        public Period[] Periods { get; set; }

        public int Total1 { get; set; }
        public int Total2 { get; set; }
        public int OverallTotal { get; set; }

        public static BasketballLiveResult GetResult(SportLiveMatch liveMatch)
        {
            var blr = new BasketballLiveResult
            {
                Match = liveMatch
            };

            var r = liveMatch.Result;
            var scores = r.GetUntilOrEmpty("(").Split('-').Select(int.Parse).ToArray();

            blr.Periods = r.ExtractAfter('(').Replace("(", "").Replace(")", "").Trim().Split(',').Select(Period.FromString).ToArray();

            blr.Total1 = scores[0];
            blr.Total2 = scores[1];
            blr.OverallTotal = scores.Sum();

            return blr;
        }
    }
}
