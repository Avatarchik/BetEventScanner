using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Entities;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryTeamsDocEntity : IDocEntity, IEntity
    {
        public bool Uploaded { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();

        public ObjectId Id { get; set; }

        public int SqlId { get; set; }
    }
}
