using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Queries
{
    public class ConversionHistoryResponse
    {
        public string CurrentedFrom { get; set; }
        public double ConvertedAmount { get; set; }
        public DateTime RequestedTime { get; set; }
        public List<HistoryItem> Histories { get; set; }
       
    }
}
