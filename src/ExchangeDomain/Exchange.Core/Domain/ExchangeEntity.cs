using Exchange.Core.Shared;

namespace Exchange.Core.Domain
{
    public class ExchangeEntity : BaseEntity
    {
        public string CurrentedFrom { get; set; }
        public double ConvertedAmount { get; set; }
        public string JsonSearchedHistory { get; set; }
    }
}
