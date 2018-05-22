using System.Configuration;
using System.IO;

namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchSettings
    {
        public ParimatchSettings()
        {
            BaseUrl = ConfigurationManager.AppSettings["ParimatchBaseUrl"];
            BaseDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["ParimatchBaseDirectory"]);
            ArchiveDirPath = new DirectoryInfo(BaseDirectory.FullName + "\\archive\\ArchiveOdds");
        }

        public string BaseUrl { get; }

        public DirectoryInfo ArchiveDirPath { get; }

        public DirectoryInfo BaseDirectory { get; }
    }
}
