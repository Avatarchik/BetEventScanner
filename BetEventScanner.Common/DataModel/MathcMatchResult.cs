using BetEventScanner.Common.Contracts;

namespace BetEventScanner.Common.DataModel
{
    public class MathcMatchResult : IMatchResult
    {
        public ITeam HomeTeam { get; set; }

        public ITeam AwayTeam { get; set; }

        public string Result { get; set; }

        public int HomeTeamScored { get; set; }

        public int AwayTeamScored { get; set; }

        public IStatistics Statistics { get; set; }
    }
}
