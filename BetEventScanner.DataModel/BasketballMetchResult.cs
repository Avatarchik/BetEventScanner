namespace BetEventScanner.DataModel
{
    public class BasketballMetchResult
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public BasketballPeriod[] Periods { get; set; }
    }
}
