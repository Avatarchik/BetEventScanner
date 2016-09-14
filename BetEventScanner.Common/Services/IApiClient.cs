namespace BetEventScanner.Common.Services
{
    public interface IApiClient
    {
        T GetData<T>(string url);
    }
}