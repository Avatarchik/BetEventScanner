using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Xml;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services
{
    public class OddsService
    {
        public List<League1> GetData()
        {
            var url = @"http://xml.cdn.betclic.com/odds_en.xml";
            var inputXml = DownloadOddsData(url);
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(inputXml);
            var leagues = TransformXmlIntoEntities(xmlDocument);
            return leagues;
        }


        private static string DownloadOddsData(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var data = client.DownloadString(url);

                    return data;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }

        }

        private static List<League1> TransformXmlIntoEntities(XmlDocument xml)
        {
            var football = xml.DocumentElement.ChildNodes[0];
            int[] selected = { 1, 2, 3, 4, 5 };

            var currentLeagueNumber = 0;
            var leagues = new List<League1>();

            foreach (XmlNode league in football.ChildNodes)
            {
                if (!selected.Contains(currentLeagueNumber))
                {
                    currentLeagueNumber++;
                    continue;
                }

                var leagueModel = new League1();
                leagues.Add(leagueModel);
                var leagueName = league.Attributes["name"].Value;
                leagueModel.Name = leagueName;

                foreach (XmlNode match in league.ChildNodes)
                {
                    if (match == null)
                    {
                        Console.WriteLine("match is null");    
                    }

                    var matchDate = match.Attributes["start_date"].Value;
                    var matchId = match.Attributes["id"].Value;
                    var matchName = match.Attributes["name"].Value;
                    var teams = matchName.Split('-');

                    var matchModel = new OddsMatch();
                    matchModel.MatchDate = Convert.ToDateTime(matchDate);
                    matchModel.MatchId = matchId;
                    matchModel.MatchName = matchName;
                    matchModel.Team1 = teams[0].Trim();
                    matchModel.Team2 = teams[1].Trim();
                    leagueModel.Maches.Add(matchModel);

                    foreach (XmlNode bet in match.ChildNodes[0])
                    {
                        foreach (XmlNode choise in bet.ChildNodes)
                        {
                            var odd = new Odd();
                            var name = choise.Attributes["name"].Value;
                            if (name.Contains("%1%"))
                            {
                                name = name.Replace("%1%", matchModel.Team1);
                            }
                            else
                            {
                                name = name.Replace("%2%", matchModel.Team2);
                            }
                            var value = choise.Attributes["odd"].Value;
                            odd.Name = name;
                            var v = decimal.Parse(value, CultureInfo.InvariantCulture);
                            odd.Value = v;

                            matchModel.Odds.Add(odd);
                        }
                    }


                }

                currentLeagueNumber++;
            }

            return leagues;
        }
    }
}
