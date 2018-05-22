namespace BetEventScanner.Common.Contracts
{
    interface IDataImport<T> where T : class, IMatchResult
    {
        void ImportData(string filePath);
    }
}
