namespace BetEventScanner.Common.Contracts
{
    public interface IApiClient
    {
        T GetData<T>(string url);
    }
}