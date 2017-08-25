using System.Collections.Generic;

namespace BetEventScanner.DataAccess.DataModel
{
    public class CountryDivisionSeason
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Division { get; set; }

        public string DivisionCode { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public ICollection<FootballMatchResult> Results { get; set; }
    }
}