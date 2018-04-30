using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.Entities;

namespace BetEventScanner.Common.Contracts
{
    public interface IMatchResult
    {
        FootballTeam HomeTeam { get; set; }

        FootballTeam AwayTeam { get; set; }

        string Result { get; set; }

        int HomeTeamScored { get; set; }

        int AwayTeamScored { get; set; }
    }
}