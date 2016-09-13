using System.Diagnostics;
using System.Timers;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    // From Daniel Freitag
    public class FootballDataService
    {
        private readonly ICountryMap _countryMap;
        private readonly string _baseUrl;
        // define timer period in appconfig
        private readonly Timer _timer;
        private FootballDataApiClient _apiClient;

        public FootballDataService(GlobalSettings globalSettings, ICountryMap countryMap, IDbProvider dbProvider)
        {
            _countryMap = countryMap;
            // todo remo it to api
            _baseUrl = globalSettings.Url;
            _apiClient = new FootballDataApiClient(globalSettings);
            _timer = new Timer(10000);
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Debugger.Break();
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
    }
}