using System;
using System.Collections.Generic;
using BetEventScanner.Providers.Parimatch.Model;

namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchApiClient
    {
        public ICollection<ParimatchFootballBetEvent> GetArchiveBetEvents(DateTime date)
        {
            return null;
        }

        public ICollection<ParimatchFootballBetEvent> GetArchiveBetEvents(ICollection<DateTime> dates)
        {
            var archiveEvents = new List<ParimatchFootballBetEvent>();
            return archiveEvents;
        }
    }
}
