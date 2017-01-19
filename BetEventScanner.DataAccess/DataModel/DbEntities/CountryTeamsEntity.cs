using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryTeamsEntity : IEntity
    {
        public int Id { get; set; } = 2;

        public bool Uploaded { get; set; } = true;

        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
