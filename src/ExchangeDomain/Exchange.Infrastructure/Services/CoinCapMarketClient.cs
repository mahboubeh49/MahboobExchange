using Exchange.Application.Common.Dto;
using Exchange.Application.Common.Enum;
using Exchange.Application.Exceptions;
using Exchange.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;


/*************************************************
Please pay attention as I understood CoinCapMarket in free API version mode, doesn't provide multiple conversions, so I had to send 5 requests to the CoinCap market for each user request as you can see in the below code
*************************************************/
namespace Exchange.Infrastructure.Services
{
    public class CoinCapMarketClient : ICoinCapMarketClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CoinCapMarketClient> _logger;
        private HttpClient _httpClient;
        private readonly IOptions<CurrenciesSetting> _currenciesSetting;
      
        public CoinCapMarketClient(ILogger<CoinCapMarketClient> logger,
            IHttpClientFactory httpClientFactory,
            IOptions<CurrenciesSetting> currenciesSetting)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _httpClient = _httpClientFactory.CreateClient("CoinMarketApi");
            _currenciesSetting = currenciesSetting;
        }
       
        public async Task<List<CurrencyResponse>> GetCurrenciesConversionAsync(string symbol, double amount)
        {
            _logger.LogInformation("GetCurrenciesConversionAsync was called");
            List<CurrencyResponse> responseList = new List<CurrencyResponse>();

            foreach (var currenty in _currenciesSetting.Value.Symbols)
            {
                var resultFromCoinMarket = await GetConversionFromCoinMarketAsync(symbol, amount, currenty);
                responseList.Add(GetCurencyConversion(symbol, resultFromCoinMarket));
            }
            return responseList; 
        }

        private CurrencyResponse GetCurencyConversion(string symbol, CoinCapResponseDto model)
        {
            var CurrencyResponseResult = new CurrencyResponse();
            //todo more time i could refacor it to dynamic
            var data = model.Data.FirstOrDefault();
            
            if (data.quote.USD != null)
            {
                CurrencyResponseResult.Amount = (double)data.quote.USD.price;
                CurrencyResponseResult.TypeName = CurrencyTypeEnum.USD.ToString(); 
            }
            if (data.quote.EUR != null)
            {
                CurrencyResponseResult.Amount = (double)data.quote.EUR.price;
                CurrencyResponseResult.TypeName = CurrencyTypeEnum.EUR.ToString();
            }
            if (data.quote.BRL != null)
            {
                CurrencyResponseResult.Amount = (double)data.quote.BRL.price;
                CurrencyResponseResult.TypeName = CurrencyTypeEnum.BRL.ToString();
            }
            if (data.quote.GBP != null)
            {
                CurrencyResponseResult.Amount = (double)data.quote.GBP.price;
                CurrencyResponseResult.TypeName = CurrencyTypeEnum.GBP.ToString();
            }
            if (data.quote.AUD != null)
            {
                CurrencyResponseResult.Amount = (double)data.quote.AUD.price;
                CurrencyResponseResult.TypeName = CurrencyTypeEnum.AUD.ToString();
            }
            
            return CurrencyResponseResult;
        }
        private async Task<CoinCapResponseDto> GetConversionFromCoinMarketAsync(string symbol, double amount, string convert)
        {
            _logger.LogInformation("Coin Market api is being called.");

            var coinmarketResponse = await _httpClient.GetAsync($"v2/tools/price-conversion?symbol={symbol}&amount={amount}&convert={convert}");

            if (coinmarketResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ApplicationBusinessException(" was not found.");
            }
            if (!coinmarketResponse.IsSuccessStatusCode)
            {
                throw new ApplicationBusinessException(" could not be reached.");
            }

            string result = coinmarketResponse.Content.ReadAsStringAsync().Result;
            var conbertlist = JsonConvert.DeserializeObject<CoinCapResponseDto>(result);

            return conbertlist;
        }
    }
}
