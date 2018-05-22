using BetEventScanner.Common.Services.TennisAbstract.Model;
using CsvHelper.Configuration;

namespace BetEventScanner.Common.Services.TennisAbstract.DataSource
{
    internal sealed class TennisAbstractPlayerMapping : CsvClassMap<AtpPlayer>
    {
        public TennisAbstractPlayerMapping()
        {
            Map(x => x.PlayerId).Index(0);
            Map(x => x.FirstName).Index(1);
            Map(x => x.LastName).Index(2);
            Map(x => x.Hand).Index(3);
            Map(x => x.BirthDate).Index(4);
            Map(x => x.СountryCode).Index(5);
        }
    }
}
