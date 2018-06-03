using BetEventScanner.DataAccess;
using BetEventScanner.DogonWeb.Models;
using System.Linq;

namespace BetEventScanner.DogonWeb.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IDefaultUnitOfWork _uow;
        public CalculateService(IDefaultUnitOfWork uow)
        {
            _uow = uow;
        }

        public PredictBetDto CalculateBets(BetInfoDto betInfo)
        {
            var existsLines = _uow.Lines.AsQueryableNotTracking().Any();
            if (!existsLines) return null;

            var predictBet = new PredictBetDto
            {
                FLine = 1,
                SLine = 2,
                TLine = 3,
                FBet = CalculateBet(betInfo.FirstLineCoef, 1),
                SBet = CalculateBet(betInfo.SecondLineCoef, 2),
                TBet = CalculateBet(betInfo.ThirdLineCoef, 3)
            };

            return predictBet;

        }

        public decimal CalculateBet(decimal currentCoef, int lineNumber)
        {

            var linesList = _uow.Lines.AsQueryableNotTracking()
                                .Where(w => w.LineNumber == lineNumber)
                                .OrderByDescending(x => x.Id)
                                .ToList();

            var lostSum = linesList.TakeWhile(t => t.Score == null).Sum(s => s.Bet);
            if (lostSum == 0) return 5;  //here should be default bet

            bool successBet = false;
            var newBet = lostSum;
            if (newBet < currentCoef) return lostSum;

            while (!successBet)
            {
                var teorityWinSum = newBet * currentCoef;
                var profit = teorityWinSum - lostSum - newBet;

                if (profit < 0)
                {
                    newBet += 2;      //any value to make bet bigger
                    continue;
                }

                var percentProfit = profit * 100 / newBet;
                if (percentProfit <= 20 && percentProfit > 10)
                {
                    return newBet;
                }
                else if (percentProfit < 10)
                {
                    newBet += 0.2M;
                }
                else
                {
                    newBet -= 0.2M;
                }
            }

            return newBet;
        }
    }
}