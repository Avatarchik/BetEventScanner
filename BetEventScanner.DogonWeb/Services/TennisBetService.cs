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
        private readonly ICalculateService _calculateService;

        public TennisBetService(IDefaultUnitOfWork uow, ICalculateService calculateService)
        {
            _uow = uow;
            _calculateService = calculateService;
        }

        public IEnumerable<BetInfoListDto> GetBetsList()
        {
            var betsList = _uow.BetInfoes.AsQueryableNotTracking()
                .Include(i => i.Lines)
                .Select(s => new BetInfoListDto
                {
                    Id = s.Id,
                    WinLine = s.WinnerLine,
                    FavoritePlayer = s.FavoritePlayer,
                    FirstPlayer = s.FirstPlayer,
                    SecondPlayer = s.SecondPlayer,
                    FLine = s.Lines.FirstOrDefault(x => x.LineNumber == 1).Coefficient.ToString() ?? string.Empty,
                    SLine = s.Lines.FirstOrDefault(x => x.LineNumber == 2).Coefficient.ToString() ?? string.Empty,
                    TLine = s.Lines.FirstOrDefault(x => x.LineNumber == 3).Coefficient.ToString() ?? string.Empty,
                    FBet = s.Lines.FirstOrDefault(x => x.LineNumber == 1).Bet,
                    SBet = s.Lines.FirstOrDefault(x => x.LineNumber == 2).Bet,
                    TBet = s.Lines.FirstOrDefault(x => x.LineNumber == 3).Bet,
                }).ToList();

            return betsList;


        }

        public PredictBetDto ProcessBetLine(BetInfoDto betInfoDto)
        {
            if (betInfoDto.ManualBet)
            {
                CreateBet(betInfoDto);
                return null;
            }

            //predict bet id any lines exists
            var calculatedBets = _calculateService.CalculateBets(betInfoDto);
            if (calculatedBets != null)
            {
                return calculatedBets;
            }
            return null;
        }

        public bool CreateCalculatedBet(BetInfoDto betInfoDto)
        {
            return CreateBet(betInfoDto);
        }

        public bool SaveCalculatedBet(BetInfoDto betInfo)
        {
            return CreateBet(betInfo);
        }

        private bool CreateBet(BetInfoDto betInfoDto)
        {
            var lines = new List<Line>();
            if (!betInfoDto.ManualBet)
            {
                lines.Add(CalculateDefaultBets(betInfoDto.FirstLineCoef, 1));
                lines.Add(CalculateDefaultBets(betInfoDto.SecondLineCoef, 2));
                lines.Add(CalculateDefaultBets(betInfoDto.ThirdLineCoef, 3));
            }
            else
            {
                lines.Add(new Line { Coefficient = betInfoDto.FirstLineCoef, LineNumber = 1, Bet = betInfoDto.FirstLineBet });
                lines.Add(new Line { Coefficient = betInfoDto.SecondLineCoef, LineNumber = 2, Bet = betInfoDto.SecondLineBet });
                lines.Add(new Line { Coefficient = betInfoDto.ThirdLineCoef, LineNumber = 3, Bet = betInfoDto.ThirdLineBet });
            }

            var betInfo = new BetInfo
            {
                FavoritePlayer = betInfoDto.FavoritePlayer,
                FirstPlayer = betInfoDto.FirstPlayer,
                SecondPlayer = betInfoDto.SecondPlayer,
                Lines = lines
            };

            _uow.BetInfoes.Create(betInfo);
            _uow.Commit();

            return true;
        }

        public bool UpdateBet(BetInfoListDto betInfoListDto)
        {
            try
            {
                var currentBetInfo = _uow.BetInfoes.AsQueryable()
                    .Include(i => i.Lines)
                    .FirstOrDefault(x => x.Id == betInfoListDto.Id);

                if (currentBetInfo == null) return false;

                currentBetInfo.WinnerLine = betInfoListDto.WinLine;

                foreach(var line in currentBetInfo.Lines)
                {
                    if (line.LineNumber == betInfoListDto.WinLine)
                    {
                        line.Score = line.Coefficient * line.Bet;
                        break;
                    }
                }

                _uow.BetInfoes.Update(currentBetInfo);
                _uow.Commit();
            }
            catch(Exception ex)
            {

            }

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