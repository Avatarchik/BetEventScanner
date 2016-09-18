using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class GeneralTableEntity : IDocEntity
    {
        public int Id { get; set; } = 1;

        public int Teams { get; set; } = 2;

        public int Competitions { get; set; } = 3;

        public int Fixtures { get; set; } = 4;

        public int Statistics { get; set; } = 5;
    }
}