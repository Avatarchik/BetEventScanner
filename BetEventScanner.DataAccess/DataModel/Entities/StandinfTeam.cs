using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class StandinfTeam
    {
        [DataMember(Name="stand_id")]
        public string StandId { get; set; }

        [DataMember(Name="stand_competition_id")]
        public string StandCompetitionId { get; set; }

        [DataMember(Name="stand_season")]
        public string StandSeason { get; set; }

        [DataMember(Name="stand_round")]
        public string StandRound { get; set; }

        [DataMember(Name="stand_stage_id")]
        public string StandStageId { get; set; }

        [DataMember(Name="stand_group")]
        public string StandGroup { get; set; }

        [DataMember(Name="stand_country")]
        public string StandCountry { get; set; }

        [DataMember(Name="stand_team_id")]
        public string StandTeamId { get; set; }

        [DataMember(Name="stand_team_name")]
        public string StandTeamName { get; set; }

        [DataMember(Name="stand_recent_form")]
        public string StandRecentForm { get; set; }

        [DataMember(Name="stand_position")]
        public string StandPosition { get; set; }

        [DataMember(Name="stand_overall_gp")]
        public string StandOverallGp { get; set; }

        [DataMember(Name="stand_overall_w")]
        public string StandOverall_W{ get; set; }

        [DataMember(Name="stand_overall_d")]
        public string StandOverall_D { get; set; }

        [DataMember(Name="stand_overall_l")]
        public string StandOverall_L { get; set; }

        [DataMember(Name="stand_overall_gs")]
        public string StandOverall_Gs { get; set; }

        [DataMember(Name="stand_overall_ga")]
        public string StandOverall_Ga { get; set; }

        [DataMember(Name="stand_home_gp")]
        public string StandHomeGp { get; set; }

        [DataMember(Name="stand_home_w")]
        public string StandHome_W { get; set; }

        [DataMember(Name="stand_home_d")]
        public string StandHome_D { get; set; }

        [DataMember(Name="stand_home_l")]
        public string StandHome_L { get; set; }

        [DataMember(Name="stand_home_gs")]
        public string StandHome_Gs { get; set; }

        [DataMember(Name="stand_home_ga")]
        public string StandHome_Ga { get; set; }

        [DataMember(Name="stand_away_gp")]
        public string StandAway_Gp { get; set; }

        [DataMember(Name="stand_away_w")]
        public string StandAway_W { get; set; }

        [DataMember(Name="stand_away_d")]
        public string StandAway_D { get; set; }

        [DataMember(Name="stand_away_l")]
        public string StandAway_L { get; set; }

        [DataMember(Name="stand_away_gs")]
        public string StandAway_Gs { get; set; }

        [DataMember(Name="stand_away_ga")]
        public string StandAway_Ga { get; set; }

        [DataMember(Name="stand_gd")]
        public string StandAway_Gd { get; set; }

        [DataMember(Name="stand_points")]
        public string StandPoints { get; set; }

        [DataMember(Name="stand_desc")]
        public string StandDesc { get; set; }
































    }
}
