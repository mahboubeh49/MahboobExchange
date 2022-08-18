using Exchange.Application.Commands.Conversion;
using Exchange.Application.Common.Dto;
using Exchange.Application.Interfaces;
using Exchange.Application.Queries;
using Exchange.Core.Domain;
using Exchange.Core.Shared;
using Microsoft.Extensions.Logging;
using Moq;

namespace Exchange.Application.UnitTest.CommandTest
{
    public class ConversionHandlerTest
    {
        private readonly Mock<ICoinCapMarketClient> _coinCapMarketClient;
        public readonly Mock<IExchangeRepository> _exchangeRepository;
        public readonly Mock<IUnitOfWork> _unitOfWork;

        public ConversionHandlerTest()
        {
            _coinCapMarketClient = new Mock<ICoinCapMarketClient>();
            _exchangeRepository = new Mock<IExchangeRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async void Handel_GivenConversionFromUSDType_ShouldCalculateFiveTypeOfCurrency()
        {
            //Arange
            List<CurrencyResponse> responseList = new List<CurrencyResponse>()
            {
               new CurrencyResponse(){ Amount = 100 , TypeName = "USD"}
            };
            var convertItem = new List<HistoryItem>()
           {
               new HistoryItem()
               {
                   Amount = 100 , TypeName = "USD"
               },
               new HistoryItem()
               {
                   Amount = 101 , TypeName = "EUR",
               },
               new HistoryItem()
               {
                   Amount = 102 , TypeName = "BRL",
               },
                new HistoryItem()
               {
                   Amount = 103 , TypeName = "GBP",
               },
                new HistoryItem()
               {
                   Amount = 103 , TypeName = "GBP",
               }
           };
           

            _exchangeRepository.Setup(x => x.AddAsync(It.IsAny<ExchangeEntity>())).Returns(Task.CompletedTask);

            _coinCapMarketClient.Setup(m => m.GetCurrenciesConversionAsync(It.IsAny<string>(), It.IsAny<double>()))
               .ReturnsAsync(responseList);
            var loggerMock = new Mock<ILogger<ConversionCommandHandler>>();

            ConversionCommand command = new ConversionCommand(It.IsAny<string>(), It.IsAny<double>());
            ConversionCommandHandler handler = new ConversionCommandHandler(loggerMock.Object,
                _coinCapMarketClient.Object,
                _exchangeRepository.Object,
                _unitOfWork.Object);

            //Act
             var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            Assert.IsType<List<CurrencyResponse>>(result);
        }

    }
}
