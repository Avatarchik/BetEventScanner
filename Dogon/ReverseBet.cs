using System;
using Dogon.Model;

namespace Dogon
{
    public class ReverseBet
    {
        public decimal BankBefore { get; set; }

        public Bet Favorite { get; set; }

        public Bet Underdog { get; set; }

        public DateTime Created { get; } = DateTime.Now;

        public decimal OperationResult { get; set; }

        public decimal Profit { get; set; }

        public bool Finished { get; set; }
    }
}