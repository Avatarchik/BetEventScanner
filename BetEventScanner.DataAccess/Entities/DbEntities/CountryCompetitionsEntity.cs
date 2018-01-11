using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsDocEntity : IDocEntity
    {
        public IList<CompetitionEntity> Competitions { get; set; } = new List<CompetitionEntity>();

        public ObjectId Id { get; set; }
    }
}