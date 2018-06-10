using BetEventScanner.DataAccess;
using BetEventScanner.DataAccess.Entities;
using BetEventScanner.DogonWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<BetInfoListDto>> GetBetsListAsync()
        {
            var betsList = await _uow.BetInfoes.AsQueryableNotTracking()
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
                }).ToListAsync();

            return betsList;
        }

        public async Task<PredictBetDto> ProcessBetLineAsync(BetInfoDto betInfoDto)
        {
            if (betInfoDto.ManualBet)
            {
                await CreateBetAsync(betInfoDto);
                return null;
            }

            //predict bet if any lines exists
            var calculatedBets = await _calculateService.CalculateBetsAsync(betInfoDto);
            if (calculatedBets != null)
            {
                return calculatedBets;
            }

            await CreateBetAsync(betInfoDto);
            return null;
        }

        public async Task<bool> CreateCalculatedBetAsync(BetInfoDto betInfoDto)
        {
            return await CreateBetAsync(betInfoDto);
        }

        private async Task<bool> CreateBetAsync(BetInfoDto betInfoDto)
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
            await _uow.CommitAsync();

            return true;
        }

        public async Task<bool> UpdateBetAsync(BetInfoListDto betInfoListDto)
        {
            try
            {
                var currentBetInfo = await _uow.BetInfoes.AsQueryable()
                    .Include(i => i.Lines)
                    .FirstOrDefaultAsync(x => x.Id == betInfoListDto.Id);

                if (currentBetInfo == null) return false;

                currentBetInfo.WinnerLine = betInfoListDto.WinLine;

                var currLine = currentBetInfo.Lines.First(x => x.LineNumber == betInfoListDto.WinLine);
                currLine.Score = currLine.Coefficient * currLine.Bet;

                _uow.BetInfoes.Update(currentBetInfo);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
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

        public async Task<bool> RemoveBetAsync(int betId)
        {
            try
            {
                var betInfo = await _uow.BetInfoes.AsQueryable()
                    .Include(i => i.Lines)
                    .FirstOrDefaultAsync(x => x.Id == betId);
                if (betInfo == null) return false;

                await _uow.BetInfoes.DeleteAsync(betId);

                await _uow.CommitAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }

    public class BetsResult
    {
        public decimal FirstBet { get; set; }

        public decimal SecondBet { get; set; }

        public decimal ThirdBet { get; set; }
    }
}