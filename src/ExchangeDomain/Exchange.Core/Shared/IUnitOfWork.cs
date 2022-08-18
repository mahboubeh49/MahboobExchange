using Exchange.Core.Domain;

namespace Exchange.Core.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        IExchangeRepository ExchangeRepository { get; }

        Task<long> SaveAsync();
    }
}
