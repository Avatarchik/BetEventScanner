using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;

namespace BetEventScanner.Providers.Strategies.Basketball
{
    public class HumptyDumptyResult
    {
        public int Total { get; set; }

        public int AllPeriodsWinOneTeamCount { get; set; }

        public List<HistoricalMatchResult> Matches { get; set; } = new List<HistoricalMatchResult>();
    }
}
