namespace BetEventScanner.Providers.Parimatch.Model
{
    public interface IParimatchEvent
    {
        string ParimatchId { get; set; }

        string Total { get; set; }

        string TotalOver { get; set; }

        string TotalUnder { get; set; }
    }
}