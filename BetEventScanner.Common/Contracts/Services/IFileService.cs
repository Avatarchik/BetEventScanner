namespace BetEventScanner.Common.Contracts.Services
{
    public interface IFileService
    {
        void MoveFile(string from, string to);

        void SaveFile<T>(string filePath, T data);

        T ReadFile<T>(string filePath);
    }
}