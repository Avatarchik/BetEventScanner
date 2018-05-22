using BetEventScanner.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetEventScanner.DogonWeb.Services
{
    public class CalculateService: ICalculateService
    {
        private readonly IDefaultUnitOfWork _uow;
        public CalculateService(IDefaultUnitOfWork uow)
        {
            _uow = uow;
        }

        public decimal CalculateBet(decimal currentCoef, int lineNumber)
        {
            return 4;
            //var prevFailedBets = _uow.BetInfo.AsQueryableNotTracking().Where(x => x.)
        }
    }
}