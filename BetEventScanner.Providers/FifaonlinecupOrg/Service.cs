using BetEventScanner.Providers.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BetEventScanner.Providers.FifaonlinecupOrg
{
    public class Service : IHeadToHeadProvider
    {
        private readonly Dictionary<string, int> playerMapping = new Dictionary<string, int>
        {
            { "olle", 87 },
            { "olego", 142 },
            { "skeptik", 82 },
            { "goodslayer", 151 },
            { "spenish", 132 },
            { "furman", 131 },
            { "a4s", 53 },
            { "kray", 157 },
            { "quavo", 96 },
            { "kiker", 130 },
            { "whitehorse", 156},
            { "hrusch",113}
        };

        public HeadToHead GetHeadToHead(string t1, string t2)
        {
            var p1n = t1.ToLower();
            var p2n = t2.ToLower();

            var h2h = new HeadToHead
            {
                Player1 = t1,
                Player2 = t2
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

                    var p1nm = item.InnerHtml.GetCssNode("td.result-player-left > a").InnerText.ExtractBetween('(', ')');
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

        public MatchResult[] GetResults(int days = 1)
        {
            var r = new List<MatchResult>();
            for (int i = 0; i < days; i++)
            {
                var html = ApliClient.Get($"http://fifaonlinecup.org/en/results-en/schedule/" + i);
                var matchNodes = html.GetCssNodes("table.table-result > tr");

                DateTime matchDate = DateTime.Now;
                var tournament = string.Empty;

                foreach (var item in matchNodes)
                {
                    if (item.InnerHtml.GetCssNodes(".result-date").Any())
                    {
                        var date = item.InnerHtml.GetCssNode("span").InnerText.Trim();
                        matchDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                        tournament = item.InnerHtml.GetCssNode("a").InnerText;
                        continue;
                    }

                    r.Add(ConvertToMatchResult(matchDate, tournament, item.InnerHtml));
                }
            }

            return r.ToArray();
        }

        private static bool TryGetDateTime(string input, out DateTime dt)
        {
            dt = DateTime.Now;

            if (input.Replace("\n", "").Trim().GetCssNode("td").Attributes["class"].Value == "result-date")
            {
                var date = input.GetCssNode("span").InnerText.Trim();
                dt = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                return true;
            }

            return false;
        }

        private static bool TryGetTournament(string input, out string tournament)
        {
            tournament = string.Empty;

            if (input.Replace("\n", "").Trim().GetCssNode("td").Attributes["class"].Value == "result-tournament")
            {
                tournament = input.Replace("\r", "").Trim();
                return true;
            }

            return false;
        }

        private static MatchResult ConvertToMatchResult(DateTime dt, string tournament, string innerHtml)
        {
            var mr = new MatchResult
            {
                Date = dt,
                Tournament = tournament
            };

            var p1nm = innerHtml.GetCssNode("td.result-player-left > a").InnerText.ExtractBetween('(', ')');
            var p1tm = innerHtml.GetCssNode("td.result-player-left").InnerText.Trim().ExtractBefore('(').Trim();
            mr.Player1 = new Player
            {
                Name = p1nm,
                Team = p1tm
            };

            if (innerHtml.GetCssNode("td.result-account > a").InnerText.ToLower() != "cancelled")
            {
                mr.HT = innerHtml.GetCssNode("td.result-account > a > span").InnerText.ExtractBetween('(', ')');
                mr.FT = innerHtml.GetCssNode("td.result-account > a").InnerText.ExtractBefore('(').Trim();
            }
            else
            {
                mr.Status = "cancelled";
            }

            var p2nm = innerHtml.GetCssNode("td.result-player-right > a").InnerText.ExtractBetween('(', ')');
            var p2tm = innerHtml.GetCssNode("td.result-player-right").InnerText.Trim().ExtractBefore('(').Trim();
            mr.Player2 = new Player
            {
                Name = p2nm,
                Team = p2tm
            };

            return mr;
        }
    }
}
