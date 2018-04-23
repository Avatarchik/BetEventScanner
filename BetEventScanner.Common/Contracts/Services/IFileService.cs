namespace BetEventScanner.Common.Contracts.Services
{
    public interface IFileService
    {
        void MoveFile(string from, string to);

        void SaveFile<T>(string filePath, T data);

        T ReadXml<T>(string filePath);

        T ReadJson<T>(string filePath);

        void WriteJson<T>(string statusFilePath, T data);
    }
}