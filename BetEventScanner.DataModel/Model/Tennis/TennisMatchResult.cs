using System.Collections.Generic;

namespace BetEventScanner.DataModel.Model.Tennis
{
    public class TennisMatchResult : TennisMatch
    {
        public string FinalScore { get; set; }

        public ICollection<string> SetsResult { get; set; }
    }
}
