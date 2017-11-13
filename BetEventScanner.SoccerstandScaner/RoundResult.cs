using System.Collections.Generic;

namespace SoccerStandParser
{
    public class RoundResult
    {
        public string RoundNumber { get; set; }

        public ICollection<MatchResultInfo> Matches { get; set; } = new List<MatchResultInfo>();
    }
}