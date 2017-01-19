using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel.DbEntities;
using BetEventScanner.DataAccess.Providers;
using Team = BetEventScanner.DataAccess.DataModel.DbEntities.Team;

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
            UploadCommon();
            UploadCountryTeams();
            UploadCompetitions();
            StoreData();
        }

        private void UploadCommon()
        {
            _entitiesToStore.AddCountries(_settings.SupportedCountries);
        }

        private void UploadCountryTeams()
        {
            foreach (var division in _settings.SupportedCountryDivisions)
            {
                var countryId = _footbalDataCountryMap.IdMap[division];

                var url = string.Concat(_settings.Url, $"/competitions/{countryId}/teams");
                var divisionTeams = RestApiService.GetData<DivisionTeamsContract>(url);

                var teamsList = new List<Team>();

                foreach (var divisionTeam in divisionTeams.Teams)
                {
                    divisionTeam.GetIdFromUrl();
                    teamsList.Add(divisionTeam.ToEntity());
                }

                var country = GetCountryNameByDivision(division);

                _entitiesToStore.AddCountryTeam(country, teamsList);
            }
        }

        private void UploadCompetitions()
        {
            var year = DateTime.Now.Year;
            var url = string.Concat(_settings.Url, $"/competitions/?season={year}");
            var competitionContracts = RestApiService.GetData<SeasonCompetitionsContract>(url);

            foreach (var competitioContract in competitionContracts)
            {
                var countryDivision = _footbalDataCountryMap.GetCompetitionByCode(competitioContract.Code);
                if (countryDivision == 0)
                {
                    continue;
                }

                if (!_settings.SupportedCountryDivisions.Contains(countryDivision))
                {
                    continue;
                }

                var country = GetCountryNameByDivision(countryDivision);

                var competitionEntity = competitioContract.ToEntity();

                _entitiesToStore.AddCompetition(country, competitionEntity);

                CreateStatiscs(country, competitioContract.Id);

                UploadFixtures(country, competitioContract.Id);
            }
        }

        private void CreateStatiscs(Country country, string id)
        {
            var statistics = new CompetitionStatisticsEntity
            {
                Id = int.Parse(id)
            };

            _entitiesToStore.AddStatistics(country, statistics);
        }


        private void UploadFixtures(Country country, string id)
        {
            var url = string.Concat(_settings.Url, $"/competitions/{id}/fixtures");
            var fixtureContracts = RestApiService.GetData<FixturesContract>(url);

            var entities = fixtureContracts.Fixtures.Select(x => x.ToEntity()).ToList();

            var fixtureEntities = new CompetitionFixturesEntity
            {
                Id = int.Parse(id),
                Fixtures = entities
            };

            _entitiesToStore.AddFixtures(country, fixtureEntities);
        }

        private  void StoreData()
        {
            var mongo = new MongoDbProvider();
            StoreCommon(mongo, _entitiesToStore.Countries);
            StoreStatistics(mongo, _entitiesToStore.Statistics);
            StoreTeams(mongo, _entitiesToStore.CountryTeamsStorage);
            StoreCompetitions(mongo, _entitiesToStore.Competitions);
            StoreFixtures(mongo, _entitiesToStore.Fixtures);
        }

        private void StoreStatistics(MongoDbProvider mongo, IDictionary<Country, CountryCompetitionsStatisticsEntity> statistics)
        {
            foreach (var stat in statistics)
            {
                mongo.InsertDocumentToCollection(stat.Key.ToString(), stat.Value);
            }
        }

        private void StoreCommon(MongoDbProvider mongo, List<Country> countries)
        {
            foreach (var country in countries)
            {
                var collectionName = country.ToString();
                mongo.CreateCollection(collectionName);
                mongo.InsertDocumentToCollection(collectionName, new GeneralTableEntity());
            }
        }

        private static void StoreTeams(MongoDbProvider mongo, IDictionary<Country, CountryTeamsEntity> countryTeams)
        {
            foreach (var countryTeamsEntity in countryTeams)
            {
                mongo.CreateCollection(countryTeamsEntity.Key.ToString());
                mongo.InsertDocumentToCollection(countryTeamsEntity.Key.ToString(), countryTeamsEntity.Value);
            }
        }

        private void StoreCompetitions(MongoDbProvider mongo, IDictionary<Country, CountryCompetitionsEntity> competitions)
        {
            foreach (var competition in competitions)
            {
                mongo.CreateCollection(competition.Key.ToString());
                mongo.InsertDocumentToCollection(competition.Key.ToString(), competition.Value);
            }
        }

        private void StoreFixtures(MongoDbProvider mongo, IDictionary<Country, CountryCompetitionFixturesEntity> fixtures)
        {
            foreach (var countryFixtures in fixtures)
            {
                mongo.CreateCollection(countryFixtures.Key.ToString());
                mongo.InsertDocumentToCollection(countryFixtures.Key.ToString(), countryFixtures.Value);
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

            throw new Exception("CountryClass by division not found");
        }
    }
}
