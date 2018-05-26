using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetEventScanner.DogonWeb.Models
{
    public class BetInfoListDto
    {
        public int Id { get; set; }
        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public int FavoritePlayer { get; set; }

        public string FLine { get; set; }

        public decimal FBet { get; set; }

        public string SLine { get; set; }

        public decimal SBet { get; set; }

        public string TLine { get; set; }

        public decimal TBet { get; set; }

        public int WinLine { get; set; }
    }
}