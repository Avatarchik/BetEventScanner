using System;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class ToppositBet
    {
        public ToppositBet(int matchId, string bet, decimal odds, decimal opOdds)
        {
            MatchId = matchId;
            Bet = bet;
            Odds = odds;
            OppositOdds = opOdds;
        }

        public int MatchId { get; set; }

        public string Bet  { get; set; }

        public decimal Value { get; set; }

        public decimal Odds { get; set; }

        public decimal OppositValue { get; set; }

        public decimal OppositOdds { get; set; }

        public DateTime DateTime { get; set; }
    }
}