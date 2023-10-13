using Bookstore.Data;
using Bookstore.Data.Models;
using Bookstore.Repository.Base;

namespace Bookstore.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly BookstoreContext _context; 
        public BookRepository(BookstoreContext context) : base(context) 
        {
            _context = context;
        }
    }
}
