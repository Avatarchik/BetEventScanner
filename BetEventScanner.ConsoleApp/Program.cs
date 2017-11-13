﻿using System;
using System.IO;
using System.Linq;
using BetEventScanner.Common.Services;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.Common.Services.FootballDataOrg;
using BetEventScanner.ConsoleApp.TheoryStrateges.BubbelLadderFromThree;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.EF;
using BetEventScanner.DataModel;
using FootballMatchResult = BetEventScanner.DataModel.FootballMatchResult;

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
            Console.WriteLine("Bubbel Ladder Three - 2");
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
                    new FootballDataOrgService().Test();
                    break;

                case 2:
                    Console.WriteLine(new Calculator().ClaculateBetValue(13.0m, 1.70m));
                    Console.ReadLine();
                    break;

                case 3:
                    var bubbelLadderFromThree = new BubbelLadderFromThree();
                    bubbelLadderFromThree.AddFlatBet(new FlatBet
                    {
                        Tournament = "Tennis. ITF. Men",
                        Player1Name = "Lozan Cristian",
                        Player2Name = "Neuchrist Maximilian",
                        SK1 = 1.25m,
                        SK1Descr = "2-0",
                        SK2 = 4.15m,
                        SK2Descr = "1-2",
                        SK3 = 6.50m,
                        SK3Descr = "0-2"
                    });

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
                    var fixtures = new FootballDataCoUkService().GetFixtures(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\fixtures.csv");
                    break;

                case 11:
                    new FixturesService().UpdateIncomingMatches();
                    break;

                default:
                    throw new NotSupportedException("Selection is not supported");
            }
        }

        private static void SqlMigration()
        {
            var files = Directory.GetFiles(@"C:\BetEventScanner\Services\FootballDataCoUk\Data");

            foreach (var file in files)
            {
                var coukservice = new FootballDataCoUkService();

                var results = coukservice.GetHistoricalMatches(file).Select(x => new DataAccess.DataModel.FootballMatchResult
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
            Func<FootballMatchResult, bool> filter = x => x.HomeOdds > 2 && x.AwayOdds > 2;

            HistoricalStatisticsProcessor.ProcessCsvFiles(new FootballDataCoUkService(), filter);

            Console.ReadLine();

            //var simulator = new Simulator();
            //simulator.Simulate(results);
        }

    }
}


