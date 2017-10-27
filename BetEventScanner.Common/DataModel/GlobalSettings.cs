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
            //GetSupportedLeagues(supportedLeagues);
        }

        public CommonSettings CommonSettings { get; set; }

        public ApiSettings ApiSettings { get; set; }

        public string Version { get; }

        public string BaseUrl { get; }

        public string ApiKey { get; }

        public string Url { get; set; }

        public IEnumerable<CountryEnum> SupportedCountries { get; set; }

        public IEnumerable<CountryDivision> SupportedCountryDivisions { get; set; }

        private void GetSupportedLeagues(string supportedLeagues)
        {
            var supported = supportedLeagues.Split(new[] { ";" }, StringSplitOptions.None).Select(int.Parse);

            var divisions = supported.Cast<CountryDivision>().ToList();

            //SupportedCountries = divisions.Select(GetCountryByDivision).Distinct();

            //SupportedCountryDivisions = divisions;
        }

        //private static CountryEntityTemp GetCountryByDivision(CountryDivision countryDivision)
        //{
        //    foreach (var map in CountryDivisionMap.Map)
        //    {
        //        if (map.Value.Contains(countryDivision))
        //        {
        //            return map.Key;
        //        }
        //    }

        //    throw new Exception("CountryClass by division not found");
        //}
    }

    public class ApiSettings
    {
    }

    public class CommonSettings
    {
    }
}
