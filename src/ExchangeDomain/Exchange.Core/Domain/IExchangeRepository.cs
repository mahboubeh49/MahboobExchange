using Exchange.Core.Shared;

namespace Exchange.Core.Domain
{
    public interface IExchangeRepository : IRepository<ExchangeEntity>
    {
        Task AddAsync(ExchangeEntity cryptocurrencyCode);

        Task<List<ExchangeEntity>> GetAllAsync();
    }
}
