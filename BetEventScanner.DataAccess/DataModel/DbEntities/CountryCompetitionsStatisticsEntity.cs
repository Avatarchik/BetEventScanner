using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsStatisticsEntity : IDocEntity
    {
        public int Id { get; set; } = 5;

        public IList<CompetitionStatisticsEntity> Statistics { get; set; } = new List<CompetitionStatisticsEntity>();
        
    }
}