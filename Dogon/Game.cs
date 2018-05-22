using System;
using System.Linq;
using Dogon.Model;

namespace Dogon
{
    public class Game
    {
        private readonly StateStorage _stateStorage;

        public Game(StateStorage stateStorage)
        {
            _stateStorage = stateStorage;
        }

        public void MakeBet(ReverseBet reverseBet)
        {
            if (_stateStorage.State.BetsList.FirstOrDefault(x => !x.Finished) != null)
            {
                return;
            }

            var bank = _stateStorage.State;

            reverseBet.BankBefore = _stateStorage.State.GetCurrentBank();
            reverseBet.Favorite.BetSum = DogonStrategy.CalculateDogon(reverseBet.Favorite.Odds, bank.Lost);
            reverseBet.Underdog.BetSum = DogonStrategy.CalculateDogon(reverseBet.Underdog.Odds, bank.Lost);

            bank.BetsList.Add(reverseBet);

            _stateStorage.Update();
        }

        public ReverseBet GetBetWithoutResult()
        {
            return _stateStorage.State.BetsList.Single(x => !x.Finished);
        }

        public void UpdateBetWithResult(ReverseBet bet)
        {
            bet.OperationResult = Math.Abs(Calculate(bet.Favorite) - Calculate(bet.Underdog));
            bet.Profit = bet.OperationResult;
            bet.Finished = true;

            _stateStorage.State.BetsList[_stateStorage.State.BetsList.IndexOf(bet)] = bet;

            _stateStorage.Update();
        }

        private decimal Calculate(Bet bet)
        {
            if (bet.IsWon)
            {
                return bet.Odds * bet.BetSum;
            }

            return bet.BetSum;
        }
    }
}