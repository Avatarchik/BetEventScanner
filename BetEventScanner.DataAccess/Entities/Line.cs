using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.DataAccess.Entities
{
    public class Line: IEntity
    {
        public int Id { get; set; }

        public int LineNumber { get; set; }

        public decimal Coefficient { get; set; }

        public decimal Bet { get; set; }

        public decimal? Score { get; set; }

        public BetInfo BetInfo { get; set; }
    }
}
