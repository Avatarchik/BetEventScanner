using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BetEventScanner.Web.ViewModels
{
    public class CsvFileViewModel
    {
        public ICollection<string> Headers { get; set; }

        public ICollection<JObject> JsonMathes { get; set; }
    }

    public class CsvFileHeadersViewModel
    {
        public string FileName { get; set; }

        public ICollection<string> Headers { get; set; }
    }
}