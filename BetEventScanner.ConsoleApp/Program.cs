using System;
using System.Collections.Generic;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services;
using BetEventScanner.Common.Services.FootbalDataCoUk;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var adminService = new AdminFootballDataService();
            //adminService.Init();

            Console.WriteLine("service started");

            var oddsService = new OddsService(new List<Division> { Division.EnglandApl });

            //var footbaldatacountrymap = new FootballDataCountryMap();
            //var footballdataapi = new FootballDataApiClient(globalsettings, footbaldatacountrymap);

            var services = new List<IFootballService>
            {
                new FootballDataCoUkService(),
                new ApiFootballDataOrgService()
            };

            var bot = new Bot(services);
            bot.Start();

            Console.WriteLine("press enter to continue...");
            Console.ReadLine();
        }
    }
}


