using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Providers.Parimatch.Model;
using HtmlAgilityPack;

namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchEventsConverter
    {
        public static ICollection<T> Convert<T>(MatchDateResolver dateResolver, ICollection<string> htmlNodes, string type) where T : class
        {
            var pmEvents = new List<T>();

            foreach (var htmlNode in htmlNodes)
            {
                var node = new HtmlDocument();
                node.LoadHtml(htmlNode);
                var table = node.QuerySelector("table[class=dt]");
                var headers = table.QuerySelectorAll("tbody[class=processed] > tr > th").Select(x => x.InnerText).ToList();
                var rows = table.QuerySelectorAll("tbody[class^=row]").ToList();

                var source = rows[0].QuerySelectorAll("tr").Where(x => !string.IsNullOrEmpty(x.InnerText)).ToList();
                for (int i = 0; i < source.Count; i++)
                {
                    var m = source[i];
                    var resultIndex = ++i;
                    if (resultIndex > source.Count - 1)
                    {
                        continue;
                    }
                    var res = source[resultIndex].QuerySelector("td > .p2r")?.InnerText;
                    if (res == null)
                    {
                        i++;
                        continue;
                    }

                    T pmEvent = null;

                    Console.WriteLine(m.InnerText);

                    if (type == "tennis")
                    {
                        pmEvent = (T)(object)ConvertToTennisEvent(dateResolver, headers, m, res);
                    }

                    if (type == "football")
                    {
                        pmEvent = (T)(object)ConvertToTennisEvent(dateResolver, headers, m, res);
                    }

                    if (pmEvent == null)
                    {
                        continue;
                    }

                    pmEvents.Add(pmEvent);
                }
            }

            return pmEvents;
        }

        public static ParimatchFootballBetEvent ConvertToFootballEvent(MatchDateResolver dateResolver, List<string> headers, HtmlNode htmlNode, string result)
        {
            var res = new ParimatchFootballBetEvent();
            var row = htmlNode.QuerySelectorAll("td").ToList();
            for (var i = 0; i < row.Count; i++)
            {
                switch (headers[i])
                {
                    case "#":
                        res.ParimatchId = row[i].InnerText;
                        break;

                    case "Date":
                        var t = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                        var dateTime = dateResolver.GetDate(t[0], t[1]);
                        //var datetime = $"{info.Year}-{dm[1]}-{dm[0]} {t[1]}";
                        //res.DateTime = DateTime.Parse(datetime);
                        res.DateTime = dateTime;
                        break;

                    case "Event":
                        var teams = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                        res.HomeTeam = teams[0];
                        res.AwayTeam = teams[1];
                        break;

                    case "Hand.":
                        var handicaps = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                        res.HomeHandicap = handicaps[0];
                        res.AwayHandicap = handicaps[1];

                        var handicapOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.HomeHandicapOdds = handicapOdds[0];
                        res.AwayHandicapOdds = handicapOdds[1];
                        break;

                    case "Total":
                        res.Total = row[i].InnerText;
                        res.TotalOver = row[++i].QuerySelector("s").InnerText;
                        res.TotalUnder = row[++i].QuerySelector("s").InnerText;
                        break;

                    case "1":
                        res.HomeWin = row[i].QuerySelector("s")?.InnerText;
                        if (res.HomeWin == null) return null;
                        break;

                    case "X":
                        res.Draw = row[i].QuerySelector("s")?.InnerText;
                        if (res.Draw == null) return null;
                        break;

                    case "2":
                        res.AwayWin = row[i].QuerySelector("s")?.InnerText;
                        if (res.AwayWin == null) return null;
                        break;

                    case "1X":
                        res.HomeWinOrDraw = row[i].QuerySelector("s")?.InnerText;
                        if (res.HomeWinOrDraw == null) return null;
                        break;

                    case "12":
                        res.NoDraw = row[i].QuerySelector("s")?.InnerText;
                        if (res.NoDraw == null) return null;
                        break;

                    case "X2":
                        res.AwayWinOrDraw = row[i].QuerySelector("s")?.InnerText;
                        if (res.AwayWinOrDraw == null) return null;
                        break;

                    case "iTotal":
                        var indTotals = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                        res.IndTotalHome = indTotals[0];
                        res.IndTotalAway = indTotals[1];

                        var indTotalOverOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.IndTotalHomeOver = indTotalOverOdds[0];
                        res.IndTotalAwayOver = indTotalOverOdds[1];

                        var indTotalUnderOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.IndTotalHomeUnder = indTotalUnderOdds[0];
                        res.IndTotalAwayUnder = indTotalUnderOdds[1];
                        break;
                }
            }

            var tempRes = result.Substring(0, result.Length - 1);
            var parsedResult = tempRes.Split('(');

            if (parsedResult.Length > 1)
            {
                res.FirstHalfScore = parsedResult[1];
                res.FinalScore = parsedResult[0];
                res.ResultStatus = "ok";
            }
            else
            {
                res.ResultStatus = parsedResult[0];
            }

            res.MatchId = res.DateTime.ToString("yyyyMMdd") + res.HomeTeam.Substring(0, 3) + res.AwayTeam.Substring(0, 3);

            return res;
        }

        public static ParimatchTennisBetEvent ConvertToTennisEvent(MatchDateResolver dateResolver, List<string> headers, HtmlNode htmlNode, string result)
        {
            var res = new ParimatchTennisBetEvent();
            var row = htmlNode.QuerySelectorAll("td").ToList();
            for (var i = 0; i < row.Count; i++)
            {
                switch (headers[i])
                {
                    case "#":
                        res.ParimatchId = row[i].InnerText;
                        break;

                    case "Date":
                        var t = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                        var dateTime = dateResolver.GetDate(t[0], t[1]);
                        res.DateTime = dateTime;
                        break;

                    case "Event":
                        var players = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                        res.Player1 = players[0];
                        res.Player2 = players[1];
                        break;

                    case "Hand.":
                        var handicaps = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                        if (handicaps.Count == 0) return null;

                        res.Player1Handicap = handicaps[0];
                        res.Player2Handicap = handicaps[1];

                        var handicapOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        res.Player1HandicapOdds = handicapOdds[0];
                        res.Player2HandicapOdds = handicapOdds[1];
                        break;

                    case "Total":
                        res.Total = row[i].InnerText;
                        res.TotalOver = row[++i].QuerySelector("s").InnerText;
                        res.TotalUnder = row[++i].QuerySelector("s").InnerText;
                        break;

                    case "1":
                        res.Player1Win = row[i].QuerySelector("s")?.InnerText;
                        break;

                    case "2":
                        res.Player2Win = row[i].QuerySelector("s")?.InnerText;
                        break;

                    case "2:0":
                        res.TwoZero = row[i].QuerySelector("s")?.InnerText;
                        if (res.TwoZero == null) return null;
                        break;

                    case "2:1":
                        res.TwoOne = row[i].QuerySelector("s")?.InnerText;
                        if (res.TwoOne == null) return null;
                        break;

                    case "1:2":
                        res.OneTwo = row[i].QuerySelector("s")?.InnerText;
                        if (res.OneTwo == null) return null;
                        break;

                    case "0:2":
                        res.ZeroTwo = row[i].QuerySelector("s")?.InnerText;
                        if (res.ZeroTwo == null) return null;
                        break;

                    case "iTotal":
                        var indTotals = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                        if (indTotals.Count == 0) return null;

                        res.Player1ITotal = indTotals[0];
                        res.Player2ITotal = indTotals[1];

                        var indTotalOverOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        if (indTotalOverOdds.Count == 2)
                        {
                            res.Player1ITotalOverOdds = indTotalOverOdds[0];
                            res.Player2ITotalOverOdds = indTotalOverOdds[1];
                        }

                        var indTotalUnderOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
                        if (indTotalUnderOdds.Count == 2)
                        {
                            res.Player1ITotalUnderOdds = indTotalUnderOdds[0];
                            res.Player2ITotalUnderOdds = indTotalUnderOdds[1];
                        }

                        break;
                }
            }

            if (!result.Contains("cancelled"))
            {
                var tempRes = result.Substring(0, result.Length - 1);
                var splitResult = tempRes.Split('(');
                var finalResult = splitResult[0];
                res.FinalScore = finalResult;
                var setsResult = splitResult[1].Split(',').ToList();
                res.SetsResult = setsResult;
                res.Status = "ok";
            }
            else
            {
                res.Status = "cancelled";
            }

            //if (parsedResult.Length > 1)
            //{
            //    res.FirstHalfScore = parsedResult[1];
            //    res.FinalScore = parsedResult[0];
            //    res.ResultStatus = "ok";
            //}
            //else
            //{
            //    res.ResultStatus = parsedResult[0];
            //}

            res.MatchId = res.DateTime.ToString("yyyyMMdd") + res.Player1.Substring(0, 3) + res.Player2.Substring(0, 3);
            return res;
        }
    }
}
