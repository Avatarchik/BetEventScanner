﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.Parimatch.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace BetEventScanner.Providers.Parimatch
{
    public class OldParimatchProvider : IOddsProvider
    {
        private readonly ParimatchSettings _settings;
        private readonly ParimatchApiClient _client;

        public OldParimatchProvider(ParimatchSettings settings)
        {
            _client = new ParimatchApiClient();
            _settings = settings;
        }

        public ICollection<string> LoadByDates(ICollection<DateTime> dates)
        {
            var distinct = dates.Distinct();
            var total = distinct.ToList().Count;
            var existingFiles = _settings.ArchiveDirPath.GetFiles().ToList();

            var res = new List<string>();

            foreach (var date in distinct)
            {
                var dt = date.ToString("yyyyMMdd");
                if (existingFiles.Any(x => x.Name == dt))
                {
                    var file = existingFiles.FirstOrDefault(x => x.Name == dt);
                    res.Add(File.ReadAllText(file.FullName));
                    continue;
                }

                var html = _client.DownloadHtml($"{_settings.BaseUrl}{dt}");
                File.WriteAllText($@"{_settings.ArchiveDirPath}\{dt}", html);
                res.Add(html);
            }

            return res;
        }

        public string LoadArchiveDate(DateTime date)
        {
            var existingFiles = _settings.ArchiveDirPath.GetFiles().ToList();

            var dt = date.ToString("yyyyMMdd");
            if (existingFiles.Any(x => x.Name == dt))
            {
                var file = existingFiles.FirstOrDefault(x => x.Name == dt);
                return File.ReadAllText(file.FullName);
            }

            var html = _client.DownloadHtml($"{_settings.BaseUrl}{dt}");
            File.WriteAllText($@"{_settings.ArchiveDirPath}\{dt}", html);
            return html;
        }

        public void Parse(ParseSettings parseSettings)
        {
            var fulleList = new Dictionary<string, ParimatchFootballBetEvent>();
            var allFiles = _settings.BaseDirectory.GetFiles().Where(x => !x.Name.Contains("oddslist")).Select(x => x.Name).ToList();

            var dateTimeResolver = new MatchDateResolver(allFiles);

            foreach (var fileName in allFiles)
            {
                Console.WriteLine(fileName);
                var html = new HtmlDocument();

                var path = $"{_settings.ArchiveDirPath.FullName}\\{fileName}";

                html.LoadHtml(File.ReadAllText(path));
                var nodes = html.DocumentNode.SelectNodes(@"//div[@class=""container gray""]");

                foreach (var node in nodes)
                {
                    var name = node.SelectSingleNode("h3");
                    if (name.InnerText.ToUpper() != parseSettings.CountryDivision)
                    {
                        continue;
                    }

                    var table = node.QuerySelector("table[class=dt]");
                    var headers = table.QuerySelectorAll("tbody[class=processed] > tr > th").Select(x => x.InnerText).ToList();
                    var rows = table.QuerySelectorAll("tbody[class^=row]").ToList();

                    var source = rows[0].QuerySelectorAll("tr").Where(x => !string.IsNullOrEmpty(x.InnerText)).ToList();
                    for (int i = 0; i < source.Count; i++)
                    {
                        //var m = source[i];
                        //var res = source[++i].QuerySelector("td > .p2r").InnerText;
                        //var fbevent = ParimatchEventsConverter.ConvertToFootballEvent(dateTimeResolver, headers, m, res);
                        //if (fbevent == null)
                        //{
                        //    continue;
                        //}

                        //if (!fulleList.ContainsKey(fbevent.MatchId))
                        //{
                        //    fulleList.Add(fbevent.MatchId, fbevent);
                        //}
                    }
                }
            }

            File.WriteAllText($@"C:\BetEventScanner\Services\Parimatch\converted\e1-16-17.json", JsonConvert.SerializeObject(fulleList.Values));
        }

        public ICollection<string> ParseArchive(string html, Func<string, bool> filter = null)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var nodes = htmlDoc.DocumentNode.SelectNodes(@"//div[@class=""container gray""]");

            var selected = new List<string>();

            foreach (var node in nodes)
            {
                var header = node.SelectSingleNode("h3")?.InnerText;
                if (string.IsNullOrEmpty(header)) continue;

                if (filter != null)
                {
                    if (filter(header.ToLower()))
                    {
                        selected.Add(node.InnerHtml);
                    }

                    continue;
                }

                selected.Add(node.InnerHtml);
            }

            return selected;
        }

        public void ParseAllTennsis()
        {
            var dt = DateTime.Now.AddDays(-290);

            var tennisMatchesFolder = new DirectoryInfo(@"C:\BetEventScanner\Services\Parimatch\archive\Results\Tennis");

            while (dt.Date <= DateTime.Now.Date)
            {
                var dateStr = dt.ToString("yyyyMMdd");

                if (File.Exists($"{tennisMatchesFolder.FullName}\\{dateStr}.json"))
                {
                    Console.WriteLine($"{dateStr} skip");
                    dt = dt.AddDays(1);
                    continue;
                }

                var data = LoadArchiveDate(dt);
                var tennis = ParseArchive(data, x => x.Contains("tennis") && !x.Contains("futures") && !x.Contains("doubles") && !x.Contains("table tennis") && !x.Contains("outright") && !x.Contains("davis cup") && !x.Contains("winner") && !x.Contains("ADDITIONAL OUTCOMES".ToLower()));
                //var pmEvents = ParimatchEventsConverter.Convert<TennisMathOdds>(new MatchDateResolver(dateStr), tennis, "tennis");
                //File.WriteAllText($@"C:\BetEventScanner\Services\Parimatch\archive\Results\Tennis\{dateStr}.json", JsonConvert.SerializeObject(pmEvents));
                //dt = dt.AddDays(1);
            }
        }

        public ICollection<IParimatchEvent> GetFutureOddsBetEvents()
        {
            var sourceHtml = _client.DownloadHtmlSelenium("https://www.parimatch.com/en/bet.html?filter=today");
            return ProcessParseFutureOddsBetEvents(sourceHtml);
        }

        public ICollection<IParimatchEvent> ParsePreMatchOdds(string sourceHtml)
        {
            return ProcessParseFutureOddsBetEvents(sourceHtml);
        }

        private ICollection<IParimatchEvent> ProcessParseFutureOddsBetEvents(string sourceHtml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(sourceHtml);

            var eventContainers = htmlDoc.QuerySelectorAll("div.container.gray").ToList();

            var result = new List<IParimatchEvent>();

            foreach (var htmlNode in eventContainers)
            {
                var odds = ParimatchEventsConverter.ConvertToOddsEvent(htmlNode);
                if (odds == null || odds.Count == 0) continue;

                result.AddRange(odds);
            }

            return result;
        }
    }
}