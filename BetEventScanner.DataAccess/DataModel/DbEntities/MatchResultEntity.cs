using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class MatchResultEntity 
    {
        public string GoalsHomeTeam { get; set; }

        public string GoalsAwayTeam { get; set; }
    }
}