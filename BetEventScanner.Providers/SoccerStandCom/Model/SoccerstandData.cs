using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.SoccerStandCom.Model
{
    public class SoccerstandData
    {
        public string Url { get; set; }

        public string Country { get; set; }

        public string Stage { get; set; }

        public string Caption { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public List<SoccerstandMatch> Matches { get; set; } = new List<SoccerstandMatch>();

        public void SetupAdditionalInfo()
        {
            var orderedMatches = Matches.OrderBy(x => x.Round).ToList();
            StartYear = orderedMatches.First().DateTime.Year;
            EndYear = orderedMatches.Last().DateTime.Year;
            var segments = Url.Split('/').Where(x=>!string.IsNullOrEmpty(x)).ToList();
            var index = segments.IndexOf("soccer") + 1;
            Country = segments[index];
            Stage = segments.Last();
            Caption = $"{Country}_{Stage}_{StartYear}_{EndYear}";
        }
    }
}