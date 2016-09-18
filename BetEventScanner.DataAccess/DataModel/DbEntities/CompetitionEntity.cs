using System;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CompetitionEntity 
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public int CurrentMatchday { get; set; }

        public int NumberOfMatchdays { get; set; }

        public int NumberOfTeams { get; set; }

        public int NumberOfGames { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
