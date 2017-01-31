using System.IO;
using System.Xml.Serialization;
using BetEventScanner.Common.Contracts.Services;

namespace BetEventScanner.Common.Services
{
    public class FileService : IFileService
    {
        public void MoveFile(string from, string to)
        {
            File.Move(from, to);
        }

        public void SaveFile<T>(string filePath, T data)
        {
            using (var write = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(write, data);
            }
        }

        public T ReadFile<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
