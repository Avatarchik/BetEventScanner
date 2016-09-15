using System.Collections.Generic;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    public class CountryTeamsEntity
    {
        public bool Uploaded { get; set; } = true;

        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();
    }
}
