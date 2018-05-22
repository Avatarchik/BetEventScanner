using Newtonsoft.Json;

namespace BetEventScanner.Providers.FootballDataOrg.Model
{
    public class BaseDataModel
    {
        [JsonProperty("_links")]
        public ApiLinksNew Links { get; set; }
    }

    public class ApiLinksNew
    {
        [JsonProperty("self")]
        public Link Self { get; set; }

        [JsonProperty("teams")]
        public Link Teams { get; set; }

        [JsonProperty("fixtures")]
        public Link Fixtures { get; set; }

        [JsonProperty("leagueTable")]
        public Link LeagueTable { get; set; }
    }

    public class Link
    {
        public string Href { get; set; }
    }
}