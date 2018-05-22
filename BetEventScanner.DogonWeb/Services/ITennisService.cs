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
        bool ProcessBetLine(BetInfoDto betInfoDto);

        IEnumerable<BetInfoListDto> GetBetsList();

        bool UpdateBet(BetInfoListDto betInfoListDto);
    }
}
