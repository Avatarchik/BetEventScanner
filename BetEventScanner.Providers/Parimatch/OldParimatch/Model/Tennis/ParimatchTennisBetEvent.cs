using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataModel.Model.Tennis;
using MongoDB.Bson;

namespace BetEventScanner.Providers.Parimatch.Model
{
    public class ParimatchTennisBetEvent : GeneralTennisSetsOdds, IParimatchEvent, IDocEntity
    {
        public string ParimatchId { get; set; }

        public EventType EventType { get; set; }

        public string Header { get; set; }

        public ObjectId Id { get; set; }
    }
}