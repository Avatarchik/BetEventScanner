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
        public static string footballUrl = "https://www.parimatch.com/en/res.html?&Date={0}&SK=21";

        public static string FootballUrl(DateTime date) =>
            string.Format(footballUrl, date.ToString("yyyyMMdd"));

        public static CyberFootballHistoricalMatchResult[] GetMatches(string container) =>
            ConvertMany(container).ToArray();


        private static IEnumerable<CyberFootballHistoricalMatchResult> ConvertMany(string container)
        {
            string competition = "";

            foreach (var item in container.GetCssNodes("table.TT > tbody"))
            {
                if (item.OuterHtml.GetCssNode("tr > th.TH") != null)
                {
                    competition = item.OuterHtml.GetCssNode("tr > th.TH").InnerText;
                    continue;
                }

                if (!competition.ToLower().Contains("cyberfootball"))
                    continue;

                yield return Create(competition, item.InnerHtml);
            }
        }

        private static CyberFootballHistoricalMatchResult Create(string competition, string html)
        {
            var monoRows = html.GetCssNodes("tr > td.Mono").Select(x => x.InnerText).ToArray();
            var namesRows = html.GetCssNodes("tr > td.Names").Select(x => x.InnerText).ToArray();

            return new CyberFootballHistoricalMatchResult
            {
                Date = monoRows[0],
                Competition = competition,
                HomeTeam = namesRows[0],
                AwayTeam = namesRows[1],
                Result = monoRows[1]
            };
        }
    }

    public class CyberFootballHistoricalMatchResult
    {
        public string Evno { get; set; }
        public SportType Sport { get; set; }
        public string Competition { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Result { get; set; }
        public string Date { get; set; }
    }
}
