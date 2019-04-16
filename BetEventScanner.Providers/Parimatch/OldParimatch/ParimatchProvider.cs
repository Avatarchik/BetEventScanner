using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetEventScanner.DataModel;
using HtmlAgilityPack;

namespace BetEventScanner.Providers.Parimatch
{
    public class HistoricalResultsParser
    {
        public static string footballUrl = $"https://www.parimatch.com/en/res.html?&Date={0}&SK=21";

        public static string FootballUrl(DateTime date) =>
            string.Format(footballUrl, date.ToString("yyyyMMdd"));

        public static HistoricalMatchResult[] GetMatches(string container) =>
            container.GetCssNodes("table.TT > tbody").Select(x => HistoricalMatchResult.FromString(x.InnerHtml)).ToArray();

        private HistoricalMatchResult[] ParseHistoricalResults(string htmlText)
        {
            var containers = htmlText.GetCssNodes("div.container");

            var l = new List<HistoricalMatchResult>();

            foreach (var c in containers)
            {
                var ms = Converter.ToHistoricalResultMatches(c.InnerHtml);
                if (ms == null || ms.Length == 0)
                    continue;

                l.AddRange(ms);
            }

            return l.ToArray();
        }

        private void ParseHistoricalResults(ParseSettings parseSettings)
        {
            var allFiles = _settings.BaseDirectory.GetFiles().Where(x => !x.Name.Contains("oddslist")).Select(x => x.Name).ToList();

            var dateTimeResolver = new MatchDateResolver(allFiles);

            foreach (var fileName in allFiles)
            {
                Console.WriteLine(fileName);
                var html = new HtmlDocument();

                var path = $"{_settings.ArchiveDirPath.FullName}\\{fileName}";

                html.LoadHtml(File.ReadAllText(path));
                var nodes = html.DocumentNode.SelectNodes(@"//div[@class=""container gray""]");

                foreach (var node in nodes)
                {
                    var name = node.SelectSingleNode("h3");
                    if (name.InnerText.ToUpper() != parseSettings.CountryDivision)
                    {
                        continue;
                    }

                    var table = node.QuerySelector("table[class=dt]");
                    var headers = table.QuerySelectorAll("tbody[class=processed] > tr > th").Select(x => x.InnerText).ToList();
                    var rows = table.QuerySelectorAll("tbody[class^=row]").ToList();

                    var source = rows[0].QuerySelectorAll("tr").Where(x => !string.IsNullOrEmpty(x.InnerText)).ToList();

                }
            }
        }
    }

    public class HistoricalMatchResult
    {
        public string Evno { get; set; }

        public SportType Sport { get; set; }

        public string Competition { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string Result { get; set; }

        public static HistoricalMatchResult FromString(string node)
        {
            return new HistoricalMatchResult();
        }
            
    }
}
