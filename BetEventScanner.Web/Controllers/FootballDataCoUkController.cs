using BetEventScanner.Providers.FootballDataCoUk;
using BetEventScanner.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BetEventScanner.Web.Controllers
{
    public class FootballDataCoUkController : Controller
    {
        private DirectoryInfo dirInfo = new DirectoryInfo(@"C:\BetEventScanner\Services\FootballDataCoUk\Data\Origin");

        public ActionResult Index()
        {
            var fileNames = new List<string>();

            foreach (var item in dirInfo.GetFiles())
            {
                fileNames.Add(item.Name);   
            }

            return View(fileNames);
        }

        public ActionResult CsvFileHeaders(string fileName)
        {
            var filePath = dirInfo.GetFiles().FirstOrDefault(x => x.Name == fileName)?.FullName;

            if (filePath == null)
            {
                throw new FileNotFoundException(fileName);
            }

            var parser = new FootballDataCoUkParser();
            var headers = parser.GetFileHeaders(filePath).ToList();


            return View(new CsvFileHeadersViewModel
            {
                FileName = fileName,
                Headers = headers
            });
        }

        public ActionResult CsvFileDetails(string fileName, ICollection<string> headers)
        {
            var filePath = dirInfo.GetFiles().FirstOrDefault(x => x.Name == fileName)?.FullName;

            if (filePath == null)
            {
                throw new FileNotFoundException(fileName);
            }

            var parser = new FootballDataCoUkParser();
            var jsonMatches = parser.GetDynamicHistoricalResults(filePath, headers);

            return PartialView(new CsvFileViewModel
            {
                Headers = headers,
                JsonMathes = jsonMatches
            });
        }
    }

   
}