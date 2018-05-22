using System;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace BetEventScanner.Common.Services.RestApiClient
{
    public class RestApiService
    {
        public static T GetData<T>(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers.Add("X-Auth-Token", "3dfb6fb9ec0848989f9f2c9b2a6043a5");
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception($"Server error (HTTP {response.StatusCode}: {response.StatusDescription}).");
                var jsonSerializer = new DataContractJsonSerializer(typeof(T));
                var objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                var entity = (T)objResponse;
                return entity;
            }
        }

        public static T GetData<T>(string url, string method = null)
        {
            using (var wc = new WebClient())
            {
                var response = wc.DownloadString(url);
                return JsonConvert.DeserializeObject<T>(response);
            }
        }
    }
}
