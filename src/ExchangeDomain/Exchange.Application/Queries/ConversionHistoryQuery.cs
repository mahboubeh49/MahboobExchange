using MediatR;

namespace Exchange.Application.Queries
{
    public record ConversionHistoryQuery() : IRequest<List<ConversionHistoryResponse>>;
}

