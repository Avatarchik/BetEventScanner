using CsvHelper.Configuration;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public class FootballDataCoUkFixture
    {
        public string Div { get; set; }

        public string Date { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string FTHG { get; set; }

        public string FTAG { get; set; }

        public string FTR { get; set; }

        public string HTHG { get; set; }

        public string HTAG { get; set; }

        public string HTR { get; set; }

        public string B365H { get; set; }

        public string B365D { get; set; }

        public string B365A { get; set; }

        public string VCH { get; set; }

        public string VCD { get; set; }

        public string VCA { get; set; }

        public string Bb1X2 { get; set; }

        public string BbMxH { get; set; }

        public string BbAvH { get; set; }

        public string BbMxD { get; set; }

        public string BbAvD { get; set; }

        public string BbMxA { get; set; }

        public string BbAvA { get; set; }

        public string BbOU { get; set; }

        public string BbMxOver25 { get; set; }

        public string BbAvOver25 { get; set; }

        public string BbMxUnder25 { get; set; }

        public string BbAvUnder25 { get; set; }

        public string BbAH { get; set; }

        public string BbAHh { get; set; }

        public string BbMxAHH { get; set; }

        public string BbAvAHH { get; set; }

        public string BbMxAHA { get; set; }

        public string BbAvAHA { get; set; }
    }

    public sealed class FootballDataCoUkFixtureMapping : CsvClassMap<FootballDataCoUkFixture>
    {
        public FootballDataCoUkFixtureMapping()
        {
            PropertyMaps.Add(Map(x => x.Div).Index(0));
            PropertyMaps.Add(Map(x => x.Date).Index(1));
            PropertyMaps.Add(Map(x => x.HomeTeam).Index(2));
            PropertyMaps.Add(Map(x => x.AwayTeam).Index(3));
            
            // Full time
            PropertyMaps.Add(Map(x => x.FTHG).Index(4));
            PropertyMaps.Add(Map(x => x.FTAG).Index(5));
            PropertyMaps.Add(Map(x => x.FTR).Index(6));
            
            // Half time
            PropertyMaps.Add(Map(x => x.HTHG).Index(7));
            PropertyMaps.Add(Map(x => x.HTAG).Index(8));
            PropertyMaps.Add(Map(x => x.HTR).Index(9));
            
            // 1x2
            PropertyMaps.Add(Map(x => x.B365H).Index(10));
            PropertyMaps.Add(Map(x => x.B365D).Index(11));
            PropertyMaps.Add(Map(x => x.B365A).Index(12));
            
            // Total 
            PropertyMaps.Add(Map(x => x.BbMxOver25).Index(40));
            PropertyMaps.Add(Map(x => x.BbAvOver25).Index(41));
            PropertyMaps.Add(Map(x => x.BbMxUnder25).Index(42));
            PropertyMaps.Add(Map(x => x.BbAvUnder25).Index(43));
            
            // Handicap
            PropertyMaps.Add(Map(x => x.BbAHh).Index(44));
            PropertyMaps.Add(Map(x => x.BbMxAHH).Index(45));
            PropertyMaps.Add(Map(x => x.BbAvAHH).Index(46));
            PropertyMaps.Add(Map(x => x.BbMxAHA).Index(47));
            PropertyMaps.Add(Map(x => x.BbAvAHA).Index(48));
        }
    }
}