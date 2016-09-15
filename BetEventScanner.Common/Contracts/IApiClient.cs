namespace BetEventScanner.Common.Contracts
{
    public interface IApiClient
    {
        T GetCountryCompetitionData<T>();
    }
}