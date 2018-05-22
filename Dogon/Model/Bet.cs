using System;

namespace Dogon.Model
{
    public class Bet
    {
        public BetStatus BetStatus { get; set; } = BetStatus.Created;

        public decimal BetSum { get; set; }

        public decimal Odds { get; set; }

        public bool IsWon { get; set; }

        public DateTime Created { get; } = DateTime.Now;
    }
}