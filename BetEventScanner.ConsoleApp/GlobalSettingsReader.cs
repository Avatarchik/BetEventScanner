using System.Configuration;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.ConsoleApp
{
    public static class GlobalSettingsReader
    {
        public static GlobalSettings GetGlobalSettings()
        {
            var scheme = ConfigurationManager.AppSettings["scheme"];
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            var version = ConfigurationManager.AppSettings["version"];
            var apiKey = ConfigurationManager.AppSettings["apiKey"];
            var supportedLeagues = ConfigurationManager.AppSettings["supportedLeagues"];

            return new GlobalSettings(scheme, baseUrl, version, apiKey, supportedLeagues);
        }
    }
}


