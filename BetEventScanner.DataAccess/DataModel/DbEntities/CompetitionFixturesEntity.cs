using System.Collections.Generic;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CompetitionFixturesEntity 
    {
        public int Id { get; set; }

        public List<FixtureDocEntity> Fixtures { get; set; } = new List<FixtureDocEntity>();
    }
}