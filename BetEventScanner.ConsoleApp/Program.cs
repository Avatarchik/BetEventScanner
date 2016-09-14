using System;
using BetEventScanner.Common.Contracts;
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

            var apikey = globalSettings.ApiKey;

            var standing = RestApiService.GetData<DivisionTeams>("http://api.football-data.org/v1/competitions/398/teams");

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

            ICountryMap footbalDataCountryMap = new FootballDataCountryMap();
            
            var footballDataApi = new FootballDataApiClient(globalSettings, footbalDataCountryMap);

            var footballDataService = new FootballDataService(footballDataApi, new MongoDbProvider());
            footballDataService.Start();

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

        }
    }
}


