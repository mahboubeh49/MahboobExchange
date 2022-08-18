namespace Exchange.ClientApp.Models
{
    public class ConversionHistoryResponse
    {
        public string CurrentedFrom { get; set; }
        public double ConvertedAmount { get; set; }
        public DateTime RequestedTime { get; set; }
        public List<HistoryItem> Histories { get; set; }

    }
}
