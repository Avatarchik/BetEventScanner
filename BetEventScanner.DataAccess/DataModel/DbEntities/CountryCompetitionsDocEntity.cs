using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsDocEntity : IDocEntity
    {
        public int Id { get; set; } = 2;

        public IList<CompetitionEntity> Competitions { get; set; } = new List<CompetitionEntity>();
    }
}