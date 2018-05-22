using System.Collections.Generic;
using System.Linq;

namespace Dogon
{
    public class BankStorage
    {
        public decimal OriginBank { get; set; } = 1000;

        public decimal Lost { get; } = 0.0m;

        public IList<ReverseBet> BetsList { get; set; } = new List<ReverseBet>();

        public decimal GetCurrentBank()
        {
            return OriginBank -  BetsList.Sum(x => x.OperationResult);
        }
    }
}