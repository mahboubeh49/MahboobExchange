using Exchange.Core.Domain;
using Exchange.Core.Shared;
using Exchange.Infrastructure.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Storage
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ExchangeDbContext _context;

        public UnitOfWork(ExchangeDbContext context)
        {
            _context = context;
        }

        public IExchangeRepository ExchangeRepository { get; }

        public async Task<long> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
