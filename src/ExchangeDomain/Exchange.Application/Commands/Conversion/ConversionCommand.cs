using Exchange.Application.Common.Dto;
using MediatR;

namespace Exchange.Application.Commands.Conversion
{
    public record ConversionCommand(string symbol, double amount) : IRequest<List<CurrencyResponse>>;
}
