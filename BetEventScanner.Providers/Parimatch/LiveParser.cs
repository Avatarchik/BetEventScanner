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

                var st = sportType.ToSportType();
                if (!st.HasValue)
                    continue;

                var subItems = le.InnerHtml.GetCssNodes("div.subitem > table");

                try
                {
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
                            SportTypeStr = sportType,
                            SportType = st.Value,
                            Team1 = teams[0],
                            Team2 = teams[1],
                            Result = result,
                        };

                        var odds = item.InnerHtml.GetCssNodes("td").Skip(2).ToList();

                        if (!string.IsNullOrEmpty(odds[0].Id))
                        {
                            slm.Win1betKey = odds[0].InnerHtml.GetCssNode("u>a").Id;
                            slm.Win1Odds = odds[0].InnerText;
                        }

                        if (!string.IsNullOrEmpty(odds[1].Id))
                        {
                            slm.DrawbetKey = odds[1].InnerHtml.GetCssNode("u>a").Id;
                            slm.DrawOdds = odds[1].InnerText;
                        }

                        if (!string.IsNullOrEmpty(odds[2].Id))
                        {
                            slm.Win2betKey = odds[2].InnerHtml.GetCssNode("u>a").Id;
                            slm.Win2Odds = odds[2].InnerText;
                        }

                        slm.Fora1Value = odds[3].InnerText;
                        slm.Fora1betKey = odds[4].InnerHtml.GetCssNode("u>a").Id;
                        slm.Fora1Odds = odds[4].InnerText;

                        slm.Fora2Value = odds[5].InnerText;
                        slm.Fora2betKey = odds[6].InnerHtml.GetCssNode("u>a").Id;
                        slm.Fora2Odds = odds[6].InnerText;

                        slm.TotalValue = odds[7].InnerText;
                        slm.TotalOverBetKey = odds[8].InnerHtml.GetCssNode("u>a").Id;
                        slm.TotalOverOdds = odds[8].InnerText;
                        slm.TotalUnderBetKey = odds[9].InnerHtml.GetCssNode("u>a").Id;
                        slm.TotalUnderOdds = odds[9].InnerText;

                        res.Add(slm);
                    }
                }
                catch (Exception)
                {
                }
            }

            return res.ToArray();
        }

        public BasketballLiveResult[] GetBasketballLiveResults(SportLiveMatch[] lr) =>
           lr.Where(x => x.SportType == SportType.Basketball).Select(BasketballLiveResult.GetResult).ToArray();

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
    }

    public static class Ex
    {
        public static SportType? ToSportType(this string sportType)
        {
            switch (sportType.ToLower())
            {
                case "football": return SportType.Football;
                case "basketball": return SportType.Basketball;
                case "tennis": return SportType.Tennis;
                default:
                    return null;
            }
        }
    }
}
