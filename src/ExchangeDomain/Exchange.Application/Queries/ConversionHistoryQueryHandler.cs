using Exchange.Application.Exceptions;
using Exchange.Core.Domain;
using Exchange.Core.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Exchange.Application.Queries
{
    public class ConversionHistoryQueryHandler : IRequestHandler<ConversionHistoryQuery, List<ConversionHistoryResponse>>
    {
        private readonly ILogger<ConversionHistoryQueryHandler> _logger;
        public readonly IExchangeRepository _exchangeRepository;
        public readonly IUnitOfWork _unitOfWork;

        public ConversionHistoryQueryHandler(ILogger<ConversionHistoryQueryHandler> logger,
            IExchangeRepository exchangeRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _exchangeRepository = exchangeRepository;
           _unitOfWork = unitOfWork;
        }

        public async Task<List<ConversionHistoryResponse>> Handle(ConversionHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ConversionHistoryQueryHandler was called.");

            var resultFromdb = await _exchangeRepository.GetAllAsync();

            var result = resultFromdb.Select(s => new ConversionHistoryResponse()
            {
                ConvertedAmount = s.ConvertedAmount,
                CurrentedFrom = s.CurrentedFrom,
                RequestedTime=s.CreatedAt,
                Histories = JsonConvert.DeserializeObject<List<HistoryItem>>(s.JsonSearchedHistory)
            }).ToList();

            return result;
        }
    }
}
