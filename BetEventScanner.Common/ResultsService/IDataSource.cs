namespace BetEventScanner.Common.ResultsService
{
    public interface IDataSource
    {
        bool DownloadFile(string url, string pathToStore);
    }
}