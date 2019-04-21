using BetEventScanner.DataModel;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    internal static class Converter
    {
        public static Dictionary<SportType, string[]> _nonSupportedOddsBetEvents = new Dictionary<SportType, string[]>
        {
            {SportType.Tennis, new string[0]},
            {SportType.Basketball, new[]{ "futures", "3 point fg", "1-st quarter", "reb", "points" } },
            {SportType.Football, new string[0]}
        };

        public static CyberFootballMatch[] ToLiveBetMatches(string html)
        {
            var betEvents = html.GetCssNodes("table");
            var r = new List<CyberFootballMatch>(betEvents.Count);

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

                r.Add(new CyberFootballMatch
                {
                    EventNo = evno,
                    Player1 = new CyberFootballPlayer
                    {
                        Name = p1n,
                        Team = p1t
                    },
                    Player2 = new CyberFootballPlayer
                    {
                        Name = p2n,
                        Team = p2t
                    },
                    OriginHtml = betEvent.InnerHtml
                });
            }

            return r.ToArray();
        }

        //internal static HistoricalMatchResult[] ToHistoricalResultMatches(string innerHtml)
        //{
        //    var name = innerHtml.GetCssNode("h3").InnerText;

        //    var names = name.ToLower().Split('.').Select(x => x.Trim()).ToArray();

        //    var sportType = names[0].ToSportType();
        //    if (!sportType.HasValue)
        //        return null;

        //    if (_nonSupportedOddsBetEvents[sportType.Value].Length == 0 || _nonSupportedOddsBetEvents[sportType.Value].Contains(names.Last()))
        //        return null;

        //    var table = innerHtml.GetCssNode("table.dt");
        //    var rows = table.InnerHtml.GetCssNodes("tbody.row1.processed, tbody.row2.processed").ToList();

        //    return rows.Select(x =>
        //    {
        //        var match = ToHistoricalResultMatch(x.InnerHtml);
        //        if (match == null)
        //            return null;

        //        match.SportType = sportType.Value;
        //        match.SportTypeStr = name;
        //        return match;
        //    }).ToArray();
        //}

        //internal static HistoricalMatchResult ToHistoricalResultMatch(string innerHtml)
        //{
        //    var match = new HistoricalMatchResult();

        //    try
        //    {
        //        var rows = innerHtml.GetCssNodes("tr").ToArray();

        //        var r0 = rows[0].InnerHtml.GetCssNodes("td");
        //        var r1 = rows[1].InnerHtml.GetCssNode("i.p2r").InnerText;

        //        match.EventNo = r0[0].InnerText;
        //        var dt = r0[1].InnerHtml.Replace("-evd", "#").Replace("evd-", "$").ExtractBetween('#', '$').Trim();
        //        var dateTime = DateTime.ParseExact(dt, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture);
        //        match.DateTime = dateTime;

        //        var teams = r0[2].InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.None).ToArray();
        //        if (teams[0].Contains("small"))
        //        {
        //            match.Team1 = teams[0].GetCssNode("small").InnerText;
        //        }
        //        else
        //        {
        //            match.Team1 = teams[0];
        //        }

        //        if (teams[1].Contains("small"))
        //        {
        //            match.Team2 = teams[1].GetCssNode("small").InnerText;
        //        }
        //        else
        //        {
        //            match.Team2 = teams[1];
        //        }

        //        var handicapValues = r0[3].InnerHtml.GetCssNodes("b");

        //        match.Fora1Value = handicapValues[0].InnerText;
        //        match.Fora2Value = handicapValues[1].InnerText;

        //        var handicapOdds = r0[4].InnerHtml.GetCssNodes("s");

        //        match.Fora1Odds = handicapOdds[0].InnerText;
        //        match.Fora2Odds = handicapOdds[1].InnerText;


        //        match.TotalValue = r0[5].InnerText;
        //        match.TotalOverOdds = r0[6].InnerHtml.GetCssNode("s").InnerText;
        //        match.TotalUnderOdds = r0[7].InnerHtml.GetCssNode("s").InnerText;

        //        match.Win1Odds = r0[8].InnerHtml.GetCssNode("s").InnerText;
        //        match.DrawOdds = r0[9].InnerText;
        //        match.Win2Odds = r0[10].InnerHtml.GetCssNode("s").InnerText;

        //        var indTotals = r0[11].InnerHtml.GetCssNodes("b");

        //        match.Ind1TotalValue = indTotals[0].InnerText;
        //        match.Ind2TotalValue = indTotals[1].InnerText;

        //        var indTotalUppers = r0[12].InnerHtml.GetCssNodes("s");

        //        match.Ind1TotalUpper = indTotalUppers[0].InnerText;
        //        match.Ind2TotalUpper = indTotalUppers[1].InnerText;

        //        var indTotalUnders = r0[13].InnerHtml.GetCssNodes("s");

        //        match.Ind1TotalUnder = indTotalUnders[0].InnerText;
        //        match.Ind2TotalUnder = indTotalUnders[1].InnerText;

        //        match.Result = r1;

        //        return match;
        //    }
        //    catch (Exception e)
        //    {
        //        match.Error = true;
        //    }

        //    return match;
        //}

             
    }
}
