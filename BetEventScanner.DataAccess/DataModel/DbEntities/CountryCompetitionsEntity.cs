using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsEntity : IDocEntity
    {
        public int Id { get; set; } = 3;

        public IList<CompetitionEntity> Competitions { get; set; } = new List<CompetitionEntity>();
    }
}