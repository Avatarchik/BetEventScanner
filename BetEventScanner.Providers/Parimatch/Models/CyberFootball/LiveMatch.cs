using BetEventScanner.DataModel;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class CyberFootballLiveMatch
    {
        public string EventNo { get; set; }
        public CyberFootballPlayer Player1 { get; set; }
        public CyberFootballPlayer Player2 { get; set; }
        public string OriginHtml { get; set; }
        public SportLiveMatch LiveMatch { get; set; }

        public static string Key(CyberFootballLiveMatch lbm) =>
            $"{lbm.EventNo}{lbm.Player1.Team}{lbm.Player1.Name}{lbm.Player2.Team}{lbm.Player1.Name}";

        public static CyberFootballLiveMatch FromLiveMatch(SportLiveMatch liveMatch)
        {
            var evno = liveMatch.EventNo;

            var p1origin = liveMatch.Team1;
            var p2origin = liveMatch.Team2;

            if (string.IsNullOrEmpty(p1origin) || string.IsNullOrEmpty(p2origin))
                return null;

            var startIndex = p1origin.IndexOf("(");
            var endIndex = p1origin.IndexOf(")");
            var p1n = p1origin.Substring(++startIndex, endIndex - startIndex);
            var p1t = p1origin.Substring(0, --startIndex).Trim();

            var startIndex2 = p2origin.IndexOf("(");
            var endIndex2 = p2origin.IndexOf(")");
            var p2n = p2origin.Substring(++startIndex2, endIndex2 - startIndex2);
            var p2t = p2origin.Substring(0, --startIndex2).Trim();

            var m = new CyberFootballLiveMatch
            {
                EventNo = evno,
                Player1 = new CyberFootballPlayer
                {
                    Name = p1n,
                    Team = p1t
                },
                Player2 = new CyberFootballPlayer
                {
                    Name = p2n,
                    Team = p2t
                },
                LiveMatch = liveMatch
            };

            return m;
        }
    }
}
