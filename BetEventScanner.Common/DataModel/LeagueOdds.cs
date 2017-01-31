using System;
using System.Collections.Generic;

namespace BetEventScanner.Common.DataModel
{
    public class LeagueOdds
    {
        public LeagueOdds()
        {
            Maches = new List<MatchOdds>();
        }

        public Division Division { get; set; }

        public int OriginSourceLeagueId { get; set; }

        public OddsSource Source { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public IList<MatchOdds> Maches { get; set; }
    }
}