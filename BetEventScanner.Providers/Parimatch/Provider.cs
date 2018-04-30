using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public class Provider
    {
        private static DirectoryInfo _baseDir = new DirectoryInfo(@"C:\BetEventScanner\Services\Parimatch\archive");
        private static string _baseUrl = @"https://www.parimatch.com/en/bet.html?ha=";

        public void Start()
        {
            //var driver = new ChromeDriver();
            //var today = DateTime.Now.Date;

            ////List<string> tableCategorieslist = File.ReadAllLines("C:\\scores\\categories.txt").ToList();

            //foreach (var date in listOfDates)
            //{
            //    var url = "https://www.parimatch.com/en/bet.html?ha=" + date;
            //    driver.Navigate
            //}
        }

        public static void ParseArchiveDates(ICollection<string> dates)
        {
            var distinct = dates.Distinct();
            var total = distinct.ToList().Count;
            var processedCount = 1.0;

            var existingFiles = _baseDir.GetFiles().Select(x => x.Name).ToList();

            foreach (var date in distinct)
            {
                var dt = DateTime.Parse(date).ToString("yyyyMMdd");
                Console.WriteLine(dt);
                if (existingFiles.Contains(dt))
                {
                    ++processedCount;
                    continue;
                }

                Console.WriteLine("Processing " + dt);
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                web.BrowserTimeout = new TimeSpan(0,0,0);
                var html = web.LoadFromBrowser($"{_baseUrl}{dt}");
                File.WriteAllText($@"C:\BetEventScanner\Services\Parimatch\{dt}", html.ParsedText);
                var percent = (int)Math.Round(((++processedCount) / total) * 100);
                Console.WriteLine($"{dt}: done!, {percent}");             
            }
        }
    }


    public class ParimatchFootballBetEvent
    {
        public DateTime DateTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string Handicap { get; set; }

        public string HomeHandicap { get; set; }

        public string AwayHandicap { get; set; }

        public string Total { get; set; }

        public string Over { get; set; }

        public string Under { get; set; }

        public string HomeWin { get; set; }

        public string Draw { get; set; }

        public string AwayWin { get; set; }

        public string HomeWinOrDraw { get; set; }

        public string NoDraw { get; set; }

        public string AwayWinOrDraw { get; set; }

        public string IndTotalHome { get; set; }

        public string IndTotalAway { get; set; }
    }
}
