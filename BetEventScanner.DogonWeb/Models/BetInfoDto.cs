using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetEventScanner.DogonWeb.Models
{
    public class BetInfoDto
    {
        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public int FavoritePlayer { get; set; }

        public decimal FirstLineCoef { get; set; }

        public decimal FirstLineBet { get; set; }

        public decimal SecondLineCoef { get; set; }

        public decimal SecondLineBet { get; set; }

        public decimal ThirdLineCoef { get; set; }

        public decimal ThirdLineBet { get; set; }

        public bool ManualBet { get; set; }
    }
}