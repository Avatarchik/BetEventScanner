using System;
using BetEventScanner.Common;
using BetEventScanner.Common.Services;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var adminService = new AdminFootballDataService();
            //adminService.Init();

            Console.WriteLine("service started");

            var globalsettings = GlobalSettingsReader.GetGlobalSettings();
            var footbaldatacountrymap = new FootballDataCountryMap();
            var footballdataapi = new FootballDataApiClient(globalsettings, footbaldatacountrymap);

            var footballdataservice = new RequestService();
            footballdataservice.Start();

            Console.WriteLine("press enter to continue...");
            Console.ReadLine();
        }
    }
}


