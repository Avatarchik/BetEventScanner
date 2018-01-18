using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace BetEventScanner.Common.Services.Csv
{
    public abstract class CsvParserBase
    {
        protected ICollection<T> Parse<T>(string filePath, CsvClassMap classMap = null)
        {
            var result = new List<T>();

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.HasHeaderRecord = true;

                if (classMap != null)
                {
                    csvReader.Configuration.RegisterClassMap(classMap);
                }

                while (csvReader.Read())
                {
                    var matchResult = csvReader.GetRecord<T>();
                    result.Add(matchResult);
                }
            }

            return result;
        }
    }
}