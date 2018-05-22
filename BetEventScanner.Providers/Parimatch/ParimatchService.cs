namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchService
    {
        private readonly ParimatchSettings settings;
        private readonly ParimatchApiClient apiClient;

        public ParimatchService(ParimatchSettings settings, ParimatchApiClient apiClient)
        {
            this.settings = settings;
            this.apiClient = apiClient;
        }
    }
}
