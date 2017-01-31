using System;
using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.Common.DataModel
{
    public class Status : IEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public bool OddsLoaded { get; set; }

        public bool FixtureLoaded { get; set; }

        public bool ServicesInitialized { get; set; }
        
    }
}