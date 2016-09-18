using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class OddsEntity
    {
        public string HomeWin { get; set; }

        public string Draw { get; set; }

        public string AwayWin { get; set; }
    }
}