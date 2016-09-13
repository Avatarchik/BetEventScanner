using System;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services
{
    public class FootballDataApiClient
    {
        private readonly GlobalSettings _settins;
        private UriBuilder _uriBuilder;

        public FootballDataApiClient(GlobalSettings settins)
        {
            _settins = settins;
            _uriBuilder = new UriBuilder(_settins.Url);
        }

        private void GetCompetitions()
        {
            var competitionUrl = _uriBuilder.Path = "/";
        }
    }
}