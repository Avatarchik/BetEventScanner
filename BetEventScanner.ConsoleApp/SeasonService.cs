using System;
using BetEventScanner.DataAccess.Entities;

namespace BetEventScanner.ConsoleApp
{
    public class SeasonService
    {
        public FootballSeason GetDivisionCurrentSeason(string divisionCode)
        {
            return GetCurrentSeasonByDivisionCode(divisionCode) ?? CreateNewCurrentSeason(divisionCode);
        }

        private FootballSeason CreateNewCurrentSeason(string divisionCode)
        {
            var result = new FootballSeason
            {
                DivisionCode = divisionCode,
                IsCurrent = true
            };

            var dt = DateTime.Now;
            if (dt.Month > 6 && dt.Month <= 12)
            {
                result.StartYear = dt.Year;
                result.EndYear = result.StartYear++;
            }

            //using (var context = new BetEventScannerContext())
            //{
            //    context.Seasons.Add(result);
            //    context.SaveChanges();
            //}

            return result;
        }

        private FootballSeason GetCurrentSeasonByDivisionCode(string divisionCode)
        {
            //using (var context = new BetEventScannerContext())
            //{
            //    return context.Seasons.FirstOrDefault(x => x.IsCurrent && x.DivisionCode == divisionCode);
            //}

            return null;
        }
    }
}