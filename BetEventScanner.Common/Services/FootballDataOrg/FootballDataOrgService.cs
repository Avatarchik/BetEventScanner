using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services.Common;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.Common.Services.FootballDataOrg.Model;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.Entities;

namespace BetEventScanner.Common.Services.FootballDataOrg
{
    public class FootballDataOrgService : IFootballService
    {
        private readonly GlobalSettings _globalSettings;
        private readonly IApiClient _apiClient;

        public FootballDataOrgService()
        {
            //_globalSettings = GlobalSettingsReader.GetGlobalSettings();
            _apiClient = new FootballDataApiClient();
            //_dbProvider = dbProvider;
        }

        public void Test()
        {
            var seasons = _apiClient.GetCountryCompetition<SeasonCompetitionsContract>();

            ApiLink fixtureLink = seasons.ToList().First(z => z.Code == "ELC").ApiLinks.Fixtures;

            var fixture = RestApiService.GetData<FixturesContract>(fixtureLink.Href);
        }

        private void UpdateCompetitions()
        {
            FootballDataCoUkService fds = new FootballDataCoUkService();
            return;
            
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


        public string Name { get; } = "ApiFootballDataOrgService";

        public IEnumerable<FootballMatchResult> GetAllResults()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FootballMatchResult> GetDivisionResults(CountryDivision countryDivision, DateTime fromDate)
        {
            throw new NotImplementedException();
        }
    }
}