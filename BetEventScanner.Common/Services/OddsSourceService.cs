using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Xml;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services
{
    public class OddsSourceService : IOddsSourceService
    {
        private readonly IEnumerable<Division> _supportedLeagues;

        private readonly IDictionary<string, Division> _divionMapping = new Dictionary<string, Division>
        {
            { "3", Division.EnglandApl }
        };

        public OddsSourceService(IEnumerable<Division> supportedLeagues)
        {
            _supportedLeagues = supportedLeagues;
        }

        public List<LeagueOdds> GetOdds()
        {
            var url = @"http://xml.cdn.betclic.com/odds_en.xml";
            var data = DownloadOddsData(url);
            var leagues = TransformXmlIntoEntities(data);
            return leagues;
        }

        private string DownloadOddsData(string url)
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
                return string.Empty;
            }

        }

        private List<LeagueOdds> TransformXmlIntoEntities(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            var football = xmlDocument.DocumentElement.ChildNodes[0];
            
            var leagues = new List<LeagueOdds>();

            foreach (XmlNode league in football.ChildNodes)
            {
                var id = league.Attributes["id"].Value;

                if (!_divionMapping.ContainsKey(id))
                {
                    continue;
                }

                var division = _divionMapping[id];

                if (!_supportedLeagues.Contains(division))
                {
                    continue;
                }

                var leagueName = league.Attributes["name"].Value;
                var leagueModel = new LeagueOdds
                {
                    Division = division,
                    OriginSourceLeagueId = int.Parse(id),
                    Source = OddsSource.BetClickCom,
                    Name = leagueName
                
                };
                leagues.Add(leagueModel);
              
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

                    if (teams.Length < 2)
                    {
                        continue;
                    }

                    var matchModel = new MatchOdds();
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
            }

            return leagues;
        }
    }
}
