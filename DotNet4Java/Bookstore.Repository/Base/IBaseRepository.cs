namespace Bookstore.Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task InsertAsync(TEntity author);
        void Update(TEntity author);
        Task DeleteAsync(int id);
    }
}
