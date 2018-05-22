using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Entities;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class BettingEntity : IDocEntity
    {
        public BettingEntity()
        {
            Id = ObjectId.GenerateNewId();
            Matches = new List<Tmatch>();
            Results = new List<Tresult>();
            
            Money = new Money(1000m);
        }

        public int InternalId { get; set; }

        public ObjectId Id { get; set; }

        public ICollection<Tmatch> Matches { get; set; }

        public ICollection<Tresult> Results { get; set; }

        

        public Money Money { get; set; }
    }
}
