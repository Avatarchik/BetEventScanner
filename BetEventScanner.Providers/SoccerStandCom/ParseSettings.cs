using System.Collections.Generic;

namespace BetEventScanner.Providers.SoccerStandCom
{
    public class ParseSettings
    {
        public ICollection<string> ArchiveSeasonUrls { get; set; }

        public string CurrentSeasonUrl { get; set; }
    }
}