using CsvHelper.Configuration;

namespace BetEventScanner.Common.ResultsService
{
    public class FootballDataResultMapping : CsvClassMap<FootaballDataResultDto>
    {
        public FootballDataResultMapping()
        {
            Map(m => m.BbMx_more_25).Name("BbMx>2.5");
            Map(m => m.BbAv_more_25).Name("BbAv>2.5");
            Map(m => m.BbMx_less_25).Name("BbMx<2.5");
            Map(m => m.BbAv_less_25).Name("BbAv<2.5");
        }
    }
}
