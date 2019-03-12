using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public class LiveService
    {
        Dictionary<string, string> events = new Dictionary<string, string>
            {
                { "apl", "football11794772Item" },
                { "bun1", "football11794770Item" },
                { "fr1", "football11803586Item" },
                { "wc", "football11803588Item" }
            };

        public string GetLiveUrl => "https://www.parimatch.com/en/live.html";

        public string[] GetEventItems() =>
            events.Values.ToArray();

        public LiveBetMatch[] ConvertToLiveBetEvets(string[] htmls) =>
            htmls.SelectMany(Converter.ToLiveBetMatches).ToArray();
    }
}
