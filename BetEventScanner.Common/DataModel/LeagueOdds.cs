using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace BetEventScanner.Common.DataModel
{
    public class LeagueOdds : IHaveObjectId
    {
        public LeagueOdds()
        {
            Maches = new List<MatchOdds>();
        }

        public ObjectId Id { get; set; }

        public Division Division { get; set; }

        public int OriginSourceLeagueId { get; set; }

        public OddsSource Source { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public IList<MatchOdds> Maches { get; set; }
    }

    public interface IHaveObjectId
    {
        ObjectId Id { get; set; }
    }
}