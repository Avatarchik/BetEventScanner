using System;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Services.RestApiClient;

namespace BetEventScanner.Providers.FootballDataOrg
{
    public class FootballDataOrgApiClient : IApiClient
    {
        private readonly UriBuilder _uriBuilder;

        //public FootballDataApiClient(GlobalSettings settins, ICountryMap countryMap)
        //{
        //    _settins = settins;
        //    _uriBuilder = new UriBuilder(_settins.Url);
        //    _countryMap = countryMap;
        //}

        public T GetCountryCompetition<T>(int year)
        {
            var url = string.Concat("http://api.football-data.org/v1/", $"competitions/?season={year}");
            var currentCompetitions = RestApiService.GetData<T>(url, "get");
            return currentCompetitions;
        }
    }
}