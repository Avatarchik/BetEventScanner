using System;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        private static bool _isStatisticsUpdated = false;

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

            if (false)
            {
                var oddsService = new OddsService();
                var oddsData = oddsService.GetData();
            }

            //mongoProvider.GetData("Test");
            ////mongoProvider.SaveData();


            //var ss = @"http://api.football-data.org/v1/soccerseasons/394";
            //var test = GetData<SoccerSeason>(ss);
            ICountryMap footbalDataCountryMap = new FootballDataCountryMap();
            var footballDataService = new FootballDataService(GlobalSettingsReader.GetGlobalSettings(), footbalDataCountryMap, null);

            footballDataService.Start();

            var germany = footballDataService.CreateCountryData(new []{"394", "395", "403"});
            var italy = footballDataService.CreateCountryData(new[] { "401" });

            //var mongoProvider = new MongoDbProvider();
            //if (mongoProvider.CreateCollection("Germany"))
            //{
            //    mongoProvider.InitializeCollection("Germany", germany);
            //}

            //if (mongoProvider.CreateCollection("Italy"))
            //{
            //    mongoProvider.InitializeCollection("Italy", italy);
            //}

            Console.ReadLine();
        }

        
    }
}


