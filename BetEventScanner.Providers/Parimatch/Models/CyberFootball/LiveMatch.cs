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

    public class StatsTable
    {
        public StatsTableItem[] Positions { get; set; }
    }

    public class StatsTableItem
    {
        public int Pos { get; set; }
        public int Team { get; set; }
        public int Played { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }
        public int Diff { get; set; }
        public int Points { get; set; }
        public Form Form { get; set; }
    }

    public class Form
    {
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
    }
}
