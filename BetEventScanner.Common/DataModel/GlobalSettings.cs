using System;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Common.DataModel
{
    public class GlobalSettings
    {
        public GlobalSettings(string scheme, string baseUrl, string version, string apiKey, string supportedLeagues)
        {
            BaseUrl = baseUrl;
            Version = version;
            ApiKey = apiKey;
            var uriBuilder = new UriBuilder
            {
                Scheme = scheme,
                Host = BaseUrl,
                Path = Version
            };
            Url = uriBuilder.ToString();
            SupportedLeagues = GetSupportedLeagues(supportedLeagues);
        }

        public CommonSettings CommonSettings { get; set; }

        public ApiSettings ApiSettings { get; set; }

        public string Version { get; }

        public string BaseUrl { get; }

        public string ApiKey { get; }

        public string Url { get; set; }

        public IEnumerable<int> SupportedLeagues { get; set; }

        private IEnumerable<int> GetSupportedLeagues(string supportedLeagues)
        {
            return supportedLeagues.Split(new[] { ";" }, StringSplitOptions.None).Select(int.Parse);
        }
    }

    public class ApiSettings
    {
    }

    public class CommonSettings
    {
    }
}
