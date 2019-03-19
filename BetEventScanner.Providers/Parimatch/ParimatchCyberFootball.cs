using System.Collections.Generic;

namespace BetEventScanner.Providers.Parimatch
{
    public static class ParimatchCyberFootball
    {
        public static Dictionary<string, string> Items { get; } = new Dictionary<string, string>
            {
                { "apl", "football11794772Item" },
                { "bun1", "football11794770Item" },
                { "fr1", "football11803586Item" },
                { "wc", "football11803588Item" },
                { "fr", "football11837462Item" },
                { "wc2","football11837464Item" }
            };
    }

    public class ParimatchCredentials
    {
        public string User { get; set; }
        public string Pasw { get; set; }
    }
}
