﻿using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class BettingEntity : IDocEntity
    {
        public BettingEntity()
        {
            Id = ObjectId.GenerateNewId();
            Matches = new List<Tmatch>();
            Money = new Money(1000m);
        }

        public int InternalId { get; set; }

        public ObjectId Id { get; set; }

        public ICollection<Tmatch> Matches { get; set; }

        public Money Money { get; set; }
    }
}
