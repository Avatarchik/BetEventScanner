using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public static class SupportedEvents
    {
        private static Dictionary<string, EventType> _supportedOddsBetEvents = new Dictionary<string, EventType>
        {
            //{ "football",  EventType.Football},
            { "tennis", EventType.Tennis }
        };

        private static List<string> _nonSupportedOddsBetEvents = new List<string>
        {
            "american football",
            "basketball",
            "baseball",
            "boxing",
            "volleyball",
            "handball",
            "lottery",
            "rugby",
            "weather in kyiv",
            "formula 1",
            "futsal",
            "ice hockey",
            "grand chess tour",
            "what?  where?  when?"
        };

        private static bool CheckForExtraRules(EventType eventType, string header)
        {
            var lowerHeader = header.ToLower();

            switch (eventType)
            {
                case EventType.None:
                    return false;
                case EventType.Football:
                    break;
                case EventType.Tennis:
                    return lowerHeader.Contains("doubles") || lowerHeader.Contains("additional");
                case EventType.TennisAtpGrandSlam:
                    break;
                default:
                    return false;
            }

            return false;
        }

        public static EventType GetSupportedEventType(string header)
        {
            var lowerHeader = header.ToLower();

            if (_nonSupportedOddsBetEvents.Any(x => lowerHeader.Contains(x)))
            {
                return EventType.None;
            }

            var supportedEvent = _supportedOddsBetEvents.FirstOrDefault(x => lowerHeader.Contains(x.Key));
            if (supportedEvent.Key == null)
            {
                return EventType.None;
            }

            if (CheckForExtraRules(supportedEvent.Value, lowerHeader))
            {
                return EventType.None;
            }

            return supportedEvent.Value;
        }
    }
}
