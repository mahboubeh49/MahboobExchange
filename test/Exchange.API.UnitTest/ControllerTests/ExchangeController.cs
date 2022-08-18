using Exchange.Application.Commands.Conversion;
using Exchange.Application.Common.Dto;
using Exchange.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Exchange.API.UnitTest.ExchangeController
{
    public class ExchangeController
    {
        private readonly Mock<IMediator> _mediator;
        
        public ExchangeController()
        {
            _mediator = new Mock<IMediator>();
        }
        [Fact]
        public async void Exchange_ControllerGetExchange_ShouldReturnSucced()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<ConversionCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(It.IsAny<List<CurrencyResponse>>());


            var loggerMock = new Mock<ILogger<Api.Controllers.ExchangeController>>();
            var dto = new ConversionCommand(It.IsAny<string>(), It.IsAny<double>());
            var sut = new Api.Controllers.ExchangeController(_mediator.Object, loggerMock.Object);
            
            // Act
            var result = await sut.GetPriceConversion(dto);

            // Assert
            Assert.Equal(
               expected: 200,
                actual: ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode
            );
        }

        [Fact]
        public async void Exchange_ControllerGetConversionHistory_ShouldReturnSucced()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<ConversionHistoryQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(It.IsAny<List<ConversionHistoryResponse>>());


            var loggerMock = new Mock<ILogger<Api.Controllers.ExchangeController>>();
            var dto = new ConversionHistoryQuery();
            var sut = new Api.Controllers.ExchangeController(_mediator.Object, loggerMock.Object);
            
            // Act
            var result = await sut.GetConversionHistory();

            // Assert
            Assert.Equal(
               expected: 200,
                actual: ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode
            );
        }
    }
}