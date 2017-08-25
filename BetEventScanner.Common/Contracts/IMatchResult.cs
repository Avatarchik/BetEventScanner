using BetEventScanner.DataAccess.DataModel;

namespace BetEventScanner.Common.Contracts
{
    public interface IMatchResult
    {
        Team HomeTeam { get; set; }

        Team AwayTeam { get; set; }

        string Result { get; set; }

        int HomeTeamScored { get; set; }

        int AwayTeamScored { get; set; }
    }
}