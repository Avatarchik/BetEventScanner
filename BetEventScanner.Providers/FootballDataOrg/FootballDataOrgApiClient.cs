using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Services.RestApiClient;

namespace BetEventScanner.Providers.FootballDataOrg
{
    public class FootballDataOrgApiClient : IApiClient
    {
        public T GetCountryCompetition<T>(int year)
        {
            var url = string.Concat("http://api.football-data.org/v1/", $"competitions/?season={year}");
            var currentCompetitions = RestApiService.GetData<T>(url, "get");
            return currentCompetitions;
        }
    }
}