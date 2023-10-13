using Bookstore.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly BookstoreContext _context;
        public readonly DbSet<T> _entity;
        public BaseRepository(BookstoreContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task InsertAsync(T t)
        {
            await _entity.AddAsync(t);
        }

        public void Update(T t)
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

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
