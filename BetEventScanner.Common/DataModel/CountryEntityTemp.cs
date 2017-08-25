using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.DataModel;
using MongoDB.Bson;

namespace BetEventScanner.Common.DataModel
{
    public class CountryEntityTemp : IDocEntity
    {
        public CountryEntityTemp(Common common, IEnumerable<Team> teams)
        {
            Common = common;
            Teams = new List<Team>(teams);
        }

        public ObjectId Id { get; set; }

        public Common Common { get; set; }

        public List<Team> Teams { get; set; }

        public List<Season> Seasons { get; set; }
    }
}
