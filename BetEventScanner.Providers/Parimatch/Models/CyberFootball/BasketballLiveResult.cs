using BetEventScanner.DataModel;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class BasketballLiveResult
    {
        public SportLiveMatch Match { get; set; }
        public int CurrentPeriod { get; set; }
        public BasketballPeriod[] Periods { get; set; }
        public int Total1 { get; set; }
        public int Total2 { get; set; }
        public int OverallTotal { get; set; }

        public static BasketballLiveResult GetResult(SportLiveMatch liveMatch)
        {
            var scores = liveMatch.Result.GetScores();

            return new BasketballLiveResult
            {
                Match = liveMatch,
                Total1 = scores.homeScore,
                Total2 = scores.awayScore,
                Periods = liveMatch.Result.GetPeriods(),
                OverallTotal = scores.homeScore + scores.awayScore,
            };
        }
    }
}
