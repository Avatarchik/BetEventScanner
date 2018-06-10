using BetEventScanner.DogonWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEventScanner.DogonWeb.Services
{
    public interface ICalculateService
    {
        Task<PredictBetDto> CalculateBetsAsync(BetInfoDto betInfo);

        Task<decimal> CalculateBetAsync(decimal currentCoef, int lineNumber);
    }
}
