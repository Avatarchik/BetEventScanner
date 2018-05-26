using BetEventScanner.DataAccess;
using BetEventScanner.DogonWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                                //.OrderByDescending(x => x.Id)
                                .OrderBy(x => x.Id)
                                .ToList();

            //.TakeWhile(t => t.Score != 0)
            // .Sum(s => s.Bet);
            var lostSum = linesList.SkipWhile(t => t.Score != 0).Sum(s => s.Bet);
            if (lostSum == 0) return 5;  //here should be default bet

            bool successBet = false;
            var newBet = lostSum;

            while (!successBet)
            {
                var teorityWinSum = newBet * currentCoef;
                //23 * 1.2  =27.6  coef = 2.18

                var profit = teorityWinSum - lostSum - newBet;

                if (profit < 0)
                {
                    newBet += 2;      //any value to make bet bigger
                    continue;
                }

                var percentProfit = profit * 100 / newBet;
                if (percentProfit < 20 && percentProfit > 10)
                {
                    return newBet;
                }
                else if (percentProfit < 10)
                {
                    newBet += 0.3M;
                }
                else
                {
                    newBet -= 0.3M;
                }
            }

            return newBet;
        }
    }
}