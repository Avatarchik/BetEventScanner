using System.Timers;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class RequestService
    {
        // define timer period in appconfig
        private readonly Timer _timer;
        private readonly FootballService _footballService;

        public RequestService()
        {
            _timer = new Timer(10000);
            _timer.Elapsed += Timer_Elapsed;
            var settings = GlobalSettingsReader.GetGlobalSettings();
            _footballService = new FootballService(settings, new FootballDataApiClient(settings, new FootballDataCountryMap()), new MongoDbProvider());
        }

        public bool Started { get; private set; }

        public void Start()
        {
            Started = true;
            _timer.Start();
        }

        public void Stop()
        {
            Started = false;
            _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            _footballService.DoWork();

            _timer.Start();
        }

        private void Test()
        {
        }

    }
}