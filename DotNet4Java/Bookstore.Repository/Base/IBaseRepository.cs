namespace Bookstore.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task InsertAsync(T author);
        void Update(T author);
        Task DeleteAsync(int id);
        Task CommitAsync();
    }
}
