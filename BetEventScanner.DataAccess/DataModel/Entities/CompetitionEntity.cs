using System;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    public class CompetitionEntity
    {
        public string Id { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public string NumberOfMatchdays { get; set; }

        public string NumberOfTeams { get; set; }

        public string NumberOfGames { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
