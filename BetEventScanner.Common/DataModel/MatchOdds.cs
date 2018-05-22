using System;
using System.Collections.Generic;

namespace BetEventScanner.Common.DataModel
{
    public class MatchOdds
    {
        public MatchOdds()
        {
            Odds = new List<Odd>();
        }

        public DateTime MatchDate { get; set; }

        public string MatchId { get; set; }

        public string MatchName { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public IList<Odd> Odds { get; set; }
    }
}
