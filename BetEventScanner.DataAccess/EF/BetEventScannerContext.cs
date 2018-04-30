using System.Data.Entity;
using BetEventScanner.DataAccess.DataModel;

namespace BetEventScanner.DataAccess.EF
{
    public class BetEventScannerContext : DbContext
    {
        public BetEventScannerContext() : base("BetEventScannerContext")
        {
        }

        public DbSet<Country> Countries { get; set; }
        
        public DbSet<City> Cities { get; set; }

        public DbSet<FootballTeam> Teams { get; set; }

        //public DbSet<FootballSeason> Seasons { get; set; }

        //public DbSet<FootballMatchResult> Results { get; set; }

        //public DbSet<FootballMatchOdds> Odds { get; set; }

        //public DbSet<IncomingMatch> IncomingMatches { get; set; }

        //public DbSet<FlatBet> FlatTennisBets { get; set; }

        //public DbSet<TennisPlayer> TennisPlayers { get; set; }
    }
}
