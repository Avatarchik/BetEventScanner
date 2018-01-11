using BetEventScanner.Common.Services.FootbalDataCoUk.Model;
using CsvHelper.Configuration;

namespace BetEventScanner.Common.Services.FootbalDataCoUk.Mappings
{
    public sealed class FootballDataCoUkFixtureMapping : CsvClassMap<FixtureMatch>
    {
        public FootballDataCoUkFixtureMapping()
        {
            Map(x => x.Div).Name("Div");
            Map(x => x.Date).Name("Date");
            Map(x => x.HomeTeam).Name("HomeTeam");
            Map(x => x.AwayTeam).Name("AwayTeam");

            Map(x => x.FTHG).Name("FTHG");
            Map(x => x.FTAG).Name("FTAG");
            Map(x => x.FTR).Name("FTR");

            Map(x => x.HTHG).Name("HTHG");
            Map(x => x.HTAG).Name("HTAG");
            Map(x => x.HTR).Name("HTR");

            Map(x => x.B365H).Name("B365H");
            Map(x => x.B365D).Name("B365D");
            Map(x => x.B365A).Name("B365A");

            Map(x => x.BbMxOver25).Name("BbMx>2.5");
            Map(x => x.BbAvOver25).Name("BbAv>2.5");
            Map(x => x.BbMxUnder25).Name("BbMx<2.5");
            Map(x => x.BbAvUnder25).Name("BbAv<2.5");

            Map(x => x.BbAHh).Name("BbAHh");
            Map(x => x.BbMxAHH).Name("BbMxAHH");
            Map(x => x.BbAvAHH).Name("BbAvAHH");
            Map(x => x.BbMxAHA).Name("BbMxAHA");
            Map(x => x.BbAvAHA).Name("BbAvAHA");
        }
    }
}