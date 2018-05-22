using System;

namespace BetEventScanner.Providers.SoccerStandCom.Model
{
    public class SoccerstandMatch
    {
        public DateTime DateTime { get; set; }

        public string DateTimeOrigin { get; set; }

        public string OriginId { get; set; }

        public string Stage { get; set; }

        public string RoundOrigin { get; set; }

        public int Round { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string ResultOrigin { get; set; }

        public int HomeScored { get; set; }

        public int AwayScored { get; set; }

        public SoccerstandMatchType Type { get; set; }

        public SourceProvider SourceProvider { get; set; }

        public string Referee { get; set; }

        public string Venue { get; set; }

        public string Attendance { get; set; }

        public override string ToString()
        {
            var result = "-";
            if (!string.IsNullOrEmpty(ResultOrigin))
            {
                result = ResultOrigin;
            }

            return $"{Stage}; {DateTime}; {Round}; {HomeTeam} {result} {AwayTeam}; {Type}";
        }
    }
}