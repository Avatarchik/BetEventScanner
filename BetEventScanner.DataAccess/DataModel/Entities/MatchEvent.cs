using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class MatchEvent
    {
        
        [DataMember(Name = "event_id")]
        public string EventId { get; set; }

        [DataMember(Name = "event_match_id")]
        public string EventMatchId { get; set; }

        [DataMember(Name = "event_type")]
        public string EventType { get; set; }

        [DataMember(Name = "event_minute")]
        public string EventMinute { get; set; }

        [DataMember(Name = "event_team")]
        public string EventTeam { get; set; }

        [DataMember(Name = "event_player")]
        public string EventPlayer { get; set; }

        [DataMember(Name = "event_player_id")]
        public string EventPlayerId { get; set; }

        [DataMember(Name = "event_result")]
        public string EventResult { get; set; }

    }
}
