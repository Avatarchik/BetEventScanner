using System;

namespace BetEventScanner.Common.DataModel
{
    public class CountrySeasonCard
    {
        public int CountryId { get; set; }

        public DateTime LastTrackTime { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
