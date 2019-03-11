using System;

namespace BetEventScanner.Providers.Parimatch
{
    public class FootballMatch
    {
        public string SportId { get; set; }

        public string CountryId { get; set; }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public Handicap Handicap { get; set; }

        public Total Total { get; set; }

        public double HomeWin { get; set; }

        public double Draw { get; set; }

        public double AwayWin { get; set; }
    }
}
