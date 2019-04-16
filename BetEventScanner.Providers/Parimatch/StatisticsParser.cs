using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;

namespace BetEventScanner.Providers.Parimatch
{
    public class StatisticsParser
    {
        public static string Url => "https://s5.sir.sportradar.com/parimatch";
        public string FootballItalySeriaA => Url + "1/category/31";

        public StatsTable GetTableStats(string html) => 
            ConvertToStatisticsTable(html);

        private static StatsTable ConvertToStatisticsTable(string html)
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
