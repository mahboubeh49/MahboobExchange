using System.Linq.Expressions;

namespace Exchange.Core.Shared
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeProperties = "", int? take = null);
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(long id);
        void Update(T entity);
        Task DeleteAsync(long id);
    }
}
