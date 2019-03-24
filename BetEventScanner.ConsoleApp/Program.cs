using System;
using System.Collections.Generic;
using BetEventScanner.Providers.FootballDataCoUk;

namespace BetEventScanner.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
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
                case 5:

                    var fds = new FootballDataCoUkService();
                    //new FootballDataCoUkService().SmartParser();
                    //var h = new List<string> { "Div", "Date", "HomeTeam", "AwayTeam", "FTHG", "FTAG", "B365H", "B365D", "B365A" };
                    var h = new List<string> { "Date" };
                    var m = new FootballDataCoUkParser().GetDynamicHistoricalResults(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\Origin\E0_1617.csv", h);

                    break;

                case 10:

                    //var matches = new FootballDataCoUkParser().GetDynamicHistoricalResults(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\Origin\E1_1617.csv", headers);

                    //new Providers.Parimatch.Provider(new Providers.Parimatch.ParimatchSettings()).Parse(new Providers.Parimatch.ParseSettings { CountryDivision = "FOOTBALL. ENGLAND. CHAMPIONSHIP" });
                    return;

                //var uow = new UnitOfWork();
                //foreach (var item in matches)
                //{
                //    l.Add(item["HomeTeam"].ToString());
                //}

                //uow.Counties.Create(new Country
                //{
                //    Name = "England",
                //    Cities = new List<City>
                //{
                //    new City { Name = "Default" }
                //}
                //});
                //var coid = uow.Commit();

                //foreach (var item in l.Distinct())
                //{
                //    uow.FootballTeams.Create(new FootballTeam
                //    {
                //        Name = item,
                //        Country = new Country
                //        {
                //            Name = "England"
                //        },
                //        City = new City
                //        {
                //            CountryId = 2,
                //            Name = "Undefined"
                //        }

                //    });
                //}

                //uow.Commit();

                default:
                    throw new NotSupportedException("Selection is not supported");
            }
        }
    }
}


