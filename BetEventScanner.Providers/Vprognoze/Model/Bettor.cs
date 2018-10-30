using System;

namespace BetEventScanner.Providers.Vprognoze.Model
{

    public class Bettor
    {
        public string Name { get; set; }
        public string Cid { get; set; }
        public string Uid { get; set; }
        public int Rank { get; set; }
        public double Profit { get; set; }
        public int TotalBets { get; set; }
        public int Win { get; set; }
        public int Lost { get; set; }
        public int Draw { get; set; }
        public double AvgKoef { get; set; }
        public int AvgTotalBet { get; set; }
        public int WinPercentage { get; set; }
        public double Roi { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
