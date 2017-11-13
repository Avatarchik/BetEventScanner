using System.Collections.Generic;

namespace SoccerStandParser
{
    public class FootballMatch
    {
        public string Country { get; set; }

        public string Stage { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string Score { get; set; }

        public string FirstHalfScore { get; set; }

        public string SecondHalfScore { get; set; }

        public string DateTime { get; set; }

        public MatchStatistics Statistics { get; set; }

        public Dictionary<string, string> Odds { get; set; } = new Dictionary<string, string>();

        public string ProviderId { get; set; }
    }

    public class MatchStatistics
    {
    }
}