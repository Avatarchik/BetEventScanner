using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class AuxTeam
    {
        [DataMember(Name = "player")]
        public FixtureStatTeam FixtureStatTeam { get; set; }
    }
}
