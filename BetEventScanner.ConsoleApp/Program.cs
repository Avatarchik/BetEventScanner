using System;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Services;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Football API only EPL

            //if (false)
            //{
            //    var comp_id = 1204;
            //    var apikey = "2bab3f7d-881b-a6d6-4bb7de8099b0";
            //    var urlStanding = $"http://football-api.com/api/?Action=standings&APIKey={apikey}&comp_id={comp_id}";

            //    var standing = RestApiService.GetData<Standing>(urlStanding);

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
            //}

            #endregion

            var globalSettings = GlobalSettingsReader.GetGlobalSettings();

            ICountryMap footbalDataCountryMap = new FootballDataCountryMap();
            var footballDataService = new FootballDataService(GlobalSettingsReader.GetGlobalSettings(), footbalDataCountryMap, null);
            footballDataService.Start();

            Console.ReadLine();
        }

        
    }
}


