namespace BetEventScanner.Common.Contracts
{
    public interface IStatisticsService
    {
        void GetData<T>(string url) where T : new();
    }
}