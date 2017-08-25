using System;

namespace BetEventScanner.DataModel
{
    public class FootballResult
    {
        public DateTime DateTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeScored { get; set; }

        public int AwayScored { get; set; }

        public string StringResult { get; set; }

        public double HomeOdds { get; set; }

        public double DrawOdds { get; set; }

        public double AwayOdds { get; set; }

        public int OverallTotal { get; set; }
    }
}