using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.DbEntities.Old
{
    [DataContract]
    public class Match
    {
        [DataMember(Name = "match_id")]
        public string MatchId { get; set; }

        [DataMember(Name = "match_static_id")]
        public string MatchStaticId { get; set; }

        [DataMember(Name = "match_comp_id")]
        public string MatchCompId { get; set; }

        [DataMember(Name = "match_date")]
        public string MatchDate { get; set; }

        [DataMember(Name = "match_formatted_date")]
        public string MatchFormattedDate { get; set; }

        [DataMember(Name = "match_season_beta")]
        public string MatchSeasonBeta { get; set; }

        [DataMember(Name = "match_week_beta")]
        public string MatchWeekBeta { get; set; }

        [DataMember(Name = "match_venue_beta")]
        public string MatchVenueBeta { get; set; }

        [DataMember(Name = "match_venue_id_beta")]
        public string MatchVenueIdBeta { get; set; }

        [DataMember(Name = "match_venue_city_beta")]
        public string MatchVenueCityBeta { get; set; }

        [DataMember(Name = "match_status")]
        public string MatchStatus { get; set; }

        [DataMember(Name = "match_timer")]
        public string MatchTimer { get; set; }

        [DataMember(Name = "match_time")]
        public string MatchTime { get; set; }

        [DataMember(Name = "match_commentary_available")]
        public string MatchCommentaryAvailable { get; set; }

        [DataMember(Name = "match_localteam_id")]
        public string MatchLocalteamId { get; set; }

        [DataMember(Name = "match_localteam_name")]
        public string MatchLocalteamName { get; set; }

        [DataMember(Name = "match_localteam_score")]
        public string MatchLocalteamScore { get; set; }

        [DataMember(Name = "match_visitorteam_id")]
        public string MatchVisitorteamId { get; set; }

        [DataMember(Name = "match_visitorteam_name")]
        public string MatchVisitorteamName { get; set; }

        [DataMember(Name = "match_visitorteam_score")]
        public string MatchVisitorteamScore { get; set; }

        [DataMember(Name = "match_ht_score")]
        public string MatchHtScore { get; set; }

        [DataMember(Name = "match_ft_score")]
        public string MatchFtScore { get; set; }

        [DataMember(Name = "match_et_score")]
        public string MatchEtScore { get; set; }

        [DataMember(Name = "match_events")]
        public MatchEvent[] MatchEvents { get; set; }
    }
}
