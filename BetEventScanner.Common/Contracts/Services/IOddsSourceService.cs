using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Contracts.Services
{
    public interface IOddsSourceService
    {
        List<LeagueOdds> GetOdds();
    }
}
