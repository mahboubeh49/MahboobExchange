namespace Exchange.Application.Common.Dto
{
    public class DataDto
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public float amount { get; set; }
        public DateTime last_updated { get; set; }
        public QuoteDto quote { get; set; }

    }
}
