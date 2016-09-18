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
				
				// Test
				
            Console.WriteLine("Service started");

            var globalSettings = GlobalSettingsReader.GetGlobalSettings();
            var footbalDataCountryMap = new FootballDataCountryMap();
            var footballDataApi = new FootballDataApiClient(globalSettings, footbalDataCountryMap);

            var footballDataService = new RequestService();
            footballDataService.Start();

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}


