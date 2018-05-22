using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;

namespace BetEventScanner.Common.Services.Csv
{
    public abstract class CsvParserBase
    {
        protected ICollection<T> Read<T>(string filePath)
        {
            var result = new List<T>();

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.HasHeaderRecord = true;

                while (csvReader.Read())
                {
                    var matchResult = csvReader.GetRecord<T>();
                    result.Add(matchResult);
                }
            }

            return result;
        }

        protected ICollection<JObject> Parse(string filePath, ICollection<string> selectedHeaders)
        {
            var result = new List<JObject>();

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                var csvParser = new CsvParser(reader);
                var headers = csvParser.Read().ToList();
                csvParser.Configuration.Delimiter = ",";
                csvParser.Configuration.IgnoreBlankLines = false;
                
                while (true)
                {
                    var row = csvParser.Read();
                    if (row == null)
                    {
                        break;
                    }

                    var jObj = new JObject();

                    foreach (var selectedHeader in selectedHeaders)
                    {
                        var index = headers.IndexOf(selectedHeader);
                        if (index > row.Length - 1)
                        {
                            jObj[selectedHeader] = "";
                        }
                        else
                        {
                            jObj[selectedHeader] = row[index];
                        }
                        
                    }

                    result.Add(jObj);
                }
            }

            return result;
        }

        protected ICollection<string> GetHeaders(string filepath)
        {
            using (var reader = new StreamReader(filepath, Encoding.UTF8))
            {
                return new CsvParser(reader).Read();
            }
        }
    }
}