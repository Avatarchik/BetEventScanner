namespace BetEventScanner.DataModel
{
    public interface IFootballResult
    {
        string HomeTeam { get; set; }

        string AwayTeam { get; set; }

        int HomeScored { get; set; }

        int AwayScored { get; set; }

        double HomeOdds { get; set; }

        double DrawOdds { get; set; }

        double AwayOdds { get; set; }
    }
}