using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class FixtureStatTeam
    {
        [DataMember(Name = "1")]
        public Player Player1 { get; set; }

        [DataMember(Name = "2")]
        public Player Player2 { get; set; }

        [DataMember(Name = "3")]
        public Player Player3 { get; set; }

        [DataMember(Name = "4")]
        public Player Player4 { get; set; }

        [DataMember(Name = "5")]
        public Player Player5 { get; set; }

        [DataMember(Name = "6")]
        public Player Player6 { get; set; }

        [DataMember(Name = "7")]
        public Player Player7 { get; set; }

        [DataMember(Name = "8")]
        public Player Player8 { get; set; }

        [DataMember(Name = "9")]
        public Player Player9 { get; set; }

        [DataMember(Name = "10")]
        public Player Player10 { get; set; }

        [DataMember(Name = "11")]
        public Player Player11 { get; set; }

        [DataMember(Name = "12")]
        public Player Player12 { get; set; }

        [DataMember(Name = "13")]
        public Player Player13 { get; set; }

        [DataMember(Name = "14")]
        public Player Player14 { get; set; }

        [DataMember(Name = "15")]
        public Player Player15 { get; set; }

        [DataMember(Name = "16")]
        public Player Player16 { get; set; }

        [DataMember(Name = "17")]
        public Player Player17 { get; set; }

        [DataMember(Name = "18")]
        public Player Player18 { get; set; }
    }
}