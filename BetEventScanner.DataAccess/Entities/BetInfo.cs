using BetEventScanner.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEventScanner.DataAccess.Entities
{
    public class BetInfo : IEntity
    {
        public int Id { get; set; }

        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public int FavoritePlayer { get; set; }

        public int WinnerLine { get; set; }

        public ICollection<Line> Lines { get; set; }
    }
}
