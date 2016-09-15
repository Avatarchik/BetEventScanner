using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiDataModel
{
    [DataContract]
    public class CompetitionContract
    {
        [DataMember(Name = "_links")]
        public ApiLinks ApiLinks { get; set; }

        [DataMember(Name = "id")]
        public string LeagueId { get; set; }

        [DataMember(Name = "league")]
        public string ShortName { get; set; }

        [DataMember(Name = "caption")]
        public string Name { get; set; }

        [DataMember(Name = "year")]
        public string Year { get; set; }

        [DataMember(Name = "numberOfMatchdays")]
        public string NumberOfMatchdays { get; set; }

        [DataMember(Name = "numberOfTeams")]
        public string NumberOfTeams { get; set; }

        [DataMember(Name = "numberOfGames")]
        public string NumberOfGames { get; set; }

        [DataMember(Name = "lastUpdated")]
        public string LastUpdated { get; set; }

    }
}