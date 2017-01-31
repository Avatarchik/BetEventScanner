using System.Collections.Generic;
using System.Timers;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services
{
    public class OddsService
    {
        private readonly IOddsSourceService _oddsSourceService;
        private readonly IRepository<LeagueOdds> _oddsRepository;
        private readonly Timer _timer;

        public OddsService(IEnumerable<Division> supportedDivisions)
        {
            _oddsSourceService = new OddsSourceService(supportedDivisions);
            _oddsRepository = new OddsRepository<LeagueOdds>();
            _timer = new Timer(3600000); 
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            var odds = _oddsSourceService.GetOdds();
            _oddsRepository.InsertEntities(odds);

            _timer.Start();
        }
    }
}
