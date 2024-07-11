using System.Linq.Expressions;

namespace Mus_Rately.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(params object[] id);

        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyCollection<T>> GetPaginatedAsync(int skipNumber, int takeNumber);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
