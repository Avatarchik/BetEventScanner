using System.Collections.Generic;
using System.Timers;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services.FootbalDataCoUk;

namespace BetEventScanner.Common.Services
{
    public class Bot
    {
        private readonly IEnumerable<IFootballService> _footballServices;
        // define timer period in appconfig
        private readonly Timer _timer;
        private IFixtureService _fixtureService;
        private readonly IStatusService _statusService;
        private Status _status;
        private IMappingService _mappingService;

        public Bot(IEnumerable<IFootballService> footballServices)
        {
            _footballServices = footballServices;
            //var settings = GlobalSettingsReader.GetGlobalSettings();
            _statusService = new StatusService();
            _timer = new Timer(10000);
            _timer.Elapsed += Timer_Elapsed;

            //_footballService = new ApiFootballDataOrgService(settings, new FootballDataApiClient(settings, new FootballDataCountryMap()), new MongoDbProvider());
        }

        public bool Started { get; private set; }

        public void Start()
        {
            Started = true;
            _timer.Start();
        }

        public void Stop()
        {
            Started = false;
            _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            ProcessStatus();
            
            _timer.Start();
        }

        private void ProcessStatus()
        {
            _status = _statusService.GetCurrentStatus() ?? new Status();

            if (_status.ServicesInitialized) return;

            Init();
            _status.ServicesInitialized = true;
            _statusService.StoreStatus(_status);
        }

        private void Init()
        {
            foreach (var footballService in _footballServices)
            {
                footballService.Init();
            }
        }
    }

    public interface IMappingService
    {
    }

    public interface IStatusService
    {
        Status GetCurrentStatus();

        void StoreStatus(Status status);
    }

    public interface IFixtureService
    {
        void GetTodayMatches();
    }
}