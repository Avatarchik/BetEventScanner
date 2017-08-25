using System;
using System.Net;

namespace BetEventScanner.Common.ResultsService
{
    public class DataSourceFootballData
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