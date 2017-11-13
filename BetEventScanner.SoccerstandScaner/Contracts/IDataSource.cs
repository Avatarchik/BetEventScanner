using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner.Contracts
{
    public interface IDataSource
    {
        SoccerstandData GetSourceData();
    }
}