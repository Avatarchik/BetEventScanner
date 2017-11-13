using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner.Contracts
{
    public interface IFootballMatchConverter
    {
        FootballMatch CreateMatchFromHtml(string providerMatchId, string matchHtml, string matchDetailsHtml);
    }
}