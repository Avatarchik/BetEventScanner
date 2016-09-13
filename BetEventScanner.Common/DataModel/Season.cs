
using System.Collections.Generic;

namespace BetEventScanner.Common.DataModel
{
    public class Season
    {
        public Season(string year, IEnumerable<Match> matches)
        {
            Year = year;
            Matches = matches;
        }

        public string Year { get; set; }
        public IEnumerable<Match> Matches { get; set; }
    }
}