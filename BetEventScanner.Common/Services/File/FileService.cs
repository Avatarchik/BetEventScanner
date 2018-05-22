using System.IO;
using System.Xml.Serialization;
using BetEventScanner.Common.Contracts.Services;
using Newtonsoft.Json;

namespace BetEventScanner.Common.Services.Common
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

        public T ReadXml<T>(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public T ReadJson<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
            }

            return default(T);
        }

        public void WriteJson<T>(string filePath, T data)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
        }
    }
}
