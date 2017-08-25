namespace BetEventScanner.ConsoleApp
{
    public class SimulationResult
    {
        public int MaxTransactionIteration { get; set; }

        public int Draw11Count { get; set; }

        public int H1Win21Count { get; set; }

        public int H2Win12Count { get; set; }

        public int MaxFixScoreTransaction { get; set; }

        public int TotalUnder15Count { get; set; }

        public int TotalUnder15Transaction { get; set; }

        public int TotalOver35Count { get; set; }

        public int TotalOver35Transaction { get; set; }

        public int HandicapH1Minus15Count { get; set; }

        public int HandicapH2Minus15Count { get; set; }

        public int HandicapTransaction { get; set; }

        public int EvenOneNotScoredCount { get; set; }

        public int EvenOneNotScoredTransaction { get; set; }
    }
}