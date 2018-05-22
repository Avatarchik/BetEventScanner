using BetEventScanner.DataAccess;
using BetEventScanner.DataAccess.Entities;
using BetEventScanner.DogonWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BetEventScanner.DogonWeb.Services
{
    public class TennisBetService : ITennisService
    {
        private readonly string DefaultBets = ConfigurationManager.AppSettings["DefaultBets"];
        private readonly IDefaultUnitOfWork _uow;

        public TennisBetService(IDefaultUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<BetInfoListDto> GetBetsList()
        {
            var betsList = _uow.BetInfoes.AsQueryableNotTracking()
                .Include(i => i.Lines)
                .Select(s => new BetInfoListDto
                {
                    Id = s.Id,
                    FavoritePlayer = s.FavoritePlayer,
                    FirstPlayer = s.FirstPlayer,
                    SecondPlayer = s.SecondPlayer,
                    FLine = s.Lines.FirstOrDefault(x => x.LineNumber == 1).Coefficient.ToString() ?? string.Empty,
                    SLine = s.Lines.FirstOrDefault(x => x.LineNumber == 2).Coefficient.ToString() ?? string.Empty,
                    TLine = s.Lines.FirstOrDefault(x => x.LineNumber == 3).Coefficient.ToString() ?? string.Empty
                }).ToList();

            return betsList;


        }

        public bool ProcessBetLine(BetInfoDto betInfoDto)
        {
            var lines = new List<Line>();
            if (!betInfoDto.ManualBet)
            {
                lines.Add(CalculateDefaultBets(betInfoDto.FirstLineCoef, 1));
                lines.Add(CalculateDefaultBets(betInfoDto.SecondLineCoef, 2));
                lines.Add(CalculateDefaultBets(betInfoDto.ThirdLineCoef, 3));
            }

            var betInfo = new BetInfo
            {
                FavoritePlayer = betInfoDto.FavoritePlayer,
                FirstPlayer = betInfoDto.FirstPlayer,
                SecondPlayer = betInfoDto.SecondPlayer,
                Lines = !betInfoDto.ManualBet ? lines : null
            };

            _uow.BetInfoes.Create(betInfo);
            _uow.Commit();

            return true;
        }

        private Line CalculateDefaultBets(decimal coef, int lineNumber)
        {
            var betsResult = GetDefaultBets();
            var betScore = 0M;

            if (coef < 2) betScore = betsResult.FirstBet;
            if (coef >= 3) betScore = betsResult.ThirdBet;
            if (coef >= 2 && coef <= 3) betScore = betsResult.SecondBet;

            return new Line
            {
                Coefficient = coef,
                Bet = betScore,
                LineNumber = lineNumber
            };

        }

        private BetsResult GetDefaultBets()
        {
            var betsList = DefaultBets.Split(';').ToList();
            var betsResult = new BetsResult
            {
                FirstBet = decimal.Parse(betsList[0]),
                SecondBet = decimal.Parse(betsList[1]),
                ThirdBet = decimal.Parse(betsList[2])
            };

            return betsResult;
        }
    }

    public class BetsResult
    {
        public decimal FirstBet { get; set; }

        public decimal SecondBet { get; set; }

        public decimal ThirdBet { get; set; }
    }
}