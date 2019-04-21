using BetEventScanner.DataModel;
using System;
using System.Globalization;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class CyberFootballMatch
    {
        public string EventNo { get; set; }
        public DateTime DateTime { get; set; }
        public string Competition { get; set; }
        public string Team1Origin { get; set; }
        public CyberFootballPlayer Player1 { get; set; }
        public string Team2Origin { get; set; }
        public CyberFootballPlayer Player2 { get; set; }
        public string OriginHtml { get; set; }
        public SportLiveMatch LiveMatch { get; set; }
        public string OriginResult { get; set; }
        public FootballResult Result { get; set; }

        public static string Key(CyberFootballMatch match) =>
            $"{match.EventNo}{match.Player1.Team}{match.Player1.Name}{match.Player2.Team}{match.Player1.Name}";

        public static CyberFootballMatch FromLiveMatch(SportLiveMatch liveMatch)
        {
            var evno = liveMatch.EventNo;

            var p1origin = liveMatch.Team1;
            var p2origin = liveMatch.Team2;

            if (string.IsNullOrEmpty(p1origin) || string.IsNullOrEmpty(p2origin))
                return null;

            var m = new CyberFootballMatch
            {
                EventNo = evno,
                Player1 = CyberFootballPlayer(p1origin),
                Player2 = CyberFootballPlayer(p2origin),
                LiveMatch = liveMatch
            };

            return m;
        }

        public static CyberFootballMatch FromHistoricalResult(CyberFootballHistoricalMatchResult historicalMatch)
        {
            var evno = historicalMatch.Evno;

            var p1origin = historicalMatch.HomeTeam;
            var p2origin = historicalMatch.AwayTeam;

            if (string.IsNullOrEmpty(p1origin) || string.IsNullOrEmpty(p2origin))
                return null;

            var m = new CyberFootballMatch();

            try
            {
                var dt = DateTime.Parse(historicalMatch.Date, CultureInfo.InvariantCulture);
                
                m.EventNo = evno;
                m.DateTime = dt;
                m.Competition = historicalMatch.Competition;
                m.Team1Origin = p1origin;
                m.Player1 = CyberFootballPlayer(p1origin);
                m.Team2Origin = p2origin;
                m.Player2 = CyberFootballPlayer(p2origin);
                m.OriginResult = historicalMatch.Result.Trim();
            }
            catch (Exception)
            {
            }

            try
            {
                m.Result = FootballResult.FromString(historicalMatch.Result);
            }
            catch (Exception)
            {
                m.Result = null;
            }

            return m; ;
        }

        private static CyberFootballPlayer CyberFootballPlayer(string str)
        {
            var startIndex = str.IndexOf("(");
            var endIndex = str.IndexOf(")");
            if (endIndex == -1)
                endIndex = str.Length;
            var p1n = str.Substring(++startIndex, endIndex - startIndex);
            var p1t = str.Substring(0, --startIndex).Trim();

            return new CyberFootballPlayer
            {
                Name = p1n,
                Team = p1t
            };
        }
    }
}
