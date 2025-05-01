using Microsoft.EntityFrameworkCore;
using ProductList.Data.Contexts;
using ProductList.Data.Interfaces;

namespace ProductList.Data.Concretes
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
      

        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }
        public virtual async Task AddAndSaveAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
            await SaveChangeAsync();
        }
        public virtual async Task UpdateAndSaveAsync(TEntity entity)
        {
            Entities.Update(entity);
            await SaveChangeAsync();
        }
        public virtual async Task DeleteAndSaveAsync(TEntity entity)
        {
            Entities.Remove(entity);
            await SaveChangeAsync();
        }

        public virtual async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public DbSet<TEntity> GetTable()
        {
            return Entities;
        }
    }
}