namespace BetEventScanner.Providers.FifaonlinecupOrg
{
    public interface IHeadToHeadProvider
    {
        HeadToHead GetHeadToHead(string t1, string t2);
    }
}