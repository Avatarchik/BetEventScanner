using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class Player
    {
        [DataMember(Name = "num")]
        public string Num { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "pos")]
        public string Pos { get; set; }

        [DataMember(Name = "posx")]
        public string Posx { get; set; }

        [DataMember(Name = "posy")]
        public string Posy { get; set; }

        [DataMember(Name = "shots_total")]
        public string ShotsTotal { get; set; }

        [DataMember(Name = "shots_on_goal")]
        public string ShotsOnGoal { get; set; }

        [DataMember(Name = "goals")]
        public string Goals { get; set; }

        [DataMember(Name = "assists")]
        public string Assists { get; set; }

        [DataMember(Name = "offsides")]
        public string Offsides { get; set; }

        [DataMember(Name = "fouls_drawn")]
        public string FoulsDrawn { get; set; }

        [DataMember(Name = "fouls_commited")]
        public string FoulsCommited { get; set; }

        [DataMember(Name = "saves")]
        public string Saves { get; set; }

        [DataMember(Name = "yellowcards")]
        public string Yellowcards { get; set; }

        [DataMember(Name = "redcards")]
        public string Redcards { get; set; }

        [DataMember(Name = "pen_score")]
        public string PenScore { get; set; }

        [DataMember(Name = "pen_miss")]
        public string PenMiss { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}