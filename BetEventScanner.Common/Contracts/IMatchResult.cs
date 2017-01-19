namespace BetEventScanner.Common.Contracts
{
    public interface IMatchResult
    {
        ITeam HomeTeam { get; set; }

        ITeam AwayTeam { get; set; }

        string Result { get; set; }

        int HomeTeamScored { get; set; }

        int AwayTeamScored { get; set; }

        IStatistics Statistics { get; set; }
    }
}