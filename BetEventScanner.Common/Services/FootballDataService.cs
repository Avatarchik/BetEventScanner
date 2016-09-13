using System;
using System.Collections.Generic;
using System.Timers;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    // From Daniel Freitag
    public class FootballDataService
    {
        private readonly ICountryMap _countryMap;
        private readonly string _baseUrl;
        // define timer period in appconfig
        private readonly Timer _timer;
        private FootballDataApiClient _apiClient;

        public FootballDataService(GlobalSettings globalSettings, ICountryMap countryMap, IDbProvider dbProvider)
        {
            _countryMap = countryMap;
            _baseUrl = globalSettings.Url;
            _apiClient = new FootballDataApiClient(globalSettings);
            _timer = new Timer(5000);
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
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

        public void GetData<T>(string url) where T : new()
        {
            throw new NotImplementedException();
        }

        public Country CreateCountryData(IEnumerable<string> countryIds)
        {
            var soccerseasons = new List<ApiSoccerSeason>();
            foreach (var countryId in countryIds)
            {
                var url = string.Concat(_baseUrl, "soccerseasons/" + countryId);
                var soccerseason = RestApiService.GetData<ApiSoccerSeason>(url);
                soccerseasons.Add(soccerseason);
            }

            var commonData = CreateCommonData(soccerseasons);
            var countryTeamsData = CreateCountryTeams(soccerseasons);
            //var seasons

            var country = new Country(commonData, countryTeamsData);

            return country;

        }

        private DataModel.Common CreateCommonData(IEnumerable<ApiSoccerSeason> soccerSeasons)
        {
            var leagues = new List<League>();
            foreach (var apiSoccerSeason in soccerSeasons)
            {
                var league = new League {LeagueId = apiSoccerSeason.LeagueId, Name = apiSoccerSeason.Name};
                leagues.Add(league);
            }
            var common = new DataModel.Common {Leagues = leagues};
            return common;
        }

        private List<Team> CreateCountryTeams(IEnumerable<ApiSoccerSeason> soccerSeasons)
        {
            var countryApiTeamsData = new List<ApiTeam>();
            var teams = new List<Team>();
            foreach (ApiSoccerSeason apiSoccerSeason in soccerSeasons)
            {
                var url = apiSoccerSeason.ApiLinks.Teams.Href;
                var countryTeams = RestApiService.GetData<ApiCountryTeams>(url);
                foreach (var apiTeam in countryTeams.Teams)
                {
                    countryApiTeamsData.Add(apiTeam);
                    var team = new Team(apiTeam);
                    teams.Add(team);
                }
            }

            return teams;
        }

        private List<Season> CreateSeasons(IEnumerable<ApiSoccerSeason> soccerSeasons)
        {
            var seasons = new List<Season>();
            foreach (var soccerSeason in soccerSeasons)
            {  
                var url = soccerSeason.ApiLinks.Fixtures.Href;
                var apiMatches = RestApiService.GetData<ApiMatches>(url);
                var matches = CreateMatches(apiMatches.Matches);
                var season = new Season(soccerSeason.Year, matches);
                seasons.Add(season);
            }
            return seasons;
        }

        private IEnumerable<Match> CreateMatches(IEnumerable<ApiMatch> apiMatches)
        {
            var list = new List<Match>();
            foreach (var apiMatch in apiMatches)
            {
                var match = new Match();
            }

            return list;
        }

    }


    
}