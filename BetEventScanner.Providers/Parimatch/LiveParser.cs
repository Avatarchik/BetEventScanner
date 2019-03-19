using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public class LiveParser
    {
        public static string LiveUrl => "https://www.parimatch.com/en/live.html";
        public static string SubLiveUrlTemplate = "https://www.parimatch.com/en/bet.html?ARDisabled=on&hl={0}";

        public CyberFootballLiveMatch[] ConvertToLiveBetEvets(string[] htmls) =>
            htmls.SelectMany(Converter.ToLiveBetMatches).ToArray();

        public SportLiveMatch[] GetListLiveEvents(string container)
        {
            var headers = container.GetCssNode("table.dt > tbody").InnerHtml.GetCssNodes("tr > th").Select(x => x.InnerText).ToList();

            var res = new List<SportLiveMatch>();

            foreach (var le in container.GetCssNodes("div.sport"))
            {
                var sportType = le.InnerHtml.GetCssNode("input.ch_s").Attributes["value"].Value;
                var subItems = le.InnerHtml.GetCssNodes("div.subitem > table");

                foreach (var item in subItems)
                {
                    var evno = item.Attributes["evno"].Value;
                    var infoBlock = item.InnerHtml.GetCssNode("tbody > tr > td.td_n > a");
                    var link = infoBlock.Attributes["href"].Value;
                    var teams = CreateTeams(infoBlock);
                    if (teams.Count < 2)
                    {
                        res.Add(SportLiveMatch.Error("name", infoBlock.FirstChild.InnerText));
                        continue;
                    }
                    var result = infoBlock.InnerHtml.GetCssNode("span.score").InnerText;

                    var slm = new SportLiveMatch
                    {
                        EventNo = evno,
                        SportType = sportType,
                        Team1 = teams[0],
                        Team2 = teams[1],
                        Result = result,
                    };

                    var requireOddsCount = GetHeaderCount(sportType);
                    var odds = item.InnerHtml.GetCssNodes("td>u>a").ToList();

                    if (odds.Count != requireOddsCount)
                    {
                        res.Add(SportLiveMatch.Error("oods!=headers", $"odds:{odds.Count}-header:{headers.Count}"));
                        continue;
                    }

                    slm.Win1betKey = odds[0].Id;
                    slm.Win1Odds = odds[0].InnerText;

                    slm.DrawbetKey = odds[1].Id;
                    slm.DrawOdds = odds[1].InnerText;

                    slm.Win2betKey = odds[2].Id;
                    slm.Win2Odds = odds[2].InnerText;

                    slm.Fora1Value = odds[3].InnerText;
                    slm.Fora1betKey = odds[4].Id;
                    slm.Fora1Odds = odds[4].InnerText;

                    slm.Fora2Value = odds[5].InnerText;
                    slm.Fora2betKey = odds[6].Id;
                    slm.Fora2Odds = odds[6].InnerText;

                    slm.TotalValue = odds[7].InnerText;
                    slm.totalOverBetKey = odds[8].Id;
                    slm.TotalOverOdds = odds[8].InnerText;
                    slm.TotalUnderBetKey = odds[9].Id;
                    slm.TotalUnderOdds = odds[9].InnerText;

                    res.Add(slm);
                }
            }

            return res.ToArray();
        }

        private (int skip, int req) GetHeaderCount(string sportType)
        {
            switch (sportType)
            {
                case "tennis": return (2, 9);

                default: return (100, 100);
            }
        }

        private List<string> CreateTeams(HtmlNode infoBlock)
        {
            if (infoBlock.FirstChild.Name == "#text")
            {
                return infoBlock.ChildNodes[0].InnerText.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(q => q.Trim()).ToList();
            }

            if (infoBlock.ChildNodes.Any(x => x.Name == "small"))
            {
                var l = new List<string>();
                if (infoBlock.ChildNodes[0].Name == "#text")
                    l.Add(infoBlock.ChildNodes[0].InnerText.RemoveDashWithTrim());
                if (infoBlock.ChildNodes[1].Name == "#small")
                    l.Add(infoBlock.ChildNodes[1].InnerText.RemoveDashWithTrim());
                return l;
            }

            return new List<string>();
        }

        public string GetSubLiveUrl(string html) =>
            string.Format(SubLiveUrlTemplate, string.Join(",", Converter.ToLiveBetMatches(html).Select(x => x.EventNo)));
    }
}
