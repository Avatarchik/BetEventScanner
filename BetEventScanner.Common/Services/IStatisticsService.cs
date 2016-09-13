namespace BetEventScanner.Common.Services
{
    public interface IStatisticsService
    {
        void GetData<T>(string url) where T : new();
    }
}