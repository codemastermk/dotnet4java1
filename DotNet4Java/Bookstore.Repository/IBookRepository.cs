using Bookstore.Data.Models;
using Bookstore.Repository.Base;

namespace Bookstore.Repository
{
    public interface IBookRepository : IBaseRepository<Book>
    {
    }
}
