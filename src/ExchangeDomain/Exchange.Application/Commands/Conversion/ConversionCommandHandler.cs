using Exchange.Application.Common.Dto;
using Exchange.Application.Interfaces;
using Exchange.Core.Domain;
using Exchange.Core.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Exchange.Application.Commands.Conversion
{
    public class ConversionCommandHandler : IRequestHandler<ConversionCommand, List<CurrencyResponse>>
    {
        private readonly ILogger<ConversionCommandHandler> _logger;
        private readonly ICoinCapMarketClient _coinCapMarketClient;
        public readonly IExchangeRepository _exchangeRepository;
        public readonly IUnitOfWork _unitOfWork;

        public ConversionCommandHandler(ILogger<ConversionCommandHandler> logger,
            ICoinCapMarketClient coinCapMarketClient,
            IExchangeRepository exchangeRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _coinCapMarketClient = coinCapMarketClient;
            _exchangeRepository = exchangeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CurrencyResponse>> Handle(ConversionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ConversionCommandHandler was called.");

            var result = await _coinCapMarketClient.GetCurrenciesConversionAsync(request.symbol, request.amount);
            await StoreToDatabase(request, result);

            return result;
        }

        private async Task StoreToDatabase(ConversionCommand request, List<CurrencyResponse> result)
        {
            _logger.LogInformation("Save database in history.");
            ExchangeEntity model = new ExchangeEntity()
            {
                CreatedAt = DateTime.UtcNow,
                CurrentedFrom = request.symbol,
                ConvertedAmount = request.amount,
                JsonSearchedHistory = Newtonsoft.Json.JsonConvert.SerializeObject(result),
            };
            await _exchangeRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }
    }
}
