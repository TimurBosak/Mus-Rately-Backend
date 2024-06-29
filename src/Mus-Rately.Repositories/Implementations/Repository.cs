using Microsoft.EntityFrameworkCore;
using Mus_Rately.Repositories.Interfaces;

namespace Mus_Rately.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;

        protected DbSet<T> dbSet;


        public Repository(DbContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }


        public async Task<T> GetByIdAsync(params object[] id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetPaginatedAsync(int skipNumber, int takeNumber)
        {
            return await GetQuery().Skip(skipNumber).Take(takeNumber).ToListAsync();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        protected virtual IQueryable<T> GetQuery()
        {
            return dbSet;
        }
    }
}
