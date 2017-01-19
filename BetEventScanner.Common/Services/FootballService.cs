using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.ResultsService;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class FootballService
    {
        private readonly GlobalSettings _globalSettings;
        private readonly IApiClient _apiClient;
        private readonly IDbProvider _dbProvider;

        public FootballService(GlobalSettings globalSettings,  IApiClient apiClient, IDbProvider dbProvider)
        {
            _globalSettings = globalSettings;
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
            FootballDataService fds = new FootballDataService();
            fds.ProcessData();

            IResultsService resService = new FootballDataCsvParser();
            var res = resService.GetResults("C:\\MongoDB\\parseData\\I1.csv");

        }

        private void UpdateStatistics()
        {
            //var stat = new StatisticsService();
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
}