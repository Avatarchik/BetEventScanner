using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using HtmlAgilityPack;
using Newtonsoft.Json;

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

        public static void LoadByDates(ICollection<string> dates)
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
                web.BrowserTimeout = new TimeSpan(0, 0, 0);
                var html = web.LoadFromBrowser($"{_baseUrl}{dt}");
                File.WriteAllText($@"C:\BetEventScanner\Services\Parimatch\{dt}", html.ParsedText);
                var percent = (int)Math.Round(((++processedCount) / total) * 100);
                Console.WriteLine($"{dt}: done!, {percent}");
            }
        }

        public static void Parse()
        {
            foreach (var file in _baseDir.GetFiles())
            {
                var fileName = file.Name;
                Console.WriteLine(fileName);
                var html = new HtmlDocument();

                var generalInfo = new GeneralInfo
                {
                    Year = fileName.Substring(0, 4)
                };

                html.LoadHtml(File.ReadAllText($@"C:\BetEventScanner\Services\Parimatch\archive\{fileName}"));
                var nodes = html.DocumentNode.SelectNodes(@"//div[@class=""container gray""]");

                foreach (var node in nodes)
                {
                    var name = node.SelectSingleNode("h3");
                    if (name.InnerText.ToUpper() != "FOOTBALL. ENGLAND. CHAMPIONSHIP")
                    {
                        continue;
                    }

                    var table = node.QuerySelector("table[class=dt]");
                    var headers = table.QuerySelectorAll("tbody[class=processed] > tr > th").Select(x => x.InnerText).ToList();
                    var rows = table.QuerySelectorAll("tbody[class^=row]").ToList();

                    var source = rows[0].QuerySelectorAll("tr").Where(x => !string.IsNullOrEmpty(x.InnerText)).ToList();
                    var objToStore = new List<ParimatchFootballBetEvent>();
                    for (int i = 0; i < source.Count; i++)
                    {
                        var m = source[i];
                        var res = source[++i].QuerySelector("td > .p2r").InnerText;
                        var fbevent = ConvertRow(generalInfo, headers, m, res);
                        if (fbevent == null)
                        {
                            continue;
                        }
                        objToStore.Add(fbevent);  
                    }

                    File.WriteAllText($@"C:\BetEventScanner\Services\Parimatch\converted\{fileName}.json", JsonConvert.SerializeObject(objToStore));

                    //foreach (var r in )
                    //{
                    //    var data = rows[1].QuerySelectorAll("tr");

                    //    //var data = rows[1].QuerySelectorAll("tr[class=bk]");

                    //    ////var matchResult = null;

                    //    //foreach (var dataItem in data)
                    //    //{
                    //    //    Convert(generalInfo, headers, data[1]);
                    //    //}
                    //}
                }
            }
        }

        private static ParimatchFootballBetEvent ConvertRow(GeneralInfo info, List<string> headers, HtmlNode htmlNode, string result)
        {
            var res = new ParimatchFootballBetEvent();
            var row = htmlNode.QuerySelectorAll("td").ToList();
            for (var i = 0; i < row.Count; i++)
            {
                if (headers[i] == "#")
                {
                    continue;
                }

                switch (headers[i])
                {
                    case "Date":
                        var t = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                        var dm = t[0].Split('/');
                        var datetime = $"{info.Year}-{dm[1]}-{dm[0]} {t[1]}";
                        res.DateTime = DateTime.Parse(datetime);
                        break;

                    case "Event":
                        var teams = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                        res.HomeTeam = teams[0];
                        res.AwayTeam = teams[1];
                        break;

                    case "Hand.":
                        var handicaps = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                        res.HomeHandicap = handicaps[0];
                        res.AwayHandicap = handicaps[1];

                        var handicapOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.HomeHandicapOdds = handicapOdds[0];
                        res.AwayHandicapOdds = handicapOdds[1];
                        break;

                    case "Total":
                        res.Total = row[i].InnerText;
                        res.TotalOver = row[++i].QuerySelector("s").InnerText;
                        res.TotalUnder = row[++i].QuerySelector("s").InnerText;
                        break;

                    case "1":
                        res.HomeWin = row[i].QuerySelector("s")?.InnerText;
                        if (res.HomeWin == null) return null;
                        break;

                    case "X":
                        res.Draw = row[i].QuerySelector("s")?.InnerText;
                        if (res.Draw == null) return null;
                        break;

                    case "2":
                        res.AwayWin = row[i].QuerySelector("s")?.InnerText;
                        if (res.AwayWin == null) return null;
                        break;

                    case "1X":
                        res.HomeWinOrDraw = row[i].QuerySelector("s")?.InnerText;
                        if (res.HomeWinOrDraw == null) return null;
                        break;

                    case "12":
                        res.NoDraw = row[i].QuerySelector("s")?.InnerText;
                        if (res.NoDraw == null) return null;
                        break;

                    case "X2":
                        res.AwayWinOrDraw = row[i].QuerySelector("s")?.InnerText;
                        if (res.AwayWinOrDraw == null) return null;
                        break;

                    case "iTotal":
                        var indTotals = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                        res.IndTotalHome = indTotals[0];
                        res.IndTotalAway = indTotals[1];

                        var indTotalOverOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.IndTotalHomeOver = indTotalOverOdds[0];
                        res.IndTotalAwayOver = indTotalOverOdds[1];

                        var indTotalUnderOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.IndTotalHomeUnder = indTotalUnderOdds[0];
                        res.IndTotalAwayUnder = indTotalUnderOdds[1];
                        break;
                }
            }

            var tempRes = result.Substring(0, result.Length - 1);
            var parsedResult = tempRes.Split('(');
            res.FirstHalfScore = parsedResult[1];
            res.FinalScore = parsedResult[0];

            return res;
        }
    }

    public class GeneralInfo
    {
        public string Year { get; set; }
    }

    public class ParimatchFootballBetEvent
    {
        public DateTime DateTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string HomeHandicap { get; set; }

        public string HomeHandicapOdds { get; set; }

        public string AwayHandicap { get; set; }

        public string AwayHandicapOdds { get; set; }

        public string Total { get; set; }

        public string TotalOver { get; set; }

        public string TotalUnder { get; set; }

        public string HomeWin { get; set; }

        public string Draw { get; set; }

        public string AwayWin { get; set; }

        public string HomeWinOrDraw { get; set; }

        public string NoDraw { get; set; }

        public string AwayWinOrDraw { get; set; }

        public string IndTotalHome { get; set; }

        public string IndTotalHomeOver { get; set; }

        public string IndTotalHomeUnder { get; set; }

        public string IndTotalAway { get; set; }

        public string IndTotalAwayOver { get; set; }

        public string IndTotalAwayUnder { get; set; }

        public string FirstHalfScore { get; set; }

        public string FinalScore { get; set; }
    }
}
