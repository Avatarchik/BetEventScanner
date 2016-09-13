namespace BetEventScanner.Common.Contracts
{
    interface IDataImport<T> where T : class, IFixture
    {
        void ImportData(string filePath);
    }
}
