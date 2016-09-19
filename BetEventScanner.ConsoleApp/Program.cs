using System;
using System.Collections.Generic;
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

            console.writeline("service started");

            var globalsettings = globalsettingsreader.getglobalsettings();
            var footbaldatacountrymap = new footballdatacountrymap();
            var footballdataapi = new footballdataapiclient(globalsettings, footbaldatacountrymap);

            var footballdataservice = new requestservice();
            footballdataservice.start();

            console.writeline("press enter to continue...");
            console.readline();
        }
    }
}


