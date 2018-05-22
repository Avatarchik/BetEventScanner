using Newtonsoft.Json;
using System;

namespace BetEventScanner.Providers.FootballDataOrg.Model
{
    public class CompetitionNew : BaseDataModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("currentMatchday")]
        public int CurrentMatchday { get; set; }

        [JsonProperty("numberOfMatchdays")]
        public int NumberOfMatchdays { get; set; }

        [JsonProperty("numberOfTeams")]
        public int NumberOfTeams { get; set; }

        [JsonProperty("numberOfGames")]
        public int NumberOfGames { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }
}