using System.Runtime.Serialization;

namespace BetEventScanner.Common
{
    [DataContract]
    public class OddsContract
    {
        [DataMember(Name = "homeWin")]
        public string HomeWin { get; set; }

        [DataMember(Name = "draw")]
        public string Draw { get; set; }

        [DataMember(Name = "awayWin")]
        public string AwayWin { get; set; }
    }
}