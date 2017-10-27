using System;
using System.IO;
using System.Linq;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.Common.Services.FootballDataOrg;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.EF;
using BetEventScanner.DataModel;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        //private static BettongService _bettongService = new BettongService();

        static void Main(string[] args)
        {
            Console.WriteLine("Service started");

            // ToDo Investigate
            //_bettongService.Load();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("For exit enter \"quit\"");
                ShowMenu();
                var choose = Console.ReadLine();
                if (choose == "quit")
                {
                    break;
                }

                try
                {
                    ProcessMenu(choose);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Prees enter to confirm");
                    Console.ReadLine();
                }
            }
            // ToDo Investigate
            //_bettongService.Save();

            Console.WriteLine("press enter to continue...");
            Console.ReadLine();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("FootbalDataOrg Test - 1");
            Console.WriteLine("TestChampionship - 6");
            Console.WriteLine("Migrate to sql - 7");
            Console.WriteLine("Fixtures - 10");
            Console.WriteLine("IncomingMatches - 11");
        }

        private static void ProcessMenu(string choose)
        {
            int chooseInt;
            if (!int.TryParse(choose, out chooseInt))
            {
                throw new Exception("Input is not parsed");
            }

            switch (chooseInt)
            {
                case 1:
                    TestFootballDataOrg();
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    TestChampionship();
                    break;

                case 7:
                    SqlMigration();
                    break;

                case 10:
                    ParseFixtures();
                    break;

                case 11:
                    GetIncomingMatches();
                    break;

                default:
                    throw new NotSupportedException("Selection is not supported");
            }
        }

        private static void GetIncomingMatches()
        {
            var fs = new FixturesService();
            fs.UpdateIncomingMatches();
        }

        private static void ParseFixtures()
        {
            var coukservice = new FootballDataCoUkService();
            var fixtures = coukservice.GetFixtures(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\fixtures.csv");


        }

        private static void SqlMigration()
        {
            var files = Directory.GetFiles(@"C:\BetEventScanner\Services\FootballDataCoUk\Data");

            foreach (var file in files)
            {
                var coukservice = new FootballDataCoUkService();

                var results = coukservice.GetHistoricalMatches(file).Select(x => new FootballMatchResult
                {
                    DateTime = x.DateTime,
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    HomeScored = x.HomeScored,
                    AwayScored = x.AwayScored,
                    Odds = new FootballMatchOdds
                    {
                        HomeWin = x.HomeOdds,
                        Draw = x.DrawOdds,
                        AwayWin = x.AwayOdds,
                        Over25 = x.Over25Odds,
                        Under25 = x.Under25Odds
                    }
                }).ToList();

                var items = file.Split('_');
                var country = items[0].Split('\\').Last();
                var divisionCode = items[1];
                var startYear = int.Parse("20" + new string(items[2].Split('.')[0].Take(2).ToArray()));
                var endYear = int.Parse("20" + new string(items[2].Split('.')[0].Skip(2).Take(2).ToArray()));
                var season = new FootballSeason
                {
                    Country = country,
                    DivisionCode = divisionCode,
                    StartYear = startYear,
                    EndYear = endYear,
                    Results = results
                };

                using (var context = new BetEventScannerContext())
                {
                    context.Seasons.Add(season);
                    context.SaveChanges();
                }
            }
        }

        private static void TestChampionship()
        {
            Func<FootballResult, bool> filter = x => x.HomeOdds > 2 && x.AwayOdds > 2;

            HistoricalStatisticsProcessor.ProcessCsvFiles(new FootballDataCoUkService(), filter);

            Console.ReadLine();

            //var simulator = new Simulator();
            //simulator.Simulate(results);
        }

        private static void TestFootballDataOrg()
        {
            var service = new FootballDataOrgService();
            service.Test();
        }
    }
}


