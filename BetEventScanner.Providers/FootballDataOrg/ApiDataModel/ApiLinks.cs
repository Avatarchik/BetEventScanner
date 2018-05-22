using Newtonsoft.Json;
using System.Runtime.Serialization;

// for football data
namespace BetEventScanner.Providers.FootballDataOrg.ApiDataModel
{
    [DataContract]
    public class ApiLinks
    {
        [DataMember(Name = "self")]
        public ApiLink Self { get; set; }

        [DataMember(Name = "soccerseason")]
        public ApiLink Soccerseason { get; set; }

        [DataMember(Name = "teams")]
        public ApiLink Teams { get; set; }

        [DataMember(Name = "fixtures")]
        public ApiLink Fixtures { get; set; }

        [DataMember(Name = "leagueTable")]
        public ApiLink LeagueTable { get; set; }

        [DataMember(Name = "players")]
        public ApiLink Players { get; set; }
    }

    public class ApiLinksTest
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("soccerseason")]
        public string Soccerseason { get; set; }

        [DataMember(Name = "teams")]
        public string Teams { get; set; }

        [DataMember(Name = "fixtures")]
        public string Fixtures { get; set; }
    }
}