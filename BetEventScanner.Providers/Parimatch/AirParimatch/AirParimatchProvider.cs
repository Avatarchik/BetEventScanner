using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.Parimatch.Model;
using HtmlAgilityPack;

namespace BetEventScanner.Providers.Parimatch
{
    public class AirParimatchProvider : IOddsProvider, IAirParimatchProvider
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
