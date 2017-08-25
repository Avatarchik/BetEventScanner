using System.Data.Entity;
using BetEventScanner.DataAccess.DataModel;

namespace BetEventScanner.DataAccess.EF
{
    public class BetEventScannerContext : DbContext
    {
        public BetEventScannerContext() : base("BetEventScannerContext")
        {
        }

        public DbSet<CountryDivisionSeason> CountryDivisionSeasons { get; set; }

        public DbSet<FootballMatchResult> FootballMatchResults { get; set; }

        public DbSet<FootballMatchOdds> FootballMatchOdds { get; set; }

        public DbSet<IncomingMatch> IncomingMatches { get; set; }
    }

    public class IncomingMatch
    {
        public int  Id { get; set; }

        public int MatchId { get; set; }
    }
}
