using BetEventScanner.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEventScanner.DataAccess.Entities
{
    public class BetInfo : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstPlayer { get; set; }

        [MaxLength(50)]
        public string SecondPlayer { get; set; }

        public int FavoritePlayer { get; set; }

        public int WinnerLine { get; set; }

        public ICollection<Line> Lines { get; set; }
    }
}
