using System;
using System.Collections.Generic;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.ResultsService;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class ApiFootballDataOrgService : IFootballService
    {
        private readonly GlobalSettings _globalSettings;
        private readonly IApiClient _apiClient;
        private readonly IDbProvider _dbProvider;

        public ApiFootballDataOrgService()
        {
            _globalSettings = GlobalSettingsReader.GetGlobalSettings();
            //_apiClient = apiClient;
            //_dbProvider = dbProvider;
        }

        public void DoWork()
        {
            UpdateCompetitions();
            UpdateStatistics();
        }

        private void UpdateCompetitions()
        {
            FootballDataCoUkService fds = new FootballDataCoUkService();
            fds.Init();

            return;
            
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
        public string Name { get; } = "ApiFootballDataOrgService";

        public void Init()
        {
        }

        public IEnumerable<IMatchResult> GetAllResults()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IMatchResult> IFootballService.GetLastResults(CountryDivision countryDivision, DateTime fromDate)
        {
            throw new NotImplementedException();
        }

        public void GetLastResults(CountryDivision countryDivision, DateTime fromDate)
        {

        }
    }
}