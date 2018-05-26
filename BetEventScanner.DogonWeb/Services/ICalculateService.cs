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
        PredictBetDto CalculateBets(BetInfoDto betInfo);

        decimal CalculateBet(decimal currentCoef, int lineNumber);
    }
}
