using System.Collections.Generic;

namespace BetEventScanner.Providers
{
    public class BalanceFinanceStrategy
    {
        public void ProcessBetEvent(IFootballBetEvent betEvent)
        {
        }
    }

    public class OppositeBet
    {
        public int EventId { get; set; }

        public Bet MainBet { get; set; }

        public Bet AuxBet { get; set; }
    }

    public class Bet
    {
        public int EventId { get; set; }

        public string Type { get; set; }

        public decimal Odds { get; set; }
    }

    public class BettingStream
    {
        private List<Bet> _bets = new List<Bet>();

        public void Add(Bet bet)
        {
            _bets.Add(bet);
        }
    }
}
