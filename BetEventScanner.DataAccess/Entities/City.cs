using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.DataAccess.DataModel
{
    public class City : IEntity
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public string Name { get; set; }
    }
}
