using BetEventScanner.Providers.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace BetEventScanner.Providers.FifaonlinecupOrg
{
    public class HeadToHead
    {
        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public MatchResult[] Results { get; set; }
    }

    public static class ApliClient
    {
        public static string Get(string url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }

    public class Service
    {
        private readonly Dictionary<string, int> playerMapping = new Dictionary<string, int>
        {
            {"olle", 87 },
            {"olego",142 },
            {"skeptik",82 },
            { "goodslayer", 151 },
            { "spenish", 132 },
        };

        public object GetRatings()
        {
            var res = ApliClient.Get("http://fifaonlinecup.org/en/rating-en");
            return null;
        }

        public HeadToHead GetHeadToHead(string p1, string p2)
        {
            var p1n = p1.ToLower();
            var p2n = p2.ToLower();

            var h2h = new HeadToHead
            {
                Player1 = p1,
                Player2 = p2
            };

            if (playerMapping.ContainsKey(p1n) && playerMapping.ContainsKey(p2n))
            {
                var html = ApliClient.Get($"http://fifaonlinecup.org/en/h2h-en?home={playerMapping[p1n]}&away={playerMapping[p2n]}");
                var matchNodes = html.GetCssNodes("table.table-result > tbody > tr");

                DateTime dt = DateTime.Now;
                var tournament = string.Empty;
                var r = new List<MatchResult>();

                foreach (var item in matchNodes)
                {
                    if (item.InnerHtml.Replace("\n", "").Trim().GetCssNode("td").Attributes["class"].Value == "result-date")
                    {
                        var date = item.InnerText.Replace("\r", "").Trim();
                        dt = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        continue;
                    }

                    if (item.InnerHtml.Replace("\n", "").Trim().GetCssNode("td").Attributes["class"].Value == "result-tournament")
                    {
                        tournament = item.InnerText.Replace("\r", "").Trim();
                        continue;
                    }

                    var p1nm = item.InnerHtml.GetCssNode("td.result-player-left > a").InnerText.ExtractBetween('(',')');
                    var p1tm = item.InnerHtml.GetCssNode("td.result-player-left").InnerText.Trim().ExtractBefore('(').Trim();

                    var htr = item.InnerHtml.GetCssNode("td.result-account > a > span").InnerText.ExtractBetween('(', ')');
                    var ftr = item.InnerHtml.GetCssNode("td.result-account > a").InnerText.ExtractBefore('(').Trim();

                    var p2nm = item.InnerHtml.GetCssNode("td.result-player-right > a").InnerText.ExtractBetween('(', ')');
                    var p2tm = item.InnerHtml.GetCssNode("td.result-player-right").InnerText.Trim().ExtractBefore('(').Trim();


                    r.Add(new MatchResult
                    {
                        Date = dt,
                        Tournament = tournament,
                        Player1 = new Player
                        {
                            Name = p1nm,
                            Team = p1tm
                        },
                        Player2 = new Player
                        {
                            Name = p2nm,
                            Team = p2tm
                        },
                        HT = htr,
                        FT = ftr
                    });
                }

                h2h.Results = r.ToArray();
            }

            return h2h;
        }

        public object GetResults()
        {
            var res = ApliClient.Get("http://fifaonlinecup.org/en/results-en");

            return null;
        }
    }

    public class MatchResult
    {
        public DateTime Date { get; set; }

        public string Tournament { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public string HT { get; set; }

        public string FT { get; set; }
    }
}
