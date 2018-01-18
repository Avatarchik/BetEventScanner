using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiDataModel
{
    [DataContract]
    public class ApiMatch
    {
        [DataMember(Name = "_links")]
        public ApiLinks ApiLinks { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "matchday")]
        public string Matchday { get; set; }

        [DataMember(Name = "homeTeamName")]
        public string HomeTeamName { get; set; }

        [DataMember(Name = "awayTeamName")]
        public string AwayTeamName { get; set; }

        [DataMember(Name = "result")]
        public ApiResult ApiResult { get; set; }
    }
}