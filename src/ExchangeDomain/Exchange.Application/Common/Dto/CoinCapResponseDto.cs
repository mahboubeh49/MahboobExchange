using Newtonsoft.Json;

namespace Exchange.Application.Common.Dto
{
    public class CoinCapResponseDto
    {
        public StatusDto Status { get; set; }

        public List<DataDto> Data { get; set; }
    }
}
