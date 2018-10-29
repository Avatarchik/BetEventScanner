using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.Parimatch.Model;
using HtmlAgilityPack;

namespace BetEventScanner.Providers.Parimatch
{
    public class Handicap
    {
        public string Hand1Value { get; set; }

        public string Hand2Value { get; set; }

        public double Hand1Odds { get; set; }

        public double Hand2Odds { get; set; }
    }

    public class FootballMatch
    {
        public string SportId { get; set; }

        public string CountryId { get; set; }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public Handicap Handicap { get; set; }

        public Total Total { get; set; }

        public double HomeWin { get; set; }

        public double Draw { get; set; }

        public double AwayWin { get; set; }
    }

    public class AirParimatchProvider : IOddsProvider
    {
        public ICollection<IParimatchEvent> GetFutureOddsBetEvents()
        {
            throw new NotImplementedException();
        }

        public ICollection<IParimatchEvent> ParsePreMatchOdds(string sourceHtml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(sourceHtml);

            var prematchSports = htmlDoc.DocumentNode.ChildNodes;

            var football = prematchSports.FirstOrDefault(x => x.Attributes.FirstOrDefault(q => q.Name == "sport-id")?.Value == "1|F");

            var footballGroups = football.QuerySelector("div.live-group-item");
            var footballCountries = footballGroups.QuerySelectorAll("prematch-country").ToList();
            footballCountries.ForEach(x => GetCountryMatches(x));

            return null;
        }

        private FootballMatch[] GetCountryMatches(HtmlNode countryMatches)
        {
            var countryId = countryMatches.Attributes.First().Value;
            var championshipId = countryMatches.QuerySelector("prematch-block-championship.live-block-championship").Attributes.FirstOrDefault(x=>x.Name == "championship-id").Value;
            var championshipName = countryMatches.QuerySelector("span.championship-name-title__text").InnerText;
            return new FootballMatch[0];
        }
    }
}
