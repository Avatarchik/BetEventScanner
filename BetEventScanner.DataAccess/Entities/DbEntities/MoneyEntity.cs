using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class Money : IDocEntity
    {
        public Money(decimal bank)
        {
            Bank = bank;
        }

        public ObjectId Id { get; set; }

        public decimal Bank { get; private set; }

        public decimal CurrentBet { get; set; }

        public decimal BaseBetValue { get; } = 3;

        public int Iteration { get; set; }

        public decimal OveralValue { get; set; }

        public decimal OppositOveralValue { get; set; }

        public decimal BasePercent { get; } = 30;

        public void CalculateOppositBetValue(ref ToppositBet oppositBet)
        {
            if (Iteration == 0)
            {
                oppositBet.Value = BaseBetValue;
                oppositBet.OppositValue = BaseBetValue;

                CurrentBet = oppositBet.Value - oppositBet.OppositValue;
                Bank -= CurrentBet;
                Iteration++;
            }

            oppositBet.Value = Calculate(OveralValue, oppositBet.Odds);
            oppositBet.OppositValue = Calculate(OppositOveralValue, oppositBet.OppositOdds);

            CurrentBet = oppositBet.Value - oppositBet.OppositValue;
            Bank -= CurrentBet;
            Iteration++;
        }

        private decimal Calculate(decimal overal, decimal odds)
        {
            decimal value = 0;

            while (value < overal * BasePercent)
            {
                value += BaseBetValue * 1.15m;
            }

            return value;
        }
    }
}
