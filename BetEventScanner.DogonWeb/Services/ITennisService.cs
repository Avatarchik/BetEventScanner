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
        Task<PredictBetDto> ProcessBetLineAsync(BetInfoDto betInfoDto);

        Task<IEnumerable<BetInfoListDto>> GetBetsListAsync();

        Task<bool> UpdateBetAsync(BetInfoListDto betInfoListDto);

        Task<bool> CreateCalculatedBetAsync(BetInfoDto betInfoDto);

        Task<bool> RemoveBetAsync(int betId);
    }
}
