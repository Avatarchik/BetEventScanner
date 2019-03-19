using BetEventScanner.Providers.Parimatch.Models.CyberFootball;

namespace BetEventScanner.Providers.Parimatch
{
    public class StatisticsParser
    {
        public static string Url => "https://s5.sir.sportradar.com/parimatch";
        public string FootballItalySeriaA => Url + "1/category/31";

        public StatsTable GetTableStats(string html) => 
            Converter.ConvertToStatisticsTable(html);
    }
}
