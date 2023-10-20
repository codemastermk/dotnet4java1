using Bookstore.Data;
using Bookstore.Data.Models;
using Bookstore.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository
{
    public class AuthorsRepository : BaseRepository<Author, BookstoreContext>, IAuthorsRepository
    {
        private readonly BookstoreContext _context;
        public AuthorsRepository(BookstoreContext context) : base(context) 
        {
            _context = context;
        }
    }
}