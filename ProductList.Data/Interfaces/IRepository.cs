using Microsoft.EntityFrameworkCore;

namespace ProductList.Data.Interfaces
{

    public interface IRepository<TEntity> : IDisposable
    where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAndSaveAsync(TEntity entity);
        Task UpdateAndSaveAsync(TEntity entity);
        Task DeleteAndSaveAsync(TEntity entity);
        Task<int> SaveChangeAsync();
        DbSet<TEntity> GetTable();
    }

}