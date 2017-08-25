using System;
using System.IO;
using System.Linq;
using BetEventScanner.Common;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.DataModel.DbEntities;
using BetEventScanner.DataAccess.EF;
using BetEventScanner.DataModel;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        private static BettongService _bettongService = new BettongService();

        static void Main(string[] args)
        {
            Console.WriteLine("Service started");

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
            _bettongService.Save();

            Console.WriteLine("press enter to continue...");
            Console.ReadLine();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("AddMatch - 1");
            Console.WriteLine("AddBet - 2");
            Console.WriteLine("AddResult - 3");
            Console.WriteLine("ShowMatches - 4");
            Console.WriteLine("CreateOppositBet - 5");
            Console.WriteLine("Fixtures");
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
                    var match = CreateMatch();
                    _bettongService.AddMatch(match);
                    break;

                case 2:
                    var bet = CreateBet();
                    _bettongService.AddBet(bet);
                    break;

                case 3:
                    var result = CreateResult();
                    _bettongService.AddResult(result);
                    break;

                case 4:
                    ShowMatches();
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

                default:
                    throw new NotSupportedException("Selection is not supported");
            }
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

                var results = coukservice.GetHistoricalMatches(file).Select(x=> new FootballMatchResult
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
                        AwayWin = x.AwayOdds
                    }
                }).ToList();

                var items = file.Split('_');
                var country = items[0].Split('\\').Last();
                var divisionCode = items[1];
                var startYear = int.Parse("20" + new string(items[2].Split('.')[0].Take(2).ToArray()));
                var endYear = int.Parse("20" + new string(items[2].Split('.')[0].Skip(2).Take(2).ToArray()));
                var countryDivisionSeason = new CountryDivisionSeason
                {
                    Country = country,
                    DivisionCode = divisionCode,
                    StartYear = startYear,
                    EndYear = endYear, 
                    Results = results
                };

                using (var context = new BetEventScannerContext())
                {
                    context.CountryDivisionSeasons.Add(countryDivisionSeason);
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

        private static Tresult CreateResult()
        {
            Console.WriteLine("Enter TeamId");
            var teamIdStr = Console.ReadLine();
            int matchId;
            if (int.TryParse(teamIdStr, out matchId))
            {
                throw new Exception("MatchId is not parsed");
            }

            Console.WriteLine("Enter HomeScored");
            var homeScoredStr = Console.ReadLine();
            int homeScored;
            if (int.TryParse(homeScoredStr, out homeScored))
            {
                throw new Exception("homeScored is not parsed");
            }

            Console.WriteLine("Enter AwayScored");
            var awayScoredStr = Console.ReadLine();
            int awayScored;
            if (int.TryParse(awayScoredStr, out awayScored))
            {
                throw new Exception("awayScored is not parsed");
            }

            return new Tresult(matchId, homeScored, awayScored);
        }

        private static Tmatch CreateMatch()
        {
            var newId = _bettongService.GetId();
            Console.WriteLine("Enter Team1");
            var team1 = Console.ReadLine();

            Console.WriteLine("Enter Team2");
            var team2 = Console.ReadLine();

            return new Tmatch(newId, team1, team2);
        }

        private static Tbet CreateBet()
        {
            Console.WriteLine("Enter MatchId");
            var matchIdstr = Console.ReadLine();
            int matchId;
            if (!int.TryParse(matchIdstr, out matchId))
            {
                throw new Exception("MatchId is not parsed");
            }

            Console.WriteLine("Enter Bet");
            var bet = Console.ReadLine();

            Console.WriteLine("Enter Value");
            var valueStr = Console.ReadLine();
            decimal value;
            if (!decimal.TryParse(valueStr, out value))
            {
                throw new Exception("Value is not parsed");
            }

            Console.WriteLine("Enter Odds");
            var oddsStr = Console.ReadLine();
            decimal odds;
            if (!decimal.TryParse(oddsStr, out odds))
            {
                throw new Exception("Odds is not parsed");
            }

            return new Tbet(matchId, bet, value, odds);
        }

        private static void ShowMatches()
        {
            foreach (var match in _bettongService.GetMatches())
            {
                Console.WriteLine($"{match.MatchId} {match.Team1} {match.Team2}");
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}


