using System.Timers;
using BetEventScanner.Common.Contracts;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class FootballService
    {
        private readonly IApiClient _apiClient;
        private readonly IDbProvider _dbProvider;

        public FootballService(IApiClient apiClient, IDbProvider dbProvider)
        {
            _apiClient = apiClient;
            _dbProvider = dbProvider;
        }

        public void DoWork()
        {
            UpdateCompetitions();
            UpdateStatistics();
        }

        private void UpdateCompetitions()
        {
            throw new System.NotImplementedException();
        }

        private void UpdateStatistics()
        {
            throw new System.NotImplementedException();
        }

        //private SeasonCompetitionsContract GetApiData()
        //{
        //   
        //    var competitions = _apiClient.GetCountryCompetitionData<SeasonCompetitionsContract>();

        //   

        //    return competitions;
        //}

        //private void StoreData(SeasonCompetitionsContract data)
        //{
        //    _dbProvider.StoreData(data);
        //}
    }

    public class RequestService
    {
        // define timer period in appconfig
        private readonly Timer _timer;
        private readonly FootballService _footballService;

        public RequestService()
        {
            _timer = new Timer(10000);
            _timer.Elapsed += Timer_Elapsed;
            _footballService = new FootballService(new FootballDataApiClient(GlobalSettingsReader.GetGlobalSettings(), new FootballDataCountryMap()), new MongoDbProvider());
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
            _footballService.DoWork();
            _timer.Start();
        }

    }
}