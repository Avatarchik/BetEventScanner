namespace BetEventScanner.DataModel.Model
{
    public class FootballMatchResult
    {
        public string FinalScore { get; set; }

        public int FinalHomeScored { get; set; }

        public int FinalAwayScored { get; set; }

        public string FirstHalfScore { get; set; }

        public int FirstHalfHomeScored { get; set; }

        public int FirstHalfAwayScored { get; set; }

        public string SecondHalfScore { get; set; }

        public int SecondHalfHomeScored { get; set; }

        public int SecondHalfAwayScored { get; set; }
    }
}