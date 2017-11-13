using System.Collections.Generic;

namespace SoccerStandParser
{
    public class MatchResultInfo
    {
        public string Title { get; set; }

        public Dictionary<string, string> RawHtmlOdds { get; set; } = new Dictionary<string, string>();
    }
}