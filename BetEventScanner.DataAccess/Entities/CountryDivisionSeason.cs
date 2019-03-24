namespace BetEventScanner.DataAccess.Entities
{
    public class FootballSeason
    {
        public string DivisionCode { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public bool IsCurrent { get; set; }
    }
}