using System.Collections.Generic;

namespace SoccerStandParser
{
    public class SoccerstandData
    {
        public string Url { get; set; }

        public List<SoccerStandMatch> SoccerStandMatches { get; set; } = new List<SoccerStandMatch>();
    }
}