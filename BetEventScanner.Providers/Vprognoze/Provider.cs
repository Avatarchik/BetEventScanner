using BetEventScanner.Providers.Vprognoze.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BetEventScanner.Providers.Vprognoze
{
    class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }

    public class VprProvider
    {
        public Bettor[] GetCurrentTopUsers(string html)
        {
            try
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var usersTable = htmlDoc.GetIdNode("ratingTable");
                return usersTable.QuerySelectorAll("tbody > tr").Select(Convert).ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public Bettor[] GetCurrentTopUsers()
        {
            var html = Get("https://vprognoze.ru/statalluser/");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var usersTable = htmlDoc.GetIdNode("ratingTable");
            return usersTable.QuerySelectorAll("tbody > tr").Select(Convert).ToArray();
        }

        private Bettor Convert(HtmlNode node)
        {
            var bettor = new Bettor();
            var nodes = node.ChildNodes.Where(x => x.Name != "#text").ToList();

            var rankStr = nodes[0].InnerText;
            bettor.Rank = int.Parse(rankStr);

            bettor.Name = nodes[1].InnerText.SkipLastN(3);

            var profitStr = nodes[2].InnerText;
            var profitValue = profitStr.SkipFirstAndLast();
            bettor.Profit = double.Parse(profitValue);

            var totalBetsStr = nodes[3].InnerText;
            bettor.TotalBets = int.Parse(totalBetsStr);

            var stats = nodes[4].InnerText.Split('/').ToList();
            var winStr = stats[0];
            var lostStr = stats[1];
            var drawStr = stats[2];
            bettor.Win = int.Parse(winStr);
            bettor.Lost = int.Parse(lostStr);
            bettor.Draw = int.Parse(drawStr);

            var avgKoefStr = nodes[5].InnerText;
            bettor.AvgKoef = double.Parse(avgKoefStr);

            var avgTotalBetStr = nodes[6].InnerText;
            bettor.AvgTotalBet = int.Parse(avgTotalBetStr);

            var winPercentageStr = nodes[7].InnerText;
            var sbstr = winPercentageStr.Substring(0, winPercentageStr.Length - 1);
            bettor.WinPercentage = int.Parse(sbstr);

            var roiStr = nodes[8].InnerText;
            bettor.Roi = double.Parse(roiStr.SkipFirstAndLast());

            bettor.Created = DateTime.UtcNow;
            bettor.Updated = DateTime.UtcNow;

            return bettor;
        }

        private string Get(string url)
        {
            string response = null;

            using (var wc = new MyWebClient())
            {
                wc.Headers.Add("Referer", "https://vprognoze.ru/");
                wc.Headers.Add("Conten-type", "text/html");
                wc.Headers.Add("Charset", "windows-1251");
                wc.Headers.Add("Accept-Encoding", "gzip, deflate, br");

                response = wc.DownloadString(url);
            }

            return response;
        }
    }
}
