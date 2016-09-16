using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [DataContract]
    public class FixtureContract
    {
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
        public MatchResultContract MatchResult { get; set; }

        [DataMember(Name = "odds")]
        public OddsContract Odds { get; set; }
    }
}