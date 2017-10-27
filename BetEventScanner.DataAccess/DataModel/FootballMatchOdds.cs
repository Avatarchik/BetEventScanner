namespace BetEventScanner.DataAccess.DataModel
{
    public class FootballMatchOdds
    {
        public int Id { get; set; }

        public double HomeWin { get; set; }

        public double Draw { get; set; }

        public double AwayWin { get; set; }

        public double HomeWinOrDraw { get; set; }

        public double NoDraw { get; set; }

        public double AwayWinOrDraw { get; set; }

        public double Over25 { get; set; }

        public double Under25 { get; set; }
    }
}