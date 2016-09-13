namespace BetEventScanner.Common.Contracts
{
    internal interface IFixture
    {
        ITeam HomeTeam { get; set; }

        ITeam AwayTeam { get; set; }

        string Result { get; set; }

        int HomeTeamScored { get; set; }

        int AwayTeamScored { get; set; }

    }
}