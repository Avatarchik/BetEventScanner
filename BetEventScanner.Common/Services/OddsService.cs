using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;
using log4net;

namespace BetEventScanner.Common.Services
{
    public class OddsService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (OddsService));
        private readonly IOddsSourceService _oddsSourceService;
        private readonly IRepository<LeagueOdds> _oddsRepository;
        private readonly Timer _timer;

        public OddsService(IEnumerable<Division> supportedDivisions)
        {
            _oddsSourceService = new OddsSourceService(supportedDivisions);
            _oddsRepository = new OddsRepository<LeagueOdds>();
            //_timer = new Timer(TimeSpan.FromHours(1).TotalMilliseconds); 
            _timer = new Timer(5000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _logger.Info("Start getting odds");
            var todayOdds = _oddsRepository.GetEntities();
            _logger.Info($"Got Today {todayOdds.Count()} odds");
            if (todayOdds.Any(x=>x.Created.Date == DateTime.UtcNow.Date))
            {
                _logger.Info($"Today odds exists");
                _timer.Start();
                return;
            }

            var odds = _oddsSourceService.GetOdds();
            _logger.Info($"Got New {odds.Count()} odds");
            _oddsRepository.InsertEntities(odds);
            _logger.Info($"Odds stored");
            _timer.Start();
        }
    }
}
