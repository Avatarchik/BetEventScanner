using System.Runtime.Serialization;

namespace BetEventScanner.Common
{
    [DataContract]
    public class MatchResultContract
    {
        [DataMember(Name = "goalsHomeTeam")]
        public string GoalsHomeTeam { get; set; }

        [DataMember(Name = "goalsAwayTeam")]
        public string GoalsAwayTeam { get; set; }

    }
}