using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.Parimatch.Model;
using HtmlAgilityPack;

namespace BetEventScanner.Providers.Parimatch
{
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

            var eventContainers = htmlDoc.QuerySelectorAll("div.event-wrap").ToList();

            return null;
        }
    }
}
