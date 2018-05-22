using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionFixturesDocEntity : IDocEntity
    {
        public IList<CompetitionFixturesEntity> CompetitionFixtures { get; set; } = new List<CompetitionFixturesEntity>();

        public ObjectId Id { get; set; }
    }
}