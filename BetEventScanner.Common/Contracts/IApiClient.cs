namespace BetEventScanner.Common.Contracts
{
    public interface IApiClient
    {
        T GetCountryCompetition<T>();
    }
}