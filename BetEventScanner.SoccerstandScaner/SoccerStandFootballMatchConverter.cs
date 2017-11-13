using BetEventScanner.SoccerstandScaner.Contracts;
using HtmlAgilityPack;
using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner
{
    public class SoccerStandFootballMatchConverter : IFootballMatchConverter
    {
        public FootballMatch CreateMatchFromHtml(string providerMatchId, string matchHtml, string matchDetailsHtml)
        {
            var match = new FootballMatch
            {
                ProviderId = providerMatchId
            };

            AttachGeneralData(match, matchHtml);
            AttachDetails(match, matchDetailsHtml);

            return match;
        }

        private void AttachGeneralData(FootballMatch match, string matchHtml)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(matchHtml);

            foreach (var htmlNode in htmlDocument.DocumentNode.ChildNodes)
            {
                var nodeClass = htmlNode.GetAttributeValue("class", "");

                if (nodeClass.Contains("time"))
                {
                    match.DateTime = htmlNode.InnerText;
                }

                if (nodeClass.Contains("team-home"))
                {
                    match.HomeTeamName = htmlNode.InnerText;
                }

                if (nodeClass.Contains("team-away"))
                {
                    match.AwayTeamName = htmlNode.InnerText;
                }

                if (nodeClass.Contains("score"))
                {
                    match.Score = htmlNode.InnerText.Replace("&nbsp;", "");
                }
            }
        }

        private void AttachDetails(FootballMatch match, string matchDetailsHtml)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(matchDetailsHtml);
        }
    }
}