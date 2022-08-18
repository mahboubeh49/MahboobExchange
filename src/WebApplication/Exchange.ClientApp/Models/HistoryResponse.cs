namespace Exchange.ClientApp.Models
{
    public class HistoryResponse : ResponseModel
    {
        public List<ConversionHistoryResponse> Histories { get; set; }
    }
}
