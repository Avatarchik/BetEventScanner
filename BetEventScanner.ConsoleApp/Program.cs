using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetEventScanner.ConsoleApp.TheoryTesters;
using BetEventScanner.DataAccess;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.Providers.FootballDataCoUk;
using BetEventScanner.Providers.FootballDataOrg;
using BetEventScanner.Providers.SoccerStandCom;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        //private static BettongService _bettongService = new BettongService();

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Service started");

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

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("FootbalDataOrg Test - 1");
            Console.WriteLine("Bubbel Ladder Three - 2");
            Console.WriteLine("SoccerStand parser - 4");
            Console.WriteLine("TestChampionship - 6");
            Console.WriteLine("Migrate to sql - 7");
            Console.WriteLine("Fixtures - 10");
            Console.WriteLine("IncomingMatches - 11");
            Console.WriteLine("IncomingMatches - 12");
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
                    break;

                case 3:
                    var parser1 = new SoccerStandParser(null, null);
                    parser1.UpdateMatchDetails();
                    break;

                case 4:
                    var parser = new SoccerStandParser(null, null);
                    parser.ParseCurrentSeasons();
                    break;

                case 5:

                    var fds = new FootballDataCoUkService();
                    //new FootballDataCoUkService().SmartParser();
                    //var h = new List<string> { "Div", "Date", "HomeTeam", "AwayTeam", "FTHG", "FTAG", "B365H", "B365D", "B365A" };
                    var h = new List<string> { "Date" };
                    var m = new FootballDataCoUkParser().GetDynamicHistoricalResults(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\Origin\E0_1617.csv", h);


                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 10:
                    var fixtures = new FootballDataCoUkService();
                    //new FootballDataCoUkService().SmartParser();
                    var headers = new List<string> { "Date" };
                    var matches = new FootballDataCoUkParser().GetDynamicHistoricalResults(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\Origin\E1_1617.csv", headers);

                    var dates = matches.Select(x => x["Date"].ToString()).ToList();

                    Providers.Parimatch.Provider.ParseArchiveDates(dates);
                    return;
                    var l = new List<string>();
                    var uow = new UnitOfWork();
                    foreach (var item in matches)
                    {
                        l.Add(item["HomeTeam"].ToString());
                    }

                    uow.Counties.Create(new Country
                    {
                        Name = "England",
                        Cities = new List<City>
                    {
                        new City { Name = "Default" }
                    }
                    });
                    var coid = uow.Commit();

                    foreach (var item in l.Distinct())
                    {
                        uow.FootballTeams.Create(new FootballTeam
                        {
                            Name = item,
                            Country = new Country
                            {
                                Name = "England"
                            },
                            City = new City
                            {
                                CountryId = 2,
                                Name = "Undefined"
                            }

                        });
                    }

                    uow.Commit();

                    break;

                case 11:
                    new FixturesService().UpdateIncomingMatches();
                    break;

                default:
                    throw new NotSupportedException("Selection is not supported");
            }
        }
    }
}


