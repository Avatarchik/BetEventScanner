using System.Runtime.Serialization;

namespace BetEventScanner.Providers.FootballDataOrg.ApiDataModel
{
    [DataContract]
    public class ApiResult
    {
        [DataMember(Name = "goalsHomeTeam")]
        public string GoalsHomeTeam { get; set; }

        [DataMember(Name = "goalsAwayTeam")]
        public ApiResult GoalsAwayTeam { get; set; }
    }
}