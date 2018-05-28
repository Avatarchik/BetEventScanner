using BetEventScanner.DogonWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEventScanner.DogonWeb.Services
{
    public interface ITennisService
    {
        PredictBetDto ProcessBetLine(BetInfoDto betInfoDto);

        IEnumerable<BetInfoListDto> GetBetsList();

        bool UpdateBet(BetInfoListDto betInfoListDto);

        bool CreateCalculatedBet(BetInfoDto betInfoDto);

        bool SaveCalculatedBet(BetInfoDto betInfo);
    }
}
