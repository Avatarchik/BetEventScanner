using BetEventScanner.Common.Contracts;
using BetEventScanner.DataAccess.DataModel;

namespace BetEventScanner.Common.DataModel
{
    public class MathcMatchResult : IMatchResult
    {
        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public string Result { get; set; }

        public int HomeTeamScored { get; set; }

        public int AwayTeamScored { get; set; }
    }
}
