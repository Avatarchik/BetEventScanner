namespace BetEventScanner.Common.Contracts
{
    public interface ITeam
    {
        string Name { get; set; }

        string Code { get; set; }

        string ShortName { get; set; }
    }
}