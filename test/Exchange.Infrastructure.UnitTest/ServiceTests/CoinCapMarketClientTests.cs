using Exchange.Application.Common.Dto;
using Exchange.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;

namespace Exchange.Infrastructure.UnitTest.ServiceTest
{
    public class CoinCapMarketClientTests
    {
        private static string _symbol = "USD";
        private static double _amount = 1.5;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<ILogger<CoinCapMarketClient>> _loggerMock; 

        public CoinCapMarketClientTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _loggerMock = new Mock<ILogger<CoinCapMarketClient>>();
        }

        [Fact]
        public async void GetProductType_GivenProductThatExist_ShouldReturnProduct()
        {
            //Arrange
            var someOptions = Options.Create(
                new CurrenciesSetting()
                {
                    Symbols = new List<string> { "USD", "EUR", "BRL", "GBP", "AUD" }
                });

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var client = new HttpClient(mockHttpMessageHandler.Object);
            var httpclient = HttpClientMock(_symbol, _amount, someOptions.Value.Symbols);

            _httpClientFactory
                .SetupSequence(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(httpclient);

            CoinCapMarketClient coinCapMarketClient = new CoinCapMarketClient(_loggerMock.Object, _httpClientFactory.Object, someOptions);

            //Act
            var result = await coinCapMarketClient.GetCurrenciesConversionAsync(_symbol, _amount);

            //Asert
            Assert.Equal(5,result.Count);

            Assert.Equal("USD", result[0].TypeName);
            Assert.Equal(1,result[0].Amount);

            Assert.Equal("EUR", result[1].TypeName);
            Assert.Equal(0.9826500000000015, result[1].Amount);

            Assert.Equal("BRL", result[2].TypeName);
            Assert.Equal(5.166500000000047, result[2].Amount);

            Assert.Equal("GBP", result[3].TypeName);
            Assert.Equal(0.8298510000000028, result[3].Amount);

            Assert.Equal("AUD", result[4].TypeName);
            Assert.Equal(1.4416080000000058, result[4].Amount);
        }

        private HttpClient HttpClientMock(string symbol,double amount, List<string> convertList)
        {
            var url = "https://pro-api.coinmarketcap.com";
            var mockHttp = new MockHttpMessageHandler();
            foreach (var item in convertList)
            {
                string filePathResponseType = $"Data\\{item.ToLower()}-response.json";
                string jsonfile = File.ReadAllText(filePathResponseType);

                mockHttp.When($"{url}*")
                    .WithQueryString("convert", item)
                    .Respond("application/json", jsonfile);
            }
            var httpClientObject = mockHttp.ToHttpClient();
            httpClientObject.BaseAddress = new Uri(url);
            return httpClientObject;
        }
    }
}
