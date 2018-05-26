using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetEventScanner.DogonWeb.Models
{
    public class PredictBetDto
    {
        public int FLine { get; set; }

        public decimal FBet { get; set; }

        public int SLine { get; set; }

        public decimal SBet { get; set; }

        public int TLine { get; set; }

        public decimal TBet { get; set; }
    }
}