using System.Collections.Generic;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel.Entities;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class AdminFootballDataService
    {
        public void Init()
        {
            var settings = GlobalSettingsReader.GetGlobalSettings();
            var footbalDataCountryMap = new FootballDataCountryMap();
            var entitiesToStore = new EntitiesToStore();
            UploadCountryTeams(settings, footbalDataCountryMap, entitiesToStore);
            StoreData(entitiesToStore);
        }

        private void UploadCountryTeams(GlobalSettings settings, ICountryMap footbalDataCountryMap, EntitiesToStore entitiesToStore)
        {
            foreach (var supportedLeague in settings.SupportedLeagues)
            {
                var country = (CountryDivisionEnum)supportedLeague;
                var countryId = footbalDataCountryMap.Map[country];

                var url = $"http://api.football-data.org/v1/competitions/{countryId}/teams";
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

                entitiesToStore.AddCountryTeam(country, teamsList);
            }
        }

        private static void StoreData(EntitiesToStore entitiesToStore)
        {
            var mongo = new MongoDbProvider();
            StoreTeams(mongo, entitiesToStore.CountryTeamsStorage);
        }

        private static void StoreTeams(MongoDbProvider mongo, IDictionary<CountryEnum, CountryTeamsEntity> countryTeams)
        {
            foreach (var countryTeamsEntity in countryTeams)
            {
                mongo.CreateCollection(countryTeamsEntity.Key.ToString());
                mongo.InsertDocumentToCollection(countryTeamsEntity.Key.ToString(), countryTeamsEntity.Value);
            }
        }
    }
}
