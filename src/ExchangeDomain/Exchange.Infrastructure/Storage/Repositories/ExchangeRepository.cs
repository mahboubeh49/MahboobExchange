using Exchange.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Infrastructure.Storage.Repositories
{
    public class ExchangeRepository : Repository<ExchangeEntity>, IExchangeRepository
    {
        private readonly ExchangeDbContext _dbContext;

        public ExchangeRepository(ExchangeDbContext context) : base(context)
        {
            this._dbContext = context;
        }
        public async Task AddAsync(ExchangeEntity cryptocurrencyCode)
        {
            await _dbContext.Exchanges.AddAsync(cryptocurrencyCode);
        }

        public async Task<List<ExchangeEntity>> GetAllAsync()
        {
            return await _dbContext.Exchanges.OrderByDescending(a => a.CreatedAt).ToListAsync();
        }

    }
}
