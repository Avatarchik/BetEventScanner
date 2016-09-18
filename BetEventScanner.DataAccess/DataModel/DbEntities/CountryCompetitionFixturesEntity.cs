using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionFixturesEntity : IDocEntity
    {
        public int Id { get; set; } = 4;

        public IList<CompetitionFixturesEntity> CompetitionFixtures { get; set; } = new List<CompetitionFixturesEntity>();
    }
}