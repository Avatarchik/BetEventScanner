using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel.DbEntities;
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
            return;
            foreach (var supportedLeague in _settings.SupportedLeagues)
            {
                var countryDivision = (CountryDivision)supportedLeague;
                var countryId = _footbalDataCountryMap.IdMap[countryDivision];

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
                var countryDivision = _footbalDataCountryMap.GetCompetitionByCode(competition.Code);
                if (countryDivision == 0)
                {
                    continue;
                }

                var country = GetCountryNameByDivision(countryDivision);
                _entitiesToStore.AddCompetition(country, competition.ToEntity());

                UploadFixtures(country, competition.Id);
            }
        }

        private void UploadFixtures(Country country, string id)
        {
            var url = string.Concat(_settings.Url, $"/competitions/{id}/fixtures");
            var fixtures = RestApiService.GetData<FixturesContract>(url);
            _entitiesToStore.AddFixtures(country, fixtures);
        }

        private  void StoreData()
        {
            var mongo = new MongoDbProvider();
            StoreTeams(mongo, _entitiesToStore.CountryTeamsStorage);
            StoreCompetitions(mongo, _entitiesToStore.Competitions);
            StoreFixtures(mongo, _entitiesToStore.Fixtures);
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

                foreach (var competitionEntity in competition.Value)
                {
                    mongo.InsertDocumentToCollection(competition.Key.ToString(), competitionEntity);
                }
            }
        }

        private void StoreFixtures(MongoDbProvider mongo, IDictionary<Country, IList<FixturesContract>> fixtures)
        {
            foreach (var countryFixtures in fixtures)
            {
                foreach (var divisionFixture in countryFixtures.Value)
                {
                    mongo.InsertDocumentToCollection(countryFixtures.Key.ToString(), divisionFixture);
                }
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
