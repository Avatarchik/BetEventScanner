namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class FootballResult
    {
        public int MatchHomeScored { get; set; }
        public int MatchAwayScored { get; set; }
        public int HalfTimeHomeScored { get; set; }
        public int HalfTimeAwayScored { get; set; }
        
        public static FootballResult FromString(string str)
        {
            var matchScores = str.GetUntilOrEmpty("(").Trim().Split(':');
            var halfTimeScores = str.ExtractBetween('(', ')').Trim().Split(':');

            return new FootballResult
            {
                MatchHomeScored = int.Parse(matchScores[0]),
                MatchAwayScored = int.Parse(matchScores[1]),
                HalfTimeHomeScored = int.Parse(halfTimeScores[0]),
                HalfTimeAwayScored = int.Parse(halfTimeScores[1]),
            };
        }
    }
}