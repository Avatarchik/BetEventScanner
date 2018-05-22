using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountrySeasonCompetitonsEntity : IEntity
    {
        public int Id { get; set; } = 2;

        public IEnumerable<CompetitionEntity> Competitions { get; set; }
    }

}
