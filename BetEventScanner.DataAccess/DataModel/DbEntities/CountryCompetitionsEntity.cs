using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryCompetitionsEntity : IEntity
    {
        public int Id { get; set; } = 3;

        public IList<CompetitionEntity> Competitions { get; set; } = new List<CompetitionEntity>();
    }
}