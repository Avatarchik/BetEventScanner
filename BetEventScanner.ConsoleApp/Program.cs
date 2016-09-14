using System;
using System.Linq;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services;
using BetEventScanner.DataAccess.DataModel.Entities;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Football API only EPL

            var globalSettings = GlobalSettingsReader.GetGlobalSettings();
            var footbalDataCountryMap = new FootballDataCountryMap();
            var mongo = new MongoDbProvider();

            foreach (var supportedLeague in globalSettings.SupportedLeagues)
            {
                var country = (CountryDivisionEnum)supportedLeague;
                var countryId = footbalDataCountryMap.Map[country];

                var url = $"http://api.football-data.org/v1/competitions/{countryId}/teams";
                var divisionTeams = RestApiService.GetData<DivisionTeams>(url);

                var collectionName = GetCountryTeamsCollectionName(country);

                foreach (var divisionTeam in divisionTeams.Teams)
                {
                    divisionTeam.GetIdFromUrl();
                }

                mongo.CreateCollection(collectionName);
                
            }

            //    foreach (var team in standing.Teams.OrderByDescending(x => x.StandPoints))
            //    {
            //        Console.WriteLine($"{team.StandPosition} {team.StandTeamName} {team.StandPoints}");
            //    }

            //    var urlMatches = $"http://football-api.com/api/?Action=today&APIKey={apikey}&comp_id={comp_id}";
            //    var matchesServerData = RestApiService.GetData<SchedulerAndResults>(urlMatches);
            //    if (matchesServerData.Matches != null)
            //    {
            //        Console.WriteLine("Today is " + matchesServerData.Matches.Count() + "matches");
            //        var firstMatch = matchesServerData.Matches.FirstOrDefault();

            //        if (firstMatch != null)
            //        {
            //            var matchId = firstMatch.MatchId;
            //            var matchUrl =
            //                $"http://football-api.com/api/?Action=commentaries&APIKey={apikey}&match_id={matchId}";

            //            var matchCommentaries = RestApiService.GetData<MatchCommentaries>(matchUrl);

            //        }
            //    }


            return;

            #endregion

            Console.WriteLine("Service started");

         
            
            var footballDataApi = new FootballDataApiClient(globalSettings, footbalDataCountryMap);

            var footballDataService = new FootballDataService(footballDataApi, new MongoDbProvider());
            footballDataService.Start();

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

        }

        private static string GetCountryTeamsCollectionName(CountryDivisionEnum countryDivision)
        {
            foreach (var map in CountDivisionMap.Map)
            {
                if (map.Value.Contains(countryDivision))
                {
                    return map.Key.ToString();
                }
            }

            throw new Exception("Country by division not found");
        }
    }
}


