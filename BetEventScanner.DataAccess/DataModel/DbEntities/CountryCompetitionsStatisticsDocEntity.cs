using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsStatisticsDocEntity : IDocEntity
    {
        public IList<CompetitionStatisticsEntity> Statistics { get; set; } = new List<CompetitionStatisticsEntity>();

        public ObjectId Id { get; set; }
    }
}