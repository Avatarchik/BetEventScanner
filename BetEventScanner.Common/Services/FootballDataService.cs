using System.Threading;
using System.Timers;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.DataAccess.Providers;
using Timer = System.Timers.Timer;

namespace BetEventScanner.Common.Services
{
    // From Daniel Freitag
    public class FootballDataService
    {
        private readonly IApiClient _apiClient;
        private readonly IDbProvider _dbProvider;
        // define timer period in appconfig
        private readonly Timer _timer;
        
        public FootballDataService(IApiClient apiClient, IDbProvider dbProvider)
        {
            _apiClient = apiClient;
            _dbProvider = dbProvider;
            _timer = new Timer(10000);
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var data = GetApiData();
            StoreData(data);
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

        private object GetApiData()
        {
            _timer.Stop();
            var data = _apiClient.GetData<SeasonCompetitionsContract>(string.Empty);
            _timer.Start();
            return data;
        }

        private void StoreData(object data)
        {
            _dbProvider.StoreData(data);
        }

    }
}