using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class CommMatchPlayerStats
    {
        [DataMember(Name = "localteam")]
        public AuxTeam Localteam { get; set; }

        [DataMember(Name = "visitorteam")]
        public AuxTeam Visitorteam { get; set; }
    }
}