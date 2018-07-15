using System;
using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.DataModel.Model
{
    public class FootballMatch
    {
        public DateTime DateTime { get; set; }

        public string Country { get; set; }

        public string Stage { get; set; }

        public int Round { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public FootballMatchResult Result { get; set; }

        public FootballMatchStatistics Statistics { get; set; }

        public IDictionary<string, string> Providers { get; set; }
    }
}