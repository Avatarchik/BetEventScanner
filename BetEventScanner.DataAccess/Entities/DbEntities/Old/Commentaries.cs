using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.DbEntities.Old
{
    [DataContract]
    public class Commentaries
    {
        [DataMember(Name = "comm_match_id")]
        public string CommMatchId { get; set; }

        [DataMember(Name = "comm_static_id")]
        public string CommStaticId { get; set; }

        //[DataMember(Name = "comm_match_info")]
        //public CommonMatchInfo CommonMatchInfo { get; set; }
    }
}