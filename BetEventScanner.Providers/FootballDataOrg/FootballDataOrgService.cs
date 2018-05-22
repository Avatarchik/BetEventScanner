using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services.FootballDataOrg.Model;
using BetEventScanner.Common.Services.RestApiClient;
using BetEventScanner.Providers.FootballDataCoUk;
using BetEventScanner.Providers.FootballDataOrg.ApiDataModel;
using BetEventScanner.Providers.FootballDataOrg.Model;

namespace BetEventScanner.Providers.FootballDataOrg
{
    public class FootballDataOrgService : IFootballService
    {
        private readonly GlobalSettings _globalSettings;
        private readonly IApiClient _apiClient;

        public FootballDataOrgService()
        {
            //_globalSettings = GlobalSettingsReader.GetGlobalSettings();
            _apiClient = new FootballDataOrgApiClient();
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

        public string Name { get; } = "ApiFootballDataOrgService";

        public SourceProvider Provider => throw new NotImplementedException();

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