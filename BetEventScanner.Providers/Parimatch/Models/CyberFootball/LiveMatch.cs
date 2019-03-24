using BetEventScanner.Providers.Domain;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class CyberFootballLiveMatch
    {
        public string EventNo { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public string OriginHtml { get; set; }

        public static string Key(CyberFootballLiveMatch lbm) =>
            $"{lbm.EventNo}{lbm.Player1.Team}{lbm.Player1.Name}{lbm.Player2.Team}{lbm.Player1.Name}";
    }
}
