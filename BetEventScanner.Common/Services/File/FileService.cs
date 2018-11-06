using System.IO;
using Newtonsoft.Json;

namespace BetEventScanner.Common.Services.Common
{
    public class FileService
    {
        public void MoveFile(string from, string to)
        {
            File.Move(from, to);
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
