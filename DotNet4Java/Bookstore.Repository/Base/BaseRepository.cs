using Bookstore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository.Base
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        public readonly TContext _context;
        public readonly DbSet<TEntity> _entity;
        public BaseRepository(TContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task InsertAsync(TEntity t)
        {
            await _entity.AddAsync(t);
        }

        public void Update(TEntity t)
        {
            _entity.Update(t);
        }

        public async Task DeleteAsync(int id)
        {
            var t = await _entity.FindAsync(id);
            if (t != null)
            {
                _context.Remove(t);
            }
        }
    }
}
