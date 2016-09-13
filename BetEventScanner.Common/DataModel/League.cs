using System.Collections.Generic;

namespace BetEventScanner.Common.DataModel
{
    public class League
    {
        public string LeagueId { get; set; }
        public string Name { get; set; }
        public string LastUpdated { get; set; }
    }

    public class League1
    {
        public League1()
        {
            Maches = new List<OddsMatch>();
        }

        public string Name { get; set; }
        public IList<OddsMatch> Maches { get; set; }
    }
}
