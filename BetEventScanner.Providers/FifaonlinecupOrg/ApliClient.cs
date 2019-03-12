using System.Net;

namespace BetEventScanner.Providers.FifaonlinecupOrg
{
    public static class ApliClient
    {
        public static string Get(string url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }
    }
}
