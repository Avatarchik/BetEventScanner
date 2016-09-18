using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
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
        }

        private void UpdateStatistics()
        {
            var stat = new StatisticsService();
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