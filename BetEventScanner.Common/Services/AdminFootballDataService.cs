using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel.Entities;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class AdminFootballDataService
    {
        private readonly GlobalSettings _settings;
        private readonly ICountryMap _footbalDataCountryMap;
        private readonly EntitiesToStore _entitiesToStore;

        public AdminFootballDataService()
        {
            _settings = GlobalSettingsReader.GetGlobalSettings();
            _footbalDataCountryMap = new FootballDataCountryMap();
            _entitiesToStore = new EntitiesToStore();
        }

        public void Init()
        {
            UploadCountryTeams();
            UploadCompetitions();
            StoreData();
        }

        private void UploadCountryTeams()
        {
            foreach (var supportedLeague in _settings.SupportedLeagues)
            {
                var countryDivision = (CountryDivision)supportedLeague;
                var countryId = _footbalDataCountryMap.Map[countryDivision];

                var url = string.Concat(_settings.Url, $"/competitions/{countryId}/teams");
                var divisionTeams = RestApiService.GetData<DivisionTeamsContract>(url);

                var teamsList = new List<TeamEntity>();

                foreach (var divisionTeam in divisionTeams.Teams)
                {
                    divisionTeam.GetIdFromUrl();
                    teamsList.Add(new TeamEntity
                    {
                        Id = divisionTeam.Id,
                        Code = divisionTeam.Code,
                        ShortName = divisionTeam.ShortName,
                        Name = divisionTeam.Name
                    });
                }

                var country = GetCountryNameByDivision(countryDivision);

                _entitiesToStore.AddCountryTeam(country, teamsList);
            }
        }

        private void UploadCompetitions()
        {
            var year = DateTime.Now.Year;
            var url = string.Concat(_settings.Url, $"/competitions/?season={year}");
            var competitions = RestApiService.GetData<SeasonCompetitionsContract>(url);

            foreach (var competition in competitions)
            {
                var competitionId = int.Parse(competition.Id);
                var countryDivision = _footbalDataCountryMap.GetCompetitionById(competitionId);
                if (countryDivision == 0)
                {
                    continue;
                }

                var country = GetCountryNameByDivision(countryDivision);
                _entitiesToStore.AddCompetition(country, new CompetitionEntity
                {
                    Id = competition.Id,
                    ShortName = competition.ShortName,
                    Name = competition.Name,
                    Year = competition.Year,
                    NumberOfMatchdays = competition.NumberOfMatchdays,
                    NumberOfTeams = competition.NumberOfTeams,
                    NumberOfGames = competition.NumberOfGames,
                    LastUpdated = DateTime.Parse( competition.LastUpdated)
                });
            }
        }

        private  void StoreData()
        {
            var mongo = new MongoDbProvider();
            StoreTeams(mongo, _entitiesToStore.CountryTeamsStorage);
            StoreCompetitions(mongo, _entitiesToStore.Competitions);
        }

        private static void StoreTeams(MongoDbProvider mongo, IDictionary<Country, CountryTeamsEntity> countryTeams)
        {
            foreach (var countryTeamsEntity in countryTeams)
            {
                mongo.CreateCollection(countryTeamsEntity.Key.ToString());
                mongo.InsertDocumentToCollection(countryTeamsEntity.Key.ToString(), countryTeamsEntity.Value);
            }
        }

        private void StoreCompetitions(MongoDbProvider mongo, IDictionary<Country, IList<CompetitionEntity>> competitions)
        {
            foreach (var competition in competitions)
            {
                mongo.CreateCollection(competition.Key.ToString());
                mongo.InsertDocumentToCollection(competition.Key.ToString(), competition.Value);
            }
        }

        private static Country GetCountryNameByDivision(CountryDivision countryDivision)
        {
            foreach (var map in CountryDivisionMap.Map)
            {
                if (map.Value.Contains(countryDivision))
                {
                    return map.Key;
                }
            }

            throw new Exception("Country by division not found");
        }
    }
}
