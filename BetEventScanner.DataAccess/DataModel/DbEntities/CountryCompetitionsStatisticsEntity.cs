using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsStatisticsEntity : IEntity
    {
        public int Id { get; set; } = 5;

        public IList<CompetitionStatisticsEntity> Statistics { get; set; } = new List<CompetitionStatisticsEntity>();
        
    }
}