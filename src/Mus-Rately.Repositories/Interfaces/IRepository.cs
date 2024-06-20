﻿namespace Mus_Rately.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(params object[] id);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetPaginatedAsync(int takeNumber, int skipNumber);
    }
}