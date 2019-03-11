using BetEventScanner.Providers.Domain;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public static class Converter
    {
        public static LiveBetMatch[] ToLiveBetMatches(string html)
        {
            var sourceMatches = html.GetCssNodes("td.td_n > a");

            var r = new List<LiveBetMatch>(sourceMatches.Count);

            sourceMatches.ForEach(x =>
            {
                var nodes = x.InnerText.Split('-');
                var cn = x.ChildNodes.Where(q => !string.IsNullOrWhiteSpace(q.InnerText) && q.InnerText.Trim() != "-" && (q.Name == "small" || q.Name == "#text")).ToList();

                var p1origin = cn[0].Name == "small" ? cn[0].InnerText : cn[0].InnerText.Replace("-", "").Trim();
                var p2origin = cn[1].Name == "#text" ? cn[1].InnerText.Replace("-", "").Trim() : cn[1].InnerText;

                var startIndex = p1origin.IndexOf("(");
                var endIndex = p1origin.IndexOf(")");
                var p1n = p1origin.Substring(++startIndex, endIndex - startIndex);
                var p1t = p1origin.Substring(0, --startIndex).Trim();

                var startIndex2 = p2origin.IndexOf("(");
                var endIndex2 = p2origin.IndexOf(")");
                var p2n = p2origin.Substring(++startIndex2, endIndex2 - startIndex2);
                var p2t = p2origin.Substring(0, --startIndex2).Trim();

                r.Add(new LiveBetMatch
                {
                    Player1 = new Player
                    {
                        Name = p1n,
                        Team = p1t
                    },
                    Player2 = new Player
                    {
                        Name = p2n,
                        Team = p2t
                    }
                });
            });

            return r.ToArray();
        }
    }
}
