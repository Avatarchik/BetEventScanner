using BetEventScanner.Providers.Vprognoze;
using BetEventScanner.Providers.Vprognoze.Model;
using System.Collections.Generic;
using System.Net;

namespace ParimatchDayOddsParser.Vprognoze
{
    public class Api
    {
        public static string Get(string url)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "text/html; charset=windows-1251");
                wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                wc.Headers.Add("Host", "vprognoze.ru");
                wc.Headers.Add("Referer", "url");
                wc.Headers.Add("Accept-Encoding", "zip, deflate, br");
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Mobile Safari/537.36");
                wc.Headers.Add("Cookie", "rerf=AAAAAFy0w/aX2yKLAxwAAg==; login_user_token=ecabb8d7774a9d9de1ba0d2079e87729; ipp_uid1=1555350521531; ipp_uid2=RLtrqTIkkwU9UMDF/CImU5V72/i3rdk/fIdfdFw==; ipp_uid=1555350521531/RLtrqTIkkwU9UMDF/CImU5V72/i3rdk/fIdfdFw==; __utma=187128303.551209789.1555350523.1555350523.1555350523.1; __utmc=187128303; __utmz=187128303.1555350523.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); autotimezone=2; ipp_static_key=1555350532534/DyNcZUKL1yXU6W5thl4opA==; __utmt=1; PHPSESSID=97ad884cfe9bfe3d59fdc42e6ca42df0; dle_user_id=974053; dle_password=97c6da89408364b296f9e9df2998d857; dle_newpm=0; module_online=1; __utmb=187128303.23.10.1555350523; ipp_key=v1555354876399/v3394724575ded878b223b2d5/0IIV8B/ILIZcf4jBME0GrA==");

                var rsp = wc.DownloadString(url);

                return rsp;
            }
        }
    }

    public class Parser
    {


        private static void ParseVprognoze()
        {
            var html = ParimatchWebBrowser.ParseWebDriver("http://vprognoze.ru/statalluser/");
            var vpr = new VprProvider();
            var bettors = vpr.GetCurrentTopUsers(html);

            var d = new Dictionary<Bettor, Bet[]>();

            foreach (var bettor in bettors)
            {
                var link = vpr.GetBettorBetLink(bettor);
                var betsHtml = ParimatchWebBrowser.ParseWebDriver(link);
                var bettotBets = vpr.ParseBettorBets(bettor, betsHtml);

                d.Add(bettor, bettotBets);
            }
        }

        private static void ParseVprognozeIncomingAllBets()
        {
            var html = ParimatchWebBrowser.ParseWebDriver("https://vprognoze.ru/?do=searchbar&page=1");
            var vpr = new VprProvider();

            var incomingBetEvents = new Dictionary<Bettor, Bet[]>();

            var betEvents = vpr.ParseIncomingBets(html);
        }
    }
}
