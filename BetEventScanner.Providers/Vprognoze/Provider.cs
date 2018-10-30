using BetEventScanner.Providers.Vprognoze.Model;
using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;

namespace BetEventScanner.Providers.Vprognoze
{
    public class VprProvider
    {
        public Bettor[] GetCurrentTopUsers(string html)
        {
            try
            {
                var usersTable = html.GetIdNode("ratingTable");
                return usersTable.QuerySelectorAll("tbody > tr").Select(ConvertToBettor).ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private Bettor ConvertToBettor(HtmlNode node)
        {
            var bettor = new Bettor();
            var nodes = node.ChildNodes.Where(x => x.Name != "#text").ToList();

            var rankStr = nodes[0].InnerText;
            bettor.Rank = int.Parse(rankStr);

            var nameNode = nodes[1];
            bettor.Name = nameNode.InnerText.SkipLastN(3);

            var linkParamsStr = nameNode.FirstChild.Attributes.FirstOrDefault(x => x.Name == "onclick").Value;
            var temp = linkParamsStr.TakeBetween("'", "'),").Split(new char[] { '\'', ',', '\'' }).Where(q => !string.IsNullOrEmpty(q)).ToArray();

            bettor.Cid = temp[1];
            bettor.Uid = temp[0];

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

        public Bet[] ParseBettorBets(Bettor bettor, string html)
        {
            var t = html.GetIdNode("usertable");
            var tt = t.QuerySelectorAll("tbody > tr[class*=\"bgr\"]").ToList();
            return tt.Select(ConvertToBettorBets).ToArray();
        }

        private Bet ConvertToBettorBets(HtmlNode node)
        {
            var nodes = node.ChildNodes.Where(x => x.Name != "#text").ToList();
            var bet = new Bet();

            var year = DateTime.Now.Year;
            var tmp = nodes[2].FirstChild.InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
            var dt = DateTime.ParseExact($"{tmp[0]}-{year} {tmp[1]}", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            bet.DateTime = dt;

            // TODO Handle express betvariants
            var compTemp = nodes[3].ChildNodes.ToList();
            bet.Competition = compTemp[0].InnerText;
            bet.BetEvent = compTemp[2].InnerText;

            bet.BetVar = nodes[4].InnerHtml;

            bet.Odds = double.Parse(nodes[5].InnerHtml);

            var resTemp = nodes[6];
            bet.EventResult = resTemp.InnerHtml;
            var br = resTemp.Attributes.FirstOrDefault(x => x.Name == "bgcolor").Value;
            bet.BetResult = FromColor(br); 

            return bet;
        }

        private BetResult FromColor(string color)
        {
            switch (color)
            {
                case "#66CC66":
                    return BetResult.Win;

                case "#FF3333":
                    return BetResult.Lost;

                case "#F0FFF0":
                    return BetResult.Draw;

                default:
                    return BetResult.None;

            }
        }

        public string GetBettorBetLink(Bettor bettor)
        {
            return $"https://vprognoze.ru/?do=cmptopall&action=rating&cid={bettor.Cid}&uid={bettor.Uid}";
        }

        public string[] GetBettotBetLinks(Bettor[] bettors)
        {
            return bettors.Select(GetBettorBetLink).ToArray();
        }
    }
}
