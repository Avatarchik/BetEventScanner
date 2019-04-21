using BetEventScanner.DataAccess.Providers;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;

namespace ParimatchDayOddsParser.Services.PmCyberFootball
{
    public class Service
    {
        public void Save(IEnumerable<CyberFootballMatch> matches)
        {
            var mongo = new MongoDbProvider("parimatch");
            mongo.InsertMany("cyberfootball", matches);
        }
    }
}
