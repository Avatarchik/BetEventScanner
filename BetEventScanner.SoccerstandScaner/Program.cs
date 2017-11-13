using System;
using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ParseSettings
            {
                //Url = "https://www.soccerstand.com/soccer/austria/tipico-bundesliga-2016-2017/"
                //Url = "https://www.soccerstand.com/soccer/belgium/jupiler-league-2016-2017/"
                Url = "https://www.soccerstand.com/soccer/switzerland/super-league-2016-2017/"
            };

            var parser = new SoccerStandParser(new SoccerStandParserStorage(), settings);
            parser.Parse();

            //var converter = new SoccerStandMatchConverter();
            //converter.Convert(new SoccerStandDataSource(settings));

            Console.WriteLine("Complete...");
            Console.ReadLine();
        }
    }
}
