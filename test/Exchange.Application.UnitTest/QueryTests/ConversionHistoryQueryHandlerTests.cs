using Exchange.Application.Queries;
using Exchange.Core.Domain;
using Exchange.Core.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace Exchange.Application.UnitTest.QueryTests
{
    public class ConversionHistoryQueryHandlerTests
    {
        public readonly Mock<IExchangeRepository> _exchangeRepository;
        public readonly Mock<IUnitOfWork> _unitOfWork;
        public ConversionHistoryQueryHandlerTests()
        {
            _exchangeRepository = new Mock<IExchangeRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async void Handel_GivenHistoriesExchange_ShouldReturnAllHistory()
        {
            //Arange
           
            var historyItem = new List<HistoryItem>()
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
            List<ExchangeEntity> exchangeEntities = new List<ExchangeEntity>()
            {
                new ExchangeEntity()
                {
                    ConvertedAmount = 1,
                    CreatedAt = DateTime.Now,
                    CurrentedFrom = "USD",
                    JsonSearchedHistory = JsonConvert.SerializeObject(historyItem )
                }
            };
            
            _exchangeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(exchangeEntities);

            var loggerMock = new Mock<ILogger<ConversionHistoryQueryHandler>>();

            ConversionHistoryQuery command = new ConversionHistoryQuery();
            ConversionHistoryQueryHandler handler = new ConversionHistoryQueryHandler(loggerMock.Object,
                _exchangeRepository.Object,
                _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.IsType<List<ConversionHistoryResponse>>(result);
        }

    }
}
