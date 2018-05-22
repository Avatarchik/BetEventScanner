using BetEventScanner.DataAccess.Contracts;
using System.Collections.Generic;

namespace BetEventScanner.DataAccess.DataModel
{
    public class Country : IEntity
    {
        public int Id { get; set; }

        public string IsoCode { get; set; }

        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
