using System.ComponentModel.DataAnnotations.Schema;

namespace BetEventScanner.DataAccess.DataModel
{
    public class FlatBet
    {
        public int Id { get; set; }

        public string Tournament { get; set; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public decimal SK1 { get; set; }

        public string SK1Descr { get; set; }

        public decimal SK2 { get; set; }

        public string SK2Descr { get; set; }

        public decimal SK3 { get; set; }

        public string SK3Descr { get; set; }

        public FlatBetResult Result { get; set; }
    }

    public class FlatBetResult
    {
        [ForeignKey("FlatBet")]
        public int Id { get; set; }

        public FlatBet FlatBet { get; set; }

        public string Result { get; set; }

        public string Set1 { get; set; }

        public string Set2 { get; set; }

        public string Set3 { get; set; }

        public string Set4 { get; set; }

        public string Set5 { get; set; }
    }
}