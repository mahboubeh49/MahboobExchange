using Exchange.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Storage.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ExchangeDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(ExchangeDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            await dbSet.AddAsync(entity);
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<T>> GetAsync(
            Expression<Func<T, bool>> filter = null
           , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
           , string includeProperties = ""
           , int? take = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take != null)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
