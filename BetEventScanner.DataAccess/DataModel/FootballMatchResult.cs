using System;

namespace BetEventScanner.DataAccess.DataModel
{
    public class FootballMatchResult
    {
        public int Id { get; set; }

        public int SeasonId { get; set; }

        public FootballSeason Season { get; set; }

        public DateTime DateTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeScored { get; set; }

        public int AwayScored { get; set; }

        public int OddsId { get; set; }

        public FootballMatchOdds Odds { get; set; }
    }
}
