using System;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services
{
    public class FootballDataApiClient : IApiClient
    {
        private readonly GlobalSettings _settins;
        private readonly ICountryMap _countryMap;
        private readonly UriBuilder _uriBuilder;

        public FootballDataApiClient(GlobalSettings settins, ICountryMap countryMap)
        {
            _settins = settins;
            _uriBuilder = new UriBuilder(_settins.Url);
            _countryMap = countryMap;
        }

        public T GetData<T>(string url)
        {
            var year = DateTime.Now.Year;
            var currentCompetitions = RestApiService.GetData<T>($"http://api.football-data.org/v1/competitions/?season={year}");
            return currentCompetitions;
        }
    }
}