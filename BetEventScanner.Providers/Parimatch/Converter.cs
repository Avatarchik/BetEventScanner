using BetEventScanner.Providers.Domain;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    internal static class Converter
    {
        public static CyberFootballLiveMatch[] ToLiveBetMatches(string html)
        {
            var betEvents = html.GetCssNodes("table");
            var r = new List<CyberFootballLiveMatch>(betEvents.Count);

            foreach (var betEvent in betEvents)
            {
                var evno = betEvent.Attributes["evno"].Value;
                var a = betEvent.InnerHtml.GetCssNode("td.td_n > a");

                var cn = a.ChildNodes.Where(q => !string.IsNullOrWhiteSpace(q.InnerText) && q.InnerText.Trim() != "-" && (q.Name == "small" || q.Name == "#text")).ToList();

                string p1origin = string.Empty;
                string p2origin = string.Empty;

                if (cn.Count > 1)
                {
                    p1origin = cn[0].Name == "small" ? cn[0].InnerText : cn[0].InnerText.Replace("-", "").Trim();
                    p2origin = cn[1].Name == "#text" ? cn[1].InnerText.Replace("-", "").Trim() : cn[1].InnerText;
                }
                else
                {
                    var n = cn.First().InnerText.Split('-').ToArray();
                    p1origin = n[0];
                    p2origin = n[1];
                }

                var startIndex = p1origin.IndexOf("(");
                var endIndex = p1origin.IndexOf(")");
                var p1n = p1origin.Substring(++startIndex, endIndex - startIndex);
                var p1t = p1origin.Substring(0, --startIndex).Trim();

                var startIndex2 = p2origin.IndexOf("(");
                var endIndex2 = p2origin.IndexOf(")");
                var p2n = p2origin.Substring(++startIndex2, endIndex2 - startIndex2);
                var p2t = p2origin.Substring(0, --startIndex2).Trim();

                r.Add(new CyberFootballLiveMatch
                {
                    EventNo = evno,
                    Player1 = new Player
                    {
                        Name = p1n,
                        Team = p1t
                    },
                    Player2 = new Player
                    {
                        Name = p2n,
                        Team = p2t
                    },
                    OriginHtml = betEvent.InnerHtml
                });
            }

            return r.ToArray();
        }

        public static StatsTable ConvertToStatisticsTable(string html)
        {
            var rows = html.GetCssNodes("div.visible-md-up > table.table.table-condensed > tbody > tr");

            var positions = new List<StatsTableItem>();

            foreach (var row in rows)
            {
                var tds = row.InnerHtml.GetCssNodes("td");

                var pos = int.Parse(tds[1].InnerText);
                var name = tds[3].InnerHtml.GetCssNode("div.hidden-xs-up.visible-sm-up.wrap").InnerText;
                var played = tds[4].InnerText;
                var win = int.Parse(tds[5].InnerText);
                var draw = int.Parse(tds[6].InnerText);
                var lost = int.Parse(tds[7].InnerText);
                var gf = int.Parse(tds[8].InnerText);
                var ga = int.Parse(tds[9].InnerText);
                var diff = int.Parse(tds[10].InnerText);
                var points = int.Parse(tds[11].InnerHtml);
                var form = ConvertToForm(tds[12].InnerHtml);

                positions.Add(new StatsTableItem
                {
                    Form = form
                });
            }

            return new StatsTable
            {
                Positions = positions.ToArray()
            };
        }

        private static Form ConvertToForm(string html) => new Form();
    }
}
