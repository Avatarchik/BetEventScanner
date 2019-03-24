namespace BetEventScanner.Providers.Parimatch.Model
{
    public interface IParimatchEvent : IOddsBetEvent
    {
        string Evno { get; set; }
    }
}