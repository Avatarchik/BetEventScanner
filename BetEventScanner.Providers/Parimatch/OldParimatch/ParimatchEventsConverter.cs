﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BetEventScanner.Providers.Parimatch.Model;
using HtmlAgilityPack;

namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchEventsConverter
    {
        public class ConvertMeta
        {
            public string Header { get; set; }
            public EventType EventType { get; set; }
            public MatchDateResolver DateResolver { get; set; }
            public IList<string> Headers { get; set; }
            public HtmlNode HtmlNode { get; set; }
            public string Result { get; set; }
        }

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
                        //pmEvent = (T)(object)ConvertToTennisOddsBetEvent(dateResolver, headers, m, res);
                    }

                    //if (type == "football")
                    //{
                    //    pmEvent = (T)ConvertToFootballOddsBetEvent(dateResolver, headers, m, res);
                    //}

                    if (pmEvent == null)
                    {
                        continue;
                    }

                    pmEvents.Add(pmEvent);
                }
            }

            return pmEvents;
        }

        //public static ParimatchFootballBetEvent ConvertToFootballEvent(MatchDateResolver dateResolver, List<string> headers, HtmlNode htmlNode, string result)
        //{
        //    var res = new ParimatchFootballBetEvent();
        //    var row = htmlNode.QuerySelectorAll("td").ToList();
        //    for (var i = 0; i < row.Count; i++)
        //    {
        //        switch (headers[i])
        //        {
        //            case "#":

        //                try
        //                {
        //                    res.ParimatchId = row[i].InnerText;
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    Debugger.Break();
        //                }

        //                break;

        //            case "Date":
        //                try
        //                {
        //                    var t = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
        //                    var dateTime = dateResolver.GetDate(t[0], t[1]);
        //                    res.DateTime = dateTime;
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    Debugger.Break();
        //                }

        //                break;

        //            case "Event":
        //                try
        //                {
        //                    var teams = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
        //                    res.HomeTeam = teams[0];
        //                    res.AwayTeam = teams[1];
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    Debugger.Break();
        //                }

        //                break;

        //            case "Hand.":
        //                try
        //                {
        //                    var handicaps = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
        //                    res.HomeHandicap = handicaps[0];
        //                    res.AwayHandicap = handicaps[1];

        //                    var handicapOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
        //                    res.HomeHandicapOdds = handicapOdds[0];
        //                    res.AwayHandicapOdds = handicapOdds[1];
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    Debugger.Break();
        //                }
        //                break;

        //            case "Total":
        //                try
        //                {
        //                    res.Total = row[i].InnerText;
        //                    res.TotalOver = row[++i].QuerySelector("s").InnerText;
        //                    res.TotalUnder = row[++i].QuerySelector("s").InnerText;
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    Debugger.Break();
        //                }
        //                break;

        //            case "1":
        //                try
        //                {
        //                    res.HomeWin = row[i].QuerySelector("s")?.InnerText;
        //                    if (res.HomeWin == null) return null;
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    Debugger.Break();
        //                }
        //                break;

        //            case "X":
        //                try
        //                {
        //                    res.Draw = row[i].QuerySelector("s")?.InnerText;
        //                    if (res.Draw == null) return null;
        //                }
        //                catch (Exception)
        //                {

        //                }
        //                break;

        //            case "2":
        //                res.AwayWin = row[i].QuerySelector("s")?.InnerText;
        //                if (res.AwayWin == null) return null;
        //                break;

        //            case "1X":
        //                res.HomeWinOrDraw = row[i].QuerySelector("s")?.InnerText;
        //                if (res.HomeWinOrDraw == null) return null;
        //                break;

        //            case "12":
        //                res.NoDraw = row[i].QuerySelector("s")?.InnerText;
        //                if (res.NoDraw == null) return null;
        //                break;

        //            case "X2":
        //                res.AwayWinOrDraw = row[i].QuerySelector("s")?.InnerText;
        //                if (res.AwayWinOrDraw == null) return null;
        //                break;

        //            case "iTotal":
        //                var indTotals = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
        //                res.IndTotalHome = indTotals[0];
        //                res.IndTotalAway = indTotals[1];

        //                var indTotalOverOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
        //                res.IndTotalHomeOver = indTotalOverOdds[0];
        //                res.IndTotalAwayOver = indTotalOverOdds[1];

        //                var indTotalUnderOdds = row[++i].QuerySelectorAll("s").Select(x => x.InnerText).ToList();
        //                res.IndTotalHomeUnder = indTotalUnderOdds[0];
        //                res.IndTotalAwayUnder = indTotalUnderOdds[1];
        //                break;
        //        }
        //    }

        //    var tempRes = result.Substring(0, result.Length - 1);
        //    var parsedResult = tempRes.Split('(');

        //    if (parsedResult.Length > 1)
        //    {
        //        res.FirstHalfScore = parsedResult[1];
        //        res.FinalScore = parsedResult[0];
        //        res.ResultStatus = "ok";
        //    }
        //    else
        //    {
        //        res.ResultStatus = parsedResult[0];
        //    }

        //    res.MatchId = res.DateTime.ToString("yyyyMMdd") + res.HomeTeam.Substring(0, 3) + res.AwayTeam.Substring(0, 3);

        //    return res;
        //}

        public static ICollection<IParimatchEvent> ConvertToOddsEvent(HtmlNode node)
        {
            var header = node.SelectSingleNode("h3")?.InnerText;
            if (header == null) return null;

            var eventType = SupportedEvents.GetSupportedEventType(header);
            if (eventType == EventType.None) return null;
            Console.WriteLine(header);
            var oddsBetEvents = new List<IParimatchEvent>();

            switch (eventType)
            {
                case EventType.Tennis:

                    oddsBetEvents.AddRange(ConvertToTennisOddsBetEvent(header, node));
                        
                    //oddsBetEvents.Add(ConvertToTennisOddsBetEvent(header, node));
                    break;

                //case EventType.Football:
                //    ConvertToFootballOddsBetEvent(header, node);
                //    break;

                default:
                    break;
            }

            return oddsBetEvents;
        }

        //public static ICollection<IParimatchEvent> ConvertToFootballOddsBetEvent(string header, HtmlNode node)
        //{
        //    var betEvents = new List<IParimatchEvent>();

        //    var lineBetEvents = node.QuerySelectorAll("tbody[class^='row'] .processed")?.ToList();

        //    //var tennisEvent = ConvertToTennisEvent(new MatchDateResolver(), headers, node, "");

        //    //return new 
        //    //{
        //    //    Header = header,
        //    //    EventType = EventType.Tennis
        //    //};

        //    return new List<IParimatchEvent>();
        //}

        public static ICollection<IParimatchEvent> ConvertToTennisOddsBetEvent(string header, HtmlNode node)
        {
            var betEvents = new List<IParimatchEvent>();

            var headers = node.QuerySelectorAll("tbody[class=processed] > tr > th").Select(x => x.InnerText).ToList();
            var rows = node.QuerySelectorAll("tbody[class^='row'] .processed").ToList();

            foreach (var item in rows)
            {
                var meta = new ConvertMeta
                {
                    Header = header,
                    EventType = EventType.Tennis,
                    DateResolver = new MatchDateResolver(),
                    Headers = headers,
                    HtmlNode = item,
                };

                var tennisEvent = ConvertToTennisBetEvent(meta);
                betEvents.Add(tennisEvent);
            }

            return betEvents;
        }

        private static IParimatchEvent ConvertToTennisBetEvent(ConvertMeta meta)
        {
            var res = new ParimatchTennisBetEvent
            {
                Header = meta.Header,
                EventType = meta.EventType,
                Tournament = meta.Header
            };
            var row = meta.HtmlNode.QuerySelectorAll("td").ToList();
            for (var i = 0; i < meta.Headers.Count; i++)
            {
                switch (meta.Headers[i])
                {
                    case "#":
                        try
                        {
                            res.ParimatchId = row[i].InnerText;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "Date":
                        try
                        {
                            var t = row[i].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None);
                            var dateTime = meta.DateResolver.GetDate(t[0], t[1]);
                            res.DateTime = dateTime;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "Event":
                        try
                        {
                            string playersHtml = string.Empty;

                            if (row[i].InnerHtml.Contains("</a>"))
                            {
                                playersHtml = row[i].QuerySelector("a").InnerHtml;
                            }
                            else
                            {
                                playersHtml = row[i].InnerHtml;
                            }

                            var players = playersHtml.Split(new[] { "<br>" }, StringSplitOptions.None).ToList();

                            res.Player1 = players[0];
                            res.Player2 = players[1];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "Hand.":
                        try
                        {
                            var handicaps = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                            if (handicaps.Count == 0) return null;

                            res.Player1Handicap = handicaps[0];
                            res.Player2Handicap = handicaps[1];

                            var handicapOdds = row[++i].QuerySelectorAll("a").Select(x => x.InnerText).ToList();
                            res.Player1HandicapOdds = handicapOdds[0];
                            res.Player2HandicapOdds = handicapOdds[1];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "Total":
                        try
                        {
                            res.Total = row[i].InnerText;
                            res.TotalOver = row[++i].QuerySelector("a").InnerText;
                            res.TotalUnder = row[++i].QuerySelector("a").InnerText;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "1":
                        try
                        {
                            res.Player1Win = row[i].QuerySelector("a")?.InnerText;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "2":
                        try
                        {
                            res.Player2Win = row[i].QuerySelector("a")?.InnerText;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "2:0":
                        try
                        {
                            res.TwoZero = row[i].QuerySelector("a")?.InnerText;
                            if (res.TwoZero == null) return null;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "2:1":
                        try
                        {
                            res.TwoOne = row[i].QuerySelector("a")?.InnerText;
                            if (res.TwoOne == null) return null;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "1:2":
                        try
                        {
                            res.OneTwo = row[i].QuerySelector("a")?.InnerText;
                            if (res.OneTwo == null) return null;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "0:2":
                        try
                        {
                            res.ZeroTwo = row[i].QuerySelector("a")?.InnerText;
                            if (res.ZeroTwo == null) return null;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Debugger.Break();
                        }
                        break;

                    case "iTotal":
                        try
                        {
                            var indTotals = row[i].QuerySelectorAll("b").Select(x => x.InnerText).ToList();
                            if (indTotals.Count == 2)
                            {
                                res.Player1ITotal = indTotals[0];
                                res.Player2ITotal = indTotals[1];
                            }

                            var indTotalOverOdds = row[++i].QuerySelectorAll("a").Select(x => x.InnerText).ToList();
                            if (indTotalOverOdds.Count == 2)
                            {
                                res.Player1ITotalOverOdds = indTotalOverOdds[0];
                                res.Player2ITotalOverOdds = indTotalOverOdds[1];
                            }

                            var indTotalUnderOdds = row[++i].QuerySelectorAll("a").Select(x => x.InnerText).ToList();
                            if (indTotalUnderOdds.Count == 2)
                            {
                                res.Player1ITotalUnderOdds = indTotalUnderOdds[0];
                                res.Player2ITotalUnderOdds = indTotalUnderOdds[1];
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                }
            }
            // ToDo commented while developing odds parsing, incoming odds not have result
            //try
            //{
            //    var result = meta.Result;
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        if (!result.Contains("cancelled"))
            //        {
            //            var tempRes = result.Substring(0, result.Length - 1);
            //            var splitResult = tempRes.Split('(');
            //            var finalResult = splitResult[0];
            //            res.FinalScore = finalResult;
            //            var setsResult = splitResult[1].Split(',').ToList();
            //            res.SetsResult = setsResult;
            //            res.Status = "ok";
            //        }
            //        else
            //        {
            //            res.Status = result;
            //        }

            //        res.MatchId = res.DateTime.ToString("yyyyMMdd") + res.Player1.Substring(0, 3) + res.Player2.Substring(0, 3);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    //Debugger.Break();
            //    //throw;
            //}

            res.Status = "Scheduled";

            var part1 = new String(res.Player1.Take(3).ToArray());
            var part2 = new String(res.Player2.Take(3).ToArray());
            var part3 = res.DateTime.ToString("ddMMyyyy");

            res.MatchId = $"{part1}{part2}{part3}";

            return res;
        }
    }
}