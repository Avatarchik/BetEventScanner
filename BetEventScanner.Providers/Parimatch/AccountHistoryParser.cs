using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public class AccountHistoryParser
    {
        public void ParseFromFile(string filePath)
        {
            ParseFromHtml(File.ReadAllText(filePath));
        }

        public void ParseFromHtml(string html)
        {
            var rows = html.GetCssNodes("div.gaming-history__row");

            Console.WriteLine($"TotalBets:{rows.Count}");

            var loses = 0;

            foreach (var item in rows)
            {
                var betEvent = item.QuerySelector("bet-event-info > .gaming-history__row");

                if (betEvent == null)
                    continue;

                var resRow = betEvent.QuerySelector(".gaming-history__coef");
                var resRowAttrsStr = resRow.Attributes.ToList().First().Value;
                var res = resRowAttrsStr.Contains("lose");
                if (res)
                {
                    loses++;
                }
            }

            Console.WriteLine($"Loses {loses}");
        }


    }
}
