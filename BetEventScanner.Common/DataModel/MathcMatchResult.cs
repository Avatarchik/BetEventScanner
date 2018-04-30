using BetEventScanner.Common.Contracts;
using BetEventScanner.DataAccess.DataModel;

namespace BetEventScanner.Common.DataModel
{
    public class MathcMatchResult : IMatchResult
    {
        public FootballTeam HomeTeam { get; set; }

        public FootballTeam AwayTeam { get; set; }

        public string Result { get; set; }

        public int HomeTeamScored { get; set; }

        public int AwayTeamScored { get; set; }
    }
}
