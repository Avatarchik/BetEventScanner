using BetEventScanner.DataAccess;
using BetEventScanner.DogonWeb.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BetEventScanner.DogonWeb.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IDefaultUnitOfWork _uow;
        public CalculateService(IDefaultUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PredictBetDto> CalculateBetsAsync(BetInfoDto betInfo)
        {
            var existsLines = _uow.Lines.AsQueryableNotTracking().Any();
            if (!existsLines) return null;

            var predictBet = new PredictBetDto
            {
                FLine = 1,
                SLine = 2,
                TLine = 3,
                FBet = await CalculateBetAsync(betInfo.FirstLineCoef, 1),
                SBet = await CalculateBetAsync(betInfo.SecondLineCoef, 2),
                TBet = await CalculateBetAsync(betInfo.ThirdLineCoef, 3)
            };

            return predictBet;

        }

        public async Task<decimal> CalculateBetAsync(decimal currentCoef, int lineNumber)
        {

            var linesList = await _uow.Lines.AsQueryableNotTracking()
                                .Where(w => w.LineNumber == lineNumber)
                                .OrderByDescending(x => x.Id)
                                .ToListAsync();

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