using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryTeamsDocEntity : IDocEntity
    {
        public bool Uploaded { get; set; }

        public List<FootballTeam> Teams { get; set; } = new List<FootballTeam>();

        public ObjectId Id { get; set; }
    }
}
