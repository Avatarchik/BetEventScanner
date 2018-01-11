namespace BetEventScanner.DataModel
{
    public class FootballMatchResult
    {
        public string FirstHalfScore { get; set; }

        public int FirstHalfHomeScored { get; set; }

        public int FirstHalfAwayScored { get; set; }

        public string SecondHalfScore { get; set; }

        public int SecondHalfHomeScored { get; set; }

        public int SecondHalfAwayScored { get; set; }
    }
}