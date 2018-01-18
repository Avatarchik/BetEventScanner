using System;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.Common.DataModel
{
    public class Status : IDocEntity
    {
        public ObjectId Id { get; set; }

        public bool OddsLoaded { get; set; }

        public bool FixtureLoaded { get; set; }

        public bool ServicesInitialized { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}