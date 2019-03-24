using System.Data.Entity;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.Entities;

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

        public DbSet<BetInfo> BetInfoes { get; set; }

        public DbSet<Line> Lines { get; set; }
    }
}
