using Exchange.Application.Common.Dto;

namespace Exchange.Application.Interfaces
{
    public interface ICoinCapMarketClient
    {
         Task<List<CurrencyResponse>> GetCurrenciesConversionAsync(string symbol, double amount);

    }
}
