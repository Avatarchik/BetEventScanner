using System.Net;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public class DataSourceFootballDataCoUk
    {
        public void DownloadFile(string url, string pathToStore)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, pathToStore);
            }
        }
    }
}