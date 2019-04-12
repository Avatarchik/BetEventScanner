using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Strategies.Basketball
{
    public class HumptyDumpty
    {
        private HumptyDumptyResult state = new HumptyDumptyResult();

        public void Process(HistoricalMatchResult[] matches)
        {
            state.Total += matches.Length;
            var r = CalculateHistorical(matches);
            state.AllPeriodsWinOneTeamCount += r.count;
            state.Matches.AddRange(r.matches);
        }

        public HumptyDumptyResult GetResult() => state;

        private static (int count, List<HistoricalMatchResult> matches)  CalculateHistorical(HistoricalMatchResult[] matches)
        {
            var allPeriodsWinOneTeam = 0;
            var l = new List<HistoricalMatchResult>();

            foreach (var match in matches)
            {
                DataModel.BasketballMetchResult result = match.Result.GetResult();
                if (result.Periods.All(x => x.Score1 >= x.Score2))
                {
                    allPeriodsWinOneTeam++;
                    l.Add(match);
                }
            }

            return (allPeriodsWinOneTeam, l);
        }
    }
}
